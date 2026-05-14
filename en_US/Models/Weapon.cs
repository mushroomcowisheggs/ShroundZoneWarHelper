using System.Collections.Generic;
namespace ShroundZoneHelper.Models
{
    public class Weapon
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public DicePool AttackDice { get; set; }
        public Range Range { get; set; }
        public ActionCost ActionCost { get; set; }
        public List<Tag> Tags { get; set; } = new();
        public bool IsDeformable { get; set; }
        public Weapon? AlternateForm { get; set; }

        // 辅助显示用
        public string AttackDiceString => $"{AttackDice.RedDice}红 {AttackDice.YellowDice}黄";
    }
}