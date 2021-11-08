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
    public partial class ListItemDiscover : UserControl
    {
        public ListItemDiscover()
        {
            InitializeComponent();
        }

        #region Properties
        private string _title;
        private string _author;
        private string _year;
        private string _rating;

        [Category("Custom Props")]
        public string Title
        {
            get { return _title; }
            set { _title = value; lblTitle.Text = value; }
        }

        [Category("Custom Props")]
        public string Author
        {
            get { return _author; }
            set { _author = value; lblAuthor.Text = value; }
        }

        [Category("Custom Props")]
        public string Year
        {
            get { return _year; }
            set { _year = value; lblYear.Text = value; }
        }

        [Category("Custom Props")]
        public string Rating
        {
            get { return _rating; }
            set { _rating = value; lblRating.Text = value; }
        }
        #endregion

        private void btnREVIEW_Click(object sender, EventArgs e)
        {

            //Form1.instance.labTitle.Text = _title;

            Form1.instance.formLoader.Controls.Clear();
            frmBookReview FrmDiscover_Vrb = new frmBookReview() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            FrmDiscover_Vrb.FormBorderStyle = FormBorderStyle.None;
            Form1.instance.formLoader.Controls.Add(FrmDiscover_Vrb);
            FrmDiscover_Vrb.Show();

            Books book = SqlClient.LoadBook(_title, _author, _rating, _year);
            frmBookReview.instance.box.Text = book.Review;

        }

        private void ListItemDiscover_Load(object sender, EventArgs e)
        {

        }
    }
}
