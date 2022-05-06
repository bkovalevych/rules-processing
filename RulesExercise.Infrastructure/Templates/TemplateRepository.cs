using Microsoft.EntityFrameworkCore;
using RulesExercise.Application.Interfaces.Templates;
using RulesExercise.Domain.Entities;
using RulesExercise.Infrastructure.Persistence;

namespace RulesExercise.Infrastructure.Templates
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TemplateRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
