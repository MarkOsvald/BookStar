
namespace Dashboard
{
    partial class ListItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnREVIEW = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DELETE = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(102, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(341, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoEllipsis = true;
            this.lblAuthor.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor.Location = new System.Drawing.Point(104, 32);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(344, 31);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author";
            this.lblAuthor.Click += new System.EventHandler(this.lblMessage_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 1);
            this.panel1.TabIndex = 3;
            // 
            // btnREVIEW
            // 
            this.btnREVIEW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnREVIEW.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnREVIEW.FlatAppearance.BorderSize = 0;
            this.btnREVIEW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnREVIEW.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnREVIEW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnREVIEW.Location = new System.Drawing.Point(580, 0);
            this.btnREVIEW.Name = "btnREVIEW";
            this.btnREVIEW.Size = new System.Drawing.Size(75, 83);
            this.btnREVIEW.TabIndex = 6;
            this.btnREVIEW.Text = "REVIEW";
            this.btnREVIEW.UseVisualStyleBackColor = false;
            this.btnREVIEW.Click += new System.EventHandler(this.btnREVIEW_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblRating);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(53, 83);
            this.panel2.TabIndex = 7;
            // 
            // lblRating
            // 
            this.lblRating.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.lblRating.ForeColor = System.Drawing.Color.Gold;
            this.lblRating.Location = new System.Drawing.Point(9, 26);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(44, 31);
            this.lblRating.TabIndex = 0;
            this.lblRating.Text = "00";
            this.lblRating.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRating.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblYear
            // 
            this.lblYear.AutoEllipsis = true;
            this.lblYear.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.Location = new System.Drawing.Point(104, 56);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(344, 27);
            this.lblYear.TabIndex = 8;
            this.lblYear.Text = "Year";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox1.Image = global::Dashboard.Properties.Resources.icons8_star_24__1_;
            this.pictureBox1.Location = new System.Drawing.Point(33, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 83);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // DELETE
            // 
            this.DELETE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.DELETE.Dock = System.Windows.Forms.DockStyle.Right;
            this.DELETE.FlatAppearance.BorderSize = 0;
            this.DELETE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DELETE.Image = global::Dashboard.Properties.Resources.icons8_trash_24;
            this.DELETE.Location = new System.Drawing.Point(655, 0);
            this.DELETE.Name = "DELETE";
            this.DELETE.Size = new System.Drawing.Size(75, 83);
            this.DELETE.TabIndex = 4;
            this.DELETE.UseVisualStyleBackColor = false;
            this.DELETE.Click += new System.EventHandler(this.DELETE_Click);
            // 
            // ListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnREVIEW);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DELETE);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblTitle);
            this.Name = "ListItem";
            this.Size = new System.Drawing.Size(730, 84);
            this.Load += new System.EventHandler(this.ListItem_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button DELETE;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnREVIEW;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblYear;
    }
}
