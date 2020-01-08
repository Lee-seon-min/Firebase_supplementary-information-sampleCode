namespace Functional_Messeenger_Client
{
    partial class Calender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calender));
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.schedule_box = new System.Windows.Forms.RichTextBox();
            this.Save_button = new System.Windows.Forms.Button();
            this.Erase_button = new System.Windows.Forms.Button();
            this.Exit_button = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(62, 162);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 0;
            this.monthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);
            // 
            // schedule_box
            // 
            this.schedule_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.schedule_box.BackColor = System.Drawing.Color.White;
            this.schedule_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.schedule_box.Location = new System.Drawing.Point(333, 156);
            this.schedule_box.MaxLength = 300;
            this.schedule_box.Name = "schedule_box";
            this.schedule_box.Size = new System.Drawing.Size(220, 175);
            this.schedule_box.TabIndex = 1;
            this.schedule_box.Text = "";
            // 
            // Save_button
            // 
            this.Save_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(95)))), ((int)(((byte)(226)))));
            this.Save_button.FlatAppearance.BorderSize = 0;
            this.Save_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_button.ForeColor = System.Drawing.Color.White;
            this.Save_button.Location = new System.Drawing.Point(472, 363);
            this.Save_button.Name = "Save_button";
            this.Save_button.Size = new System.Drawing.Size(88, 42);
            this.Save_button.TabIndex = 2;
            this.Save_button.Text = "저장하기";
            this.Save_button.UseVisualStyleBackColor = false;
            this.Save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // Erase_button
            // 
            this.Erase_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(95)))), ((int)(((byte)(226)))));
            this.Erase_button.FlatAppearance.BorderSize = 0;
            this.Erase_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Erase_button.ForeColor = System.Drawing.Color.White;
            this.Erase_button.Location = new System.Drawing.Point(327, 363);
            this.Erase_button.Name = "Erase_button";
            this.Erase_button.Size = new System.Drawing.Size(86, 42);
            this.Erase_button.TabIndex = 3;
            this.Erase_button.Text = "지우기";
            this.Erase_button.UseVisualStyleBackColor = false;
            this.Erase_button.Click += new System.EventHandler(this.Erase_button_Click);
            // 
            // Exit_button
            // 
            this.Exit_button.BackColor = System.Drawing.Color.White;
            this.Exit_button.FlatAppearance.BorderSize = 0;
            this.Exit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit_button.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Exit_button.Location = new System.Drawing.Point(508, 38);
            this.Exit_button.Name = "Exit_button";
            this.Exit_button.Size = new System.Drawing.Size(71, 45);
            this.Exit_button.TabIndex = 4;
            this.Exit_button.Text = "X";
            this.Exit_button.UseVisualStyleBackColor = false;
            this.Exit_button.Click += new System.EventHandler(this.Exit_button_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.Location = new System.Drawing.Point(2, 2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(814, 35);
            this.panel.TabIndex = 9;
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_down);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_move);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_up);
            // 
            // Calender
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(590, 439);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.Exit_button);
            this.Controls.Add(this.Erase_button);
            this.Controls.Add(this.Save_button);
            this.Controls.Add(this.schedule_box);
            this.Controls.Add(this.monthCalendar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Calender";
            this.Text = "Calender";
            this.Load += new System.EventHandler(this.Calender_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.RichTextBox schedule_box;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Button Erase_button;
        private System.Windows.Forms.Button Exit_button;
        private System.Windows.Forms.Panel panel;
    }
}