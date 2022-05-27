
namespace Testing.BORROW
{
    partial class ucBorrow
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
            this.btnsearchID = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtage = new System.Windows.Forms.TextBox();
            this.btnselect = new System.Windows.Forms.Button();
            this.txtyearsec = new System.Windows.Forms.TextBox();
            this.txtname = new System.Windows.Forms.TextBox();
            this.btnsearchQR = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnfind = new System.Windows.Forms.Button();
            this.txtstudentid = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnissue = new System.Windows.Forms.Button();
            this.lblborrowedbooks = new System.Windows.Forms.Label();
            this.btnclear = new System.Windows.Forms.Button();
            this.btnselectbook = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.book_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnsearchID
            // 
            this.btnsearchID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(203)))));
            this.btnsearchID.FlatAppearance.BorderSize = 0;
            this.btnsearchID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearchID.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnsearchID.ForeColor = System.Drawing.Color.White;
            this.btnsearchID.Location = new System.Drawing.Point(14, 32);
            this.btnsearchID.Name = "btnsearchID";
            this.btnsearchID.Size = new System.Drawing.Size(178, 33);
            this.btnsearchID.TabIndex = 39;
            this.btnsearchID.Text = "Search by Student #";
            this.btnsearchID.UseVisualStyleBackColor = false;
            this.btnsearchID.Click += new System.EventHandler(this.btnsearchID_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGrid);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtage);
            this.groupBox1.Controls.Add(this.btnselect);
            this.groupBox1.Controls.Add(this.txtyearsec);
            this.groupBox1.Controls.Add(this.txtname);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(403, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 178);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Borrower Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.label3.Location = new System.Drawing.Point(61, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 25);
            this.label3.TabIndex = 57;
            this.label3.Text = "Age :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.label2.Location = new System.Drawing.Point(61, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 25);
            this.label2.TabIndex = 56;
            this.label2.Text = "Year&Section :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.label1.Location = new System.Drawing.Point(61, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 25);
            this.label1.TabIndex = 55;
            this.label1.Text = "Name :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtage
            // 
            this.txtage.Enabled = false;
            this.txtage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtage.Location = new System.Drawing.Point(215, 97);
            this.txtage.Name = "txtage";
            this.txtage.Size = new System.Drawing.Size(200, 26);
            this.txtage.TabIndex = 53;
            // 
            // btnselect
            // 
            this.btnselect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(203)))));
            this.btnselect.FlatAppearance.BorderSize = 0;
            this.btnselect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnselect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnselect.ForeColor = System.Drawing.Color.White;
            this.btnselect.Location = new System.Drawing.Point(215, 129);
            this.btnselect.Name = "btnselect";
            this.btnselect.Size = new System.Drawing.Size(200, 35);
            this.btnselect.TabIndex = 49;
            this.btnselect.Text = "Select";
            this.btnselect.UseVisualStyleBackColor = false;
            this.btnselect.Visible = false;
            this.btnselect.Click += new System.EventHandler(this.btnselect_Click);
            // 
            // txtyearsec
            // 
            this.txtyearsec.Enabled = false;
            this.txtyearsec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtyearsec.Location = new System.Drawing.Point(215, 62);
            this.txtyearsec.Name = "txtyearsec";
            this.txtyearsec.Size = new System.Drawing.Size(200, 26);
            this.txtyearsec.TabIndex = 50;
            // 
            // txtname
            // 
            this.txtname.Enabled = false;
            this.txtname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtname.Location = new System.Drawing.Point(215, 30);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(200, 26);
            this.txtname.TabIndex = 48;
            // 
            // btnsearchQR
            // 
            this.btnsearchQR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(203)))));
            this.btnsearchQR.FlatAppearance.BorderSize = 0;
            this.btnsearchQR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearchQR.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnsearchQR.ForeColor = System.Drawing.Color.White;
            this.btnsearchQR.Location = new System.Drawing.Point(14, 71);
            this.btnsearchQR.Name = "btnsearchQR";
            this.btnsearchQR.Size = new System.Drawing.Size(178, 33);
            this.btnsearchQR.TabIndex = 41;
            this.btnsearchQR.Text = "Search by QR";
            this.btnsearchQR.UseVisualStyleBackColor = false;
            this.btnsearchQR.Click += new System.EventHandler(this.btnsearchQR_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnfind);
            this.groupBox2.Controls.Add(this.txtstudentid);
            this.groupBox2.Controls.Add(this.btnsearchID);
            this.groupBox2.Controls.Add(this.btnsearchQR);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(3, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 151);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Student";
            // 
            // btnfind
            // 
            this.btnfind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(203)))));
            this.btnfind.FlatAppearance.BorderSize = 0;
            this.btnfind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnfind.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfind.ForeColor = System.Drawing.Color.White;
            this.btnfind.Location = new System.Drawing.Point(198, 108);
            this.btnfind.Name = "btnfind";
            this.btnfind.Size = new System.Drawing.Size(190, 33);
            this.btnfind.TabIndex = 48;
            this.btnfind.Text = "Find";
            this.btnfind.UseVisualStyleBackColor = false;
            this.btnfind.Visible = false;
            this.btnfind.Click += new System.EventHandler(this.btnfind_Click);
            // 
            // txtstudentid
            // 
            this.txtstudentid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtstudentid.Location = new System.Drawing.Point(198, 71);
            this.txtstudentid.Multiline = true;
            this.txtstudentid.Name = "txtstudentid";
            this.txtstudentid.Size = new System.Drawing.Size(190, 33);
            this.txtstudentid.TabIndex = 46;
            this.txtstudentid.Visible = false;
            this.txtstudentid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtstudentid_KeyDown);
            this.txtstudentid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtstudentid_KeyPress);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.listView1.Enabled = false;
            this.listView1.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(15, 235);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(719, 196);
            this.listView1.TabIndex = 43;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Barcode ID";
            this.columnHeader9.Width = 150;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Book Title";
            this.columnHeader10.Width = 260;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Author";
            this.columnHeader12.Width = 197;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Date Issued";
            this.columnHeader13.Width = 0;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Due Date";
            this.columnHeader14.Width = 120;
            // 
            // btnissue
            // 
            this.btnissue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(203)))));
            this.btnissue.Enabled = false;
            this.btnissue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnissue.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnissue.ForeColor = System.Drawing.Color.White;
            this.btnissue.Location = new System.Drawing.Point(740, 235);
            this.btnissue.Name = "btnissue";
            this.btnissue.Size = new System.Drawing.Size(112, 97);
            this.btnissue.TabIndex = 44;
            this.btnissue.Text = "Issue";
            this.btnissue.UseVisualStyleBackColor = false;
            this.btnissue.Click += new System.EventHandler(this.btnissue_Click);
            // 
            // lblborrowedbooks
            // 
            this.lblborrowedbooks.AutoSize = true;
            this.lblborrowedbooks.Enabled = false;
            this.lblborrowedbooks.Font = new System.Drawing.Font("Segoe UI Black", 16.75F, System.Drawing.FontStyle.Bold);
            this.lblborrowedbooks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblborrowedbooks.Location = new System.Drawing.Point(9, 198);
            this.lblborrowedbooks.Name = "lblborrowedbooks";
            this.lblborrowedbooks.Size = new System.Drawing.Size(176, 31);
            this.lblborrowedbooks.TabIndex = 46;
            this.lblborrowedbooks.Text = "Borrow Books";
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclear.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnclear.ForeColor = System.Drawing.Color.White;
            this.btnclear.Location = new System.Drawing.Point(740, 340);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(112, 91);
            this.btnclear.TabIndex = 47;
            this.btnclear.Text = "Clear";
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnselectbook
            // 
            this.btnselectbook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(203)))));
            this.btnselectbook.Enabled = false;
            this.btnselectbook.FlatAppearance.BorderSize = 0;
            this.btnselectbook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnselectbook.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnselectbook.ForeColor = System.Drawing.Color.White;
            this.btnselectbook.Location = new System.Drawing.Point(201, 208);
            this.btnselectbook.Name = "btnselectbook";
            this.btnselectbook.Size = new System.Drawing.Size(190, 23);
            this.btnselectbook.TabIndex = 53;
            this.btnselectbook.Text = "Select Book";
            this.btnselectbook.UseVisualStyleBackColor = false;
            this.btnselectbook.Click += new System.EventHandler(this.btnselectbook_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(237)))));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 40);
            this.panel1.TabIndex = 54;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Black", 19.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(2, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 37);
            this.label6.TabIndex = 47;
            this.label6.Text = "Borrowing";
            // 
            // book_name
            // 
            this.book_name.HeaderText = "book_name";
            this.book_name.Name = "book_name";
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Qty,
            this.book_name});
            this.dataGrid.Location = new System.Drawing.Point(-31, -5);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(240, 150);
            this.dataGrid.TabIndex = 56;
            this.dataGrid.Visible = false;
            // 
            // ucBorrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnselectbook);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.lblborrowedbooks);
            this.Controls.Add(this.btnissue);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucBorrow";
            this.Size = new System.Drawing.Size(865, 446);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnsearchID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnsearchQR;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnissue;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Label lblborrowedbooks;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.TextBox txtstudentid;
        private System.Windows.Forms.TextBox txtyearsec;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.Button btnfind;
        private System.Windows.Forms.Button btnselect;
        private System.Windows.Forms.Button btnselectbook;
        private System.Windows.Forms.TextBox txtage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn book_name;
    }
}
