using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using MySql.Data.MySqlClient;

namespace Testing.BOOKS
{
    public partial class BARCODE_RESULT : Form
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

        misc_class misc = new misc_class();
        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();
        public BARCODE_RESULT()
        {
            InitializeComponent();
        }
        FilterInfoCollection filinfo;
        VideoCaptureDevice captureDevice;
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop(); 
            this.Close();          
        }     
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                BarcodeReader read = new BarcodeReader();
                Result result = read.Decode((Bitmap)pictureBox1.Image);
                if (result != null)
                {
                    timer1.Stop();
                    txtqr.Text = result.ToString();
                    groupBox1.Enabled = true;
                    btnAdd.Enabled = true;                 
                    if (captureDevice.IsRunning)
                    {
                        captureDevice.Stop();
                    }
                    validate(result.ToString());

                }
                else if (result == null)
                {
                    timer1.Stop();
                    timer1.Start();
                }
            }
        }

        private void reset()
        {
            txtauthor.Text = "";
            txtlname.Text = "";
            txtqty.Text = "";
            //groupBox1.Enabled = false;
            btnAdd.Enabled = false;
            cmbcategory.Text = "";
        }
        public static string book_status;    
        private void populate(string barcode)
        {
            DateTime date;
            reset();
            btnAdd.Enabled = true;
            
            sql.Query($"select * from books_tb where barcode_number='{barcode}'");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    txtlname.Text = (dr["title"].ToString());
                    txtauthor.Text = (dr["author"].ToString());
                    cmbcategory.Text = (dr["category"].ToString());
                    txtpublisher.Text = (dr["publisher"].ToString());
                    string year = (dr["year"].ToString());
                    DateTime.TryParseExact(year, "yyyy", null, System.Globalization.DateTimeStyles.None, out date);
                    dtpyear.Value = date;
                }
            }
        }

        private void get_status(string barcode)
        {
            reset();
            btnAdd.Enabled = true;
            sql.Query($"select * from books_tb where barcode_number='{barcode}'");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    book_status = dr["archive"].ToString();
                }
            }
        }

        private void validate(string barcode_number)
        {
            get_status(barcode_number);
            sql.Query($"select * from books_tb where barcode_number='{barcode_number}'");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                if (book_status == "OK")
                {
                    txtqty.Enabled = true;
                    btnAdd.Enabled = true;
                    cmbcategory.Enabled = true;
                    MessageBox.Show("Book found", "Book Found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    populate(barcode_number);
                }
                else if (book_status == "Archived")
                {
                    populate(barcode_number);
                    btnAdd.Enabled = false;
                    txtqty.Enabled = false;
                    cmbcategory.Enabled = false;
                    MessageBox.Show("Book Archived!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Book not found, add it??", "Notice", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (ADD_BOOK frm = new ADD_BOOK("Add", "", txtqr.Text, 2))
                    {
                        frm.ShowDialog();
                        this.Close();
                    }
                }
                else if (result == DialogResult.No)
                {
                    reset();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtqr.Text = "";
           
            captureDevice = new VideoCaptureDevice(filinfo[cmbcam.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();
            timer1.Start();
            reset();
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void BARCODE_RESULT_Load(object sender, EventArgs e)
        {
            filinfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterinfo in filinfo)
            {
                cmbcam.Items.Add(filterinfo.Name);
                cmbcam.SelectedIndex = 0;
            }
            misc.cat_to_cmb(cmbcategory);         
        }

        private void BARCODE_RESULT_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void cmbcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {
            using (ADD_CAT frm = new ADD_CAT())
            {
                frm.ShowDialog();
            }
            misc.cat_to_cmb(cmbcategory);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtqty.Text == "")
            {
                MessageBox.Show(this, "Please fill-up fields to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                sql.Query($"update books_tb set qty= qty + '{int.Parse(txtqty.Text)}', category = '{cmbcategory.Text}', total= total +'{int.Parse(txtqty.Text)}' where barcode_number = '{txtqr.Text}'");
                if (sql.HasException(true)) return;
                MessageBox.Show(this, "Book updated successfully!", "Books", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
           
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {

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
                btnAdd.Focus();
            }
            else
                e.Handled = true;
        }
    }
}
