using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KozeevSQLApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm logform = new LoginForm();
            logform.Show();
        }
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
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
        private void LoadData()
        {
            DB db = new DB();
            DataTable dtable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM `excursions` ", db.GetConnection());
            adapter.SelectCommand = mySqlCommand;
            adapter.Fill(dtable);
            Datatable.DataSource = dtable;


            Datatable.Columns[0].HeaderText = "Код";
            Datatable.Columns[1].HeaderText = "Код стендиста";
            Datatable.Columns[2].HeaderText = "Код посетителя";
            Datatable.Columns[3].HeaderText = "Код стенда";
            Datatable.Columns[4].HeaderText = "Название экскурсии";
            Datatable.Columns[5].HeaderText = "Цена";
            Datatable.Columns[6].HeaderText = "Дата начала";
            Datatable.Columns[7].HeaderText = "Дата окончания";


            Datatable.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.ColumnHeadersDefaultCellStyle.Font = new Font(Datatable.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
        }
        private void LoadData1()
        {
            DB db = new DB();
            DataTable dtable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM `standassistants` ", db.GetConnection());
            adapter.SelectCommand = mySqlCommand;
            adapter.Fill(dtable);
            Datatable.DataSource = dtable;

            Datatable.Columns[0].HeaderText = "Код стендиста";
            Datatable.Columns[1].HeaderText = "Имя";
            Datatable.Columns[2].HeaderText = "Отчество";
            Datatable.Columns[3].HeaderText = "Фамилия";
            Datatable.Columns[4].HeaderText = "Контакты";
            Datatable.Columns[5].HeaderText = "Типы стендов";


            Datatable.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.ColumnHeadersDefaultCellStyle.Font = new Font(Datatable.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
        }
        private void LoadData2()
        {
            DB db = new DB();
            DataTable dtable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM `stands` ", db.GetConnection());
            adapter.SelectCommand = mySqlCommand;
            adapter.Fill(dtable);
            Datatable.DataSource = dtable;

            Datatable.Columns[0].HeaderText = "Код стенда";
            Datatable.Columns[1].HeaderText = "Название стенда";
            Datatable.Columns[2].HeaderText = "Информация о стенде";
            Datatable.Columns[3].HeaderText = "Тип стенда";
           
            Datatable.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Datatable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.ColumnHeadersDefaultCellStyle.Font = new Font(Datatable.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
        }
        private void LoadData3()
        {
            DB db = new DB();
            DataTable dtable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM `visitors` ", db.GetConnection());
            adapter.SelectCommand = mySqlCommand;
            adapter.Fill(dtable);
            Datatable.DataSource = dtable;

            Datatable.Columns[0].HeaderText = "Код_посетителя";
            Datatable.Columns[1].HeaderText = "Имя";
            Datatable.Columns[2].HeaderText = "Отчество";
            Datatable.Columns[3].HeaderText = "Фамилия";


            Datatable.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Datatable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Datatable.ColumnHeadersDefaultCellStyle.Font = new Font(Datatable.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadData2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadData3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadData1();
        }
    }
}
