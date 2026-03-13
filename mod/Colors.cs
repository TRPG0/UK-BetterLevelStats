using UnityEngine;

namespace BetterLevelStats
{
    public static class Colors
    {
        public static Color P => new Color(1, 0.686f, 0);
        public static Color S => Color.red;
        public static Color A => new Color(1, 0.4157f, 0);
        public static Color B => new Color(1, 0.8471f, 0);
        public static Color C => new Color(0.2980f, 1, 0);
        public static Color D => new Color(0, 0.5804f, 1);
        public static Color Assist => new Color(0.2980f, 0.6f, 0.9020f);
        public static Color Cheat => new Color(0.25f, 1, 0.25f);

        public static Color GetRankColor(Ranks rank)
        {
            switch (rank)
            {
                case Ranks.S: return S;
                case Ranks.A: return A;
                case Ranks.B: return B;
                case Ranks.C: return C;
                case Ranks.D: return D;
                default: return Color.white;
            }
        }
    }
}
