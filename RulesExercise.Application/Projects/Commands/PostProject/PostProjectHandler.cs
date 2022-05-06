using AutoMapper;
using MediatR;
using RulesExercise.Application.Helpers;
using RulesExercise.Application.Interfaces.BackgroundJobHelpers;
using RulesExercise.Application.Interfaces.Templates;
using RulesExercise.Application.Rules;
using RulesExercise.Application.Rules.Models;
using RulesExercise.Domain.Entities;
using RulesExercise.Domain.Enums;
using ISenderFactory = RulesExercise.Application.Interfaces.Senders.ISenderFactory;

namespace RulesExercise.Application.Projects.Commands.PostProject
{
    public class PostProjectHandler : IRequestHandler<PostProjectCommand, string>
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ITemplateManager _templateManager;
        private readonly ISenderFactory _senderFactory;
        private readonly RulesManager _rulesManager;
        private readonly IMapper _mapper;
        private readonly IBackgroundWorkerService _backgroundWorker;

        public PostProjectHandler(
            ITemplateRepository templateRepository,
            ITemplateManager templateManager,
            ISenderFactory senderFactory,
            RulesManager rulesManager,
            IBackgroundWorkerService backgroundWorker,
            IMapper mapper)
        {
            _templateRepository = templateRepository;
            _templateManager = templateManager;
            _senderFactory = senderFactory;
            _rulesManager = rulesManager;
            _mapper = mapper;
            _backgroundWorker = backgroundWorker;
        }
        public async Task<string> Handle(PostProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request);
            var effects = _rulesManager.GetEffectsByProject(project);
            foreach (var effect in effects)
            {
                await ProcessEffect(effect, project);
            }
            
            return effects.Any() 
                ? "rules passed"
                : "rules did not pass";
        }

        private async Task ProcessEffect(Effect effect, Project project)
        {
            var template = await _templateRepository.GetTemplateAsync(effect.TemplateId);
            var values = GetPlaceHolders(effect, project);
            var subject = _templateManager.FormatFromTemplate(template.Subject, values);
            var message = _templateManager.FormatFromTemplate(template.Message, values);
            
            var typeSender = Enum.Parse<Channel>(effect.Type, true);
            var sender = _senderFactory.GetSenderForChannel(typeSender);
            
            _backgroundWorker.Enqueue(() => sender.SendMessageAsync(subject, message));
        }

        private Dictionary<string, object> GetPlaceHolders(Effect effect, Project project)
        {
            var result = effect.PlaceHolders.ToDictionary(
                it => it.Key,
                it => project.GetField(it.Key));
            return result;
        }
    }
}
