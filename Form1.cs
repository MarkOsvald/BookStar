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
using Dashboard.Tables;

namespace Dashboard
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public Panel formLoader;
        public Label lab1;
        public Label labTitle;

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
            AddBooksForDiscovery();
            instance = this;
            lab1 = label1;
            labTitle = lblTitle;
            formLoader = this.PnlFormLoader;

            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNavigation.Height = btnAddBook.Height;
            pnlNavigation.Top = btnAddBook.Top;
            pnlNavigation.Left = btnAddBook.Left;

            lblTitle.Text = "Add Book";
            this.PnlFormLoader.Controls.Clear();
            frmAddBook FrmAddBook_Vrb = new frmAddBook() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            FrmAddBook_Vrb.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(FrmAddBook_Vrb);
            FrmAddBook_Vrb.Show(); ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            pnlNavigation.Height = btnDashboard.Height;
            pnlNavigation.Top = btnDashboard.Top;
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
            pnlNavigation.Left = btnAddBook.Left;
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

        public static void AddBooksForDiscovery()
        {
            Books book1 = new Books
            {
                Book = "The Four Winds",
                Author = "Kristin Hannah",
                Year = 2021,
                Review = "From the number-one bestselling author of The Nightingale and The Great Alone comes a powerful American epic about love and heroism and hope, set during the Great Depression, a time when the country was in crisis and at war with itself, when millions were out of work and even the land seemed to have turned against them.",
                Rating = 8
            };
            Books book2 = new Books
            {
                Book = "A Promised Land",
                Author = "Barack Obama",
                Year = 2020,
                Review = "A powerful book with lots of insights into great leadership.”—Bill Gates, GatesNotes",
                Rating = 10
            };
            Books book3 = new Books
            {
                Book = "The Testaments",
                Author = "Margaret Atwood",
                Year = 2019,
                Review = "The Testaments is a 2019 novel by Margaret Atwood. It is a sequel to The Handmaid's Tale (1985).[2] The novel is set 15 years after the events of The Handmaid's Tale. It is narrated by: Aunt Lydia, a character from the previous novel; Agnes, a young woman living in Gilead; and Daisy, a young woman living in Canada.",
                Rating = 9
            };
            Books book4 = new Books
            {
                Book = "The Bad Beginning",
                Author = "Daniel Handler",
                Year = 1999,
                Review = "The Bad Beginning is the first novel of the children's novel series A Series of Unfortunate Events by Lemony Snicket. The novel tells the story of three children, Violet, Klaus, and Sunny Baudelaire, who become orphans following a fire and are sent to live with Count Olaf, who attempts to steal their inheritance.",
                Rating = 9
            };
            Books book5 = new Books
            {
                Book = "Norwegian Wood",
                Author = "Haruki Murakami",
                Year = 1987,
                Review = "Norwegian Wood (ノルウェイの森, Noruwei no Mori) is a 1987 novel by Japanese author Haruki Murakami.[1] The novel is a nostalgic story of loss and burgeoning sexuality.[2] It is told from the first-person perspective of Toru Watanabe, who looks back on his days as a college student living in Tokyo.[3] Through Watanabe's reminiscences, readers see him develop relationships with two very different women—the beautiful yet emotionally troubled Naoko, and the outgoing, lively Midori.",
                Rating = 9
            };
            Books book6 = new Books
            {
                Book = "Zhan Guo Ce",
                Author = "Liu Xiang",
                Year = 1939,
                Review = "The Zhan Guo Ce, (W-G: Chan-kuo T'se), also known in English as the Strategies of the Warring States or Annals of the Warring States, is an ancient Chinese text that contains anecdotes of political manipulation and warfare during the Warring States period (5th to 3rd centuries BC).[1] It is an important text of the Warring States Period as it describes the strategies and political views of the School of Diplomacy and reveals the historical and social characteristics of the period.",
                Rating = 10
            };
            SqlClient.SaveBook(book1);
            SqlClient.SaveBook(book2);
            SqlClient.SaveBook(book3);
            SqlClient.SaveBook(book4);
            SqlClient.SaveBook(book5);
            SqlClient.SaveBook(book6);

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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
