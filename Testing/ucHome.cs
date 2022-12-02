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
    public partial class ucHome : UserControl
    {
        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();
        SQLControl sql = new SQLControl();
        public ucHome()
        {
            InitializeComponent();
        }

        private void ucHome_Load(object sender, EventArgs e)
        {
            student_count();
            tx_count();
            book_count();
            return_lst();

            borrow_list();
        }
        
        private void borrow_list()
        {
            sql.Query(@"SELECT TOP 7 books_tb.book_id, sum(books_tb.bcount) as totalqty, books_tb.barcode_number, title from books_tb INNER JOIN transac_tb ON books_tb.book_id = transac_tb.book_id
            where MONTH(transac_tb.date_issue) = MONTH(getDate())
            group by books_tb.book_id, books_tb.bcount, books_tb.barcode_number, books_tb.title order by sum(books_tb.bcount) desc ");
            if (sql.HasException(true)) return;
            foreach(DataRow dr in sql.DBDT.Rows)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(dr["book_id"]));
                ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["title"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["totalqty"])),
                };
                item.SubItems.AddRange(subitems);
                lstborrow.Items.Add(item);
            }
        }
        private void return_lst()
        {
            sql.Query(@"select top 7 books_tb.title as title , student_tb.student_id as ID from transac_tb t inner join books_tb on t.book_id = books_tb.book_id inner join student_tb 
            on t.student_id = student_tb.student_id where t.status = 'Complete' ");
            if (sql.HasException(true)) return;
            foreach(DataRow dr in sql.DBDT.Rows)
            {
                ListViewItem item = new ListViewItem(Convert.ToString(dr["title"]));
                ListViewItem.ListViewSubItem[] subitems = new ListViewItem.ListViewSubItem[]
                {
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["title"])),
                        new ListViewItem.ListViewSubItem(item, Convert.ToString(dr["ID"])),
                };
                item.SubItems.AddRange(subitems);
                lstreturn.Items.Add(item);
            }
        }
        private void student_count()
        {
            int a = 0;
            int count = int.Parse(sql.ReturnResult($"select count(student_id) from student_tb"));
            if (count == 0)
            {
                lblstudentcount.Text = $"{a:0000}";
            }
            else
            {
                lblstudentcount.Text = sql.ReturnResult($"select count(student_id) from student_tb");
            }
        }

        private void book_count()
        {
            int b = 0;
            int count = int.Parse(sql.ReturnResult($"select count(book_id) from books_tb"));
            if (count == 0)
            {
                lblbookcount.Text = $"{b:0000}";
            }
            else
            {
                lblbookcount.Text = sql.ReturnResult($"select count(book_id) from books_tb");
            }
        }
        private void tx_count()
        {
        
            int c = 0;
            int count = int.Parse(sql.ReturnResult($"select count(transac_id) from transac_tb where MONTH(date_issue) = MONTH(getDate()) "));
            if (count == 0)
            {
                lbltxcount.Text = $"{c:0000}";
            }
            else
            {
                lbltxcount.Text = sql.ReturnResult($"select count(transac_id) from transac_tb where MONTH(date_issue) = MONTH(getDate()) ");
            }

        }
    }
}
