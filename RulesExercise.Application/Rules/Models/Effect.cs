namespace RulesExercise.Application.Rules.Models
{
    public class Effect
    {
        public string Type { get; set; } = default!;

        public int TemplateId { get; set; }

        public Dictionary<string, object> PlaceHolders { get; set; } = new Dictionary<string, object>();
    }
}
