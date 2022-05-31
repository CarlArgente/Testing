using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Testing
{
    public partial class Main_Menu : Form
    {

        public static string user;
        private Button currentButton;
        SQLControl sql = new SQLControl();
        public Main_Menu()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        private void Main_Menu_Load(object sender, EventArgs e)
        {
            lblwelcome.Text = "Hi, " + user + " !";
            ActiveButton(btnhome);
            ucHome uc = new ucHome();
            change_menu(uc);
        }
        public void ActiveButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = Color.FromArgb(255, 255, 255);
                    Color color2 = Color.FromArgb(89, 191, 122);
                    panel2.BackColor = Color.FromArgb(255, 255, 255);
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = color2;
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));               
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panel1.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(89, 191, 122);
                    previousBtn.ForeColor = Color.FromArgb(255, 255, 255);
                    panel2.BackColor = Color.FromArgb(89, 191, 122);
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    
                }
            }
        }

        private void btnhome_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            ucHome uc = new ucHome();
            change_menu(uc);
            panel2.Top = btnhome.Top;
            panel2.Height = btnhome.Height;
        }
        private void change_menu(UserControl uc = null)
        {
            panelUC.Controls.Clear();
            panelUC.Controls.Add(uc);
        }

        private void btnbooks_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            Testing.BOOKS.ucBooks uc = new BOOKS.ucBooks();
            change_menu(uc);
            panel2.Top = btnbooks.Top;
            panel2.Height = btnbooks.Height;
        }

        private void btnbooks_Enter(object sender, EventArgs e)
        {
            btnbooks.Image = Properties.Resources.book_green;
        }

        private void btnbooks_Leave(object sender, EventArgs e)
        {
            btnbooks.Image = Properties.Resources.book_white;
        }

        private void btnhome_Enter(object sender, EventArgs e)
        {
            btnhome.Image = Properties.Resources.home_green;
        }

        private void btnhome_Leave(object sender, EventArgs e)
        {
            btnhome.Image = Properties.Resources.home_white;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            Testing.STUDENTS.ucStudents uc = new STUDENTS.ucStudents();
            change_menu(uc);
            panel2.Top = button1.Top;
            panel2.Height = button1.Height;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.student_green;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.student_white;
        }

        private void btnborrow_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            Testing.BORROW.ucBorrow uc = new BORROW.ucBorrow();
            change_menu(uc);
            panel2.Top = btnborrow.Top;
            panel2.Height = btnborrow.Height;
        }

        private void btnborrow_Enter(object sender, EventArgs e)
        {
            btnborrow.Image = Properties.Resources.borrow_green;
        }

        private void btnborrow_Leave(object sender, EventArgs e)
        {
            btnborrow.Image = Properties.Resources.borrow_white;
        }

        private void btnreturn_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            Testing.RETURN.ucReturn uc = new RETURN.ucReturn();
            change_menu(uc);
            panel2.Top = btnreturn.Top;
            panel2.Height = btnreturn.Height;
        }

        private void btnreturn_Leave(object sender, EventArgs e)
        {
            btnreturn.Image = Properties.Resources.return_white;
        }

        private void btnreturn_Enter(object sender, EventArgs e)
        {
            btnreturn.Image = Properties.Resources.return_green;
        }

        private void btnbooks_ParentChanged(object sender, EventArgs e)
        {
        }

        private void btnbooks_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnaboutus_Click(object sender, EventArgs e)
        {

        }

        private void btnabout_Click(object sender, EventArgs e)
        {
            using (frmaboutus frm = new frmaboutus())
            {
                frm.ShowDialog();
            }
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            if (sql.ReturnResult($"select role from user_tb where user_id = {User.user_id}" ).Equals("ADMIN"))
            {
                using (Testing.SETTINGS.frmSettings frm = new SETTINGS.frmSettings())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(this, "You have no permission on this module. Please contact your admin.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
