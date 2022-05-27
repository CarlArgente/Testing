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

namespace Testing.BORROW
{
    public partial class frmQR : Form
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
        DataTable dt = new DataTable();
        dbconnect db = new dbconnect();
        public frmQR()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        FilterInfoCollection filinfo;
        VideoCaptureDevice captureDevice;
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                BarcodeReader read = new BarcodeReader();
                Result result = read.Decode((Bitmap)pictureBox1.Image);
                if (result != null)
                {                
                    timer1.Stop();
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
        SQLControl sql = new SQLControl();
        private void validate(string student_id)
        {
            sql.Query($"select * from student_tb where student_id='{student_id}'");
            if (sql.HasException(true)) return;
            if (sql.DBDT.Rows.Count > 0)
            {
                ucBorrow.connection = "found";
                MessageBox.Show("Student Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ucBorrow.student_id = student_id;
                populate(student_id);
            }
            else
            {
                MessageBox.Show("Student not Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();

        }
        private void populate(string id)
        {          
            sql.Query($"select * from student_tb where student_id='{id}'");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    ucBorrow.name = (dr["name"].ToString());
                    ucBorrow.year = (dr["year"].ToString());
                    ucBorrow.section = (dr["section"].ToString());
                    ucBorrow.age = (dr["age"].ToString());
                }
            }
        }

        private void btnissue_Click(object sender, EventArgs e)
        {           
                 
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void frmQR_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void frmQR_Load(object sender, EventArgs e)
        {
            filinfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterinfo in filinfo)
            {
                comboBox1.Items.Add(filterinfo.Name);
                comboBox1.SelectedIndex = 0;
            }
        }

        private void btnSCAN_Click(object sender, EventArgs e)
        {           
            captureDevice = new VideoCaptureDevice(filinfo[comboBox1.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();
            timer1.Start();
        }
    }
}
