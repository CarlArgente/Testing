using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.BOOKS
{
    public partial class BookViewer : Form
    {
        #region
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

        #endregion
        SQLControl sql = new SQLControl();
        private void load_items(string search = "")
        {
            listView1.Items.Clear();
            if (search == "")
            {
                sql.Query($"select * from books_tb where archive = 'OK' ORDER BY title ASC");
            }
            else
            {
                sql.Query($"select * from books_tb where title like '%{search}%' AND archive = 'OK' ORDER BY title ASC ");
            }

            if (sql.DBDT.Rows.Count > 0)
            {
                foreach (DataRow dr in sql.DBDT.Rows)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(dr["book_id"]));
                    ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["barcode_number"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["title"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["author"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["qty"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["total"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["publisher"])),
                        new ListViewItem.ListViewSubItem(item,  Convert.ToString(dr["year"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["category"]))
                    };
                    item.SubItems.AddRange(subitems);
                    listView1.Items.Add(item);
                }
            }
            else
            {
                sql.Query($"select * from books_tb where barcode_number like '%{search}%' AND archive = 'OK'  ORDER BY title ASC ");
                if (sql.DBDT.Rows.Count > 0)
                {
                    foreach (DataRow dr in sql.DBDT.Rows)
                    {
                        ListViewItem item = new ListViewItem(Convert.ToString(dr["book_id"]));
                        ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                        {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["barcode_number"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["title"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["author"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["qty"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["total"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["publisher"])),
                        new ListViewItem.ListViewSubItem(item,  Convert.ToString(dr["year"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["category"]))
                        };
                        item.SubItems.AddRange(subitems);
                        listView1.Items.Add(item);
                    }
                }
            }
        }
        public BookViewer()
        {
            InitializeComponent();
            load_items();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            load_items(textBox1.Text);
            pbLogo.Image = null;
            txtSelectedBook.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtSelectedBook.Text.Equals(""))
            {
                MessageBox.Show(this, "Please select book first", "Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Choosing Directory
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.InitialDirectory = "C:\\Users";
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    DialogResult result = MessageBox.Show("You are about to download the E-book file of the selected book. Proceed?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        sql.Query($"SELECT pdf_file FROM books_tb WHERE title= '{txtSelectedBook.Text}' ");
                        if (sql.HasException(true)) return;
                        if (sql.DBDT.Rows.Count > 0)
                        {
                            foreach (DataRow dr in sql.DBDT.Rows)
                            {
                                byte[] fileData = (byte[])dr[0];
                                String path = dialog.FileName + "\\" + txtSelectedBook.Text + ".pdf";
                                File.WriteAllBytes(path, fileData);
                                MessageBox.Show(this, "Successfully Downloaded E-Book File.", "Download E-Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Byte[] ImageByteArray;
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtSelectedBook.Text = listView1.SelectedItems[0].SubItems[2].Text;

                //
                sql.Query("SELECT preview_image FROM books_tb WHERE book_id='" + int.Parse(listView1.SelectedItems[0].SubItems[0].Text) + "'  ");
                if (sql.HasException(true)) return;
                if (sql.DBDT.Rows.Count > 0)
                {
                    foreach (DataRow dr in sql.DBDT.Rows)
                    {
                        if (!dr["preview_image"].Equals(DBNull.Value))
                        {
                            byte[] img = ((byte[])dr["preview_image"]);
                            ImageByteArray = img;
                            pbLogo.Image = Image.FromStream(new MemoryStream(img));
                        }
                        else
                        {
                            pbLogo.Image = null;
                        }
                    }
                }

            }

        }
    }
}
