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
        // 动态军表
        private ArmyList leftArmy;
        private ArmyList rightArmy;
        private ListView listLeft;
        private ListView listRight;
        private Label lblLeftPoints;
        private Label lblRightPoints;

        // 底部操作控件
        private TextBox txtDamageValue;
        private Button btnDamage, btnHeal, btnProne, btnSuppress, btnClearEffects, btnDeleteUnit, btnRestart;

        public MainForm()
        {
            // 基础窗体设置（不再包含具体控件）
            this.Text = "遮蔽区战争辅助程序";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            StartNewGame();
        }

        /// <summary>
        /// 重新开始对局：选择阵营，重建界面
        /// </summary>
        private void StartNewGame()
        {
            using (var selectForm = new FactionSelectForm())
            {
                if (selectForm.ShowDialog() != DialogResult.OK)
                {
                    // 用户取消则退出程序
                    Application.Exit();
                    return;
                }

                // 清空现有控件（如果有）
                this.Controls.Clear();

                // 根据选择构建界面
                BuildUI(selectForm.LeftFaction, selectForm.RightFaction);

                // 初始化军表对象
                leftArmy = new ArmyList
                {
                    Name = $"左军 {GetFactionName(selectForm.LeftFaction)} 军表",
                    Faction = selectForm.LeftFaction,
                    MaxPoints = 1000
                };
                rightArmy = new ArmyList
                {
                    Name = $"右军 {GetFactionName(selectForm.RightFaction)} 军表",
                    Faction = selectForm.RightFaction,
                    MaxPoints = 1000
                };

                RefreshUnitLists();
            }
        }

        private string GetFactionName(Faction faction) => faction == Faction.Mechanized ? "机兵" : "烟烬";

        /// <summary>
        /// 动态创建左右军表界面
        /// </summary>
        private void BuildUI(Faction leftFaction, Faction rightFaction)
        {
            // 左侧面板
            var panelLeft = new Panel { Dock = DockStyle.Left, Width = 500, Padding = new Padding(10) };
            var lblLeftTitle = new Label
            {
                Text = $"{GetFactionName(leftFaction)}阵营",
                Font = new Font("微软雅黑", 12, FontStyle.Bold),
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
            listLeft.Columns.Add("单位", 120);
            listLeft.Columns.Add("HP", 50);
            listLeft.Columns.Add("ARM", 50);
            listLeft.Columns.Add("MOV", 50);
            listLeft.Columns.Add("武器", 100);
            listLeft.Columns.Add("状态", 80);
            listLeft.SelectedIndexChanged += List_SelectedIndexChanged;

            lblLeftPoints = new Label { Text = "总分: 0 / 1000", Location = new Point(10, 450), AutoSize = true };
            var btnAddLeft = new Button { Text = "编制单位", Location = new Point(350, 445), Size = new Size(100, 30) };
            btnAddLeft.Click += (s, e) => OpenArmyBuilder(leftFaction, leftArmy);

            panelLeft.Controls.AddRange(new Control[] { lblLeftTitle, listLeft, lblLeftPoints, btnAddLeft });

            // 右侧面板
            var panelRight = new Panel { Dock = DockStyle.Right, Width = 500, Padding = new Padding(10) };
            var lblRightTitle = new Label
            {
                Text = $"{GetFactionName(rightFaction)}阵营",
                Font = new Font("微软雅黑", 12, FontStyle.Bold),
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
            listRight.Columns.Add("单位", 120);
            listRight.Columns.Add("HP", 50);
            listRight.Columns.Add("ARM", 50);
            listRight.Columns.Add("MOV", 50);
            listRight.Columns.Add("武器", 100);
            listRight.Columns.Add("状态", 80);
            listRight.SelectedIndexChanged += List_SelectedIndexChanged;

            lblRightPoints = new Label { Text = "总分: 0 / 1000", Location = new Point(10, 450), AutoSize = true };
            var btnAddRight = new Button { Text = "编制单位", Location = new Point(350, 445), Size = new Size(100, 30) };
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

            var lblDamage = new Label { Text = "伤害值:", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft };
            txtDamageValue = new TextBox { Text = "1", Width = 50, Margin = new Padding(5, 3, 5, 3) };
            btnDamage = new Button { Text = "造成伤害", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnDamage.Click += BtnDamage_Click;
            btnHeal = new Button { Text = "治疗", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnHeal.Click += BtnHeal_Click;
            btnProne = new Button { Text = "切换俯卧", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnProne.Click += BtnProne_Click;
            btnSuppress = new Button { Text = "切换压制", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnSuppress.Click += BtnSuppress_Click;
            btnClearEffects = new Button { Text = "清除状态", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnClearEffects.Click += BtnClearEffects_Click;
            btnDeleteUnit = new Button { Text = "删除单位", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            btnDeleteUnit.Click += BtnDeleteUnit_Click;
            btnRestart = new Button { Text = "重新开始", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.LightCoral };
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
            lblLeftPoints.Text = $"总分: {leftArmy.CurrentPoints} / {leftArmy.MaxPoints}";
            lblRightPoints.Text = $"总分: {rightArmy.CurrentPoints} / {rightArmy.MaxPoints}";
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
                item.SubItems.Add(u.EquippedWeapon?.Name ?? "无");
                string status = "";
                if (u.IsProne) status += "俯卧 ";
                if (u.IsSuppressed) status += "压制 ";
                if (u.IsShocked) status += "震撼 ";
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
            if (unit == null) { MessageBox.Show("请先选择一个单位。", "提示"); return; }
            if (!int.TryParse(txtDamageValue.Text, out int dmg) || dmg <= 0) return;
            unit.TakeDamage(dmg);
            RefreshUnitLists();
        }

        private void BtnHeal_Click(object sender, EventArgs e)
        {
            var unit = GetSelectedUnit();
            if (unit == null) { MessageBox.Show("请先选择一个单位。", "提示"); return; }
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
                MessageBox.Show("请先选择一个单位。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ArmyList targetArmy = null;
            if (leftArmy.Units.Any(entry => entry.Unit.Id == unit.Id))
                targetArmy = leftArmy;
            else if (rightArmy.Units.Any(entry => entry.Unit.Id == unit.Id))
                targetArmy = rightArmy;

            if (targetArmy == null) return;

            var confirmResult = MessageBox.Show(
                $"确定要从 {targetArmy.Name} 中删除单位 \"{unit.Name}\" 吗？",
                "确认删除",
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
            var result = MessageBox.Show("重新开始将清除所有军表数据并重新选择阵营。确定吗？", "确认重新开始", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                StartNewGame();
            }
        }
    }
}