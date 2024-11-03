using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Logger;

public class SeriLogFileLogger : SeriLoggerServiceBase
{
    private readonly IConfiguration _configuration;
    public SeriLogFileLogger(IConfiguration configuration) : base(null)
    {
        _configuration = configuration;
        var logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                             .Get<FileLogConfiguration>() ??
                         throw new Exception(SerilogMessages.NullOptionsMessage);

        var logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + logConfig.FolderPath, ".txt");

        Logger = new LoggerConfiguration()
              .WriteTo.File(
                  logFilePath,
                  LogEventLevel.Verbose,
                  rollingInterval: RollingInterval.Day,
                  retainedFileCountLimit: null,
                  fileSizeLimitBytes: 5000000,
                  outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
              .CreateLogger();


    }
}