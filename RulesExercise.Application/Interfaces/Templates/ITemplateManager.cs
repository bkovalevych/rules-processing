using RulesExercise.Domain.Entities;

namespace RulesExercise.Application.Interfaces.Templates
{
    public interface ITemplateManager
    {
        Task<Template> GetTemplateAsync(int id);

        Task DeleteTemplateByIdAsync(int id);
        
        Task<Template> UpdateTemplate(Template template);

        Task<Template> AddTemplateAsync(Template template);
        
        string FormatFromTemplate(string template, Dictionary<string, object> Values);
    }
}
