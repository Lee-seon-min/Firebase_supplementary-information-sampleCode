using System;
using System.Drawing;
using System.Windows.Forms;

namespace Functional_Messeenger_Client
{
    public partial class Calender : Form
    {
        Quary quary = null;
        bool isMove = false;
        Point fpt = new Point();
        CalenderControl calenderControler = new CalenderControl();
        public string ID
        {
            get;set;
        }
        public Calender(string id)
        {
            InitializeComponent();
            ID = id;
            FontSet();
        }
        private void FontSet() {
            Font MemFont = new Font(FontBox.externalFont.Families[0], 12);
            Font buttonFont = new Font(FontBox.externalFont.Families[3], 11);
            schedule_box.Font = MemFont;
            Erase_button.Font = buttonFont;
            Save_button.Font = buttonFont;
        }
        private void Calender_Load(object sender, EventArgs e)
        {
            quary = new Quary();
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            string Date = monthCalendar.SelectionStart.ToString("yyyy-MM-dd");
            int checkNum=calenderControler.request_setCalender(ID, Date, schedule_box.Text);
            if (checkNum == 0)
            {
                MessageBox.Show("수정이 완료되었습니다.");
            }
            else
            {
                MessageBox.Show("추가가 완료되었습니다.");
            }
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            schedule_box.Clear();
            string Date = monthCalendar.SelectionStart.ToString("yyyy-MM-dd");
            schedule_box.Text = calenderControler.request_viewCalender(ID, Date); //Time속성은 primary key로!
        }

        private void Erase_button_Click(object sender, EventArgs e)
        {
            schedule_box.Clear();
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel_down(object sender, MouseEventArgs e)
        {
            isMove = true;
            fpt = new Point(e.X, e.Y);
        }

        private void Panel_move(object sender, MouseEventArgs e)
        {
            if (isMove && (e.Button & MouseButtons.Left) == MouseButtons.Left)
                this.Location = new Point(this.Left - (fpt.X - e.X), this.Top - (fpt.Y - e.Y));
        }

        private void Panel_up(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
