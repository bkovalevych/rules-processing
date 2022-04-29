using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RulesExercise.Application.Rules;

namespace RulesExercise.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RulesSettings>(config =>
            {
                configuration.GetSection(nameof(RulesSettings))
                .Bind(config);
            });

            return services;
        }
    }
}
