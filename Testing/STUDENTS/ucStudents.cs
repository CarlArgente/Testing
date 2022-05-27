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
    public partial class ucStudents : UserControl
    {
        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();
        SQLControl sql = new SQLControl();
        public ucStudents()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            using (ADD_STUDENT frm = new ADD_STUDENT("ADD"))
            {
                frm.ShowDialog();
                load_items();
                reset();
            }
        }

        private void load_items(string search = "")
        {
            listView1.Items.Clear();
            sql.Query($"select * from student_tb where student_id like '%{search}%' or lname like '%{search}%' or fname like '%{search}%' ");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(dr["student_id"]));
                    ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["name"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["year"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["section"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["email"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["gender"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["age"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["fname"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["mname"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["lname"]))
                    };
                    item.SubItems.AddRange(subitems);
                    listView1.Items.Add(item);
                }
            }
        }

        private void ucStudents_Load(object sender, EventArgs e)
        {
            load_items();
            btnupdate.Enabled = false;
        }
        public static int student_id, age;
        public static string name, year, section, email, gender, fname, mname, lname;
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                txtfname.Text = listView1.SelectedItems[0].SubItems[7].Text;
                txtmname.Text = listView1.SelectedItems[0].SubItems[8].Text;
                txtlname.Text = listView1.SelectedItems[0].SubItems[9].Text;
                txtage.Text = listView1.SelectedItems[0].SubItems[6].Text;
                btnadd.Enabled = false;
                btnupdate.Enabled = true;
                student_id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                age = int.Parse(listView1.SelectedItems[0].SubItems[6].Text);
                txtid.Text = $"{student_id: 0000}";
                name = listView1.SelectedItems[0].SubItems[1].Text;
                year = listView1.SelectedItems[0].SubItems[2].Text;
                section = listView1.SelectedItems[0].SubItems[3].Text;
                email = listView1.SelectedItems[0].SubItems[4].Text;
                gender = listView1.SelectedItems[0].SubItems[5].Text;
                fname = listView1.SelectedItems[0].SubItems[7].Text;
                mname = listView1.SelectedItems[0].SubItems[8].Text;
                lname = listView1.SelectedItems[0].SubItems[9].Text;
            }
            else
            {
                btnadd.Enabled = false;
            }
        }
        private void reset()
        {
            txtage.Text = "";
            txtfname.Text = "";
            txtlname.Text = "";
            txtmname.Text = "";
            txtsearch.Text = "";
            btnupdate.Enabled = false;
            btnadd.Enabled = true;
            txtid.Text = "XXXX-XXXX";
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            load_items(txtsearch.Text);
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            using (Testing.STUDENTS.ADD_STUDENT frm = new ADD_STUDENT("UPDATE"))
            {
                frm.ShowDialog();
                load_items();
                reset();
            }
        }
    }
}
