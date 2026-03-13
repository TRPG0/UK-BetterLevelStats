using HarmonyLib;

namespace BetterLevelStats.Patches
{
    [HarmonyPatch(typeof(LevelStats), "Start")]
    public class LevelStats_Start_Patch
    {
        public static void Postfix(LevelStats __instance)
        {
            if (__instance.cyberGrind) __instance.GetOrAddComponent<BetterCGStats>();
            else if (!__instance.secretLevel) __instance.GetOrAddComponent<BetterLevelStats>();
        }
    }
}
