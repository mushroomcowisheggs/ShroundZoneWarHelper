using System;
using System.Windows.Forms;
using ShroundZoneHelper.Models;

namespace ShroundZoneHelper.Forms {
    public partial class FactionSelectForm : Form {
        private ComboBox cmbLeft;
        private ComboBox cmbRight;
        private Button btnOK;
        private Button btnCancel;
        
        public Faction LeftFaction { get; private set; }
        public Faction RightFaction { get; private set; }
        
        private void BtnOK_Click(object sender, EventArgs e)
        {
            LeftFaction = (Faction)cmbLeft.SelectedItem;
            RightFaction = (Faction)cmbRight.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void InitializeComponent() {
            this.Text = "选择双方阵营";
            this.Size = new System.Drawing.Size(320, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblLeft = new Label { Text = "玩家1军表阵营：", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            cmbLeft = new ComboBox { Location = new System.Drawing.Point(150, 17), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLeft.Items.AddRange(new object[] { Faction.Mechanized, Faction.Ember });
            cmbLeft.SelectedIndex = 0;

            Label lblRight = new Label { Text = "玩家2军表阵营：", Location = new System.Drawing.Point(20, 60), AutoSize = true };
            cmbRight = new ComboBox { Location = new System.Drawing.Point(150, 57), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRight.Items.AddRange(new object[] { Faction.Mechanized, Faction.Ember });
            cmbRight.SelectedIndex = 0;

            btnOK = new Button { Text = "开始对战", Location = new System.Drawing.Point(50, 100), Size = new System.Drawing.Size(80, 30) };
            btnOK.Click += BtnOK_Click;
            btnCancel = new Button { Text = "退出", Location = new System.Drawing.Point(160, 100), Size = new System.Drawing.Size(80, 30) };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lblLeft, cmbLeft, lblRight, cmbRight, btnOK, btnCancel });
        }
        
        public FactionSelectForm() {
            InitializeComponent();
        }
    }
}