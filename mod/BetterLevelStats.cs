using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BetterLevelStats
{
    public class BetterLevelStats : MonoBehaviour
    {
        public static BetterLevelStats Instance;

        public LevelStats levelStats;

        public VerticalLayoutGroup thisLayoutGroup;
        public VerticalLayoutGroup parentLayoutGroup;

        public GameObject levelName;
        public GameObject difficulty;
        public GameObject bar;

        public GameObject timeCurrent;
        public StatRequirement timeS;
        public StatRequirement timeA;
        public StatRequirement timeB;
        public StatRequirement timeC;
        public StatRequirement timeNextBest;

        public GameObject killsCurrent;
        public StatRequirement killsS;
        public StatRequirement killsA;
        public StatRequirement killsB;
        public StatRequirement killsC;
        public StatRequirement killsNextBest;

        public GameObject styleCurrent;
        public StatRequirement styleS;
        public StatRequirement styleA;
        public StatRequirement styleB;
        public StatRequirement styleC;
        public StatRequirement styleNextBest;

        public GameObject secrets;
        public GameObject challengeCurrent;
        public GameObject assists;

        public StatCustom restarts;
        public StatCustom finalRank;

        public RankRequirement NextBestTime
        {
            get
            {
                float seconds = StatsManager.Instance.seconds;

                for (int i = 3; i >= 0; i--)
                {
                    if (seconds < StatsManager.Instance.timeRanks[i]) return new RankRequirement((Ranks)i + 1, StatsManager.Instance.timeRanks[i]);
                }
                return new RankRequirement(Ranks.D, 0);
            }
        }

        public RankRequirement NextBestKills
        {
            get
            {
                int kills = StatsManager.Instance.kills;

                for (int i = 0; i <= 3; i++)
                {
                    if (kills < StatsManager.Instance.killRanks[i]) return new RankRequirement((Ranks)i + 1, StatsManager.Instance.killRanks[i]);
                }
                return new RankRequirement(Ranks.S, StatsManager.Instance.killRanks[3]);
            }
        }

        public RankRequirement NextBestStyle
        {
            get
            {
                int style = StatsManager.Instance.stylePoints;

                for (int i = 0; i <= 3; i++)
                {
                    if (style < StatsManager.Instance.styleRanks[i]) return new RankRequirement((Ranks)i + 1, StatsManager.Instance.styleRanks[i]);
                }
                return new RankRequirement(Ranks.S, StatsManager.Instance.styleRanks[3]);
            }
        }

        public string FinalRank
        {
            get
            {
                if (AssistController.Instance.cheatsEnabled) return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.Cheat)}>-</color>";

                int score = 0;

                for (int i = 0; i <= 3; i++)
                {
                    if (StatsManager.Instance.seconds < StatsManager.Instance.timeRanks[i]) score++;
                }

                for (int i = 0; i <= 3; i++)
                {
                    if (StatsManager.Instance.kills >= StatsManager.Instance.killRanks[i]) score++;
                }

                for (int i = 0; i <= 3; i++)
                {
                    if (StatsManager.Instance.stylePoints >= StatsManager.Instance.styleRanks[i]) score++;
                }

                score -= StatsManager.Instance.restarts;

                if (score < 0) score = 0;

                if (score == 12 && StatsManager.Instance.majorUsed) score = 11;

                if (score == 12) return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.P)}>P</color>";

                float f = (float)score / 3f;
                score = Mathf.RoundToInt(f);

                switch (score)
                {
                    case 1:
                        if (StatsManager.Instance.majorUsed) return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.Assist)}>C</color>*";
                        else return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.C)}>C</color>";
                    case 2:
                        if (StatsManager.Instance.majorUsed) return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.Assist)}>B</color>*";
                        else return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.B)}>B</color>";
                    case 3:
                        if (StatsManager.Instance.majorUsed) return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.Assist)}>A</color>*";
                        else return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.A)}>A</color>";
                    case 4:
                    case 5:
                    case 6:
                        if (StatsManager.Instance.majorUsed) return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.Assist)}>S</color>*";
                        else return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.S)}>S</color>";
                    default:
                        if (StatsManager.Instance.majorUsed) return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.Assist)}>D</color>*";
                        else return $"<color=#{ColorUtility.ToHtmlStringRGBA(Colors.D)}>D</color>";
                }
            }
        }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                levelStats = GetComponent<LevelStats>();
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
                killsCurrent = transform.Find("Kills Title").gameObject;
                styleCurrent = transform.Find("Style Title").gameObject;
                secrets = transform.Find("Secrets Title").gameObject;
                challengeCurrent = transform.Find("Challenge Title").gameObject;
                assists = transform.Find("Assists Title").gameObject;

                timeCurrent.transform.Find("Time").GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -15, 0);
                timeCurrent.transform.Find("Time Rank").GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                killsCurrent.transform.Find("Kills").GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -15, 0);
                killsCurrent.transform.Find("Kills Rank").GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                styleCurrent.transform.Find("Style").GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -15, 0);
                styleCurrent.transform.Find("Style Rank").GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                secrets.transform.Find("Secret 1").GetComponent<RectTransform>().Translate(20, 0, 0, Space.Self);
                secrets.transform.Find("Secret 2").GetComponent<RectTransform>().Translate(20, 0, 0, Space.Self);
                secrets.transform.Find("Secret 3").GetComponent<RectTransform>().Translate(20, 0, 0, Space.Self);
                secrets.transform.Find("Secret 4").GetComponent<RectTransform>().Translate(20, 0, 0, Space.Self);
                secrets.transform.Find("Secret 5").GetComponent<RectTransform>().Translate(20, 0, 0, Space.Self);
                challengeCurrent.transform.Find("Challenge").GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -15, 0);
                assists.transform.Find("Assists").GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, -15, 0);

                GameObject timeSobj = GameObject.Instantiate(timeCurrent, transform);
                timeSobj.name = "Time (S)";
                Component.Destroy(timeSobj.GetComponent<TextMeshProUGUI>());
                timeSobj.AddComponent<LayoutElement>().minHeight = 8;
                foreach (TextMeshProUGUI text in timeSobj.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    text.color = new Color(1, 1, 1, 0.5f);
                    text.fontSize = 18;
                }
                timeSobj.transform.Find("Time Rank").GetComponent<TextMeshProUGUI>().margin = new Vector4(0, 0, 2, 0);
                timeSobj.transform.MoveAfterSibling(timeCurrent.transform, true);
                timeS = timeSobj.AddComponent<StatRequirement>();
                timeS.Init(timeSobj.transform.Find("Time").GetComponent<TextMeshProUGUI>(), timeSobj.transform.Find("Time Rank").GetComponent<TextMeshProUGUI>());

                GameObject timeAobj = GameObject.Instantiate(timeSobj, transform);
                timeAobj.name = "Time (A)";
                timeAobj.transform.MoveAfterSibling(timeSobj.transform, true);
                timeA = timeAobj.GetComponent<StatRequirement>();

                GameObject timeBobj = GameObject.Instantiate(timeAobj, transform);
                timeBobj.name = "Time (B)";
                timeBobj.transform.MoveAfterSibling(timeAobj.transform, true);
                timeB = timeBobj.GetComponent<StatRequirement>();

                GameObject timeCobj = GameObject.Instantiate(timeBobj, transform);
                timeCobj.name = "Time (C)";
                timeCobj.transform.MoveAfterSibling(timeBobj.transform, true);
                timeC = timeCobj.GetComponent<StatRequirement>();

                GameObject timeNBobj = GameObject.Instantiate(timeCobj, transform);
                timeNBobj.name = "Time (Next Best)";
                timeNBobj.transform.MoveAfterSibling(timeCobj.transform, true);
                timeNextBest = timeNBobj.GetComponent<StatRequirement>();

                timeS.SetText(GetTimeStringFromInt(StatsManager.Instance.timeRanks[3]), "S", Colors.S);
                timeA.SetText(GetTimeStringFromInt(StatsManager.Instance.timeRanks[2]), "A", Colors.A);
                timeB.SetText(GetTimeStringFromInt(StatsManager.Instance.timeRanks[1]), "B", Colors.B);
                timeC.SetText(GetTimeStringFromInt(StatsManager.Instance.timeRanks[0]), "C", Colors.C);
                timeNextBest.SetText(GetTimeStringFromInt(StatsManager.Instance.timeRanks[3]), "S", Colors.S);

                GameObject killsSobj = GameObject.Instantiate(timeSobj, transform);
                killsSobj.name = "Kills (S)";
                killsSobj.transform.MoveAfterSibling(killsCurrent.transform, true);
                killsS = killsSobj.GetComponent<StatRequirement>();

                GameObject killsAobj = GameObject.Instantiate(killsSobj, transform);
                killsAobj.name = "Kills (A)";
                killsAobj.transform.MoveAfterSibling(killsSobj.transform, true);
                killsA = killsAobj.GetComponent<StatRequirement>();

                GameObject killsBobj = GameObject.Instantiate(killsAobj, transform);
                killsBobj.name = "Kills (B)";
                killsBobj.transform.MoveAfterSibling(killsAobj.transform, true);
                killsB = killsBobj.GetComponent<StatRequirement>();

                GameObject killsCobj = GameObject.Instantiate(killsBobj, transform);
                killsCobj.name = "Kills (C)";
                killsCobj.transform.MoveAfterSibling(killsBobj.transform, true);
                killsC = killsCobj.GetComponent<StatRequirement>();

                GameObject killsNBobj = GameObject.Instantiate(killsCobj, transform);
                killsNBobj.name = "Kills (Next Best)";
                killsNBobj.transform.MoveAfterSibling(killsCobj.transform, true);
                killsNextBest = killsNBobj.GetComponent<StatRequirement>();

                killsS.SetText(StatsManager.Instance.killRanks[3].ToString(), "S", Colors.S);
                killsA.SetText(StatsManager.Instance.killRanks[2].ToString(), "A", Colors.A);
                killsB.SetText(StatsManager.Instance.killRanks[1].ToString(), "B", Colors.B);
                killsC.SetText(StatsManager.Instance.killRanks[0].ToString(), "C", Colors.C);
                killsNextBest.SetText(StatsManager.Instance.killRanks[0].ToString(), "C", Colors.C);

                GameObject styleSobj = GameObject.Instantiate(timeSobj, transform);
                styleSobj.name = "Style (S)";
                styleSobj.transform.MoveAfterSibling(styleCurrent.transform, true);
                styleS = styleSobj.GetComponent<StatRequirement>();

                GameObject styleAobj = GameObject.Instantiate(styleSobj, transform);
                styleAobj.name = "Style (A)";
                styleAobj.transform.MoveAfterSibling(styleSobj.transform, true);
                styleA = styleAobj.GetComponent<StatRequirement>();

                GameObject styleBobj = GameObject.Instantiate(styleAobj, transform);
                styleBobj.name = "Style (B)";
                styleBobj.transform.MoveAfterSibling(styleAobj.transform, true);
                styleB = styleBobj.GetComponent<StatRequirement>();

                GameObject styleCobj = GameObject.Instantiate(styleBobj, transform);
                styleCobj.name = "Style (C)";
                styleCobj.transform.MoveAfterSibling(styleBobj.transform, true);
                styleC = styleCobj.GetComponent<StatRequirement>();

                GameObject styleNBobj = GameObject.Instantiate(styleCobj, transform);
                styleNBobj.name = "Style (Next Best)";
                styleNBobj.transform.MoveAfterSibling(styleCobj.transform, true);
                styleNextBest = styleNBobj.GetComponent<StatRequirement>();

                styleS.SetText(StatsManager.Instance.styleRanks[3].ToString(), "S", Colors.S);
                styleA.SetText(StatsManager.Instance.styleRanks[2].ToString(), "A", Colors.A);
                styleB.SetText(StatsManager.Instance.styleRanks[1].ToString(), "B", Colors.B);
                styleC.SetText(StatsManager.Instance.styleRanks[0].ToString(), "C", Colors.C);
                styleNextBest.SetText(StatsManager.Instance.styleRanks[0].ToString(), "C", Colors.C);

                GameObject restartsObj = GameObject.Instantiate(assists, transform);
                restartsObj.name = "Restarts Title";
                restarts = restartsObj.AddComponent<StatCustom>();
                restarts.Init(restartsObj.GetComponent<TextMeshProUGUI>(), restartsObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                restarts.SetDesc("RESTARTS:");
                restarts.SetValue("0");

                GameObject finalRankObj = GameObject.Instantiate(restartsObj, transform);
                finalRankObj.name = "Final Rank Title";
                finalRank = finalRankObj.GetComponent<StatCustom>();
                finalRank.statValue.alignment = TextAlignmentOptions.Right;
                finalRank.SetDesc("FINAL RANK:");
                finalRank.SetValue(FinalRank);

                ChangeMode(ConfigManager.levelMode.value);
                if (ConfigManager.levelMode.value == RankMode.SingleRank) ChangeTargetRank(ConfigManager.targetRank.value);

                ShowName(ConfigManager.showLevelName.value);
                ShowDifficulty(ConfigManager.showLevelDifficulty.value);
                ShowSecrets(ConfigManager.showLevelSecrets.value);
                ShowChallenge(ConfigManager.showLevelChallenge.value);
                assists.SetActive(ConfigManager.showLevelAssists.value);
                restarts.gameObject.SetActive(ConfigManager.showLevelRestarts.value);
                finalRank.gameObject.SetActive(ConfigManager.showLevelFinalRank.value);
            }
            else
            {
                DestroyImmediate(this);
            }
        }

        public void Update()
        {
            if (timeNextBest && timeNextBest.gameObject.activeSelf)
            {
                if (NextBestTime.Rank == Ranks.D) timeNextBest.SetText("-", NextBestTime.Rank.ToString(), Colors.GetRankColor(NextBestTime.Rank));
                else timeNextBest.SetText(GetTimeStringFromInt(NextBestTime.Value), NextBestTime.Rank.ToString(), Colors.GetRankColor(NextBestTime.Rank));
            }
            if (killsNextBest && killsNextBest.gameObject.activeSelf)
            {
                killsNextBest.SetText(NextBestKills.Value.ToString(), NextBestKills.Rank.ToString(), Colors.GetRankColor(NextBestKills.Rank));
            }
            if (styleNextBest && styleNextBest.gameObject.activeSelf)
            {
                styleNextBest.SetText(NextBestStyle.Value.ToString(), NextBestStyle.Rank.ToString(), Colors.GetRankColor(NextBestStyle.Rank));
            }
            if (restarts && restarts.gameObject.activeSelf)
            {
                if (StatsManager.Instance.restarts > 0) restarts.SetValue($"<color=red>{StatsManager.Instance.restarts}</color>");
                else restarts.SetValue("0");
            }
            if (finalRank && finalRank.gameObject.activeSelf)
            {
                finalRank.SetValue(FinalRank);
            }
        }

        public void ChangeMode(RankMode mode)
        {
            if (mode == RankMode.AllRanks)
            {
                if (ConfigManager.showLevelTime.value)
                {
                    timeS.gameObject.SetActive(true);
                    timeA.gameObject.SetActive(true);
                    timeB.gameObject.SetActive(true);
                    timeC.gameObject.SetActive(true);
                    timeNextBest.gameObject.SetActive(false);
                }

                if (ConfigManager.showLevelKills.value)
                {
                    killsS.gameObject.SetActive(true);
                    killsA.gameObject.SetActive(true);
                    killsB.gameObject.SetActive(true);
                    killsC.gameObject.SetActive(true);
                    killsNextBest.gameObject.SetActive(false);
                }

                if (ConfigManager.showLevelStyle.value)
                {
                    styleS.gameObject.SetActive(true);
                    styleA.gameObject.SetActive(true);
                    styleB.gameObject.SetActive(true);
                    styleC.gameObject.SetActive(true);
                    styleNextBest.gameObject.SetActive(false);
                }
            }
            else if (mode == RankMode.SingleRank)
            {
                ChangeTargetRank(ConfigManager.targetRank.value);
            }
            else if (mode == RankMode.NextBest)
            {
                if (ConfigManager.showLevelTime.value)
                {
                    timeS.gameObject.SetActive(false);
                    timeA.gameObject.SetActive(false);
                    timeB.gameObject.SetActive(false);
                    timeC.gameObject.SetActive(false);
                    timeNextBest.gameObject.SetActive(true);
                }

                if (ConfigManager.showLevelKills.value)
                {
                    killsS.gameObject.SetActive(false);
                    killsA.gameObject.SetActive(false);
                    killsB.gameObject.SetActive(false);
                    killsC.gameObject.SetActive(false);
                    killsNextBest.gameObject.SetActive(true);
                }
                
                if (ConfigManager.showLevelStyle.value)
                {
                    styleS.gameObject.SetActive(false);
                    styleA.gameObject.SetActive(false);
                    styleB.gameObject.SetActive(false);
                    styleC.gameObject.SetActive(false);
                    styleNextBest.gameObject.SetActive(true);
                }
            }
            else if (mode == RankMode.None)
            {
                timeS.gameObject.SetActive(false);
                timeA.gameObject.SetActive(false);
                timeB.gameObject.SetActive(false);
                timeC.gameObject.SetActive(false);
                timeNextBest.gameObject.SetActive(false);

                killsS.gameObject.SetActive(false);
                killsA.gameObject.SetActive(false);
                killsB.gameObject.SetActive(false);
                killsC.gameObject.SetActive(false);
                killsNextBest.gameObject.SetActive(false);

                styleS.gameObject.SetActive(false);
                styleA.gameObject.SetActive(false);
                styleB.gameObject.SetActive(false);
                styleC.gameObject.SetActive(false);
                styleNextBest.gameObject.SetActive(false);
            }
        }

        public void ChangeTargetRank(TargetRank targetRank)
        {
            if (ConfigManager.targetRank.value == TargetRank.S)
            {
                if (ConfigManager.showLevelTime.value)
                {
                    timeS.gameObject.SetActive(true);
                    timeA.gameObject.SetActive(false);
                    timeB.gameObject.SetActive(false);
                    timeC.gameObject.SetActive(false);
                    timeNextBest.gameObject.SetActive(false);
                }
                
                if (ConfigManager.showLevelKills.value)
                {
                    killsS.gameObject.SetActive(true);
                    killsA.gameObject.SetActive(false);
                    killsB.gameObject.SetActive(false);
                    killsC.gameObject.SetActive(false);
                    killsNextBest.gameObject.SetActive(false);
                }
                
                if (ConfigManager.showLevelStyle.value)
                {
                    styleS.gameObject.SetActive(true);
                    styleA.gameObject.SetActive(false);
                    styleB.gameObject.SetActive(false);
                    styleC.gameObject.SetActive(false);
                    styleNextBest.gameObject.SetActive(false);
                }
            }
            else if (ConfigManager.targetRank.value == TargetRank.A)
            {
                if (ConfigManager.showLevelTime.value)
                {
                    timeS.gameObject.SetActive(false);
                    timeA.gameObject.SetActive(true);
                    timeB.gameObject.SetActive(false);
                    timeC.gameObject.SetActive(false);
                    timeNextBest.gameObject.SetActive(false);
                }
                
                if (ConfigManager.showLevelKills.value)
                {
                    killsS.gameObject.SetActive(false);
                    killsA.gameObject.SetActive(true);
                    killsB.gameObject.SetActive(false);
                    killsC.gameObject.SetActive(false);
                    killsNextBest.gameObject.SetActive(false);
                }

                if (ConfigManager.showLevelStyle.value)
                {
                    styleS.gameObject.SetActive(false);
                    styleA.gameObject.SetActive(true);
                    styleB.gameObject.SetActive(false);
                    styleC.gameObject.SetActive(false);
                    styleNextBest.gameObject.SetActive(false);
                }
            }
            else if (ConfigManager.targetRank.value == TargetRank.B)
            {
                if (ConfigManager.showLevelTime.value)
                {
                    timeS.gameObject.SetActive(false);
                    timeA.gameObject.SetActive(false);
                    timeB.gameObject.SetActive(true);
                    timeC.gameObject.SetActive(false);
                    timeNextBest.gameObject.SetActive(false);
                }
                
                if (ConfigManager.showLevelKills.value)
                {
                    killsS.gameObject.SetActive(false);
                    killsA.gameObject.SetActive(false);
                    killsB.gameObject.SetActive(true);
                    killsC.gameObject.SetActive(false);
                    killsNextBest.gameObject.SetActive(false);
                }

                if (ConfigManager.showLevelStyle.value)
                {
                    styleS.gameObject.SetActive(false);
                    styleA.gameObject.SetActive(false);
                    styleB.gameObject.SetActive(true);
                    styleC.gameObject.SetActive(false);
                    styleNextBest.gameObject.SetActive(false);
                }
            }
            else if (ConfigManager.targetRank.value == TargetRank.C)
            {
                if (ConfigManager.showLevelTime.value)
                {
                    timeS.gameObject.SetActive(false);
                    timeA.gameObject.SetActive(false);
                    timeB.gameObject.SetActive(false);
                    timeC.gameObject.SetActive(true);
                    timeNextBest.gameObject.SetActive(false);
                }
                
                if (ConfigManager.showLevelKills.value)
                {
                    killsS.gameObject.SetActive(false);
                    killsA.gameObject.SetActive(false);
                    killsB.gameObject.SetActive(false);
                    killsC.gameObject.SetActive(true);
                    killsNextBest.gameObject.SetActive(false);
                }
                
                if (ConfigManager.showLevelStyle.value)
                {
                    styleS.gameObject.SetActive(false);
                    styleA.gameObject.SetActive(false);
                    styleB.gameObject.SetActive(false);
                    styleC.gameObject.SetActive(true);
                    styleNextBest.gameObject.SetActive(false);
                }
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

        public void ShowTime(bool value)
        {
            if (value)
            {
                timeCurrent.SetActive(true);
                ChangeMode(ConfigManager.levelMode.value);
            }
            else
            {
                timeCurrent.SetActive(false);
                timeS.gameObject.SetActive(false);
                timeA.gameObject.SetActive(false);
                timeB.gameObject.SetActive(false);
                timeC.gameObject.SetActive(false);
                timeNextBest.gameObject.SetActive(false);
            }
        }

        public void ShowKills(bool value)
        {
            if (value)
            {
                killsCurrent.SetActive(true);
                ChangeMode(ConfigManager.levelMode.value);
            }
            else
            {
                killsCurrent.SetActive(false);
                killsS.gameObject.SetActive(false);
                killsA.gameObject.SetActive(false);
                killsB.gameObject.SetActive(false);
                killsC.gameObject.SetActive(false);
                killsNextBest.gameObject.SetActive(false);
            }
        }

        public void ShowStyle(bool value)
        {
            if (value)
            {
                styleCurrent.SetActive(true);
                ChangeMode(ConfigManager.levelMode.value);
            }
            else
            {
                styleCurrent.SetActive(false);
                styleS.gameObject.SetActive(false);
                styleA.gameObject.SetActive(false);
                styleB.gameObject.SetActive(false);
                styleC.gameObject.SetActive(false);
                styleNextBest.gameObject.SetActive(false);
            }
        }

        public void ShowSecrets(bool value)
        {
            if (StatsManager.Instance.secretObjects.Length > 0) secrets.SetActive(ConfigManager.showLevelSecrets.value);
            else secrets.SetActive(false);
        }

        public void ShowChallenge(bool value)
        {
            if (levelStats.challenge != null) challengeCurrent.SetActive(value);
        }

        public void ShowAssists(bool value)
        {
            assists.SetActive(value);
        }

        public void ShowRestarts(bool value)
        {
            restarts.gameObject.SetActive(value);
        }

        public void ShowFinalRank(bool value)
        {
            finalRank.gameObject.SetActive(value);
        }

        public string GetTimeStringFromInt(int value) => $"{value/60}:{value%60:D2}.000";
    }
}
