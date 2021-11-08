using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class frmBookReview : Form
    {
        public static frmBookReview instance;
        public RichTextBox box;
        public frmBookReview()
        {
            InitializeComponent();
            instance = this;
            box = richTextBox1;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Form1.instance.labTitle.Text == "Your Books")
            {
                Form1.instance.labTitle.Text = "Your Books";
                Form1.instance.formLoader.Controls.Clear();
                frmYourBooks FrmYourBooks_Vrb = new frmYourBooks() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                FrmYourBooks_Vrb.FormBorderStyle = FormBorderStyle.None;
                Form1.instance.formLoader.Controls.Add(FrmYourBooks_Vrb);
                FrmYourBooks_Vrb.Show();
            }
            else
            {
                Form1.instance.labTitle.Text = "Discover";
                Form1.instance.formLoader.Controls.Clear();
                frmDiscover FrmDiscover_Vrb = new frmDiscover() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                FrmDiscover_Vrb.FormBorderStyle = FormBorderStyle.None;
                Form1.instance.formLoader.Controls.Add(FrmDiscover_Vrb);
                FrmDiscover_Vrb.Show();
            }
        }
    }
}
