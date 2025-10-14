using SPTarkov.DI.Annotations;

[Injectable(InjectionType.Singleton)]
public class UNTARLogger
{
    private readonly bool _enableLogs;
    private readonly ILogger<UNTARLogger> _logger;
    public UNTARLogger(
        ILogger<UNTARLogger> logger,
        ConfigController configController)
    {
        _enableLogs = configController.ModConfig.debug.logs;
        _logger = logger;
    }

    public void Info(string message)
    {
        if (_enableLogs)
        {
            _logger.LogInformation($"[UNTAR Mod] {message}");
        }
    }
    public void Warn(string message)
    {
        _logger.LogWarning($"[UNTAR Mod] WARNING: {message}");
    }
    public void Error(string message)
    {
        _logger.LogError($"[UNTAR Mod] ERROR: {message}");
    }
}