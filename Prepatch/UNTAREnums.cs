using System.Collections.Generic;
using TacticalToasterUNTARGH.Prepatches;

namespace TacticalToasterUNTARGH;

public static class UNTAREnums
{
    public const int BotUNTARValue = 1170;
    public const string BotUNTARName = "followeruntar";
    public const string BotUNTARRole = "UNTAR";

    private static int currentIndex = 0;

    public static List<UNTARBot> UNTARTypes;
    public static Dictionary<int, UNTARBot> UNTARTypesDict;

    private static void Add(UNTARBot bot)
    {
        if (UNTARTypes == null)
        {
            UNTARTypes = new List<UNTARBot>();
        }
        UNTARTypes.Add(bot);

        if (UNTARTypesDict == null)
        {
            UNTARTypesDict = new Dictionary<int, UNTARBot>();
        }
        UNTARTypesDict[bot.wildSpawnType] = bot;
    }

    public static void AddBot(string typeName, int brain = -1, string scavRole = BotUNTARRole, List<int> suitableList = null)
    {
        var wildSpawnType = BotUNTARValue + currentIndex;
        var bot = new UNTARBot
        {
            wildSpawnType = wildSpawnType,
            typeName = typeName,
            scavRole = scavRole,
            brain = brain
        };
        Add(bot);
        if (suitableList != null)
        {
            suitableList.Add(wildSpawnType);
        }
        currentIndex++;
    }
}

public struct UNTARBot
{
    public int wildSpawnType = 1170;
    public string typeName = "followeruntar";
    public string scavRole = "UNTAR";
    public int brain;

    public UNTARBot()
    {
    }
}