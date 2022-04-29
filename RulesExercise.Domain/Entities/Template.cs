namespace RulesExercise.Domain.Entities
{
    public class Template
    {
        public int Id { get; set; }

        public string Type { get; set; } = default!;

        public string Subject { get; set; } = default!;

        public string Message { get; set; } = default!;
    }
}
