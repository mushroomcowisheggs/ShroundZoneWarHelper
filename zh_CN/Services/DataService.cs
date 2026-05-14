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
                // 机兵侧
                new Weapon { Name = "机兵突击步枪", AttackDice = new DicePool(3,1,0), Range = new Range{Min=1, Max=6}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "机兵冲锋枪", AttackDice = new DicePool(2,1,0), Range = new Range{Min=1, Max=4}, ActionCost = ActionCost.Short },
                new Weapon { Name = "机兵轻机枪", AttackDice = new DicePool(0,4,0), Range = new Range{Min=1, Max=6}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "机兵重机枪", AttackDice = new DicePool(4,0,0), Range = new Range{Min=1, Max=5}, ActionCost = ActionCost.Long },
                new Weapon { Name = "机兵射手步枪", AttackDice = new DicePool(3,1,0), Range = new Range{Min=1, Max=8}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "反器材狙击步枪", AttackDice = new DicePool(3,0,0), Range = new Range{Min=1, Max=10}, ActionCost = ActionCost.Long,
                    Tags = { new Tag { Name = "穿甲", Value = 2 } } },
                new Weapon { Name = "突击霰弹枪", AttackDice = new DicePool(2,2,0), Range = new Range{Min=1, Max=4}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "机兵手枪", AttackDice = new DicePool(0,2,0), Range = new Range{Min=1, Max=5}, ActionCost = ActionCost.Short,
                    Tags = { new Tag { Name = "近战射击" } } },
                new Weapon { Name = "榴弹发射器", AttackDice = new DicePool(2,1,0), Range = new Range{Min=1, Max=8}, ActionCost = ActionCost.Long,
                    Tags = { new Tag { Name = "曲射" }, new Tag { Name = "扩散", Value = 1 } } },
                new Weapon { Name = "开山砍刀", AttackDice = new DicePool(4,1,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Long,
                    Tags = { new Tag { Name = "威胁", Value = 1 } } },
                // 烟烬侧
                new Weapon { Name = "针式 PDW", AttackDice = new DicePool(0,3,0), Range = new Range{Min=1, Max=5}, ActionCost = ActionCost.Short },
                new Weapon { Name = "衔尾蛇突击步枪", AttackDice = new DicePool(1,3,0), Range = new Range{Min=1, Max=5}, ActionCost = ActionCost.Medium },
                new Weapon { Name = "审判官霰弹", AttackDice = new DicePool(1,2,0), Range = new Range{Min=1, Max=3}, ActionCost = ActionCost.Short,
                    Tags = { new Tag { Name = "近战射击" } } },
                new Weapon { Name = "火石射手步枪", AttackDice = new DicePool(1,3,0), Range = new Range{Min=1, Max=7}, ActionCost = ActionCost.Long },
                new Weapon { Name = "猎龙大弓", AttackDice = new DicePool(0,4,0), Range = new Range{Min=1, Max=6}, ActionCost = ActionCost.Medium,
                    Tags = { new Tag { Name = "压制" }, new Tag { Name = "变形武器" } },
                    IsDeformable = true,
                    AlternateForm = new Weapon { Name = "猎龙大弓(变形)", AttackDice = new DicePool(3,0,0), Range = new Range{Min=1, Max=9}, ActionCost = ActionCost.Long,
                        Tags = { new Tag { Name = "穿甲", Value = 1 }, new Tag { Name = "扩散", Value = 1 } } } },
                new Weapon { Name = "侵蚀葫芦", AttackDice = new DicePool(1,4,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Long,
                    Tags = { new Tag { Name = "压制" }, new Tag { Name = "曲射" } } },
                new Weapon { Name = "烟漫大剑", AttackDice = new DicePool(2,2,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Medium,
                    Tags = { new Tag { Name = "威胁", Value = 1 }, new Tag { Name = "变形武器" } },
                    IsDeformable = true,
                    AlternateForm = new Weapon { Name = "烟漫大剑(变形)", AttackDice = new DicePool(4,0,0), Range = new Range{Min=1, Max=7}, ActionCost = ActionCost.Medium } },
                new Weapon { Name = "高周波镰刀", AttackDice = new DicePool(1,4,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Medium, 
                    Tags = { new Tag { Name = "顺劈" } } 
                },
                new Weapon { Name = "战术匕首", AttackDice = new DicePool(1,2,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Short }, 
                new Weapon { Name = "冲击炮台", AttackDice = new DicePool(1,2,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Medium }, 
                new Weapon { Name = "机炮炮台", AttackDice = new DicePool(3,2,0), Range = new Range{Min=1, Max=1}, ActionCost = ActionCost.Medium }
            };
        }

        private static void InitializeUnitPresets()
        {
            UnitPresets = new List<Unit>
            {
                // 机兵
                new Unit { Name = "方阵步兵", Faction = Faction.Mechanized, PointsCost = 130, MaxHP = 5, CurrentHP = 5, ARM = 2, MOV = 4, ED = 2,
                    Skills = { new Skill { Name = "机动掩体", Description = "敌方攻击友军时视为重型掩体" } 
                    }, 
                    AllowedWeaponNames = new List<string> { "机兵突击步枪", "机兵手枪", "开山砍刀", "突击霰弹枪" } 
                },
                new Unit { Name = "御械师", Faction = Faction.Mechanized, PointsCost = 200, MaxHP = 6, CurrentHP = 6, ARM = 2, MOV = 5, ED = 4,
                    Skills = { new Skill { Name = "指挥光环", Description = "爆发3内友军ARM+1" },
                               new Skill { Name = "战场维修", ActionCost = ActionCost.Medium, Description = "回复2点生命" },
                               new Skill { Name = "战术包抄", ActionCost = ActionCost.Medium, Description = "指定友军获得一个免费短动作" } 
                    }, 
                    AllowedWeaponNames = new List<string> { "机兵突击步枪", "突击霰弹枪", "机兵手枪", "机兵轻机枪", "开山砍刀" }
                },
                new Unit { Name = "浪人", Faction = Faction.Mechanized, PointsCost = 170, MaxHP = 5, CurrentHP = 5, ARM = 3, MOV = 6, ED = 0,
                    Skills = { new Skill { Name = "次声波发生器", ActionCost = ActionCost.Short, Description = "爆发2内敌人震撼" },
                               new Skill { Name = "敏捷", Description = "白骰⚡视为闪避" } 
                    }, 
                    AllowedWeaponNames = new List<string> { "开山砍刀", "突击霰弹枪", "机兵突击步枪", "机兵冲锋枪" }
                },
                new Unit { Name = "战斗框架", Faction = Faction.Mechanized, PointsCost = 180, MaxHP = 8, CurrentHP = 8, ARM = 1, MOV = 3, ED = 3, 
                    AllowedWeaponNames = new List<string> { "机兵轻机枪", "榴弹发射器", "反器材狙击步枪" }
                },
                new Unit { Name = "火枪手", Faction = Faction.Mechanized, PointsCost = 150, MaxHP = 5, CurrentHP = 5, ARM = 1, MOV = 5, ED = 2,
                    Skills = { new Skill { Name = "卓越射击", Description = "攻击骰⚡算作一次命中" } }, 
                    AllowedWeaponNames = new List<string> { "机兵突击步枪", "反器材狙击步枪", "机兵手枪", "机兵射手步枪" }
                },
                // 烟烬
                new Unit { Name = "步兵单位", Faction = Faction.Ember, PointsCost = 140, MaxHP = 5, CurrentHP = 5, ARM = 2, MOV = 4, ED = 2,
                    Skills = { new Skill { Name = "无定型身躯", Description = "HP归零时重投防御骰" },
                               new Skill { Name = "塑胶炸弹", ActionCost = ActionCost.Short, LimitedUses = 1, Description = "延时爆炸4红伤害扩散2" } }, 
                    AllowedWeaponNames = new List<string> { "衔尾蛇突击步枪", "火石射手步枪", "侵蚀葫芦" }
                },
                new Unit { Name = "食尸鬼单位", Faction = Faction.Ember, PointsCost = 170, MaxHP = 6, CurrentHP = 6, ARM = 3, MOV = 5, ED = 1,
                    Skills = { new Skill { Name = "无定型身躯" },
                               new Skill { Name = "恐惧脉冲", ActionCost = ActionCost.Short, LimitedUses = 2, Description = "科技对抗4黄，成功则震撼" } }, 
                    AllowedWeaponNames = new List<string> { "审判官霰弹", "针式 PDW", "高周波镰刀", "战术匕首" }
                },
                new Unit { Name = "精英单位", Faction = Faction.Ember, PointsCost = 220, MaxHP = 7, CurrentHP = 7, ARM = 2, MOV = 4, ED = 3,
                    Skills = { new Skill { Name = "无定型身躯" },
                               new Skill { Name = "集束炸弹", ActionCost = ActionCost.Medium, LimitedUses = 2, Description = "2红2黄多目标2扩散1" } }, 
                    AllowedWeaponNames = new List<string> { "火石射手步枪", "猎龙大弓", "烟漫大剑", "高周波镰刀" }
                },
                new Unit { Name = "无定型炮台", Faction = Faction.Ember, PointsCost = 90, MaxHP = 4, CurrentHP = 4, ARM = 1, MOV = 4, ED = 1,
                    Skills = { new Skill { Name = "扎根", ActionCost = ActionCost.Short, Description = "MOV=0 ARM+2" } }, 
                    AllowedWeaponNames = new List<string> { "冲击炮台" }
                },
                new Unit { Name = "无定型机炮塔", Faction = Faction.Ember, PointsCost = 120, MaxHP = 5, CurrentHP = 5, ARM = 1, MOV = 3, ED = 1,
                    Skills = { new Skill { Name = "扎根", ActionCost = ActionCost.Short } }, 
                    AllowedWeaponNames = new List<string> { "机炮炮台" }
                }
            };
        }

        public static List<Weapon> GetAllWeaponsForFaction(Faction faction)
        {
            // 保留函数：所有武器均可选用
            return AllWeapons;
        }
    }
}