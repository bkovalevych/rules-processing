using MediatR;

namespace RulesExercise.Application.Projects.Commands.PostProject
{
    public class PostProjectCommand : IRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Stage { get; set; }

        public List<int> Categories { get; set; } = new List<int>();

        public DateTimeOffset Created_at { get; set; }

        public DateTimeOffset Updated_at { get; set; }
    }
}
