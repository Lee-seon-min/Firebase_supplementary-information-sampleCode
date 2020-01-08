using System;
using System.Drawing;
using System.Windows.Forms;

namespace Functional_Messeenger_Client
{
    public partial class LoginForm : Form
    {
        Quary quary = new Quary();
        loginControl loginControler = new loginControl();
        bool isMove = false;
        Point fpt =new Point();
        public LoginForm()
        {
            InitializeComponent();
            Font font = new Font(FontBox.externalFont.Families[0], 12);
            Font font2 = new Font(FontBox.externalFont.Families[0], 9);
            idbox.Font = font;
            pwbox.Font = font;
            loginButton.Font = font2;
            joinButton.Font = font2;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            bool flag = false;
            flag = loginControler.isCorrect(idbox.Text, pwbox.Text);
            if (flag) {
                Home home = new Home(idbox.Text);
                this.Opacity = 0;
                home.ShowDialog();
                this.Opacity = 1;
            }
            else
            {
                MessageBox.Show("아이디나 비밀번호를 잘못 입력하였습니다.");
                return;
            }
        }
        private void joinButton_Click(object sender, EventArgs e)
        {
            JoinForm joinform = new JoinForm();
            joinform.Show();
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

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
