
namespace Testing.STUDENTS
{
    partial class frmselect_book
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
            this.lblborrowedbooks = new System.Windows.Forms.Label();
            this.btnsearchID = new System.Windows.Forms.Button();
            this.btnsearchQR = new System.Windows.Forms.Button();
            this.txttitle = new System.Windows.Forms.TextBox();
            this.txtauthor = new System.Windows.Forms.TextBox();
            this.lblstudentid = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtbarcodeid = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.datepick = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.txtfindID = new System.Windows.Forms.TextBox();
            this.listbook = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblborrowedbooks
            // 
            this.lblborrowedbooks.AutoSize = true;
            this.lblborrowedbooks.Enabled = false;
            this.lblborrowedbooks.Font = new System.Drawing.Font("Segoe UI Black", 21.75F, System.Drawing.FontStyle.Bold);
            this.lblborrowedbooks.ForeColor = System.Drawing.Color.Black;
            this.lblborrowedbooks.Location = new System.Drawing.Point(1, 2);
            this.lblborrowedbooks.Name = "lblborrowedbooks";
            this.lblborrowedbooks.Size = new System.Drawing.Size(182, 40);
            this.lblborrowedbooks.TabIndex = 47;
            this.lblborrowedbooks.Text = "Select Book";
            // 
            // btnsearchID
            // 
            this.btnsearchID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(203)))));
            this.btnsearchID.FlatAppearance.BorderSize = 0;
            this.btnsearchID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearchID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearchID.ForeColor = System.Drawing.Color.White;
            this.btnsearchID.Location = new System.Drawing.Point(12, 55);
            this.btnsearchID.Name = "btnsearchID";
            this.btnsearchID.Size = new System.Drawing.Size(173, 33);
            this.btnsearchID.TabIndex = 48;
            this.btnsearchID.Text = "Search Barcode or title";
            this.btnsearchID.UseVisualStyleBackColor = false;
            this.btnsearchID.Click += new System.EventHandler(this.btnsearchID_Click);
            // 
            // btnsearchQR
            // 
            this.btnsearchQR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.btnsearchQR.FlatAppearance.BorderSize = 0;
            this.btnsearchQR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearchQR.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnsearchQR.ForeColor = System.Drawing.Color.White;
            this.btnsearchQR.Location = new System.Drawing.Point(193, 55);
            this.btnsearchQR.Name = "btnsearchQR";
            this.btnsearchQR.Size = new System.Drawing.Size(173, 33);
            this.btnsearchQR.TabIndex = 49;
            this.btnsearchQR.Text = "Scan Barcode";
            this.btnsearchQR.UseVisualStyleBackColor = false;
            this.btnsearchQR.Click += new System.EventHandler(this.btnsearchQR_Click);
            // 
            // txttitle
            // 
            this.txttitle.Enabled = false;
            this.txttitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txttitle.Location = new System.Drawing.Point(132, 68);
            this.txttitle.Name = "txttitle";
            this.txttitle.Size = new System.Drawing.Size(217, 26);
            this.txttitle.TabIndex = 50;
            // 
            // txtauthor
            // 
            this.txtauthor.Enabled = false;
            this.txtauthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtauthor.Location = new System.Drawing.Point(132, 100);
            this.txtauthor.Name = "txtauthor";
            this.txtauthor.Size = new System.Drawing.Size(217, 26);
            this.txtauthor.TabIndex = 51;
            // 
            // lblstudentid
            // 
            this.lblstudentid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblstudentid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblstudentid.Location = new System.Drawing.Point(5, 71);
            this.lblstudentid.Name = "lblstudentid";
            this.lblstudentid.Size = new System.Drawing.Size(85, 19);
            this.lblstudentid.TabIndex = 52;
            this.lblstudentid.Text = "Book Title: ";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.textBox2.Location = new System.Drawing.Point(5, 103);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(85, 19);
            this.textBox2.TabIndex = 53;
            this.textBox2.Text = "Author:";
            // 
            // txtbarcodeid
            // 
            this.txtbarcodeid.Enabled = false;
            this.txtbarcodeid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtbarcodeid.Location = new System.Drawing.Point(132, 36);
            this.txtbarcodeid.Name = "txtbarcodeid";
            this.txtbarcodeid.Size = new System.Drawing.Size(217, 26);
            this.txtbarcodeid.TabIndex = 54;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.textBox4.Location = new System.Drawing.Point(5, 39);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(85, 19);
            this.textBox4.TabIndex = 55;
            this.textBox4.Text = "Barcode ID:";
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.textBox5.Location = new System.Drawing.Point(5, 140);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(85, 19);
            this.textBox5.TabIndex = 56;
            this.textBox5.Text = "Due Date: ";
            // 
            // datepick
            // 
            this.datepick.CustomFormat = "yyyy-MM-dd";
            this.datepick.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.datepick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datepick.Location = new System.Drawing.Point(132, 135);
            this.datepick.Name = "datepick";
            this.datepick.Size = new System.Drawing.Size(217, 26);
            this.datepick.TabIndex = 57;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.datepick);
            this.groupBox1.Controls.Add(this.txttitle);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.txtauthor);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.lblstudentid);
            this.groupBox1.Controls.Add(this.txtbarcodeid);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 180);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transaction Details";
            // 
            // btnsubmit
            // 
            this.btnsubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.btnsubmit.Enabled = false;
            this.btnsubmit.FlatAppearance.BorderSize = 0;
            this.btnsubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsubmit.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnsubmit.ForeColor = System.Drawing.Color.White;
            this.btnsubmit.Location = new System.Drawing.Point(12, 335);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(173, 33);
            this.btnsubmit.TabIndex = 59;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.UseVisualStyleBackColor = false;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btncancel.FlatAppearance.BorderSize = 0;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btncancel.ForeColor = System.Drawing.Color.White;
            this.btncancel.Location = new System.Drawing.Point(193, 335);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(173, 33);
            this.btncancel.TabIndex = 60;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // txtfindID
            // 
            this.txtfindID.Enabled = false;
            this.txtfindID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtfindID.Location = new System.Drawing.Point(12, 104);
            this.txtfindID.Name = "txtfindID";
            this.txtfindID.Size = new System.Drawing.Size(354, 26);
            this.txtfindID.TabIndex = 58;
            this.txtfindID.TextChanged += new System.EventHandler(this.txtfindID_TextChanged);
            // 
            // listbook
            // 
            this.listbook.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader12,
            this.columnHeader2,
            this.columnHeader3});
            this.listbook.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.listbook.FullRowSelect = true;
            this.listbook.GridLines = true;
            this.listbook.HideSelection = false;
            this.listbook.Location = new System.Drawing.Point(372, 12);
            this.listbook.MultiSelect = false;
            this.listbook.Name = "listbook";
            this.listbook.Size = new System.Drawing.Size(568, 356);
            this.listbook.TabIndex = 62;
            this.listbook.UseCompatibleStateImageBehavior = false;
            this.listbook.View = System.Windows.Forms.View.Details;
            this.listbook.SelectedIndexChanged += new System.EventHandler(this.listbook_SelectedIndexChanged);
            this.listbook.Click += new System.EventHandler(this.listbook_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Barcode #";
            this.columnHeader9.Width = 140;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Book Title";
            this.columnHeader10.Width = 174;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Author";
            this.columnHeader12.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Available";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Total";
            // 
            // frmselect_book
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(952, 380);
            this.Controls.Add(this.listbook);
            this.Controls.Add(this.txtfindID);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnsubmit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnsearchQR);
            this.Controls.Add(this.btnsearchID);
            this.Controls.Add(this.lblborrowedbooks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmselect_book";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmselect_book";
            this.Load += new System.EventHandler(this.frmselect_book_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblborrowedbooks;
        private System.Windows.Forms.Button btnsearchID;
        private System.Windows.Forms.Button btnsearchQR;
        private System.Windows.Forms.TextBox txttitle;
        private System.Windows.Forms.TextBox txtauthor;
        private System.Windows.Forms.TextBox lblstudentid;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtbarcodeid;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.DateTimePicker datepick;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.TextBox txtfindID;
        private System.Windows.Forms.ListView listbook;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}