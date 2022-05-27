using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using MySql.Data.MySqlClient;

namespace Testing.BORROW
{
    public partial class frmbook_SCAN : Form
    {
        SQLControl sql = new SQLControl();
        public frmbook_SCAN()
        {
            InitializeComponent();
        }
        FilterInfoCollection filinfo;
        VideoCaptureDevice captureDevice;
        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                BarcodeReader read = new BarcodeReader();
                Result result = read.Decode((Bitmap)pictureBox1.Image);
                if (result != null)
                {
                    timer1.Stop();
                    if (captureDevice.IsRunning)
                    {
                        captureDevice.Stop();
                    }
                    validate(result.ToString());
                }
                else if (result == null)
                {
                    timer1.Stop();
                    timer1.Start();
                }
            }
        }
        private void validate(string barcodenum)
        {
            sql.Query($"select * from books_tb where barcode_number='{barcodenum}'");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                populate(barcodenum);
                if (book_status == "OK")
                {
                    MessageBox.Show("Book Found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    STUDENTS.frmselect_book.status = 1;
                }
                else if (book_status == "Archived")
                {
                    MessageBox.Show("Book Archived!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    STUDENTS.frmselect_book.status = 2;
                    db.CloseConnection();
                    this.Close();
                    return;
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Book not Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public static string book_status;
        private void populate(string barcodenum)
        {
            sql.Query($"select * from books_tb where barcode_number='{barcodenum}'");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    STUDENTS.frmselect_book.title = (dr["title"].ToString());
                    STUDENTS.frmselect_book.author = (dr["author"].ToString());
                    STUDENTS.frmselect_book.barcode = (dr["barcode_number"].ToString());
                    STUDENTS.frmselect_book.book_id = int.Parse((dr["book_id"].ToString()));
                    STUDENTS.frmselect_book.available = int.Parse((dr["qty"].ToString()));
                    STUDENTS.frmselect_book.total = int.Parse((dr["total"].ToString()));
                    book_status = (dr["archive"].ToString());
                }
            }
        }
        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void frmbook_SCAN_Load(object sender, EventArgs e)
        {
            filinfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterinfo in filinfo)
            {
                comboBox1.Items.Add(filterinfo.Name);
                comboBox1.SelectedIndex = 0;
            }
        }

        private void btnSCAN_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filinfo[comboBox1.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();
            timer1.Start();
        }
    }
}
