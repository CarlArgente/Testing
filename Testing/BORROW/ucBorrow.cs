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
using Testing.RECEIPT;

namespace Testing.BORROW
{
    public partial class ucBorrow : UserControl
    {
        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();
        public ucBorrow()
        {
            InitializeComponent();
            dataGrid.Rows.Clear();
            sql.Query("DELETE FROM temp_borrow_tb");
            if (sql.HasException(true)) return;
        }

        private void btnsearchID_Click(object sender, EventArgs e)
        {
            txtstudentid.Visible = true;
            btnfind.Visible = true;
            txtstudentid.Focus();
            sql.Query("DELETE FROM temp_borrow_tb");
            if (sql.HasException(true)) return;
        }

        private void btnsearchQR_Click(object sender, EventArgs e)
        {
            sql.Query("DELETE FROM temp_borrow_tb");
            if (sql.HasException(true)) return;
            reset();
            txtstudentid.Visible = false;
            btnfind.Visible = false;
            using (frmQR frm = new frmQR())
            {
                frm.ShowDialog();
            }
            if (connection == "found")
            {
                txtname.Text = name;
                txtyearsec.Text = year + " - " + section;
                txtage.Text = age;
                btnselect.Visible = true;
            }
            else if (connection == "not found")
            {
                reset();
            }


        }

        private void btnfind_Click(object sender, EventArgs e)
        {
            if (txtstudentid.Text == "")
            {
                MessageBox.Show("Please Fill up all Fields", "Warning", MessageBoxButtons.OK);
                return;
            }
            find_student(txtstudentid.Text);
        }

        private void reset()
        {
            txtstudentid.Visible = false;
            btnfind.Visible = false;
            txtname.Text = "";
            txtage.Text = "";
            txtyearsec.Text = "";
            listView1.Items.Clear();
            listView1.Enabled = false;
            lblborrowedbooks.Enabled = false;
            btnissue.Enabled = false;
            btnselectbook.Enabled = false;
            btnselect.Visible = false;
            btnselect.Enabled = true;
        }
        public static string student_id, year, section, age, name, connection;
       
        private void btnselectbook_Click(object sender, EventArgs e)
        {
            
            using (Testing.STUDENTS.frmselect_book frm = new STUDENTS.frmselect_book())
            {
                frm.add_book += Frm_add_book;
                frm.ShowDialog();
                if (_book == null)
                {
                    return;
                }
                else
                {
                    method_1();
                }
            }
            //Load in datagrid
            dataGrid.Rows.Clear();
            sql.Query("SELECT * FROM temp_borrow_tb");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    dataGrid.Rows.Add(dr["qty"].ToString(), dr["book_title"].ToString());
                }
            }
            btnissue.Enabled = true;
        }
        private List<borrow_book> order = new List<borrow_book>();
        private void method_1()
        {
            foreach(borrow_book book in order)
            {
                if(_book.book_id == book.books.book_id)
                {
                    order.Remove(book);
                    break;
                }
            }
            order.Add(new borrow_book()
            {
                books = _book,
                qty = 1
            });
            load_list(order);

        }
        
        private void load_list(List<borrow_book> books)
        {
            listView1.Items.Clear();
            foreach (borrow_book i in books)
            {
                ListViewItem item = new ListViewItem(i.books.book_id.ToString());
                ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                {
                    new ListViewItem.ListViewSubItem(item, i.books.barcode_id),
                    new ListViewItem.ListViewSubItem(item, i.books.book_title),
                    new ListViewItem.ListViewSubItem(item, i.books.book_author),
                    new ListViewItem.ListViewSubItem(item, i.books.issue),
                    new ListViewItem.ListViewSubItem(item, i.books.due),
                };
                item.SubItems.AddRange(subitems);
                listView1.Items.Add(item);
            }

        }

        private books _book;
        private void Frm_add_book(books book)
        {
            _book = book;
        }

        private void btnissue_Click(object sender, EventArgs e)
        {
            PrintBorrowBook();

            issue();
            order.Clear();
            reset();
            sql.Query("DELETE FROM temp_borrow_tb");
            if (sql.HasException(true)) return;
        }
        SQLControl sql = new SQLControl();
        private void update_stocks()
        {
            foreach (borrow_book i in order)
            {
                sql.Query($"update books_tb set qty=qty - '{i.qty}', bcount =bcount + '{i.qty}' where book_id= '{i.books.book_id}'");
                if (sql.HasException(true)) return;
            }
        }

        private void txtstudentid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtstudentid_KeyPress(object sender, KeyPressEventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        int penalty;
        private void issue()
        {
            foreach(borrow_book book in order)
            {
                sql.Query($"insert into transac_tb (student_id, book_id, date_issue, date_due, barcode_number, status) values" +
                        $" ('{int.Parse(student_id)}', '{book.books.book_id}', '{book.books.issue}', '{book.books.due}', '{book.books.barcode_id}', '{"Ongoing"}')");
                if (sql.HasException(true)) return;
            }
            update_stocks();
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            int borrowCount = int.Parse(sql.ReturnResult($"SELECT COUNT(*) as total FROM books_tb INNER JOIN transac_tb ON books_tb.book_id = transac_tb.book_id " +
                $"where student_id = {txtstudentid.Text} and status like 'Ongoing' ")); 
            if (borrowCount < 5)
            {
                btnselectbook.Enabled = true;
                lblborrowedbooks.Enabled = true;
                listView1.Enabled = true;
                txtstudentid.Visible = false;
                btnfind.Visible = false;
                btnselect.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please return book first.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            reset();
        }
        private void PrintBorrowBook()
        {
            BorrowReceipt borrowReceipt = new BorrowReceipt();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Qty"));
            dt.Columns.Add(new DataColumn("Name"));
            DataRow dataRow;
            dataRow = dt.NewRow();
            foreach (DataGridViewRow dgv in dataGrid.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value);
            }

            borrowReceipt.SetDataSource(dt);

            borrowReceipt.SetParameterValue("Date_issued", DateTime.Now.ToString("yyyy-MM-dd") );
            borrowReceipt.SetParameterValue("Issued_by", User.name);
            borrowReceipt.SetParameterValue("Issued_to", txtname.Text);

            try
            {
                borrowReceipt.PrintOptions.NoPrinter = false;
                borrowReceipt.PrintOptions.PrinterName = "Microsoft Print to PDF";
                borrowReceipt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                borrowReceipt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;
                borrowReceipt.PrintToPrinter(1, false, 0, 0);
            }
            catch (Exception)
            {

            }
            finally
            {
                if (borrowReceipt.IsLoaded)
                {
                    borrowReceipt.Close();
                    //rpt.Dispose();
                }
            }
        }
        private void find_student(string id) 
        {
            reset();
            sql.Query($"select * from student_tb where student_id='{id}'");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    txtname.Text = (dr["name"].ToString());
                    txtage.Text = (dr["age"].ToString());
                    year = (dr["year"].ToString());
                    section = (dr["section"].ToString());
                    student_id = (dr["student_id"].ToString());
                    txtyearsec.Text = year + " - " + section;
                }
                btnselect.Visible = true;
            }
            else
            {
                MessageBox.Show("Student not found", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
