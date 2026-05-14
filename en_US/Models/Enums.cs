namespace ShroundZoneHelper.Models
{
    public enum Faction { Mechanized, Ember }
    public enum ActionCost { Short, Medium, Long }
    public enum StatusEffectType { Suppressed, Prone, Rooted, Shocked }

    public enum DiceSymbol
    {
        Red_SolidHeavy,      // ⚫
        Red_HollowHeavy,     // ○
        Red_HollowLight,     // ◎
        Red_Lightning,       // ⚡
        Yellow_DoubleLight,  // ●×2
        Yellow_Light,        // ●
        Yellow_HollowLight,  // ◎
        Yellow_Lightning,    // ⚡
        Yellow_Blank,        // 空
        White_SolidDefense,  // ▲
        White_DoubleHollow,  // △×2
        White_Dodge,         // ⇲
        White_Lightning,     // ⚡
        White_Blank          // 空
    }

    public struct DicePool
    {
        public int RedDice { get; set; }
        public int YellowDice { get; set; }
        public int WhiteDice { get; set; }

        public DicePool(int red, int yellow, int white)
        {
            RedDice = red;
            YellowDice = yellow;
            WhiteDice = white;
        }
    }

    public struct Range
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public bool IsMelee => Max == 1 && Min == 1;
        public bool RequiresLineOfSight => Max > 1; // 远程需视线
    }
}