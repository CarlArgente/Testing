using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.WindowsAPICodePack.Dialogs;
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
        Byte[] ImageByteArray;
        private void ucBooks_Load(object sender, EventArgs e)
        {
            load_items();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            load_items(textBox1.Text);
            pbLogo.Image = null;
            txtSelectedBook.Text = "";
        }
        public static int book_id;
        public static string title, author, desc, cat;
        public static string barcode_num, year, publisher;

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtSelectedBook.Text.Equals(""))
            {
                MessageBox.Show(this, "Please select book first", "Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Choosing Directory
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.InitialDirectory = "C:\\Users";
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    DialogResult result = MessageBox.Show("You are about to download the E-book file of the selected book. Proceed?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        sql.Query($"SELECT pdf_file FROM books_tb WHERE title= '{txtSelectedBook.Text}' ");
                        if (sql.HasException(true)) return;
                        if (sql.DBDT.Rows.Count > 0)
                        {
                            foreach (DataRow dr in sql.DBDT.Rows)
                            {
                                byte[] fileData = (byte[])dr[0];
                                String path = dialog.FileName + "\\" + txtSelectedBook.Text + ".pdf";
                                File.WriteAllBytes(path, fileData);
                                MessageBox.Show(this, "Successfully Downloaded E-Book File.", "Download E-Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

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
                txtSelectedBook.Text = listView1.SelectedItems[0].SubItems[2].Text;
                //
                sql.Query("SELECT preview_image FROM books_tb WHERE book_id='" + int.Parse(listView1.SelectedItems[0].SubItems[0].Text) + "'  ");
                if (sql.HasException(true)) return;
                if (sql.DBDT.Rows.Count > 0)
                {
                    foreach (DataRow dr in sql.DBDT.Rows)
                    {
                        if (!dr["preview_image"].Equals(DBNull.Value))
                        {
                            byte[] img = ((byte[])dr["preview_image"]);
                            ImageByteArray = img;
                            pbLogo.Image = Image.FromStream(new MemoryStream(img));
                        }
                        else
                        {
                            pbLogo.Image = null;
                        }
                    }
                }
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
