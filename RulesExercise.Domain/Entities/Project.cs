namespace RulesExercise.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        
        public string? Stage { get; set; }

        public List<int> Categories { get; set; } = new List<int>();

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        private readonly Dictionary<string, string> _values;

        public string GetField(string key)
        {
            return _values[key];
        }
        public Project()
        {
            _values = new Dictionary<string, string>()
            {
                [nameof(Project.Id)] = Id.ToString(),
                [nameof(Project.Name)] = Name,
                [nameof(Project.Description)] = Description,
                [nameof(Project.Stage)] = Stage,
                [nameof(Project.CreatedAt)] = CreatedAt.ToString(),
                [nameof(Project.UpdatedAt)] = UpdatedAt.ToString(),
            };
        }
    }
}
