using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KozeevSQLApp
{
    public partial class RegistrForm : Form
    {
        public RegistrForm()
        {
            InitializeComponent();
            userName.Text = "Введите имя";
            userName.ForeColor = Color.Gray;
            userSurname.Text = "Введите фамилию";
            userSurname.ForeColor = Color.Gray;
            LoginBox.Text = "Введите логин";
            LoginBox.ForeColor = Color.Gray;
            PassBox.UseSystemPasswordChar = false;
            PassBox.Text = "Введите пароль";
            PassBox.ForeColor = Color.Gray;
            this.PassBox.AutoSize = false;
            this.PassBox.Size = new Size(this.PassBox.Size.Width, 46);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены что хотите выйти ?", "Сообщение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }
        Point lastPoint;
        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            if (userName.Text == "Введите имя")
            {
                MessageBox.Show("Пожалуйста, введите имя");
                return;
            }
            if (userSurname.Text == "Введите фамилию")
            {
                MessageBox.Show("Пожалуйста, введите фамилию");
                return;
            }
            if (LoginBox.Text == "Введите логин")
            {
                MessageBox.Show("Пожалуйста, введите логин");
                return;
            }
            if (PassBox.Text == "Введите пароль")
            {
                MessageBox.Show("Пожалуйста, введите пароль");
                return;
            }
            if (CheckUsers())
                return;
            DB db = new DB();
            MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO `users` (`name`, `surname`, `login`, `password`) VALUES (@name, @surname, @login, @pass )",db.GetConnection());
            mySqlCommand.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginBox.Text;
            mySqlCommand.Parameters.Add("@pass", MySqlDbType.VarChar).Value = PassBox.Text;
            mySqlCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = userName.Text;
            mySqlCommand.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userSurname.Text;
            db.openConnect();

            if (mySqlCommand.ExecuteNonQuery() == 1)

                MessageBox.Show("Аккаунт был создан");
            else
                MessageBox.Show("Аккаунт не был создан");
            db.closeConnect();
        }
        public Boolean CheckUsers()
        {
            DB db = new DB();
            DataTable dtable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.GetConnection());
            mySqlCommand.Parameters.Add("@uL", MySqlDbType.VarChar).Value = LoginBox.Text;
            adapter.SelectCommand = mySqlCommand;
            adapter.Fill(dtable);

            if (dtable.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже используется, введите другой");
                return true;
            }
            else
            {
                return false;
            }
        }
        private void userName_Enter(object sender, EventArgs e)
        {
           if( userName.Text == "Введите имя")
            {
                userName.Text = "";
                userName.ForeColor = Color.Black;
            }
        }

        private void userName_Leave(object sender, EventArgs e)
        {
            if (userName.Text == "")
            {
                userName.Text = "Введите имя";
                userName.ForeColor = Color.Gray;
            }
        }

        private void userSurname_Enter(object sender, EventArgs e)
        {
            if (userSurname.Text == "Введите фамилию")
            {
                userSurname.Text = "";
                userSurname.ForeColor = Color.Black;
            }
        }

        private void userSurname_Leave(object sender, EventArgs e)
        {
            if (userSurname.Text == "")
            {
                userSurname.Text = "Введите фамилию";
                userSurname.ForeColor = Color.Gray;
            }
        }

        private void LoginBox_Enter(object sender, EventArgs e)
        {
            if (LoginBox.Text == "Введите логин")
            {
                LoginBox.Text = "";
                LoginBox.ForeColor = Color.Black;
            }
        }

        private void LoginBox_Leave(object sender, EventArgs e)
        {
            if (LoginBox.Text == "")
            {
                LoginBox.Text = "Введите логин";
                LoginBox.ForeColor = Color.Gray;
            }
        }

        private void PassBox_Enter(object sender, EventArgs e)
        {
            if (PassBox.Text == "Введите пароль")
            {
                PassBox.Text = "";
                PassBox.UseSystemPasswordChar = true;
                PassBox.ForeColor = Color.Black;
            }
        }

        private void PassBox_Leave(object sender, EventArgs e)
        {
            if (PassBox.Text == "")
            {
                PassBox.UseSystemPasswordChar = false;
                PassBox.Text = "Введите пароль";
                PassBox.ForeColor = Color.Gray;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.SkyBlue;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }
        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            PassBox.UseSystemPasswordChar = false;
            pictureBox6.Image = Image.FromFile("C:/Users/AlexKennway/source/repos/KozeevSQLApp/KozeevSQLApp/images/eye.png");
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            PassBox.UseSystemPasswordChar = true;
            pictureBox6.Image = Image.FromFile("C:/Users/AlexKennway/source/repos/KozeevSQLApp/KozeevSQLApp/images/blockeye.png");
            if (PassBox.Text == "Введите пароль")
            {
                PassBox.UseSystemPasswordChar = false;
                PassBox.ForeColor = Color.Gray;
            }
        }
    }
}
