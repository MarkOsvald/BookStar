using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dashboard.Tables;

namespace Dashboard
{
    public partial class frmRegister : Form
    {
        public string lang = "en-EU";

        public frmRegister()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> userlist = new List<string>();
            userlist = SqlClient.LoadUsers();

            if (txtUsername.Text == "" || txtPassword.Text == "" || txtComPassword.Text == "")
            {
                MessageBox.Show("Username and Password fields are empty", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (userlist.Contains(txtUsername.Text))
            {
                MessageBox.Show("This username is taken", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtComPassword.Text = "";
                txtUsername.Focus();
            }
            else if (txtPassword.Text == txtComPassword.Text)
            {
                Users user = new Users
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Language = lang
                };
                SqlClient.SaveUser(user);

                txtUsername.Text = "";
                txtPassword.Text = "";
                txtComPassword.Text = "";

                MessageBox.Show("Your Account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new frmLogin().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Passwords does not match, Please Re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtComPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtComPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtComPassword.PasswordChar = '•';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtComPassword.Text = "";
            txtUsername.Focus();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            lang = "en-EU";
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            Controls.Clear();
            InitializeComponent();
        }

        private void btnEstonian_Click(object sender, EventArgs e)
        {
            lang = "et";
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            Controls.Clear();
            InitializeComponent();
        }
    }
}
