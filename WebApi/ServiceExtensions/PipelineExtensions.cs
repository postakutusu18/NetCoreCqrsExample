using System.Reflection;

namespace WebApi.ServiceExtensions;

public static class PipelineExtensions
{
    public static IServiceCollection AddPipelineExtensions(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
        });
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));

        return services;
    }
}
