namespace RulesExercise.Application.Models
{
    public class ProjectsRequestDto
    {
        public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
        public class ProjectDto
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string Stage { get; set; }

            public List<int> Categories { get; set; } = new List<int>();

            public long CreatedAt { get; set; }

            public long ModifiedAt { get; set; }
        }
    }
}
