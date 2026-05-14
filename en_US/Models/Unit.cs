using System.Collections.Generic;
using System.Linq;

namespace ShroundZoneHelper.Models
{
    public class Unit
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public Faction Faction { get; set; }
        public int PointsCost { get; set; }

        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int ARM { get; set; }
        public int MOV { get; set; }
        public int ED { get; set; }

        public bool IsProne { get; set; }
        public bool IsSuppressed { get; set; }
        public bool IsShocked { get; set; }
        public bool IsRooted { get; set; }

        public Weapon? EquippedWeapon { get; set; }
        public List<Skill> Skills { get; set; } = new();
        public List<StatusEffect> ActiveEffects { get; set; } = new();
        public List<string> AllowedWeaponNames { get; set; } = new();

        // Determine whether unit can act (suppressed units can only take short actions; simplified here)
        public bool CanAct()
        {
            return CurrentHP > 0 && !IsShocked;
        }

        // Effective defense dice (base ED + cover, etc.; here returns base only)
        public int GetEffectiveDefenseDice()
        {
            return ED;
        }

        // Receive damage; armor ignored for now, can refine later
        public void TakeDamage(int physicalDamage, bool ignoreArmor = true)
        {
            if (physicalDamage <= 0) return;
            int finalDamage = ignoreArmor ? physicalDamage : Math.Max(0, physicalDamage - ARM);
            CurrentHP = Math.Max(0, CurrentHP - finalDamage);
        }

        public void ApplyEffect(StatusEffect effect)
        {
            // Simple stacking logic; refine later
            var existing = ActiveEffects.FirstOrDefault(e => e.Type == effect.Type);
            if (existing != null)
                existing.Duration = effect.Duration;
            else
                ActiveEffects.Add(effect);

            // Apply immediate state flags
            switch (effect.Type)
            {
                case StatusEffectType.Suppressed: IsSuppressed = true; break;
                case StatusEffectType.Prone: IsProne = true; break;
                case StatusEffectType.Rooted: IsRooted = true; break;
                case StatusEffectType.Shocked: IsShocked = true; break;
            }
        }

        public void RemoveEffect(StatusEffectType type)
        {
            ActiveEffects.RemoveAll(e => e.Type == type);
            switch (type)
            {
                case StatusEffectType.Suppressed: IsSuppressed = false; break;
                case StatusEffectType.Prone: IsProne = false; break;
                case StatusEffectType.Rooted: IsRooted = false; break;
                case StatusEffectType.Shocked: IsShocked = false; break;
            }
        }
    }
}