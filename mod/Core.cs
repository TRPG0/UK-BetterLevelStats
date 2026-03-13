using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;

namespace BetterLevelStats;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class Core : BaseUnityPlugin
{
    public const string PluginGUID = "trpg.betterlevelstats";
    public const string PluginName = "BetterLevelStats";
    public const string PluginVersion = "1.0.0";

    public static string workingPath;
    public static string workingDir;

    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        Logger = base.Logger;

        Harmony Harmony = new Harmony(PluginName);
        Harmony.PatchAll();

        workingPath = Assembly.GetExecutingAssembly().Location;
        workingDir = Path.GetDirectoryName(workingPath);

        ConfigManager.Init();
    }
}

public enum Ranks
{
    D,
    C,
    B,
    A,
    S
}