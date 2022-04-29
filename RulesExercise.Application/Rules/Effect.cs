namespace RulesExercise.Application.Rules
{
    public class Effect
    {
        public string Type { get; set; } = default!;

        public int TemplateId { get; set; }

        public Dictionary<string, string> PlaceHolders { get; set; } = new Dictionary<string, string>();
    }
}
