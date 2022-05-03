using RulesExercise.Application.Interfaces.Templates;
using RulesExercise.Domain.Entities;

namespace RulesExercise.Infrastructure.Templates
{
    public class TemplateManager : ITemplateManager
    {
        public Task<Template> AddTemplateAsync(Template template)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTemplateByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public string FormatFromTemplate(string template, Dictionary<string, string> Values)
        {
            return string.Join("\n", Values.Values);
        }

        public async Task<Template> GetTemplateAsync(int id)
        {
            return new Template()
            {
                Id = id,
                Message = "",
                Subject = "",
                Type = "Telegram"
            };
        }

        public Task<Template> UpdateTemplate(Template template)
        {
            throw new NotImplementedException();
        }
    }
}
