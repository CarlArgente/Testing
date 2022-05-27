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
using System.Runtime.InteropServices;

namespace Testing
{
    class misc_class
    {
        dbconnect db = new dbconnect();
        DataTable dt = new DataTable();
        SQLControl sql = new SQLControl();
        public void add_category(string category, Form frm)
        {
            if (category.Equals(""))
            {
                MessageBox.Show(frm, "Please input category first!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "insert into misc_tb (category) values" +
                       $"('{category}')";
                sql.Query(query);
                if (sql.HasException(true)) return;
                MessageBox.Show(frm, "New category added successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void add_year(string year, Form frm)
        {
            if (year.Equals(""))
            {
                MessageBox.Show(frm, "Please input year first!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "insert into misc_tb (year) values" +
                          $"('{year}')";
                sql.Query(query);
                if (sql.HasException(true)) return;
                MessageBox.Show(frm, "Year added successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void add_condition(string condition, Form frm)
        {
            if (condition.Equals(""))
            {
                MessageBox.Show(frm, "Please input condition first!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "insert into misc_tb (misc_tb.condition) values" +
                           $"('{condition}')";
                sql.Query(query);
                if (sql.HasException(true)) return;
                MessageBox.Show(frm, "Condition added successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void add_section(string section, Form frm)
        {

            if (section.Equals(""))
            {
                MessageBox.Show(frm, "Please input section first!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = "insert into misc_tb (section) values" +
                            $"('{section}')";
                sql.Query(query);
                if (sql.HasException(true)) return;
                MessageBox.Show(frm, "Section added successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void cat_to_cmb(ComboBox cmb)
        {
            cmb.Items.Clear();
            sql.Query($"select misc_id, category from misc_tb where category IS NOT NULL");
            if (sql.HasException(true)) return;
            if(sql.DBDT.Rows.Count > 0)
            {
                foreach(DataRow dr in sql.DBDT.Rows)
                {
                    cmb.Items.Add(dr["category"].ToString());
                }
            }
        }

        public void year_to_cmb(ComboBox cmb)
        {
            cmb.Items.Clear();
            sql.Query($"select misc_id, year from misc_tb where year IS NOT NULL");
            if (sql.HasException(true)) return;
            if (sql.DBDT.Rows.Count > 0)
            {
                foreach (DataRow dr in sql.DBDT.Rows)
                {
                    cmb.Items.Add(dr["year"].ToString());
                }
            }
        }

        public void section_to_cmb(ComboBox cmb)
        {
            cmb.Items.Clear();
            sql.Query($"select misc_id, section from misc_tb where section IS NOT NULL");
            if (sql.HasException(true)) return;
            if (sql.DBDT.Rows.Count > 0)
            {
                foreach (DataRow dr in sql.DBDT.Rows)
                {
                    cmb.Items.Add(dr["section"].ToString());
                }
            }
        }
        public void condition_to_cmb(ComboBox cmb)
        {
            cmb.Items.Clear();
            sql.Query($"select misc_id, misc_tb.condition from misc_tb where misc_tb.condition IS NOT NULL");
            if (sql.HasException(true)) return;
            if (sql.DBDT.Rows.Count > 0)
            {
                foreach (DataRow dr in sql.DBDT.Rows)
                {
                    cmb.Items.Add(dr["condition"].ToString());
                }
            }
        }

    }
}
