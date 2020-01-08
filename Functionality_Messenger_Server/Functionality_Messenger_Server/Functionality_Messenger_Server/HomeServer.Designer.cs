namespace Functionality_Messenger_Server
{
    partial class HomeServer
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
            this.statusBox = new System.Windows.Forms.RichTextBox();
            this.listenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(12, 12);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(374, 397);
            this.statusBox.TabIndex = 0;
            this.statusBox.Text = "";
            // 
            // listenButton
            // 
            this.listenButton.Location = new System.Drawing.Point(12, 415);
            this.listenButton.Name = "listenButton";
            this.listenButton.Size = new System.Drawing.Size(374, 23);
            this.listenButton.TabIndex = 1;
            this.listenButton.Text = "서버개설";
            this.listenButton.UseVisualStyleBackColor = true;
            this.listenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // HomeServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 450);
            this.Controls.Add(this.listenButton);
            this.Controls.Add(this.statusBox);
            this.Name = "HomeServer";
            this.Text = "HomeServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerClosing);
            this.Load += new System.EventHandler(this.HomeServer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox statusBox;
        private System.Windows.Forms.Button listenButton;
    }
}