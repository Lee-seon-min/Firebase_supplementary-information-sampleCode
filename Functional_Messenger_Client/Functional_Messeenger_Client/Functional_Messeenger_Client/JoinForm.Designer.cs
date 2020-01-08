namespace Functional_Messeenger_Client
{
    partial class JoinForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JoinForm));
            this.tryJoinButton = new System.Windows.Forms.Button();
            this.idCheckBox = new System.Windows.Forms.Button();
            this.checkPwBox = new System.Windows.Forms.TextBox();
            this.pwbox = new System.Windows.Forms.TextBox();
            this.idbox = new System.Windows.Forms.TextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tryJoinButton
            // 
            this.tryJoinButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(95)))), ((int)(((byte)(226)))));
            this.tryJoinButton.FlatAppearance.BorderSize = 0;
            this.tryJoinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tryJoinButton.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tryJoinButton.ForeColor = System.Drawing.Color.White;
            this.tryJoinButton.Location = new System.Drawing.Point(73, 351);
            this.tryJoinButton.Name = "tryJoinButton";
            this.tryJoinButton.Size = new System.Drawing.Size(178, 32);
            this.tryJoinButton.TabIndex = 7;
            this.tryJoinButton.Text = "가입하기";
            this.tryJoinButton.UseVisualStyleBackColor = false;
            this.tryJoinButton.Click += new System.EventHandler(this.tryJoinButton_Click);
            // 
            // idCheckBox
            // 
            this.idCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(140)))), ((int)(((byte)(254)))));
            this.idCheckBox.FlatAppearance.BorderSize = 0;
            this.idCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.idCheckBox.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.idCheckBox.ForeColor = System.Drawing.Color.White;
            this.idCheckBox.Location = new System.Drawing.Point(217, 179);
            this.idCheckBox.Name = "idCheckBox";
            this.idCheckBox.Size = new System.Drawing.Size(63, 25);
            this.idCheckBox.TabIndex = 6;
            this.idCheckBox.Text = "중복확인";
            this.idCheckBox.UseVisualStyleBackColor = false;
            this.idCheckBox.Click += new System.EventHandler(this.idCheckBox_Click);
            // 
            // checkPwBox
            // 
            this.checkPwBox.BackColor = System.Drawing.SystemColors.Menu;
            this.checkPwBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkPwBox.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkPwBox.Location = new System.Drawing.Point(50, 292);
            this.checkPwBox.MaxLength = 15;
            this.checkPwBox.Name = "checkPwBox";
            this.checkPwBox.Size = new System.Drawing.Size(214, 37);
            this.checkPwBox.TabIndex = 5;
            // 
            // pwbox
            // 
            this.pwbox.BackColor = System.Drawing.SystemColors.Menu;
            this.pwbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pwbox.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pwbox.Location = new System.Drawing.Point(50, 237);
            this.pwbox.MaxLength = 15;
            this.pwbox.Name = "pwbox";
            this.pwbox.Size = new System.Drawing.Size(214, 37);
            this.pwbox.TabIndex = 4;
            // 
            // idbox
            // 
            this.idbox.BackColor = System.Drawing.SystemColors.Menu;
            this.idbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.idbox.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.idbox.Location = new System.Drawing.Point(50, 179);
            this.idbox.MaxLength = 15;
            this.idbox.Name = "idbox";
            this.idbox.Size = new System.Drawing.Size(164, 37);
            this.idbox.TabIndex = 3;
            this.idbox.TextChanged += new System.EventHandler(this.idbox_TextChanged);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Controls.Add(this.button1);
            this.panel.Location = new System.Drawing.Point(0, 2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(317, 27);
            this.panel.TabIndex = 8;
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_down);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_move);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_up);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(274, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // JoinForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(317, 478);
            this.Controls.Add(this.idbox);
            this.Controls.Add(this.pwbox);
            this.Controls.Add(this.checkPwBox);
            this.Controls.Add(this.idCheckBox);
            this.Controls.Add(this.tryJoinButton);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "JoinForm";
            this.Text = "JoinForm";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button tryJoinButton;
        private System.Windows.Forms.Button idCheckBox;
        private System.Windows.Forms.TextBox checkPwBox;
        private System.Windows.Forms.TextBox pwbox;
        private System.Windows.Forms.TextBox idbox;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button button1;
    }
}