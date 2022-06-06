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

namespace Testing
{
    public partial class Form1 : Form
    {
       
        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }
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
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtpassword.Text == "" || txtusername.Text == "")
            {
                MessageBox.Show("Please fill up all fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                focus();
                return;
            }
            try
            {
                string username = txtusername.Text;
                string password = txtpassword.Text;
                sql.Query($"select * from user_tb where username ='{username}' ");
                if (sql.DBDT.Rows.Count > 0)
                {
                    foreach(DataRow dr in sql.DBDT.Rows)
                    {
                        if (password == dr["password"].ToString())
                        {
                            User.name = dr["fname"].ToString();
                            User.user_id = int.Parse(dr["user_id"].ToString());
                            using (Main_Menu frm = new Main_Menu())
                            {
                                Main_Menu.user = dr["username"].ToString();
                                this.Hide();
                                frm.ShowDialog();
                            }
                        }
                        else
                            MessageBox.Show(this, "Invalid Username/ Password", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        focus();
                    }
                }
                else
                    MessageBox.Show(this, "Invalid Username/ Password", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void focus()
        {
            txtusername.Clear();
            txtpassword.Clear();
            txtusername.Focus();
        }
        SQLControl sql = new SQLControl();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            txtpassword.Text = "";
            txtusername.Text = "";
            txtusername.Focus();
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                btnlogin_Click(sender, e);
            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                btnlogin_Click(sender, e);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Application.Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            new LoginAsStudent().Show();
            this.Hide();
        }
    }
}
