using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;


namespace Core.CrossCuttingConcerns.Logging.Serilog.Logger;

public class GraylogLogger : SeriLoggerServiceBase
{
    public GraylogLogger(IConfiguration configuration) : base(null)
    {
        GraylogConfiguration logConfiguration = configuration
            .GetSection("SeriLogConfigurations:GraylogConfiguration")
            .Get<GraylogConfiguration>();

        Logger = new LoggerConfiguration().WriteTo
            .Graylog(
                new GraylogSinkOptions
                {
                    HostnameOrAddress = logConfiguration.HostnameOrAddress,
                    Port = logConfiguration.Port,
                    TransportType = TransportType.Udp
                }
            )
            .CreateLogger();
    }
}
