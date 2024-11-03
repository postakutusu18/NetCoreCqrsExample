using Application;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.ElasticSearch.Models;
using Core.Localization;
using Core.Mailing;
using Core.Security.Encryption;
using Core.Security.Jwt;
using Core.Security.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using WebApi;

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


const string tokenOptionsConfigurationSection = "TokenOptions";
TokenOptions tokenOptions =
    builder.Configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
    ?? throw new InvalidOperationException($"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration.");
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });



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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(opt =>
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
