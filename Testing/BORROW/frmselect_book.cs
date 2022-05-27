using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Testing.STUDENTS
{
    public partial class frmselect_book : Form
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
        public delegate void DoEvent(books book);
        public event DoEvent add_book;
        public frmselect_book()
        {
            InitializeComponent();
        }
        DateTime date = DateTime.Now;
        private void listbook_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Today;
            if (listbook.SelectedItems.Count > 0)
            {
                txtbarcodeid.Text = listbook.SelectedItems[0].SubItems[1].Text;
                txttitle.Text = listbook.SelectedItems[0].SubItems[2].Text;
                txtauthor.Text = listbook.SelectedItems[0].SubItems[3].Text;
                btnsubmit.Enabled = true;
                book_id = int.Parse(listbook.SelectedItems[0].Text);
                barcode = listbook.SelectedItems[0].SubItems[1].Text;
                title = listbook.SelectedItems[0].SubItems[2].Text;
                author = listbook.SelectedItems[0].SubItems[3].Text;
                available = int.Parse(listbook.SelectedItems[0].SubItems[4].Text);
                total = int.Parse(listbook.SelectedItems[0].SubItems[5].Text);
            }
            else
            {
                btnsubmit.Enabled = false;
            }
        }
        public static string barcode, title, author;
        public static int book_id, status, available, total;
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            books i = new books()
            {
                book_id = book_id,
                barcode_id = barcode,
                book_title = title,
                book_author = author,
                available = available,
                total = total,
                issue = date.ToString("yyyy-MM-dd"),
                due = datepick.Value.ToString("yyyy-MM-dd")

            };
            if (i.available <= 0)
            {
                MessageBox.Show(this, "Book not available!", "Books", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            add_book(i);
            //INSERT INTO TEMP
            sql.Query("SELECT book_title FROM temp_borrow_tb WHERE book_title = '" + txttitle.Text + "' ");
            if (sql.DBDT.Rows.Count > 0)
            {
                sql.Query("UPDATE temp_borrow_tb SET qty = qty + 1 WHERE book_title = '" + txttitle.Text + "' ");
                if (sql.HasException(true)) return;
            }
            else
            {
                sql.Query("INSERT INTO temp_borrow_tb(qty,book_title) VALUES (1, '" + txttitle.Text + "')");
                if (sql.HasException(true)) return;
            }
            this.Hide();
        }

        private void frmselect_book_Load(object sender, EventArgs e)
        {
            load_items();
        }
        public void load_items(string search = "")
        {
            listbook.Items.Clear();
            sql.Query($"select * from books_tb where books_tb.archive = '{"OK"}' and barcode_number like '%{search}%'");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(dr["book_id"]));
                    ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["barcode_number"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["title"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["author"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["qty"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["total"]))

                    };
                    item.SubItems.AddRange(subitems);
                    listbook.Items.Add(item);
                }
            }
        }

        private void btnsearchID_Click(object sender, EventArgs e)
        {
            txtfindID.Enabled = true;
            txtfindID.Focus();
        }

        private void txtfindID_TextChanged(object sender, EventArgs e)
        {
            load_items(txtfindID.Text);
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }


        private void listbook_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnsearchQR_Click(object sender, EventArgs e)
        {
            txtfindID.Enabled = false;
            using (BORROW.frmbook_SCAN frm  = new BORROW.frmbook_SCAN())
            {
                frm.ShowDialog();
            }
            if(status == 1)
            {
                txtauthor.Text = author;
                txtbarcodeid.Text = barcode;
                txtauthor.Text = author;
                txttitle.Text = title;
                load_items(barcode);
                listbook.Select();
                btnsubmit.Enabled = true;
            }
            else if (status == 2)
            {
                load_items();
            }
        }
    }
}
