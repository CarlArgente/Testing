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

namespace Testing.RETURN
{
    public partial class frmcompleted : Form
    {
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

        public frmcompleted()
        {
            InitializeComponent();
        }
        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmcompleted_Load(object sender, EventArgs e)
        {
            load_names();
        }
        SQLControl sql = new SQLControl();
        private void load_names(string id = "")
        {
            lstnames.Items.Clear();
            sql.Query($"SELECT student_tb.student_id, student_tb.name, count(transac_tb.status) as status from student_tb INNER JOIN transac_tb ON " +
            $"student_tb.student_id = transac_tb.student_id where  transac_tb.status like 'Complete' and student_tb.student_id like '%{id}%' " +
            $"group by student_tb.student_id, student_tb.name");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(dr["student_id"]));
                    ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["name"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["status"]))
                    };
                    item.SubItems.AddRange(subitems);
                    lstnames.Items.Add(item);
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            load_names(txtsearch.Text);
            lsttransac.Items.Clear();
        }
        static int id;
        private void lstnames_Click(object sender, EventArgs e)
        {
            if (lstnames.SelectedItems.Count > 0)
            {
                id = int.Parse(lstnames.SelectedItems[0].SubItems[0].Text);
                load_transaction(id, "Complete");
            }
        }
        private void load_transaction(int id, string status)
        {
            lsttransac.Items.Clear();
            sql.Query($"SELECT * FROM books_tb INNER JOIN transac_tb ON books_tb.book_id = transac_tb.book_id where student_id ={id} and status like '%{status}%' ");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(dr["transac_id"]));
                    ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["barcode_number"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["title"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToDateTime(dr["date_due"]).ToString("yyyy/MM/dd")),
                        new ListViewItem.ListViewSubItem(item, Convert.ToDateTime(dr["date_returned"]).ToString("yyyy/MM/dd")),
                        new ListViewItem.ListViewSubItem(item, Convert.ToInt32(dr["penalty"]).ToString("N")),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["condition"]))
                    };
                    item.SubItems.AddRange(subitems);
                    lsttransac.Items.Add(item);
                }
            }
        }
    }
}
