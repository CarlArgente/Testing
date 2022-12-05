using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.SETTINGS
{
    public partial class Users : Form
    {
        SQLControl sql = new SQLControl();
        private void reset()
        {
            txtUsername.Text = "";
            txtName.Text = "";
            txtsearch.Text = "";
            cmbRole.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            btnupdate.Enabled = false;
            btnadd.Enabled = true;
            txtid.Text = "XXXX-XXXX";
        }
        private void load_user(string search = "")
        {
            listView1.Items.Clear();
            sql.Query($"select * from user_tb where user_id like '%{search}%' or lname like '%{search}%' or fname like '%{search}%' ");
            if (sql.HasException(true)) return;
            if (sql.DBDT.Rows.Count > 0)
            {
                foreach (DataRow dr in sql.DBDT.Rows)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(dr["user_id"]));
                    ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["name"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["username"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["role"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["status"]))
                    };
                    item.SubItems.AddRange(subitems);
                    listView1.Items.Add(item);
                }
            }
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
        public Users()
        {
            InitializeComponent();
            load_user();
            reset();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            using (AddUsers frm = new AddUsers())
            {
                frm.ShowDialog();
                load_user();
                reset();
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            load_user(txtsearch.Text);
        }
        int user_id;
       
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                btnadd.Enabled = false;
                btnupdate.Enabled = true;
                user_id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                txtid.Text = $"{user_id: 0000}";
                txtName.Text = listView1.SelectedItems[0].SubItems[1].Text;
                txtUsername.Text = listView1.SelectedItems[0].SubItems[2].Text;
                cmbRole.SelectedItem = listView1.SelectedItems[0].SubItems[3].Text;
                cmbStatus.SelectedItem = listView1.SelectedItems[0].SubItems[4].Text;
            }
            else
            {
                btnadd.Enabled = false;
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You are about to update the selected user. Proceed?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                sql.Query($"update user_tb set name = '{txtName.Text}', username = '{txtUsername.Text}', role = '{cmbRole.SelectedItem.ToString()}', status = '{cmbStatus.SelectedItem.ToString()}' where user_id = {user_id} ");
                if (sql.HasException(true)) return;
                load_user();
                reset();
                MessageBox.Show(this, "User updated successfully!", "Users", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You are about to permanently removed this user. Proceed?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                sql.Query($"delete from user_tb where user_id = {user_id}");
                if (sql.HasException(true)) return;
                load_user();
                reset();
                MessageBox.Show(this, "User removed successfully!", "Users", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
