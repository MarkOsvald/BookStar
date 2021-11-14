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
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
                Users user = new Users
                {
                    Username = Form1.instance.lab1.Text,
                    Password = txtPassword.Text,
                };

            if (String.IsNullOrEmpty(txtPassword.Text) || String.IsNullOrEmpty(txtNewPassword.Text) || String.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                MessageBox.Show("Password fields are empty", "Change password Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (SqlClient.LoginUser(user) == false)
            {
                MessageBox.Show("Wrong password", "Change password Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtPassword.Focus();
            }
            else if (txtNewPassword.Text == txtConfirmPassword.Text)
            {
                Users user2 = new Users
                {
                    Username = Form1.instance.lab1.Text,
                    Password = txtNewPassword.Text,
                };
                SqlClient.ChangePassword(user2);

                txtPassword.Text = "";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";

                MessageBox.Show("Your Password has been Successfully Changed", "Change password Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Passwords does not match, Please Re-enter", "Change password Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void btnCLEAR_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtPassword.Focus();
        }

        private void button1_Click(object sender, EventArgs e)

        {

        }
        private void btnEnglish_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-EU");
            this.Controls.Clear();
            InitializeComponent();
        }

        private void btnEstonian_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("et");
            this.Controls.Clear();
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
