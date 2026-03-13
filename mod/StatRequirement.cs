using TMPro;
using UnityEngine;

namespace BetterLevelStats
{
    public class StatRequirement : MonoBehaviour
    {
        public TextMeshProUGUI statNumber;
        public TextMeshProUGUI statRank;

        public void Init(TextMeshProUGUI statNumber, TextMeshProUGUI statRank)
        {
            this.statNumber = statNumber;
            this.statRank = statRank;
        }

        public void SetText(string value, string rank, Color color)
        {
            if (value == "-") statNumber.text = "-:--.---";
            else statNumber.text = value;

            statRank.text = $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{rank}</color>";
        }
    }
}
