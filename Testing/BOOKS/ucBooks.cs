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

namespace Testing.BOOKS
{
    public partial class ucBooks : UserControl
    {
        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();
        SQLControl sql = new SQLControl();
        public ucBooks()
        {
            InitializeComponent();
        }
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
                foreach(DataRow dr in sql.DBDT.Rows)
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

        private void ucBooks_Load(object sender, EventArgs e)
        {
            load_items();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            load_items(textBox1.Text);
        }
        public static int book_id;
        public static string title, author, desc, cat;
        public static string barcode_num, year, publisher;
        
        private void btnupdate_Click(object sender, EventArgs e)
        {
            using (BOOKS.ADD_BOOK frm = new BOOKS.ADD_BOOK("Update","","",3))
            {            
                frm.ShowDialog();
                load_items();
            }
        }

        private void btnarchive_Click(object sender, EventArgs e)
        {
            using (frmArchive frm = new frmArchive())
            {
                frm.ShowDialog();
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                book_id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                barcode_num = listView1.SelectedItems[0].SubItems[1].Text;
                title = listView1.SelectedItems[0].SubItems[2].Text;
                cat = listView1.SelectedItems[0].SubItems[8].Text;
                author = listView1.SelectedItems[0].SubItems[3].Text;
                year = listView1.SelectedItems[0].SubItems[7].Text;
                publisher = listView1.SelectedItems[0].SubItems[6].Text;
                btnupdate.Enabled = true;
            }
            else
            {
                btnupdate.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (BOOKS.BARCODE_RESULT frm = new BOOKS.BARCODE_RESULT())
            {
                frm.ShowDialog();
            }
            load_items();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (BOOKS.ADD_BOOK frm = new BOOKS.ADD_BOOK("Add","","",1))
            {
                frm.ShowDialog();
                load_items();
            }
        }

    }
}
