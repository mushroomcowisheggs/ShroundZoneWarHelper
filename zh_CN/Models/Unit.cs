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

        // 判断单位是否可行动（压制状态只能短动作，此处简化）
        public bool CanAct()
        {
            return CurrentHP > 0 && !IsShocked;
        }

        // 有效防御骰数量（基础 ED + 掩体等，此处只返回基础）
        public int GetEffectiveDefenseDice()
        {
            return ED;
        }

        // 承受伤害，暂时忽略护甲，后续可细化
        public void TakeDamage(int physicalDamage, bool ignoreArmor = true)
        {
            if (physicalDamage <= 0) return;
            int finalDamage = ignoreArmor ? physicalDamage : Math.Max(0, physicalDamage - ARM);
            CurrentHP = Math.Max(0, CurrentHP - finalDamage);
        }

        public void ApplyEffect(StatusEffect effect)
        {
            // 简单叠加逻辑，正式可细化
            var existing = ActiveEffects.FirstOrDefault(e => e.Type == effect.Type);
            if (existing != null)
                existing.Duration = effect.Duration;
            else
                ActiveEffects.Add(effect);

            // 立即影响状态标志
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