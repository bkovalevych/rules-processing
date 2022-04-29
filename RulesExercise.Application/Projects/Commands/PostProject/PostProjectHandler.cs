using AutoMapper;
using MediatR;
using RulesExercise.Application.Interfaces.Templates;
using RulesExercise.Domain.Entities;

namespace RulesExercise.Application.Projects.Commands.PostProject
{
    public class PostProjectHandler : IRequestHandler<PostProjectCommand>
    {
        private readonly ITemplateManager _templateManager;
        private readonly IMapper _mapper;

        public PostProjectHandler(ITemplateManager templateManager, IMapper mapper)
        {
            _templateManager = templateManager;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(PostProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request);

            return Unit.Value;
        }
    }
}
