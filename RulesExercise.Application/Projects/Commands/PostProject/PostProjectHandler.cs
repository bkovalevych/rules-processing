using AutoMapper;
using MediatR;
using RulesExercise.Application.Interfaces.Templates;
using RulesExercise.Application.Rules;
using RulesExercise.Application.Rules.Models;
using RulesExercise.Domain.Entities;
using ISender = RulesExercise.Application.Interfaces.Senders.ISender;

namespace RulesExercise.Application.Projects.Commands.PostProject
{
    public class PostProjectHandler : IRequestHandler<PostProjectCommand>
    {
        private readonly ITemplateManager _templateManager;
        private readonly ISender _sender;
        private readonly RulesManager _rulesManager;
        private readonly IMapper _mapper;

        public PostProjectHandler(ITemplateManager templateManager, ISender sender, RulesManager rulesManager, IMapper mapper)
        {
            _templateManager = templateManager;
            _sender = sender;
            _rulesManager = rulesManager;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(PostProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request);
            var effects = _rulesManager.GetEffectsByProject(project);
            var tasks = effects.Select(it => ProcessEffect(it, project));
            await Task.WhenAll(tasks);
            return Unit.Value;
        }

        private async Task ProcessEffect(Effect effect, Project project)
        {
            var template = await _templateManager.GetTemplateAsync(effect.TemplateId);
            var values = effect.PlaceHolders.ToDictionary(it => it.Key, it => project.GetField(it.Value));
            var subject = _templateManager.FormatFromTemplate(template.Subject, values);
            var message = _templateManager.FormatFromTemplate(template.Message, values);
            _sender.SetSenderType(effect.Type);
            await _sender.SendToAsync("email", subject, message);
        }
    }
}
