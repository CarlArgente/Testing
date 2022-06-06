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
    public partial class ADD_STUDENT : Form
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
        DataTable dt = new DataTable();
        dbconnect db = new dbconnect();
        string gender, student_id, _method;
        public ADD_STUDENT(string method)
        {
            InitializeComponent();
            _method = method;
        }

        private void ADD_STUDENT_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            misc.section_to_cmb(cmbsection);
            misc.year_to_cmb(cmbyear);
            if(_method == "ADD")
            {
                btnAdd.Visible = true;
                btnUPDATE.Visible = false;
            }
            else if (_method == "UPDATE")
            {
                btnAdd.Visible = false;
                btnUPDATE.Visible = true;
                txtlname.Text = ucStudents.lname;
                txtfname.Text = ucStudents.fname;
                txtmname.Text = ucStudents.mname;
                txtage.Text = ucStudents.age.ToString();
                txtemail.Text = ucStudents.email;
                cmbsection.Text = ucStudents.section;
                cmbyear.Text = ucStudents.year;
                if(ucStudents.gender == "Male")
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else if (ucStudents.gender == "Female")
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
            }
        }
    
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblyear_Click(object sender, EventArgs e)
        {
            using (ADD_MISC frm = new ADD_MISC("year"))
            {
                frm.ShowDialog();
            }
            misc.year_to_cmb(cmbyear);
            
        }

        private void lblsection_Click(object sender, EventArgs e)
        {
            using (ADD_MISC frm = new ADD_MISC("section"))
            {
                frm.ShowDialog();
            }
            misc.section_to_cmb(cmbsection);
        }

        private void txtage_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtlname_TextChanged(object sender, EventArgs e)
        {

        }
        private void manufacturerOrSupplierTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void txtlname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                e.Handled = true;
            }
        }

        private void txtfname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                e.Handled = true;
            }
        }

        private void txtmname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                e.Handled = true;
            }
        }

        private void btnUPDATE_Click(object sender, EventArgs e)
        {
            string name = txtfname.Text + " " + txtmname.Text + ". " + txtlname.Text;
            sql.Query($"update student_tb set lname='{txtlname.Text}', fname = '{txtfname.Text}', mname='{txtmname.Text}', email='{txtemail.Text}', age = '{int.Parse(txtage.Text)}', year = '{cmbyear.Text}', section = '{cmbsection.Text}', gender='{gender}', name='{name}' where student_id = '{ucStudents.student_id}'");
            if (sql.HasException(true)) return;
            MessageBox.Show(this, "Student Information updated successfully!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtlname.Text == "" || txtfname.Text == "" || txtmname.Text == "" || txtemail.Text == "" || txtage.Text == "" || cmbsection.Text == "" || cmbyear.Text == "")
            {
                MessageBox.Show(this, "Please fill-up all fields to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string name = txtfname.Text + " " + txtmname.Text + ". " + txtlname.Text;

                sql.Query($"insert into student_tb (fname, mname, lname, name, year, section, gender, email, age) values" +
                        $"('{txtfname.Text}','{txtmname.Text + "."}','{txtlname.Text}', '{name}', '{cmbyear.Text}', '{cmbsection.Text}', '{gender}', '{txtemail.Text}', '{int.Parse(txtage.Text)}')");
                if (sql.HasException(true)) return;
                MessageBox.Show(this, "Student added successfully!", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
          
        }
    }
}
