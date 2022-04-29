using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RulesExercise.Application.Rules.Models;

namespace RulesExercise.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<List<RuleSetting>>(config =>
            {
                configuration.GetSection("rules")
                .Bind(config);
            });

            return services;
        }
    }
}
