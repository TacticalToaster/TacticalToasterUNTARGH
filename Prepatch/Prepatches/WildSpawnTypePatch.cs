using BepInEx.Logging;
using EFT;
using Mono.Cecil;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TacticalToasterUNTARGH.Prepatches;

public static class UNTARWildSpawnTypePatch
{
    public static IEnumerable<string> TargetDLLs { get; } = new[] { "Assembly-CSharp.dll" };

    public static List<int> suitableList;

    public static void Patch(ref AssemblyDefinition assembly)
    {
        if (!ShouldPatchAssembly())
        {
            Logger.CreateLogSource("UNTAR Go Home Prepatch")
                  .LogWarning("UNTAR Go Home plugin not detected, not patching assembly. Make sure you have installed or uninstalled the mod correctly.");
            return;
        }

        suitableList = new List<int>();

        UNTAREnums.AddBot("followeruntar", brain: (int)WildSpawnType.followerSanitar, suitableList: suitableList);
        UNTAREnums.AddBot("bossuntarlead", brain: (int)WildSpawnType.exUsec, suitableList: suitableList);
        UNTAREnums.AddBot("followeruntarmarksman", brain: (int)WildSpawnType.exUsec, suitableList: suitableList);
        UNTAREnums.AddBot("bossuntarofficer", brain: (int)WildSpawnType.exUsec, suitableList: suitableList);

        var wildSpawnType = assembly.MainModule.GetType("EFT.WildSpawnType");

        foreach (var botType in UNTAREnums.UNTARTypes)
        {
            Utils.AddEnumValue(ref wildSpawnType, botType.typeName, botType.wildSpawnType);
        }

        //Utils.AddEnumValue(ref wildSpawnType, UNTAREnums.BotUNTARName, UNTAREnums.BotUNTARValue);
    }

    public static bool IsUNTAR(this WildSpawnType role)
    {
        return UNTAREnums.UNTARTypesDict.ContainsKey((int)role);
    }

    private static bool ShouldPatchAssembly()
    {
        var patcherLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var bepDir = Directory.GetParent(patcherLoc);
        var modDllLoc = Path.Combine(bepDir.FullName, "plugins", "UNTARGH", "UNTARGHPlugin.dll");

        return File.Exists(modDllLoc);
    }
}
