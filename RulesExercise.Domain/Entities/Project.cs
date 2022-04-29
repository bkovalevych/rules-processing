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
    }
}
