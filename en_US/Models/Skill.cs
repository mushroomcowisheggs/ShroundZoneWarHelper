using System.Collections.Generic;

namespace ShroundZoneHelper.Models
{
    public class Skill
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public ActionCost? ActionCost { get; set; }
        public int? LimitedUses { get; set; }
        public int RemainingUses { get; set; }
        public Range? Range { get; set; }
        public List<Tag> Tags { get; set; } = new();
        public string Description { get; set; } = string.Empty;

        public bool CanUse => !LimitedUses.HasValue || RemainingUses > 0;
        public void ConsumeUse() { if (LimitedUses.HasValue) RemainingUses--; }
        public void ResetUses() { RemainingUses = LimitedUses ?? 0; }
    }
}