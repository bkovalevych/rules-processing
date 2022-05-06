using DotLiquid;
using DotLiquid.NamingConventions;
using RulesExercise.Application.Interfaces.Templates;

namespace RulesExercise.Infrastructure.Templates
{
    public class TemplateManager : ITemplateManager
    {
        public TemplateManager()
        {
            Template.NamingConvention = new CSharpNamingConvention();
        }

        public string FormatFromTemplate(string template, Dictionary<string, object> Values)
        {
            var parsedTemplate = Template.Parse(template);
            var hashValues = Hash.FromDictionary(Values);
            var result = parsedTemplate.Render(hashValues);
            return result;
        }
    }
}
