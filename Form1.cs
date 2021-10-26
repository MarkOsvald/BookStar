using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Dashboard
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public Label lab1;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public Form1()
        {
            InitializeComponent();
            instance = this;
            lab1 = label1;

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNavigation.Height = btnDashboard.Height;
            pnlNavigation.Top = btnDashboard.Top;
            pnlNavigation.Left = btnDashboard.Left;

            lblTitle.Text = "Dashboard";
            this.PnlFormLoader.Controls.Clear();
            frmDashboard FrmDashboard_Vrb = new frmDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            FrmDashboard_Vrb.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(FrmDashboard_Vrb);
            FrmDashboard_Vrb.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            pnlNavigation.Height = btnDashboard.Height;
            pnlNavigation.Top = btnDashboard.Top;
            pnlNavigation.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Dashboard";
            this.PnlFormLoader.Controls.Clear();
            frmDashboard FrmDashboard_Vrb = new frmDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            FrmDashboard_Vrb.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(FrmDashboard_Vrb);
            FrmDashboard_Vrb.Show();
        }

        private void BtnAddBook_Click(object sender, EventArgs e)
        {
            pnlNavigation.Height = btnAddBook.Height;
            pnlNavigation.Top = btnAddBook.Top;
            btnAddBook.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Add Book";
            this.PnlFormLoader.Controls.Clear();
            frmAddBook FrmAddBook_Vrb = new frmAddBook() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            FrmAddBook_Vrb.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(FrmAddBook_Vrb);
            FrmAddBook_Vrb.Show();
        }

        private void btnYourBook_Click(object sender, EventArgs e)
        {
            pnlNavigation.Height = btnYourBooks.Height;
            pnlNavigation.Top = btnYourBooks.Top;
            btnYourBooks.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Your Books";
            this.PnlFormLoader.Controls.Clear();
            frmYourBooks FrmYourBooks_Vrb = new frmYourBooks() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            FrmYourBooks_Vrb.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(FrmYourBooks_Vrb);
            FrmYourBooks_Vrb.Show();
        }

        private void btnDiscover_Click(object sender, EventArgs e)
        {
            pnlNavigation.Height = btnDiscover.Height;
            pnlNavigation.Top = btnDiscover.Top;
            btnDiscover.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Discover";
            this.PnlFormLoader.Controls.Clear();
            frmDiscover FrmDiscover_Vrb = new frmDiscover() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            FrmDiscover_Vrb.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(FrmDiscover_Vrb);
            FrmDiscover_Vrb.Show();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNavigation.Height = btnSettings.Height;
            pnlNavigation.Top = btnSettings.Top;
            btnSettings.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Settings";
            this.PnlFormLoader.Controls.Clear();
            frmSettings FrmSettings_Vrb = new frmSettings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            FrmSettings_Vrb.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(FrmSettings_Vrb);
            FrmSettings_Vrb.Show();
        }

        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnAnalytics_Leave(object sender, EventArgs e)
        {
            btnAddBook.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnCalendar_Leave(object sender, EventArgs e)
        {
            btnYourBooks.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnDiscover.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnSettings_Leave(object sender, EventArgs e)
        {
            btnSettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PnlFormLoader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
