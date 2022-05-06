namespace RulesExercise.Application.Interfaces.Templates
{
    public interface ITemplateManager
    {
        string FormatFromTemplate(string template, Dictionary<string, object> Values);
    }
}
