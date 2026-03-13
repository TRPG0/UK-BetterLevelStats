using TMPro;
using UnityEngine;

namespace BetterLevelStats
{
    public class StatCustom : MonoBehaviour
    {
        public TextMeshProUGUI statDesc;
        public TextMeshProUGUI statValue;

        public void Init(TextMeshProUGUI statDesc, TextMeshProUGUI statValue)
        {
            this.statDesc = statDesc;
            this.statValue = statValue;
        }

        public void SetDesc(string text)
        {
            statDesc.text = text;
        }

        public void SetValue(string text)
        {
            statValue.text = text;
        }
    }
}
