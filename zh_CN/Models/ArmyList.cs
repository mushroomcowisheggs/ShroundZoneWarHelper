using System.Collections.Generic;
using System.Linq;

namespace ShroundZoneHelper.Models
{
    public class ArmyList
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public Faction Faction { get; set; }
        public int MaxPoints { get; set; }
        public List<UnitEntry> Units { get; set; } = new();

        public int CurrentPoints => Units.Sum(u => u.IndividualCost);
        public bool IsValid => CurrentPoints <= MaxPoints;

        public void AddUnit(Unit unit)
        {
            Units.Add(new UnitEntry { Unit = unit, IndividualCost = unit.PointsCost });
        }

        public void RemoveUnit(Unit unit)
        {
            Units.RemoveAll(e => e.Unit.Id == unit.Id);
        }
    }

    public class UnitEntry
    {
        public Unit Unit { get; set; } = new();
        public int IndividualCost { get; set; }
    }
}