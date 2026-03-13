using UnityEngine;

namespace BetterLevelStats
{
    public struct RankRequirement
    {
        public Ranks Rank;
        public int Value;

        public RankRequirement(Ranks rank, int value)
        {
            Rank = rank;
            Value = value;
        }

        public override string ToString() => $"{Value} <color=#{Colors.GetRankColor(Rank)}>{Rank.ToString()}</color>";
    }
}
