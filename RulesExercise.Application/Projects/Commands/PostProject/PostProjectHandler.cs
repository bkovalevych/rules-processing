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
        private readonly ITemplateManager _templateManager;
        private readonly ISenderFactory _senderFactory;
        private readonly RulesManager _rulesManager;
        private readonly IMapper _mapper;
        private readonly IBackgroundWorkerService _backgroundWorker;

        public PostProjectHandler(ITemplateManager templateManager,
            ISenderFactory senderFactory,
            RulesManager rulesManager,
            IBackgroundWorkerService backgroundWorker,
            IMapper mapper)
        {
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
            
            return "ok";
        }

        private async Task ProcessEffect(Effect effect, Project project)
        {
            var template = await _templateManager.GetTemplateAsync(effect.TemplateId);
            var values = effect.PlaceHolders.ToDictionary(
                it => SnakeCaseToCamelCaseConverter.FromSnakeCaseToCamelCase(it.Key), 
                it => (object)project.GetField(SnakeCaseToCamelCaseConverter.FromSnakeCaseToCamelCase(it.Key)));
            
            var subject = _templateManager.FormatFromTemplate(template.Subject, values);
            var message = _templateManager.FormatFromTemplate(template.Message, values);
            
            var typeSender = Enum.Parse<Channel>(effect.Type, true);
            var sender = _senderFactory.GetSenderForChannel(typeSender);
            
            _backgroundWorker.Enqueue(() => sender.SendMessageAsync(subject, message));
        }
    }
}
