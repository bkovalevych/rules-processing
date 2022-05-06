using Microsoft.EntityFrameworkCore;
using RulesExercise.Application.Interfaces.Templates;
using RulesExercise.Infrastructure.Persistence;
using DotLiquid.NamingConventions;
using TemplateParser = DotLiquid.Template;
using Template = RulesExercise.Domain.Entities.Template;
using DotLiquid;

namespace RulesExercise.Infrastructure.Templates
{
    internal class TemplateManager : ITemplateManager
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TemplateManager(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            TemplateParser.NamingConvention = new CSharpNamingConvention();
        }
        public async Task<Template> AddTemplateAsync(Template template)
        {
            _applicationDbContext.Templates.Add(template);
            await _applicationDbContext.SaveChangesAsync();
            return template;
        }

        public async Task DeleteTemplateByIdAsync(int id)
        {
            var template = await _applicationDbContext.Templates.FirstOrDefaultAsync(t => t.Id == id);
            if (template == null)
            {
                return;
            }
            _applicationDbContext.Templates.Remove(template);
            await _applicationDbContext.SaveChangesAsync();
        }

        public string FormatFromTemplate(string template, Dictionary<string, object> Values)
        {
            var parsedTemplate = TemplateParser.Parse(template);
            var hashValues = Hash.FromDictionary(Values);
            var result = parsedTemplate.Render(hashValues);
            return result;
        }

        public async Task<Template> GetTemplateAsync(int id)
        {
            var template = await _applicationDbContext
                .Templates
                .FirstOrDefaultAsync(it => it.Id == id);
            return template;
        }

        public async Task<Template> UpdateTemplate(Template template)
        {
            _applicationDbContext.Update(template);
            await _applicationDbContext.SaveChangesAsync();
            return template;
        }
    }
}
