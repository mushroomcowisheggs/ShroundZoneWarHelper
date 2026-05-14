using ShroundZoneHelper.Models;
using ShroundZoneHelper.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ShroundZoneHelper.Forms
{
    public partial class ArmyBuilderForm : Form
    {
        private Faction faction;
        private ArmyList armyList;
        private ComboBox? cmbPreset;
        private ComboBox? cmbWeapon;
        private Button? btnAdd, btnCancel;

        public ArmyBuilderForm(Faction faction, ArmyList armyList)
        {
            this.faction = faction;
            this.armyList = armyList;
            InitializeComponent();
            LoadPresets();
        }

        private void InitializeComponent()
        {
            this.Text = $"Create {faction} Unit";
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var lblPreset = new Label { Text = "Select Unit Preset:", Location = new Point(20, 20), AutoSize = true };
            cmbPreset = new ComboBox { Location = new Point(20, 45), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblWeapon = new Label { Text = "Equip Weapon:", Location = new Point(20, 80), AutoSize = true };
            cmbWeapon = new ComboBox { Location = new Point(20, 105), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            btnAdd = new Button { Text = "Add", Location = new Point(240, 45), Size = new Size(80, 30) };
            btnAdd.Click += BtnAdd_Click;
            btnCancel = new Button { Text = "Cancel", Location = new Point(240, 100), Size = new Size(80, 30) };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lblPreset, cmbPreset, lblWeapon, cmbWeapon, btnAdd, btnCancel });
        }

        private void LoadPresets()
        {
            var presets = DataService.UnitPresets.Where(u => u.Faction == faction).ToList();
            cmbPreset!.DataSource = presets;
            cmbPreset.DisplayMember = "Name";
            cmbPreset.SelectedIndexChanged += (s, e) => LoadWeapons();
            LoadWeapons();
        }

        private void LoadWeapons() {
            if (cmbPreset!.SelectedItem is not Unit preset) { return; }
            var allowedWeapons = DataService.AllWeapons.Where(w => preset.AllowedWeaponNames.Contains(w.Name)).ToList();
            cmbWeapon!.DataSource = allowedWeapons;
            cmbWeapon.DisplayMember = "Name";
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (cmbPreset!.SelectedItem is not Unit preset) return;
            if (cmbWeapon!.SelectedItem is not Weapon weapon) return;

            // Clone a new unit instance
            var newUnit = new Unit
            {
                Name = preset.Name,
                Faction = preset.Faction,
                PointsCost = preset.PointsCost,
                MaxHP = preset.MaxHP,
                CurrentHP = preset.MaxHP,
                ARM = preset.ARM,
                MOV = preset.MOV,
                ED = preset.ED,
                Skills = preset.Skills.Select(s => new Skill
                {
                    Name = s.Name,
                    ActionCost = s.ActionCost,
                    LimitedUses = s.LimitedUses,
                    RemainingUses = s.LimitedUses ?? 0,
                    Description = s.Description
                }).ToList(),
                EquippedWeapon = weapon
            };

            armyList.AddUnit(newUnit);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}