namespace ShroundZoneHelper.Models
{
    public class Tag
    {
        public string Name { get; set; } = string.Empty;
        public int? Value { get; set; }
        public override string ToString() => Value.HasValue ? $"{Name} {Value}" : Name;
    }
}