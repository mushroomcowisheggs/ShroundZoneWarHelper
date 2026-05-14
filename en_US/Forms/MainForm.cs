using ShroundZoneHelper.Models;
using ShroundZoneHelper.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShroundZoneHelper.Forms
{
    public partial class MainForm : Form
    {
        // Dynamic army lists
        private ArmyList leftArmy;
        private ArmyList rightArmy;
        private ListView listLeft;
        private ListView listRight;
        private Label lblLeftPoints;
        private Label lblRightPoints;

        // Bottom action controls
        private TextBox txtDamageValue;
        private Button btnDamage, btnHeal, btnProne, btnSuppress, btnClearEffects, btnDeleteUnit, btnRestart;

        public MainForm()
        {
            // Base form settings (controls created dynamically)
                this.Text = "Shround Zone War Helper";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            StartNewGame();
        }

        /// <summary>
        /// Restart match: select factions and rebuild UI
        /// </summary>
        private void StartNewGame()
        {
            using (var selectForm = new FactionSelectForm())
            {
                if (selectForm.ShowDialog() != DialogResult.OK)
                {
                    // If user cancels, exit application
                    Application.Exit();
                    return;
                }

                // Clear existing controls (if any)
                this.Controls.Clear();

                // Build UI according to selection
                BuildUI(selectForm.LeftFaction, selectForm.RightFaction);

                // 初始化军表对象
                leftArmy = new ArmyList
                {
                    Name = $"Left Army {GetFactionName(selectForm.LeftFaction)} Roster",
                    Faction = selectForm.LeftFaction,
                    MaxPoints = 1000
                };
                rightArmy = new ArmyList
                {
                    Name = $"Right Army {GetFactionName(selectForm.RightFaction)} Roster",
                    Faction = selectForm.RightFaction,
                    MaxPoints = 1000
                };

                RefreshUnitLists();
            }
        }

        private string GetFactionName(Faction faction) => faction == Faction.Mechanized ? "Mechanized" : "Ember";

        /// <summary>
        /// 动态创建左右军表界面
        /// </summary>
        private void BuildUI(Faction leftFaction, Faction rightFaction)
        {
            // 左侧面板
            var panelLeft = new Panel { Dock = DockStyle.Left, Width = 500, Padding = new Padding(10) };
            var lblLeftTitle = new Label
            {
                Text = $"{GetFactionName(leftFaction)} Faction",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            listLeft = new ListView
            {
                Location = new Point(10, 40),
                Size = new Size(460, 400),
                View = View.Details,
                FullRowSelect = true,
                MultiSelect = false
            };
            listLeft.Columns.Add("Unit", 120);
            listLeft.Columns.Add("HP", 50);
            listLeft.Columns.Add("ARM", 50);
            listLeft.Columns.Add("MOV", 50);
                listLeft.Columns.Add("Weapon", 100);
                listLeft.Columns.Add("Status", 80);
            listLeft.SelectedIndexChanged += List_SelectedIndexChanged;

            lblLeftPoints = new Label { Text = "Total Points: 0 / 1000", Location = new Point(10, 450), AutoSize = true };
            var btnAddLeft = new Button { Text = "Add Unit", Location = new Point(350, 445), Size = new Size(100, 30) };
            btnAddLeft.Click += (s, e) => OpenArmyBuilder(leftFaction, leftArmy);

            panelLeft.Controls.AddRange(new Control[] { lblLeftTitle, listLeft, lblLeftPoints, btnAddLeft });

            // 右侧面板
            var panelRight = new Panel { Dock = DockStyle.Right, Width = 500, Padding = new Padding(10) };
            var lblRightTitle = new Label
            {
                Text = $"{GetFactionName(rightFaction)} Faction",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            listRight = new ListView
            {
                Location = new Point(10, 40),
                Size = new Size(460, 400),
                View = View.Details,
                FullRowSelect = true,
                MultiSelect = false
            };
            listRight.Columns.Add("Unit", 120);
            listRight.Columns.Add("HP", 50);
            listRight.Columns.Add("ARM", 50);
            listRight.Columns.Add("MOV", 50);
                listRight.Columns.Add("Weapon", 100);
                listRight.Columns.Add("Status", 80);
            listRight.SelectedIndexChanged += List_SelectedIndexChanged;

            lblRightPoints = new Label { Text = "Total Points: 0 / 1000", Location = new Point(10, 450), AutoSize = true };
            var btnAddRight = new Button { Text = "Add Unit", Location = new Point(350, 445), Size = new Size(100, 30) };
            btnAddRight.Click += (s, e) => OpenArmyBuilder(rightFaction, rightArmy);

            panelRight.Controls.AddRange(new Control[] { lblRightTitle, listRight, lblRightPoints, btnAddRight });

            // 底部操作栏
            var panelBottom = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Padding = new Padding(10, 8, 10, 8),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var lblDamage = new Label { Text = "Damage:", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft };
            txtDamageValue = new TextBox { Text = "1", Width = 50, Margin = new Padding(5, 3, 5, 3) };
            btnDamage = new Button { Text = "Deal Damage", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnDamage.Click += BtnDamage_Click;
            btnHeal = new Button { Text = "Heal", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnHeal.Click += BtnHeal_Click;
            btnProne = new Button { Text = "Toggle Prone", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
                btnProne.Click += BtnProne_Click;
            btnSuppress = new Button { Text = "Toggle Suppressed", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnSuppress.Click += BtnSuppress_Click;
            btnClearEffects = new Button { Text = "Clear Effects", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnClearEffects.Click += BtnClearEffects_Click;
            btnDeleteUnit = new Button { Text = "Delete Unit", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnDeleteUnit.Click += BtnDeleteUnit_Click;
            btnRestart = new Button { Text = "Restart", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.LightCoral };
            btnRestart.Click += BtnRestart_Click;

            panelBottom.Controls.AddRange(new Control[] {
                lblDamage, txtDamageValue,
                btnDamage, btnHeal, btnProne, btnSuppress, btnClearEffects, btnDeleteUnit, btnRestart
            });

            this.Controls.Add(panelLeft);
            this.Controls.Add(panelRight);
            this.Controls.Add(panelBottom);
        }

        private void OpenArmyBuilder(Faction faction, ArmyList army)
        {
            var builder = new ArmyBuilderForm(faction, army);
            if (builder.ShowDialog() == DialogResult.OK)
            {
                RefreshUnitLists();
            }
        }

        private void RefreshUnitLists()
        {
            RefreshListView(listLeft, leftArmy);
            RefreshListView(listRight, rightArmy);
            lblLeftPoints.Text = $"Total Points: {leftArmy.CurrentPoints} / {leftArmy.MaxPoints}";
            lblRightPoints.Text = $"Total Points: {rightArmy.CurrentPoints} / {rightArmy.MaxPoints}";
        }

        private void RefreshListView(ListView lv, ArmyList army)
        {
            if (lv == null || army == null) return;
            lv.Items.Clear();
            foreach (var entry in army.Units)
            {
                var u = entry.Unit;
                var item = new ListViewItem(u.Name);
                item.SubItems.Add($"{u.CurrentHP}/{u.MaxHP}");
                item.SubItems.Add(u.ARM.ToString());
                item.SubItems.Add(u.MOV.ToString());
                item.SubItems.Add(u.EquippedWeapon?.Name ?? "None");
                    string status = "";
                if (u.IsProne) status += "Prone ";
                if (u.IsSuppressed) status += "Suppressed ";
                if (u.IsShocked) status += "Shocked ";
                item.SubItems.Add(status.Trim());
                item.Tag = u;
                lv.Items.Add(item);
            }
        }

        private Unit GetSelectedUnit()
        {
            ListView activeLv = null;
            if (listLeft.Focused || listLeft.SelectedItems.Count > 0) activeLv = listLeft;
            else if (listRight.Focused || listRight.SelectedItems.Count > 0) activeLv = listRight;

            if (activeLv?.SelectedItems.Count > 0)
                return activeLv.SelectedItems[0].Tag as Unit;
            return null;
        }

        private void List_SelectedIndexChanged(object sender, EventArgs e) { }

        private void BtnDamage_Click(object sender, EventArgs e)
        {
            var unit = GetSelectedUnit();
            if (unit == null) { MessageBox.Show("Please select a unit first.", "Notice"); return; }
            if (!int.TryParse(txtDamageValue.Text, out int dmg) || dmg <= 0) return;
            unit.TakeDamage(dmg);
            RefreshUnitLists();
        }

        private void BtnHeal_Click(object sender, EventArgs e)
        {
            var unit = GetSelectedUnit();
            if (unit == null) { MessageBox.Show("Please select a unit first.", "Notice"); return; }
            if (!int.TryParse(txtDamageValue.Text, out int heal) || heal <= 0) heal = 1;
            unit.CurrentHP = Math.Min(unit.MaxHP, unit.CurrentHP + heal);
            RefreshUnitLists();
        }

        private void BtnProne_Click(object sender, EventArgs e)
        {
            var unit = GetSelectedUnit();
            if (unit == null) return;
            unit.IsProne = !unit.IsProne;
            RefreshUnitLists();
        }

        private void BtnSuppress_Click(object sender, EventArgs e)
        {
            var unit = GetSelectedUnit();
            if (unit == null) return;
            unit.IsSuppressed = !unit.IsSuppressed;
            RefreshUnitLists();
        }

        private void BtnClearEffects_Click(object sender, EventArgs e)
        {
            var unit = GetSelectedUnit();
            if (unit == null) return;
            unit.IsProne = unit.IsSuppressed = unit.IsShocked = unit.IsRooted = false;
            unit.ActiveEffects.Clear();
            RefreshUnitLists();
        }

        private void BtnDeleteUnit_Click(object sender, EventArgs e)
        {
            var unit = GetSelectedUnit();
            if (unit == null)
            {
                MessageBox.Show("Please select a unit first.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ArmyList targetArmy = null;
            if (leftArmy.Units.Any(entry => entry.Unit.Id == unit.Id))
                targetArmy = leftArmy;
            else if (rightArmy.Units.Any(entry => entry.Unit.Id == unit.Id))
                targetArmy = rightArmy;

            if (targetArmy == null) return;

            var confirmResult = MessageBox.Show(
                $"Are you sure you want to remove unit \"{unit.Name}\" from {targetArmy.Name}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                targetArmy.RemoveUnit(unit);
                RefreshUnitLists();
            }
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Restarting will clear all rosters and re-select factions. Continue?", "Confirm Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                StartNewGame();
            }
        }
    }
}