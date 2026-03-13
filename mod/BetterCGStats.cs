using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BetterLevelStats
{
    public class BetterCGStats : MonoBehaviour
    {
        public static BetterCGStats Instance;

        public LevelStats levelStats;
        public StatsManager statsManager;

        public VerticalLayoutGroup thisLayoutGroup;
        public VerticalLayoutGroup parentLayoutGroup;

        public GameObject levelName;
        public GameObject difficulty;
        public GameObject bar;

        public GameObject timeCurrent;
        public GameObject timePB;
        public GameObject waveCurrent;
        public GameObject wavePB;
        public GameObject enemiesLeft;

        public StatCustom killsCurrent;
        public GameObject killsPB;
        public StatCustom styleCurrent;
        public GameObject stylePB;
        
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                levelStats = GetComponent<LevelStats>();
                statsManager = StatsManager.Instance;
                parentLayoutGroup = transform.parent.gameObject.AddComponent<VerticalLayoutGroup>();
                thisLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();

                transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(285, 1000);

                parentLayoutGroup.childForceExpandHeight = false;
                thisLayoutGroup.childForceExpandHeight = false;
                thisLayoutGroup.padding = new RectOffset(10, 10, 10, 10);
                thisLayoutGroup.spacing = 10;

                levelName = transform.Find("Title").gameObject;

                difficulty = GameObject.Instantiate(levelName, transform);
                difficulty.name = "Difficulty";
                difficulty.transform.MoveAfterSibling(levelName.transform, true);
                difficulty.AddComponent<DifficultyTitle>().lines = true;

                bar = transform.Find("Panel").gameObject;
                bar.AddComponent<LayoutElement>().minHeight = 3;

                timeCurrent = transform.Find("Time Title").gameObject;
                waveCurrent = transform.Find("Kills Title").gameObject;
                enemiesLeft = transform.Find("Style Title").gameObject;

                timeCurrent.transform.Find("Time").GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -15, 0);
                waveCurrent.transform.Find("Kills").GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -15, 0);
                enemiesLeft.transform.Find("Style").GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -15, 0);

                CyberRankData cyberRankData = GameProgressSaver.GetBestCyber();
                int currentDifficulty = PrefsManager.Instance.GetInt("difficulty");

                timePB = GameObject.Instantiate(timeCurrent, transform);
                timePB.name = "Time (Personal Best)";
                TextMeshProUGUI bestTimeText = timePB.GetComponent<TextMeshProUGUI>();
                bestTimeText.fontSize = 18;
                bestTimeText.text = "BEST TIME:";
                bestTimeText.color = new Color(0.6f, 0.6f, 0.6f, 0.5f);
                TextMeshProUGUI bestTimeValue = timePB.transform.Find("Time").GetComponent<TextMeshProUGUI>();
                bestTimeValue.fontSize = 18;
                bestTimeValue.color = new Color(1, 1, 1, 0.5f);
                bestTimeValue.text = GetTimeStringFromFloat(cyberRankData.time[currentDifficulty]);
                timePB.transform.MoveAfterSibling(timeCurrent.transform, true);

                wavePB = GameObject.Instantiate(timePB, transform);
                wavePB.name = "Wave (Personal Best)";
                wavePB.GetComponent<TextMeshProUGUI>().text = "BEST WAVE:";
                wavePB.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = ((int)cyberRankData.preciseWavesByDifficulty[currentDifficulty]).ToString();
                wavePB.transform.MoveAfterSibling(waveCurrent.transform, true);

                GameObject killsObj = GameObject.Instantiate(waveCurrent, transform);
                killsObj.name = "Kills";
                killsCurrent = killsObj.AddComponent<StatCustom>();
                killsCurrent.Init(killsCurrent.GetComponent<TextMeshProUGUI>(), killsCurrent.transform.Find("Kills").GetComponent<TextMeshProUGUI>());
                killsCurrent.SetDesc("KILLS:");
                killsObj.transform.MoveAfterSibling(timePB.transform, true);

                killsPB = GameObject.Instantiate(timePB, transform);
                killsPB.name = "Kills (Personal Best)";
                killsPB.GetComponent<TextMeshProUGUI>().text = "BEST KILLS:";
                killsPB.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = cyberRankData.kills[currentDifficulty].ToString();
                killsPB.transform.MoveAfterSibling(killsObj.transform, true);

                GameObject styleObj = GameObject.Instantiate(waveCurrent, transform);
                styleObj.name = "Style";
                styleCurrent = styleObj.AddComponent<StatCustom>();
                styleCurrent.Init(styleCurrent.GetComponent<TextMeshProUGUI>(), styleCurrent.transform.Find("Kills").GetComponent<TextMeshProUGUI>());
                styleCurrent.SetDesc("STYLE:");
                styleObj.transform.MoveAfterSibling(killsPB.transform, true);

                stylePB = GameObject.Instantiate(timePB, transform);
                stylePB.name = "Style (Personal Best)";
                stylePB.GetComponent<TextMeshProUGUI>().text = "BEST STYLE:";
                stylePB.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = cyberRankData.style[currentDifficulty].ToString();
                stylePB.transform.MoveAfterSibling(styleObj.transform, true);

                ShowName(ConfigManager.showCGName.value);
                ShowDifficulty(ConfigManager.showCGDifficulty.value);
                ShowTime(ConfigManager.showCGTime.value);
                ShowKills(ConfigManager.showCGKills.value);
                ShowStyle(ConfigManager.showCGStyle.value);
                ShowWave(ConfigManager.showCGWave.value);
                ShowEnemiesLeft(ConfigManager.showCGEnemiesLeft.value);
                ShowBests(ConfigManager.showCGBests.value);
            }
            else
            {
                DestroyImmediate(this);
            }
        }

        public void Update()
        {
            if (killsCurrent && killsCurrent.gameObject.activeSelf)
            {
                killsCurrent.SetValue(statsManager.kills.ToString());
            }
            if (styleCurrent && styleCurrent.gameObject.activeSelf)
            {
                styleCurrent.SetValue(statsManager.stylePoints.ToString());
            }
        }

        public void ShowName(bool value)
        {
            if (value)
            {
                levelName.SetActive(true);
                bar.SetActive(true);
            }
            else
            {
                levelName.SetActive(false);
                if (!difficulty.activeSelf) bar.SetActive(false);
            }
        }

        public void ShowDifficulty(bool value)
        {
            if (value)
            {
                difficulty.SetActive(true);
                bar.SetActive(true);
            }
            else
            {
                difficulty.SetActive(false);
                if (!levelName.activeSelf) bar.SetActive(false);
            }
        }

        public void ShowBests(bool value)
        {
            if (value)
            {
                timePB.SetActive(timeCurrent.activeSelf);
                killsPB.SetActive(killsCurrent.gameObject.activeSelf);
                stylePB.SetActive(styleCurrent.gameObject.activeSelf);
                wavePB.SetActive(waveCurrent.activeSelf);
            }
            else
            {
                timePB.SetActive(false);
                killsPB.SetActive(false);
                stylePB.SetActive(false);
                wavePB.SetActive(false);
            }
        }

        public void ShowTime(bool value)
        {
            if (value)
            {
                timeCurrent.SetActive(true);
                ShowBests(ConfigManager.showCGBests.value);
            }
            else
            {
                timeCurrent.SetActive(false);
                timePB.SetActive(false);
            }
        }

        public void ShowKills(bool value)
        {
            if (value)
            {
                killsCurrent.gameObject.SetActive(true);
                ShowBests(ConfigManager.showCGBests.value);
            }
            else
            {
                killsCurrent.gameObject.SetActive(false);
                killsPB.SetActive(false);
            }
        }

        public void ShowStyle(bool value)
        {
            if (value)
            {
                styleCurrent.gameObject.SetActive(true);
                ShowBests(ConfigManager.showCGBests.value);
            }
            else
            {
                styleCurrent.gameObject.SetActive(false);
                stylePB.SetActive(false);
            }
        }

        public void ShowWave(bool value)
        {
            if (value)
            {
                waveCurrent.SetActive(true);
                ShowBests(ConfigManager.showCGBests.value);
            }
            else
            {
                waveCurrent.SetActive(false);
                wavePB.SetActive(false);
            }
        }

        public void ShowEnemiesLeft(bool value)
        {
            enemiesLeft.SetActive(value);
        }

        public string GetTimeStringFromFloat(float value)
        {
            int minutes = (int)value / 60;
            return $"{minutes}:" + (value - (minutes * 60)).ToString("00.000");
        }
    }
}
