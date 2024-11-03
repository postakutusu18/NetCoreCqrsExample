namespace Core.CrossCuttingConcerns.Logging.Serilog.Abstraction;
public interface ILogger
{
    void Trace(string message);

    void Critical(string message);

    void Information(string message);

    void Warning(string message);

    void Debug(string message);

    void Error(string message);
}