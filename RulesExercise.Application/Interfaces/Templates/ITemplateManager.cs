﻿using RulesExercise.Domain.Entities;

namespace RulesExercise.Application.Interfaces.Templates
{
    public interface ITemplateManager
    {
        Task<Template> GetTemplateAsync(int id);

        Task DeleteTemplateByIdAsync(int id);
        
        Task<Template> UpdateTemplate(Template template);

        Task<Template> AddTemplateAsync(Template template);
        
        string FormatFromTemplate(Template template, Dictionary<string, string> Values);
    }
}
