﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing
{
    public class SQLControl
    {
        //public static string constring = @"Server=tcp:librarysystem.database.windows.net,1433;Initial Catalog=db_library;Persist Security Info=False;User ID=sqlserver;
        //Password=library123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static string constring = @"Server=.;Database=dbLibrary;User ID=sa;Password=123123";

        public SqlConnection DBCon = new SqlConnection(constring);
        private SqlCommand DBCmd;
        // DB DATA
        public SqlDataAdapter DBDA = new SqlDataAdapter();
        public DataTable DBDT;
        // QUERY PARAMETERS
        public List<SqlParameter> Params = new List<SqlParameter>();
        // QUERY STATISTICS
        public int RecordCount;
        public string Exception;

        public SQLControl()
        {

        }

        // ALLOW CONNECTION STRING OVERRIDE
        public SQLControl(string ConnectionString)
        {
            DBCon = new SqlConnection(ConnectionString);
        }

        //SQL CONNECTION CHECK
        public string CheckConnection()
        {
            string connectionStatus = "No database";

            try
            {
                DBCon.Open();
                if (DBCon.State == ConnectionState.Open)
                {
                    MessageBox.Show("connected");
                    connectionStatus = "success";
                    DBCon.Close();
                }
                else
                {
                    MessageBox.Show("failed");

                    connectionStatus = "failed";
                }
            }
            catch (Exception) { }

            return connectionStatus;
        }


        // EXECUTE QUERY SUB
        public void Query(string Query, bool ReturnIdentity = false)
        {
            // RESET QUERY STATS
            RecordCount = 0;
            Exception = "";

            try
            {
                DBCon.Open();

                // CREATE DB COMMAND
                DBCmd = new SqlCommand(Query, DBCon);

                // LOAD PARAMS INTO DB COMMAND
                Params.ForEach(p => DBCmd.Parameters.Add(p));

                // CLEAR PARAM LIST
                Params.Clear();

                // EXECUTE COMMAND & FILL DATASET
                DBDT = new DataTable();
                DBDA = new SqlDataAdapter(DBCmd);
                RecordCount = DBDA.Fill(DBDT);

                if (ReturnIdentity == true)
                {
                    string ReturnQuery = "SELECT IDENT_CURRENT('tbl_temp_modifiers') As LastID;";
                    // @@IDENTITY - SESSION
                    // SCOPE_IDENTITY() - SESSION & SCOPE
                    // IDENT_CURRENT(tablename) - LAST IDENT IN TABLE, ANY SCOPE, ANY SESSION
                    DBCmd = new SqlCommand(ReturnQuery, DBCon);
                    DBDT = new DataTable();
                    DBDA = new SqlDataAdapter(DBCmd);
                    RecordCount = DBDA.Fill(DBDT);
                }
            }
            catch (System.Exception ex)
            {
                // CAPTURE ERROR
                Exception = "ExecQuery Error: \n" + ex.Message;
            }
            finally
            {
                // CLOSE CONNECTION
                if (DBCon.State == ConnectionState.Open)
                    DBCon.Close();
            }
        }

        public string ReturnResult(string Query)
        {
            // RESET QUERY STATS
            RecordCount = 0;
            Exception = "";
            string r1 = "";
            try
            {
                DBCon.Open();

                // CREATE DB COMMAND
                DBCmd = new SqlCommand(Query, DBCon);

                // LOAD PARAMS INTO DB COMMAND
                Params.ForEach(p => DBCmd.Parameters.Add(p));

                // CLEAR PARAM LIST
                Params.Clear();

                // EXECUTE COMMAND & FILL DATASET
                DBDT = new DataTable();
                DBDA = new SqlDataAdapter(DBCmd);
                r1 = DBCmd.ExecuteScalar().ToString();
                RecordCount = DBDA.Fill(DBDT);
                return r1;
            }
            catch (System.Exception ex)
            {
                // CAPTURE ERROR
                Exception = "ExecQuery Error: \n" + ex.Message;
                return "";
            }
            finally
            {
                // CLOSE CONNECTION
                if (DBCon.State == ConnectionState.Open)
                    DBCon.Close();
            }
        }


        // ADD PARAMS
        public void AddParam(string Name, object Value)
        {
            SqlParameter NewParam = new SqlParameter(Name, Value);
            Params.Add(NewParam);
        }

        public bool HasException(bool Report = false)
        {
            if (string.IsNullOrEmpty(Exception))
            {
                return false;
            }

            if (Report == true)
            {
                MessageBox.Show(Exception, "SQL Control Error");
                return true;
            }
            else
                return false;
        }
    }
}
