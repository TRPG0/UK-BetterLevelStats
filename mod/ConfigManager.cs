using PluginConfig.API;
using PluginConfig.API.Fields;
using System.IO;

namespace BetterLevelStats
{
    public class ConfigManager
    {
        public static PluginConfigurator config = null;

        public static ConfigPanel levelPanel;
        public static EnumField<RankMode> levelMode;
        public static EnumField<TargetRank> targetRank;

        public static BoolField showLevelName;
        public static BoolField showLevelDifficulty;
        public static BoolField showLevelTime;
        public static BoolField showLevelKills;
        public static BoolField showLevelStyle;
        public static BoolField showLevelSecrets;
        public static BoolField showLevelChallenge;
        public static BoolField showLevelAssists;
        public static BoolField showLevelRestarts;
        public static BoolField showLevelFinalRank;

        public static ConfigPanel cgPanel;
        public static BoolField showCGBests;
        public static BoolField showCGName;
        public static BoolField showCGDifficulty;
        public static BoolField showCGTime;
        public static BoolField showCGKills;
        public static BoolField showCGStyle;
        public static BoolField showCGWave;
        public static BoolField showCGEnemiesLeft;

        public static void Init()
        {
            if (config != null) return;

            config = PluginConfigurator.Create(Core.PluginName, Core.PluginGUID);

            string iconPath = Path.Combine(Core.workingDir, "icon.png");
            if (File.Exists(iconPath)) config.SetIconWithURL(iconPath);

            levelPanel = new ConfigPanel(config.rootPanel, "LEVELS", "levelPanel");

            levelMode = new EnumField<RankMode>(levelPanel, "RANK REQUIREMENTS", "levelMode", RankMode.None);
            levelMode.SetEnumDisplayName(RankMode.AllRanks, "ALL RANKS");
            levelMode.SetEnumDisplayName(RankMode.SingleRank, "SINGLE RANK");
            levelMode.SetEnumDisplayName(RankMode.NextBest, "NEXT BEST RANK");
            levelMode.SetEnumDisplayName(RankMode.None, "NONE");
            levelMode.postValueChangeEvent += (RankMode value) =>
            {
                BetterLevelStats.Instance?.ChangeMode(value);
            };

            targetRank = new EnumField<TargetRank>(levelPanel, "SINGLE RANK TARGET", "targetRank", TargetRank.S);
            targetRank.postValueChangeEvent += (TargetRank value) =>
            {
                if (levelMode.value == RankMode.SingleRank) BetterLevelStats.Instance?.ChangeTargetRank(value);
            };

            showLevelName = new BoolField(levelPanel, "SHOW LEVEL NAME", "showLevelName", true);
            showLevelName.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowName(value);
            };

            showLevelDifficulty = new BoolField(levelPanel, "SHOW DIFFICULTY", "showLevelDifficulty", false);
            showLevelDifficulty.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowDifficulty(value);
            };

            showLevelTime = new BoolField(levelPanel, "SHOW TIME", "showLevelTime", true);
            showLevelTime.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowTime(value);
            };

            showLevelKills = new BoolField(levelPanel, "SHOW KILLS", "showLevelKills", true);
            showLevelKills.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowKills(value);
            };

            showLevelStyle = new BoolField(levelPanel, "SHOW STYLE", "showLevelStyle", true);
            showLevelStyle.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowStyle(value);
            };

            showLevelSecrets = new BoolField(levelPanel, "SHOW SECRETS", "showLevelSecrets", true);
            showLevelSecrets.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowSecrets(value);
            };

            showLevelChallenge = new BoolField(levelPanel, "SHOW CHALLENGE", "showLevelChallenge", true);
            showLevelChallenge.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowChallenge(value);
            };

            showLevelAssists = new BoolField(levelPanel, "SHOW MAJOR ASSISTS", "showLevelAssists", true);
            showLevelAssists.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowAssists(value);
            };

            showLevelRestarts = new BoolField(levelPanel, "SHOW RESTARTS", "showLevelRestarts", false);
            showLevelRestarts.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowRestarts(value);
            };

            showLevelFinalRank = new BoolField(levelPanel, "SHOW FINAL RANK", "showLevelFinalRank", false);
            showLevelFinalRank.postValueChangeEvent += (bool value) =>
            {
                BetterLevelStats.Instance?.ShowFinalRank(value);
            };

            cgPanel = new ConfigPanel(config.rootPanel, "THE CYBER GRIND", "cgPanel");

            showCGBests = new BoolField(cgPanel, "SHOW PERSONAL BESTS", "showCGBests", false);
            showCGBests.postValueChangeEvent += (bool value) =>
            {
                BetterCGStats.Instance?.ShowBests(value);
            };

            showCGName = new BoolField(cgPanel, "SHOW LEVEL NAME", "showCGName", true);
            showCGName.postValueChangeEvent += (bool value) =>
            {
                BetterCGStats.Instance?.ShowName(value);
            };

            showCGDifficulty = new BoolField(cgPanel, "SHOW DIFFICULTY", "showCGDifficulty", false);
            showCGDifficulty.postValueChangeEvent += (bool value) =>
            {
                BetterCGStats.Instance?.ShowDifficulty(value);
            };

            showCGTime = new BoolField(cgPanel, "SHOW TIME", "showCGTime", true);
            showCGTime.postValueChangeEvent += (bool value) =>
            {
                BetterCGStats.Instance?.ShowTime(value);
            };

            showCGKills = new BoolField(cgPanel, "SHOW KILLS", "showCGKills", false);
            showCGKills.postValueChangeEvent += (bool value) =>
            {
                BetterCGStats.Instance?.ShowKills(value);
            };

            showCGStyle = new BoolField(cgPanel, "SHOW STYLE", "showCGStyle", false);
            showCGStyle.postValueChangeEvent += (bool value) =>
            {
                BetterCGStats.Instance?.ShowStyle(value);
            };

            showCGWave = new BoolField(cgPanel, "SHOW WAVE", "showCGWave", true);
            showCGWave.postValueChangeEvent += (bool value) =>
            {
                BetterCGStats.Instance?.ShowWave(value);
            };

            showCGEnemiesLeft = new BoolField(cgPanel, "SHOW ENEMIES LEFT", "showCGEnemiesLeft", true);
            showCGEnemiesLeft.postValueChangeEvent += (bool value) =>
            {
                BetterCGStats.Instance?.ShowEnemiesLeft(value);
            };
        }
    }

    public enum RankMode
    {
        AllRanks,
        SingleRank,
        NextBest,
        None
    }

    public enum TargetRank
    {
        C,
        B,
        A,
        S
    }
}
