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
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();

            string username = SqlClient.GetActiveUsername();
            string lang = SqlClient.GetLanguage(username);
            List<Books> listOfBooks = SqlClient.LoadUserBooks(username);
            int numberOfBooksInt = listOfBooks.Count;
            string numberOfBooks = listOfBooks.Count.ToString();
            string timestamp = SqlClient.GetTimestamp(username);
            DateTime dateTime = DateTime.Parse(timestamp);
            DateTime dateTimeNow = DateTime.Now;

            var output = SqlClient.CheckIfBooksEmpty(username);
            if(output)
            {
                var listOfBooks2 = SqlClient.LoadUserBooksOrderByAsc(username);
                var book = listOfBooks2[0];
                lblAuthor.Text = book.Author;
                lblBookName.Text = book.Book;
            }

            double days = (dateTime - dateTimeNow).TotalSeconds / 60 / 60 / 24 * -1;
            if(days >= 0)
            {
                days = 1;
            }

            int daysInt = Convert.ToInt32(days);


            lblTotalBooks.Text = numberOfBooks;
            circularProgressBar1.Text = numberOfBooks + "%";
            circularProgressBar1.Value = numberOfBooksInt;
            lblNumberOfBooks.Text = numberOfBooks + " / 100";

            if (lang == "en-EU")
            {
                lblTimestamp.Text = "User since: " + timestamp;
                lblDetails.Text = "Details of last " + daysInt + " days";
            }
            else
            {
                lblTimestamp.Text = "Kasutaja alates: " + timestamp;
                lblDetails.Text = "Detailid viimasest " + daysInt + " päevast";
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblNumberOfBooks_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
