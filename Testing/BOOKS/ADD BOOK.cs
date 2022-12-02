using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Testing.BOOKS
{
    public partial class ADD_BOOK : Form
    {
        SQLControl sql = new SQLControl();
        //DROP SHADOW START HERE===================================================================================
        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        //DROP SHADOW ENDS HERE===================================================================================

        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();
        misc_class misc = new misc_class();
        static string _barcode_num;
        string choice1;
        int _form;
        string barcodenum;
        public ADD_BOOK(string choice, string book_id, string barcode_num, int form)
        {
            InitializeComponent();
            choice1 = choice;
            _barcode_num = barcode_num;
            _form = form;

            
            if (_form == 2)
            {
                barcodenum = _barcode_num;
                txtbarcode.Text = _barcode_num;
                txtbarcode.Enabled = false;
            }
            else if (_form == 3)
            {
                txtbarcode.Text = ucBooks.barcode_num;
                txtbarcode.Enabled = false;
            }
        }      
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string publisher;
            int bcount = 0;
            if (_form == 1)
            {
                barcodenum = txtbarcode.Text;
            }
            else if (_form == 2)
            {
                barcodenum = _barcode_num;
                txtbarcode.Text = _barcode_num;
                txtbarcode.Enabled = false;
            }

            if (txttitle.Text == "" || txtqty.Text == "" || txtauthor.Text == "" || txtEBookFile.Text == "")
            {
                MessageBox.Show(this, "Please fill-up all fields to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtpublisher.Text == "")
            {
                publisher = "None";
            }
            else
            {
                publisher = txtpublisher.Text;
            }

            string filePath = path;
            // Converts text file(.txt) into byte[]
            byte[] fileData = File.ReadAllBytes(filePath);
            if (ImageByteArray != null)
            {
                sql.AddParam("@pic", ImageByteArray);
                sql.AddParam("@fileData", fileData);

                sql.Query($"insert into books_tb (preview_image,pdf_file,title, author, category, qty, total, barcode_number, bcount, books_tb.archive, publisher, year) values" +
                $"(@pic,@fileData,'{txttitle.Text}','{txtauthor.Text}','{cmbcategory.Text}','{int.Parse(txtqty.Text)}', '{int.Parse(txtqty.Text)}', '{barcodenum}', '{bcount}', '{"OK"}', '{publisher}', '{dtpyear.Value.ToString("yyyy")}')");
                if (sql.HasException(true)) return;
                MessageBox.Show(this, "Book added successfully!", "Books", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show(this, "The preview image is required!", "Books", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
            else if ((int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
        
        private void ADD_BOOK_Load(object sender, EventArgs e)
        {
            misc.cat_to_cmb(cmbcategory);
            if (choice1 == "Add")
            {
                lbltitle.Text = "Add Book";
                btnAdd.Visible = true;
                btnupdate.Visible = false;
                btnArchive.Visible = false;
            }
            else
            {              
                label5.Text = "Add qty:";
                lbltitle.Text = "Update Book";
                btnAdd.Visible = false;
                btnupdate.Visible = true;
                txtauthor.Enabled = false;
                txttitle.Enabled = false;
                txtpublisher.Enabled = false;
                dtpyear.Enabled = false;
                txtauthor.Text = ucBooks.author;
                cmbcategory.Text = ucBooks.cat;
                txttitle.Text = ucBooks.title;
                txtpublisher.Text = ucBooks.publisher;
                button1.Visible = false;

                DateTime dt;
                //DateTime.TryParseExact(ucBooks.year, "yyyy",null, System.Globalization.DateTimeStyles.None, out dt) ;
                //dtpyear.Value = dt;
                txtqty.Focus();

                sql.Query("SELECT preview_image FROM books_tb WHERE book_id='" + ucBooks.book_id + "'  ");
                if (sql.HasException(true)) return;
                if(sql.DBDT.Rows.Count > 0)
                {
                    foreach(DataRow dr in sql.DBDT.Rows)
                    {
                        if (!dr["preview_image"].Equals(DBNull.Value))
                        {
                            byte[] img = ((byte[])dr["preview_image"]);
                            ImageByteArray = img;
                            pbLogo.Image = Image.FromStream(new MemoryStream(img));
                        }
                    }
                }
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            int i;
            if(txtqty.Text == "")
            {
                i = 0;
            }
            else
            {
                i = int.Parse(txtqty.Text);
            }
            sql.Query($"update books_tb set qty= qty + '{i}', category = '{cmbcategory.Text}', total= total + '{i}' where book_id = '{ucBooks.book_id}'");
            if (sql.HasException(true)) return;
            MessageBox.Show(this, "Book updated successfully!", "Books", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            using (Testing.BOOKS.ADD_CAT frm = new ADD_CAT())
            {
                frm.ShowDialog();
            }
            misc.cat_to_cmb(cmbcategory);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            //checker if the book is borrowed
            int book_id = int.Parse(sql.ReturnResult($"select book_id FROM books_tb where barcode_number = '{txtbarcode.Text}' "));
            sql.Query($"select book_id from transac_tb where book_id={book_id} and status='Ongoing' ");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                MessageBox.Show(this, "You cannot archieve this book!", "Books", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to add book " + "\"" + txttitle.Text + "\"" + " to archived?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    using (BOOKS.frmremarks frm = new BOOKS.frmremarks())
                    {
                        frm.ShowDialog();
                    }
                    sql.Query($"update books_tb set books_tb.archive = '{"Archived"}', remarks = '{frmremarks.remarks}' where book_id = '{ucBooks.book_id}'");
                    if (sql.HasException(true)) return;
                    MessageBox.Show(this, "Book archived successfully!", "Books", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();

                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
          
        }
        String path;
        String strFilePath = "";
        private void btnAttach_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Pdf Files|*.pdf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.FileName;
            }
            txtEBookFile.Text = path;
        }
        Byte[] ImageByteArray;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                strFilePath = dialog.FileName;
                pbLogo.Image = new Bitmap(strFilePath);
            }
            //for updating image
            if (strFilePath == "")
            {
                if (ImageByteArray != null)
                {
                    if (ImageByteArray.Length != 0)
                    {
                        ImageByteArray = new byte[] { };
                    }
                }
            }
            else
            {
                Image temp = new Bitmap(strFilePath);
                MemoryStream stream = new MemoryStream();
                temp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                ImageByteArray = stream.ToArray();
                
            }
        }
    }
    
}
