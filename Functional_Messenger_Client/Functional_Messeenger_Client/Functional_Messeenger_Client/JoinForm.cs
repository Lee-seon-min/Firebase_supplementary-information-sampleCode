using System;
using System.Drawing;
using System.Windows.Forms;

namespace Functional_Messeenger_Client
{
    public partial class JoinForm : Form
    {
        Quary quary = new Quary();
        joinControl joinControler = new joinControl();
        bool flag = false;
        bool isMove = false;
        Point fpt = new Point();
       
        public JoinForm()
        {
            InitializeComponent();
            
            Font font = new Font(FontBox.externalFont.Families[0], 16);
            Font font2 = new Font(FontBox.externalFont.Families[0], 9);
            idbox.Font = font;
            pwbox.Font = font;
            checkPwBox.Font = font;
            idCheckBox.Font = font2;
            tryJoinButton.Font = font2;
        }

        private void idCheckBox_Click(object sender, EventArgs e)
        {
            flag = false;
            if (idbox.Text.Equals(""))
            {
                MessageBox.Show("사용할 ID를 입력하십시오.");
                return;
            }
            flag = joinControler.isDuple(idbox.Text);
            if (!flag)
            {
                MessageBox.Show("중복된 아이디입니다.");
                return;
            }
            else
            {
                MessageBox.Show("사용가능한 아이디입니다.");
                return;
            }
        }

        private void tryJoinButton_Click(object sender, EventArgs e)
        {
            if (pwbox.Text.Equals(""))
            {
                MessageBox.Show("비밀번호를 입력해 주십시오.");
                return;
            }
            try
            { 
                if (flag && (pwbox.Text == checkPwBox.Text))
                {
                    joinControler.request_InsertUser(idbox.Text, pwbox.Text);
                    MessageBox.Show("회원가입이 완료되었습니다.");
                    this.Close();
                }
                else if (!flag)
                    MessageBox.Show("ID중복확인필요.");
                else
                    MessageBox.Show("PW와 확인란이 서로 다릅니다.");
            }
            catch (Exception err) { }
            finally {
                quary.connection.Close();
            }
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

        private void idbox_TextChanged(object sender, EventArgs e)
        {
            flag = false;
        }
    }
}
