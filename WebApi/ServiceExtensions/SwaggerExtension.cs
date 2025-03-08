using Core.Security.WebApi;
using Microsoft.OpenApi.Models;

namespace WebApi.ServiceExtensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition(
                name: "Bearer",
                securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer YOUR_TOKEN\". \r\n\r\n"
                        + "`Enter your token in the text input below.`"
                }
            );
            opt.OperationFilter<BearerSecurityRequirementOperationFilter>();
        });
        return services;
    }
}