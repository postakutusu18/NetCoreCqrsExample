using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog.Abstraction;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.ElasticSearch;
using Core.ElasticSearch.Models;
using Core.Localization;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Core.Security.DependencyInjection;
using Core.Security.Jwt;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration,
        MailSettings mailSettings,
        ElasticSearchConfig elasticSearchConfig,
        TokenOptions tokenOptions
    )
    {

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
            //    configuration.AddOpenBehavior(typeof(PerformanceBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>(_ => new MailKitMailService(mailSettings));
        services.AddSingleton<ILogger, SeriLogFileLogger>(_ => new SeriLogFileLogger(configuration));
        services.AddSingleton<ILogger, MsSqlLogger>(_=>new MsSqlLogger(configuration));
        services.AddSingleton<IElasticSearch, ElasticSearchManager>(_ => new ElasticSearchManager(elasticSearchConfig));

        //services.AddSingleton<LoggerServiceBase, FileLogger>();
        //services.AddSingleton<IElasticSearch, ElasticSearchManager>();

        services.AddYamlResourceLocalization();
        services.AddSecurityServices<Guid, int, Guid>(tokenOptions);

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IUserService, UserManager>();


        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
      this IServiceCollection services,
      Assembly assembly,
      Type type,
      Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
  )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
