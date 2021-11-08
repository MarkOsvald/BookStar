using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dashboard.Tables;

namespace Dashboard
{
    public partial class frmAddBook : Form
    {

        public frmAddBook()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddBookAddBook_Click(object sender, EventArgs e)
        {

            Books book = new Books
            {
                Author = txtAuthor.Text,
                Book = txtBookName.Text,
                Year = Decimal.ToInt32(numericUpDownYear.Value),
                Review = txtReview.Text,
                Rating = Decimal.ToInt32(numericUpDownRating.Value),
                Username = Form1.instance.lab1.Text
            };

            if(book.Book == "" || book.Review == "")
            {
                MessageBox.Show("Book name and Review both needed!", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlClient.SaveBook(book);
            MessageBox.Show("Book added!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAddBookClear_Click(object sender, EventArgs e)
        {
            txtAuthor.Text = "";
            txtBookName.Text = "";
            numericUpDownYear.Value = 0;
            txtReview.Text = "";
            numericUpDownRating.Value = 1;
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
