
namespace Dashboard
{
    partial class frmAddBook
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddBook));
            this.label2 = new System.Windows.Forms.Label();
            this.txtBookName = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReview = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownRating = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddBookAddBook = new System.Windows.Forms.Button();
            this.btnAddBookClear = new System.Windows.Forms.Button();
            this.numericUpDownYear = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.label2.Name = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtBookName
            // 
            resources.ApplyResources(this.txtBookName, "txtBookName");
            this.txtBookName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(79)))), ((int)(((byte)(99)))));
            this.txtBookName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBookName.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtBookName.Name = "txtBookName";
            this.txtBookName.TextChanged += new System.EventHandler(this.txtBookName_TextChanged);
            // 
            // txtAuthor
            // 
            resources.ApplyResources(this.txtAuthor, "txtAuthor");
            this.txtAuthor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(79)))), ((int)(((byte)(99)))));
            this.txtAuthor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAuthor.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtAuthor.Name = "txtAuthor";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.label3.Name = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtReview
            // 
            resources.ApplyResources(this.txtReview, "txtReview");
            this.txtReview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(79)))), ((int)(((byte)(99)))));
            this.txtReview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReview.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtReview.Name = "txtReview";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.label4.Name = "label4";
            // 
            // numericUpDownRating
            // 
            resources.ApplyResources(this.numericUpDownRating, "numericUpDownRating");
            this.numericUpDownRating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(79)))), ((int)(((byte)(99)))));
            this.numericUpDownRating.ForeColor = System.Drawing.Color.DarkKhaki;
            this.numericUpDownRating.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRating.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRating.Name = "numericUpDownRating";
            this.numericUpDownRating.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(165)))), ((int)(((byte)(169)))));
            this.label5.Name = "label5";
            // 
            // btnAddBookAddBook
            // 
            resources.ApplyResources(this.btnAddBookAddBook, "btnAddBookAddBook");
            this.btnAddBookAddBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.btnAddBookAddBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBookAddBook.FlatAppearance.BorderSize = 0;
            this.btnAddBookAddBook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnAddBookAddBook.Name = "btnAddBookAddBook";
            this.btnAddBookAddBook.UseVisualStyleBackColor = false;
            this.btnAddBookAddBook.Click += new System.EventHandler(this.btnAddBookAddBook_Click);
            // 
            // btnAddBookClear
            // 
            resources.ApplyResources(this.btnAddBookClear, "btnAddBookClear");
            this.btnAddBookClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(79)))), ((int)(((byte)(99)))));
            this.btnAddBookClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddBookClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnAddBookClear.Name = "btnAddBookClear";
            this.btnAddBookClear.UseVisualStyleBackColor = false;
            this.btnAddBookClear.Click += new System.EventHandler(this.btnAddBookClear_Click);
            // 
            // numericUpDownYear
            // 
            resources.ApplyResources(this.numericUpDownYear, "numericUpDownYear");
            this.numericUpDownYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(79)))), ((int)(((byte)(99)))));
            this.numericUpDownYear.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.numericUpDownYear.Maximum = new decimal(new int[] {
            2022,
            0,
            0,
            0});
            this.numericUpDownYear.Name = "numericUpDownYear";
            this.numericUpDownYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // frmAddBook
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.Controls.Add(this.numericUpDownYear);
            this.Controls.Add(this.btnAddBookClear);
            this.Controls.Add(this.btnAddBookAddBook);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownRating);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReview);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBookName);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAddBook";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBookName;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReview;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownRating;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddBookAddBook;
        private System.Windows.Forms.Button btnAddBookClear;
        private System.Windows.Forms.NumericUpDown numericUpDownYear;
    }
}