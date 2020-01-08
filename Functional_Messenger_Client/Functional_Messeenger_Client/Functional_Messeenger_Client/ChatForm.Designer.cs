namespace Functional_Messeenger_Client
{
    partial class ChatForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.ChatBox = new System.Windows.Forms.RichTextBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.Send_button = new System.Windows.Forms.Button();
            this.Myschedule_date = new System.Windows.Forms.DateTimePicker();
            this.Sharing_button = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.userlistUpdator = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChatBox
            // 
            this.ChatBox.BackColor = System.Drawing.Color.White;
            this.ChatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChatBox.Location = new System.Drawing.Point(24, 155);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.ReadOnly = true;
            this.ChatBox.Size = new System.Drawing.Size(376, 400);
            this.ChatBox.TabIndex = 0;
            this.ChatBox.Text = "";
            // 
            // textBox
            // 
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox.Location = new System.Drawing.Point(31, 604);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(299, 23);
            this.textBox.TabIndex = 1;
            // 
            // Send_button
            // 
            this.Send_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(95)))), ((int)(((byte)(226)))));
            this.Send_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Send_button.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Send_button.ForeColor = System.Drawing.Color.White;
            this.Send_button.Location = new System.Drawing.Point(351, 590);
            this.Send_button.Name = "Send_button";
            this.Send_button.Size = new System.Drawing.Size(62, 58);
            this.Send_button.TabIndex = 2;
            this.Send_button.Text = "->";
            this.Send_button.UseVisualStyleBackColor = false;
            this.Send_button.Click += new System.EventHandler(this.Send_button_Click);
            // 
            // Myschedule_date
            // 
            this.Myschedule_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Myschedule_date.Location = new System.Drawing.Point(100, 110);
            this.Myschedule_date.Name = "Myschedule_date";
            this.Myschedule_date.Size = new System.Drawing.Size(200, 28);
            this.Myschedule_date.TabIndex = 3;
            // 
            // Sharing_button
            // 
            this.Sharing_button.BackColor = System.Drawing.Color.White;
            this.Sharing_button.Location = new System.Drawing.Point(306, 110);
            this.Sharing_button.Name = "Sharing_button";
            this.Sharing_button.Size = new System.Drawing.Size(94, 29);
            this.Sharing_button.TabIndex = 4;
            this.Sharing_button.Text = "공유하기";
            this.Sharing_button.UseVisualStyleBackColor = false;
            this.Sharing_button.Click += new System.EventHandler(this.Sharing_button_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.Controls.Add(this.button2);
            this.panel.Location = new System.Drawing.Point(0, 1);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(428, 23);
            this.panel.TabIndex = 9;
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_down);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_move);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_up);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(386, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // userlistUpdator
            // 
            this.userlistUpdator.BackColor = System.Drawing.Color.Transparent;
            this.userlistUpdator.FlatAppearance.BorderSize = 0;
            this.userlistUpdator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userlistUpdator.Location = new System.Drawing.Point(10, 651);
            this.userlistUpdator.Name = "userlistUpdator";
            this.userlistUpdator.Size = new System.Drawing.Size(1, 1);
            this.userlistUpdator.TabIndex = 10;
            this.userlistUpdator.UseVisualStyleBackColor = false;
            this.userlistUpdator.Click += new System.EventHandler(this.userlistUpdator_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(425, 660);
            this.Controls.Add(this.userlistUpdator);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.Sharing_button);
            this.Controls.Add(this.Myschedule_date);
            this.Controls.Add(this.Send_button);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.ChatBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChatForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ChatBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button Send_button;
        private System.Windows.Forms.DateTimePicker Myschedule_date;
        private System.Windows.Forms.Button Sharing_button;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button userlistUpdator;
    }
}

