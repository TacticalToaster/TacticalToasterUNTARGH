

namespace TacticalToasterUNTARGH;

public class MainConfig
{
    public DebugConfig debug { get; set; }
    public PatrolConfig patrols { get; set; }

    public Dictionary<string, MapConfig> locations { get; set; }
}

public class DebugConfig
{
    public bool logs { get; set; }
    public bool spawnAlways { get; set; }
    public bool spawnInstantlyAlways { get; set; }
}

public class PatrolConfig
{
    public int minOfficerSize { get; set; }
    public int minSecondLeaderSize { get; set; }
    public float officerChance { get; set; }
    public float secondLeaderChance { get; set; }
}

public class MapConfig
{
    public bool enablePatrols { get; set; }
    public float patrolChance { get; set; }
    public int patrolAmount { get; set; }
    public int patrolMin { get; set; }
    public int patrolMax { get; set; }
    public List<string> patrolZones { get; set; }
    public int patrolTimeMin { get; set; }
    public int patrolTimeMax { get; set; }
}