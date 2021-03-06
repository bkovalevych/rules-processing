using AutoMapper.EquivalencyExpression;
using Hangfire;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RulesExercise.Application.Interfaces.BackgroundJobHelpers;
using RulesExercise.Application.Interfaces.Senders;
using RulesExercise.Application.Interfaces.Templates;
using RulesExercise.Application.Rules;
using RulesExercise.Application.Rules.Models;
using RulesExercise.Infrastructure.BackgroundJobService;
using RulesExercise.Infrastructure.Persistence;
using RulesExercise.Infrastructure.Senders;
using RulesExercise.Infrastructure.Senders.Smtp;
using RulesExercise.Infrastructure.Senders.Telegram;
using RulesExercise.Infrastructure.Templates;

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
            services.Configure<SmtpConfiguration>(config =>
            {
                configuration.GetSection(nameof(SmtpConfiguration))
                .Bind(config);
            });
            services.Configure<TelegramConfiguration>(config =>
            {
                configuration.GetSection(nameof(TelegramConfiguration))
                .Bind(config);
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => 
                    {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    })
                );
            services.AddScoped<ITemplateManager, TemplateManager>();
            services.AddScoped<ISenderFactory, SenderFactory>();
            services.AddScoped<TelegramSender>();
            services.AddScoped<SmtpSender>();
            services.AddSingleton(provider =>
            {
                var options = provider.GetRequiredService<IOptions<List<RuleSetting>>>();
                return new RulesManager(options.Value);
            });
            AddHangFire(services);
            services.AddScoped<IBackgroundWorkerService, BackgroundWorkerService>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddAutoMapper(assemblies)
                    .AddAutoMapper(cfg => cfg.AddCollectionMappers());
            services.AddMediatR(assemblies);
           

            return services;
        }

        private static void AddHangFire(IServiceCollection services)
        {
            services.AddHangfire(gloabalConf => gloabalConf
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage());

            services.AddHangfireServer();
        }
    }
}
