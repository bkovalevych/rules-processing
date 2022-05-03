using AutoMapper;
using MediatR;
using RulesExercise.Application.Helpers;
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

        public PostProjectHandler(ITemplateManager templateManager, ISenderFactory senderFactory, RulesManager rulesManager, IMapper mapper)
        {
            _templateManager = templateManager;
            _senderFactory = senderFactory;
            _rulesManager = rulesManager;
            _mapper = mapper;
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
            var values = effect.PlaceHolders.ToDictionary(it => it.Key, it => project.GetField(SnakeCaseToCamelCaseConverter.FromSnakeCaseToCamelCase(it.Value)));
            var subject = _templateManager.FormatFromTemplate(template.Subject, values);
            var message = _templateManager.FormatFromTemplate(template.Message, values);
            var sender = _senderFactory.GetSenderForChannel(Channel.Telegram);
            await sender.SendMessageAsync(subject, message);
        }
    }
}
