using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Logger;

public class MongoDbLogger : SeriLoggerServiceBase
{
    public MongoDbLogger(IConfiguration configuration) : base(null)
    {
        MongoDbConfiguration? logConfiguration = configuration
            .GetSection("SeriLogConfigurations:MongoDbConfiguration")
            .Get<MongoDbConfiguration>();

        Logger = new LoggerConfiguration().WriteTo
            .MongoDBBson(cfg =>
            {
                MongoClient client = new(logConfiguration.ConnectionString);
                IMongoDatabase? mongoDbInstance = client.GetDatabase(logConfiguration.Collection);
                cfg.SetMongoDatabase(mongoDbInstance);
            })
            .CreateLogger();
    }
}
