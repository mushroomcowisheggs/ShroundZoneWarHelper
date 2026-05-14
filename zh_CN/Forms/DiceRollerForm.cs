using ShroundZoneHelper.Models;
using System;
using System.Windows.Forms;

namespace ShroundZoneHelper.Forms
{
    public partial class DiceRollerForm : Form
    {
        private Unit targetUnit;
        private TextBox? txtRedHits, txtYellowDouble, txtYellowSingle, txtYellowLightning;
        private CheckBox? chkIgnoreArmor;
        private Button? btnApply;

        public DiceRollerForm(Unit target)
        {
            targetUnit = target;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "快速伤害结算";
            this.Size = new Size(300, 280);
            this.StartPosition = FormStartPosition.CenterParent;

            int y = 20;
            AddLabel("红骰实心重击 (⚫):", y);
            txtRedHits = AddTextBox(y); y += 30;
            AddLabel("黄骰双重轻击 (●×2):", y);
            txtYellowDouble = AddTextBox(y); y += 30;
            AddLabel("黄骰轻击 (●):", y);
            txtYellowSingle = AddTextBox(y); y += 30;
            AddLabel("黄骰闪电 (⚡):", y);
            txtYellowLightning = AddTextBox(y); y += 30;

            chkIgnoreArmor = new CheckBox { Text = "无视护甲（黄色攻击）", Location = new Point(20, y), AutoSize = true };
            Controls.Add(chkIgnoreArmor);
            y += 30;

            btnApply = new Button { Text = "应用伤害", Location = new Point(80, y), Size = new Size(100, 30) };
            btnApply.Click += BtnApply_Click;
            Controls.Add(btnApply);
        }

        private void AddLabel(string text, int y)
        {
            Controls.Add(new Label { Text = text, Location = new Point(20, y), AutoSize = true });
        }

        private TextBox AddTextBox(int y)
        {
            var tb = new TextBox { Location = new Point(150, y - 3), Width = 50, Text = "0" };
            Controls.Add(tb);
            return tb;
        }

        private void BtnApply_Click(object? sender, EventArgs e)
        {
            int red = int.TryParse(txtRedHits!.Text, out var r) ? r : 0;
            int yellowDouble = int.TryParse(txtYellowDouble!.Text, out var yd) ? yd : 0;
            int yellowSingle = int.TryParse(txtYellowSingle!.Text, out var ys) ? ys : 0;
            int lightning = int.TryParse(txtYellowLightning!.Text, out var l) ? l : 0;

            int physicalDamage = red;
            int yellowDamage = yellowDouble * 2 + yellowSingle;

            if (chkIgnoreArmor!.Checked)
                physicalDamage += yellowDamage;
            else
                physicalDamage += yellowDamage;

            // 规则：红色伤害受护甲减免，黄色伤害直接扣血
            int finalPhysical = red;
            int finalYellow = yellowDamage;
            targetUnit.TakeDamage(finalPhysical, false);
            targetUnit.CurrentHP = Math.Max(0, targetUnit.CurrentHP - finalYellow); // 黄色伤害直接扣

            // 处理压制（闪电符号）
            if (lightning > 0)
                targetUnit.IsSuppressed = true;

            MessageBox.Show($"造成 {finalPhysical} 物理伤害 + {finalYellow} 科技伤害，{(lightning > 0 ? "目标被压制" : "")}");
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}