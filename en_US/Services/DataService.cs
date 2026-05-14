using ShroundZoneHelper.Models;
using System.Collections.Generic;
using System.Linq;
using Range = ShroundZoneHelper.Models.Range;

namespace ShroundZoneHelper.Services
{
    public static class DataService
    {
        public static List<Weapon> AllWeapons { get; private set; } = new();
        public static List<Unit> UnitPresets { get; private set; } = new();

        static DataService()
        {
            AllWeapons.Clear();
            UnitPresets.Clear();
            InitializeWeapons();
            InitializeUnitPresets();
        }

        private static void InitializeWeapons()
        {
            AllWeapons = new List<Weapon>
            {
                // Mech faction weapons
                new Weapon { Name = "Mech Assault Rifle", AttackDice = new DicePool(3,1,0), Range = new Range{Min=1, Max=6}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "Mech SMG", AttackDice = new DicePool(2,1,0), Range = new Range{Min=1, Max=4}, ActionCost = ActionCost.Short },
                new Weapon { Name = "Mech LMG", AttackDice = new DicePool(0,4,0), Range = new Range{Min=1, Max=6}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "Mech HMG", AttackDice = new DicePool(4,0,0), Range = new Range{Min=1, Max=5}, ActionCost = ActionCost.Long },
                new Weapon { Name = "Mech Marksman Rifle", AttackDice = new DicePool(3,1,0), Range = new Range{Min=1, Max=8}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "Anti-Material Sniper Rifle", AttackDice = new DicePool(3,0,0), Range = new Range{Min=1, Max=10}, ActionCost = ActionCost.Long,
                    Tags = { new Tag { Name = "ArmorPiercing", Value = 2 } } },
                new Weapon { Name = "Assault Shotgun", AttackDice = new DicePool(2,2,0), Range = new Range{Min=1, Max=4}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "Mech Pistol", AttackDice = new DicePool(0,2,0), Range = new Range{Min=1, Max=5}, ActionCost = ActionCost.Short,
                    Tags = { new Tag { Name = "CloseRangeShot" } } },
                new Weapon { Name = "Grenade Launcher", AttackDice = new DicePool(2,1,0), Range = new Range{Min=1, Max=8}, ActionCost = ActionCost.Long,
                    Tags = { new Tag { Name = "Indirect" }, new Tag { Name = "Spread", Value = 1 } } },
                new Weapon { Name = "Kaisan Cleaver", AttackDice = new DicePool(4,1,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Long,
                    Tags = { new Tag { Name = "Threat", Value = 1 } } },
                // Ember faction weapons
                new Weapon { Name = "Needle PDW", AttackDice = new DicePool(0,3,0), Range = new Range{Min=1, Max=5}, ActionCost = ActionCost.Short },
                new Weapon { Name = "Serpent Assault Rifle", AttackDice = new DicePool(1,3,0), Range = new Range{Min=1, Max=5}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "Judicator Shotgun", AttackDice = new DicePool(1,2,0), Range = new Range{Min=1, Max=3}, ActionCost = ActionCost.Short,
                    Tags = { new Tag { Name = "CloseRangeShot" } } },
                new Weapon { Name = "Flint Marksman Rifle", AttackDice = new DicePool(1,3,0), Range = new Range{Min=1, Max=7}, ActionCost = ActionCost.Long },
                new Weapon { Name = "Dragonhunter Longbow", AttackDice = new DicePool(0,4,0), Range = new Range{Min=1, Max=6}, ActionCost = ActionCost.Medium,
                    Tags = { new Tag { Name = "Suppress" }, new Tag { Name = "Deformable" } },
                    IsDeformable = true,
                    AlternateForm = new Weapon { Name = "Dragonhunter Longbow (Alt)", AttackDice = new DicePool(3,0,0), Range = new Range{Min=1, Max=9}, ActionCost = ActionCost.Long,
                        Tags = { new Tag { Name = "ArmorPiercing", Value = 1 }, new Tag { Name = "Spread", Value = 1 } } } },
                new Weapon { Name = "Erosion Gourd", AttackDice = new DicePool(1,4,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Long,
                    Tags = { new Tag { Name = "Suppress" }, new Tag { Name = "Indirect" } } },
                new Weapon { Name = "Smog Greatsword", AttackDice = new DicePool(2,2,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Medium,
                    Tags = { new Tag { Name = "Threat", Value = 1 }, new Tag { Name = "Deformable" } },
                    IsDeformable = true,
                    AlternateForm = new Weapon { Name = "Smog Greatsword (Alt)", AttackDice = new DicePool(4,0,0), Range = new Range{Min=1, Max=7}, ActionCost = ActionCost.Medium } },
                new Weapon { Name = "High-Frequency Sickle", AttackDice = new DicePool(1,4,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Medium, 
                    Tags = { new Tag { Name = "Cleave" } } 
                },
                new Weapon { Name = "Tactical Dagger", AttackDice = new DicePool(1,2,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Short }, 
                new Weapon { Name = "Impact Turret", AttackDice = new DicePool(1,2,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Medium }, 
                new Weapon { Name = "AutoCannon Turret", AttackDice = new DicePool(3,2,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Medium }
            };
        }

        private static void InitializeUnitPresets()
        {
            UnitPresets = new List<Unit>
            {
                // Mech units
                new Unit { Name = "Phalanx Infantry", Faction = Faction.Mechanized, PointsCost = 130, MaxHP = 5, CurrentHP = 5, ARM = 2, MOV = 4, ED = 2,
                    Skills = { new Skill { Name = "Mobile Cover", Description = "Treated as heavy cover when enemies attack allies" } 
                    }, 
                    AllowedWeaponNames = new List<string> { "Mech Assault Rifle", "Mech Pistol", "Kaisan Cleaver", "Assault Shotgun" } 
                },
                new Unit { Name = "Machinist", Faction = Faction.Mechanized, PointsCost = 200, MaxHP = 6, CurrentHP = 6, ARM = 2, MOV = 5, ED = 4,
                    Skills = { new Skill { Name = "Command Aura", Description = "Allies within 3 gain +1 ARM" },
                               new Skill { Name = "Field Repair", ActionCost = ActionCost.Medium, Description = "Restore 2 HP" },
                               new Skill { Name = "Tactical Flank", ActionCost = ActionCost.Medium, Description = "Grant a friendly unit a free short action" } 
                    }, 
                    AllowedWeaponNames = new List<string> { "Mech Assault Rifle", "Assault Shotgun", "Mech Pistol", "Mech LMG", "Kaisan Cleaver" }
                },
                new Unit { Name = "Ronin", Faction = Faction.Mechanized, PointsCost = 170, MaxHP = 5, CurrentHP = 5, ARM = 3, MOV = 6, ED = 0,
                    Skills = { new Skill { Name = "Infrasonic Generator", ActionCost = ActionCost.Short, Description = "Shock enemies within 2" },
                               new Skill { Name = "Agility", Description = "White dice ⚡ counts as dodge" } 
                    }, 
                    AllowedWeaponNames = new List<string> { "Kaisan Cleaver", "Assault Shotgun", "Mech Assault Rifle", "Mech SMG" }
                },
                new Unit { Name = "Combat Frame", Faction = Faction.Mechanized, PointsCost = 180, MaxHP = 8, CurrentHP = 8, ARM = 1, MOV = 3, ED = 3, 
                    AllowedWeaponNames = new List<string> { "Mech LMG", "Grenade Launcher", "Anti-Material Sniper Rifle" }
                },
                new Unit { Name = "Rifleman", Faction = Faction.Mechanized, PointsCost = 150, MaxHP = 5, CurrentHP = 5, ARM = 1, MOV = 5, ED = 2,
                    Skills = { new Skill { Name = "Superior Shot", Description = "Attack dice ⚡ counts as a hit" } }, 
                    AllowedWeaponNames = new List<string> { "Mech Assault Rifle", "Anti-Material Sniper Rifle", "Mech Pistol", "Mech Marksman Rifle" }
                },
                // Ember units
                new Unit { Name = "Infantry Unit", Faction = Faction.Ember, PointsCost = 140, MaxHP = 5, CurrentHP = 5, ARM = 2, MOV = 4, ED = 2,
                    Skills = { new Skill { Name = "Amorphous Body", Description = "When HP reaches 0, reroll defense dice" },
                               new Skill { Name = "Plastic Bomb", ActionCost = ActionCost.Short, LimitedUses = 1, Description = "Delayed explosion: 4 red damage, spread 2" } }, 
                    AllowedWeaponNames = new List<string> { "Serpent Assault Rifle", "Flint Marksman Rifle", "Erosion Gourd" }
                },
                new Unit { Name = "Ghoul Unit", Faction = Faction.Ember, PointsCost = 170, MaxHP = 6, CurrentHP = 6, ARM = 3, MOV = 5, ED = 1,
                    Skills = { new Skill { Name = "Amorphous Body" },
                               new Skill { Name = "Fear Pulse", ActionCost = ActionCost.Short, LimitedUses = 2, Description = "Tech contest 4 yellow; on success, target is shocked" } }, 
                    AllowedWeaponNames = new List<string> { "Judicator Shotgun", "Needle PDW", "High-Frequency Sickle", "Tactical Dagger" }
                },
                new Unit { Name = "Elite Unit", Faction = Faction.Ember, PointsCost = 220, MaxHP = 7, CurrentHP = 7, ARM = 2, MOV = 4, ED = 3,
                    Skills = { new Skill { Name = "Amorphous Body" },
                               new Skill { Name = "Cluster Bomb", ActionCost = ActionCost.Medium, LimitedUses = 2, Description = "2 red 2 yellow multi-target, spread 1" } }, 
                    AllowedWeaponNames = new List<string> { "Flint Marksman Rifle", "Dragonhunter Longbow", "Smog Greatsword", "High-Frequency Sickle" }
                },
                new Unit { Name = "Shapeshift Turret", Faction = Faction.Ember, PointsCost = 90, MaxHP = 4, CurrentHP = 4, ARM = 1, MOV = 4, ED = 1,
                    Skills = { new Skill { Name = "Root", ActionCost = ActionCost.Short, Description = "MOV=0 ARM+2" } }, 
                    AllowedWeaponNames = new List<string> { "Impact Turret" }
                },
                new Unit { Name = "Shapeshift Autocannon", Faction = Faction.Ember, PointsCost = 120, MaxHP = 5, CurrentHP = 5, ARM = 1, MOV = 3, ED = 1,
                    Skills = { new Skill { Name = "Root", ActionCost = ActionCost.Short } }, 
                    AllowedWeaponNames = new List<string> { "AutoCannon Turret" }
                }
            };
        }

        public static List<Weapon> GetAllWeaponsForFaction(Faction faction)
        {
            // Reserved function: all weapons are available
            return AllWeapons;
        }
    }
}