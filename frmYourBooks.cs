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
    public partial class frmYourBooks : Form
    {
        public frmYourBooks()
        {
            InitializeComponent();
            populateItems();
        }

        private void frmYourBooks_Load(object sender, EventArgs e)
        {

        }

        public void populateItems()
        {
            var username = Form1.instance.lab1.Text;
            var listOfBooks = SqlClient.LoadUserBooks(username);
            ListItem[] listItems = new ListItem[listOfBooks.Count + 1];
            int i = 0;

            foreach (var book in listOfBooks)
            {
                i++;
                listItems[i] = new ListItem
                {
                    Title = book.Book,
                    Author = book.Author,
                    Year = book.Year.ToString(),
                    Rating = book.Rating.ToString()
                };

                if (flowLayoutPanel1.Controls.Count < 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                }
                else
                {
                    flowLayoutPanel1.Controls.Add(listItems[i]);
                }
            }
        }

    }
}
