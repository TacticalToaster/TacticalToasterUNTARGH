using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Utils;

namespace TacticalToasterUNTARGH;

[Injectable(InjectionType.Singleton)]
public class UNTARLogger
{
    private readonly bool _enableLogs;
    private readonly ISptLogger<UNTARLogger> _logger;
    public UNTARLogger(
        ISptLogger<UNTARLogger> logger,
        ConfigController configController)
    {
        _enableLogs = configController.ModConfig.debug.logs;
        _logger = logger;
    }

    public void Info(string message)
    {
        if (_enableLogs)
        {
            _logger.Info($"[UNTAR Mod] {message}");
        }
    }
    public void Warn(string message)
    {
        _logger.Warning($"[UNTAR Mod] WARNING: {message}");
    }
    public void Error(string message)
    {
        _logger.Error($"[UNTAR Mod] ERROR: {message}");
    }
}