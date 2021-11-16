
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListItem));
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
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.Name = "lblTitle";
            // 
            // lblAuthor
            // 
            resources.ApplyResources(this.lblAuthor, "lblAuthor");
            this.lblAuthor.AutoEllipsis = true;
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Click += new System.EventHandler(this.lblMessage_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel1.Name = "panel1";
            // 
            // btnREVIEW
            // 
            resources.ApplyResources(this.btnREVIEW, "btnREVIEW");
            this.btnREVIEW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.btnREVIEW.FlatAppearance.BorderSize = 0;
            this.btnREVIEW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnREVIEW.Name = "btnREVIEW";
            this.btnREVIEW.UseVisualStyleBackColor = false;
            this.btnREVIEW.Click += new System.EventHandler(this.btnREVIEW_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.lblRating);
            this.panel2.Name = "panel2";
            // 
            // lblRating
            // 
            resources.ApplyResources(this.lblRating, "lblRating");
            this.lblRating.ForeColor = System.Drawing.Color.Gold;
            this.lblRating.Name = "lblRating";
            this.lblRating.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblYear
            // 
            resources.ApplyResources(this.lblYear, "lblYear");
            this.lblYear.AutoEllipsis = true;
            this.lblYear.Name = "lblYear";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox1.Image = global::Dashboard.Properties.Resources.icons8_star_24__1_;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // DELETE
            // 
            resources.ApplyResources(this.DELETE, "DELETE");
            this.DELETE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.DELETE.FlatAppearance.BorderSize = 0;
            this.DELETE.Image = global::Dashboard.Properties.Resources.icons8_trash_24;
            this.DELETE.Name = "DELETE";
            this.DELETE.UseVisualStyleBackColor = false;
            this.DELETE.Click += new System.EventHandler(this.DELETE_Click);
            // 
            // ListItem
            // 
            resources.ApplyResources(this, "$this");
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
