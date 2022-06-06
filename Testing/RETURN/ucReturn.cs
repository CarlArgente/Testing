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
using System.Globalization;

namespace Testing.RETURN
{
    public partial class ucReturn : UserControl
    {
        DataTable dt = new DataTable();
        dbconnect db = new dbconnect();
        SQLControl sql = new SQLControl();
        public ucReturn()
        {
            InitializeComponent();
        }

        private void ucReturn_Load(object sender, EventArgs e)
        {
            load_names();
            button1.Enabled = false;
        }
        public static string student_id;
        private void load_names(string id = "")
        {
            lstnames.Items.Clear();
            sql.Query($"select student_tb.student_id, student_tb.name, count(transac_tb.status) as 'status' from student_tb, transac_tb " +
            $"where student_tb.student_id = transac_tb.student_id and transac_tb.status like 'Ongoing' and student_tb.student_id like '%{id}%' group by student_tb.student_id, student_tb.name ");
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

        static int id, transac_id;
        
        private void lstnames_Click(object sender, EventArgs e)
        {
            if (lstnames.SelectedItems.Count > 0)
            {
                id = int.Parse(lstnames.SelectedItems[0].SubItems[0].Text);
                load_transaction(id, "Ongoing");
            }
            button1.Enabled = false;
        }
        
        private void load_transaction(int id, string status)
        {
            lsttransac.Items.Clear();
            sql.Query($"SELECT * FROM books_tb INNER JOIN transac_tb ON books_tb.book_id = transac_tb.book_id " +
                $"where student_id ='{id}' and status like '%{status}%'");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(dr["transac_id"]));
                    ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["title"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["author"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToDateTime(dr["date_issue"]).ToString("yyyy/MM/dd")),
                        new ListViewItem.ListViewSubItem(item, Convert.ToDateTime(dr["date_due"]).ToString("yyyy/MM/dd")),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["status"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["barcode_number"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["book_id"])),
                    };
                    item.SubItems.AddRange(subitems);
                    lsttransac.Items.Add(item);
                }
            }
        }
        int penaltyy;
        private int penalty()
        {

            sql.Query($"select penalty from misc_tb where penalty is NOT NULL");
            if(sql.DBDT.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return int.Parse(sql.ReturnResult($"select penalty from misc_tb where penalty is NOT NULL"));
            }
        }
        static string penaltyyy;
        string title, due;
        int aw;
        double HowManyDaysFromToday(DateTime appointment)
        {
            var today = DateTime.Today; //like DateTime.Now but with no time aspect
            var appDay = appointment.Date;
            return appDay.Subtract(today).TotalDays;
        }
        private void lsttransac_Click(object sender, EventArgs e)
        {
            if (lstnames.SelectedItems.Count > 0)
            {
                DateTime d1 = DateTime.Now.AddDays(-1);
                DateTime d2 = DateTime.Parse(lsttransac.SelectedItems[0].SubItems[4].Text);
                string d3;
                title = lsttransac.SelectedItems[0].SubItems[1].Text;
                due = lsttransac.SelectedItems[0].SubItems[4].Text;
                transac_id = int.Parse(lsttransac.SelectedItems[0].SubItems[0].Text);
                // d2 = DateTime.Parse(lsttransac.SelectedItems[0].SubItems[5].Text);
                
                d3 = String.Format("{0:t}", lsttransac.SelectedItems[0].SubItems[4].Text);
                TimeSpan t = d1 - d2;
                double daycount = Math.Round(t.TotalDays);

                penalty();               
                string a;

                penaltyyy = (Convert.ToInt32(daycount) * penaltyy).ToString();

                if (d1 == d2)
                {
                    aw = 0;
                }
                else
                {
                    if (!sql.ReturnResult($"SELECT penalty FROM transac_tb where transac_id = {lsttransac.SelectedItems[0].SubItems[0].Text}").Equals(""))
                    {
                        aw = (Convert.ToInt32(daycount) * int.Parse(sql.ReturnResult($"SELECT penalty FROM transac_tb where transac_id = {lsttransac.SelectedItems[0].SubItems[0].Text} ")));
                    }
                    else
                    {
                        aw = 0;
                    }
                }
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            load_names(txtsearch.Text);
            lsttransac.Items.Clear();
        }
        public static string condition;
        public static bool bmp;
        DateTime date = DateTime.Now;     
        private void button1_Click(object sender, EventArgs e)
        {       
            using (frmReturn_condition frm = new frmReturn_condition(title,aw,due))
            {
                frm.ShowDialog();
            }
            if (bmp == true)
            {
                DialogResult result = MessageBox.Show("Kindly double check the returned book condition, " + "\"" + condition + "\"", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    update_book(int.Parse(lsttransac.SelectedItems[0].SubItems[7].Text));
                    load_transaction(id, "Ongoing");
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            else if (bmp == false)
            {
                load_transaction(id, "Ongoing");
                button1.Enabled = false;
                return;
            }
        }

        private void update_book(int book_id)
        {
            sql.Query($"update books_tb set qty= qty + 1 where book_id = '{book_id}'");
            if (sql.HasException(true)) return;
            update_status(transac_id, int.Parse(penaltyyy));
            MessageBox.Show("Book " + lsttransac.SelectedItems[0].SubItems[6].Text + " has been successfully returned", "Books", MessageBoxButtons.OK, MessageBoxIcon.Information);
            load_names();
        }

        private void lsttransac_ColumnClick(object sender, ColumnClickEventArgs e)
        {

        }

        private void btnscan_Click(object sender, EventArgs e)
        {
            using (frmQR frm = new frmQR())
            {
                frm.ShowDialog();
            }
            if (RETURN.frmQR.scan_result == "Found")
            {
                load_names(student_id);
                load_transaction(int.Parse(student_id), "Ongoing");
            }
            else if (RETURN.frmQR.scan_result == "Not Found")
            {
                load_names();
            }

        }

        private void lsttransac_DragLeave(object sender, EventArgs e)
        {
            
        }

        private void lsttransac_Leave(object sender, EventArgs e)
        {
            //button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (frmcompleted frm = new frmcompleted())
            {
                frm.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lsttransac.Items.Clear();
            button1.Enabled = false;
            load_names();
        }

        private void txtpenalty_TextChanged(object sender, EventArgs e)
        {

        }

        private void update_status(int transac_id, int penalty)
        {
            string return_date = date.ToString("yyyy-MM-dd");
            sql.Query($"update transac_tb set transac_tb.status = 'Complete', penalty ='{penalty}', transac_tb.condition = '{condition}', date_returned = '{return_date}' where transac_id = '{transac_id}'");
            if (sql.HasException(true))return;

        }
    }
}
