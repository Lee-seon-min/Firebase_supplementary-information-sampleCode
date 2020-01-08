namespace Functionality_Messenger_Server
{
    partial class ChatServer
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
            this.StatusBox = new System.Windows.Forms.RichTextBox();
            this.ListenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StatusBox
            // 
            this.StatusBox.Location = new System.Drawing.Point(12, 12);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ReadOnly = true;
            this.StatusBox.Size = new System.Drawing.Size(374, 397);
            this.StatusBox.TabIndex = 0;
            this.StatusBox.Text = "";
            // 
            // ListenButton
            // 
            this.ListenButton.Location = new System.Drawing.Point(12, 415);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(374, 23);
            this.ListenButton.TabIndex = 1;
            this.ListenButton.Text = "서버개설";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // ChatServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(398, 450);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.StatusBox);
            this.Name = "ChatServer";
            this.Text = "ChatServer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox StatusBox;
        private System.Windows.Forms.Button ListenButton;
    }
}

