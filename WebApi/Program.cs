using Application;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.ElasticSearch.Models;
using Core.Localization;
using Core.Mailing;
using Core.Security.Jwt;
using Persistence;
using WebApi;
using WebApi.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationServices(
    configuration:builder.Configuration,
    mailSettings: builder.Configuration.GetSection("MailSettings").Get<MailSettings>()
        ?? throw new InvalidOperationException("MailSettings section cannot found in configuration."),
    elasticSearchConfig: builder.Configuration.GetSection("ElasticSearchConfig").Get<ElasticSearchConfig>()
        ?? throw new InvalidOperationException("ElasticSearchConfig section cannot found in configuration."),
    tokenOptions: builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>()
        ?? throw new InvalidOperationException("TokenOptions section cannot found in configuration.")
);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddJwtBearerExtension(builder.Configuration);

// Add services to the container.
builder.Services.AddMemoryCache();
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = "localhost:6379";
//    options.InstanceName = "mySchema:"; 
//});
builder.Services.AddSingleton<ICacheManager,MemoryCacheManager>();
//builder.Services.AddSingleton<ICacheManager, RedisCacheManager>();
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    })
);
//builder.Services.AddPipelineExtensions();

builder.Services.AddSwaggerExtension();

var app = builder.Build();
//app.UseMiddleware<AddClaimsMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
const string webApiConfigurationSection = "WebAPIConfiguration";
WebApiConfiguration webApiConfiguration =
    app.Configuration.GetSection(webApiConfigurationSection).Get<WebApiConfiguration>()
    ?? throw new InvalidOperationException($"\"{webApiConfigurationSection}\" section cannot found in configuration.");
app.UseCors(opt => opt.WithOrigins(webApiConfiguration.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.UseResponseLocalization();
app.Run();
