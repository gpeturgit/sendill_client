using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data;
using System.Data.OleDb;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Data.SqlClient;
using Dapper;

namespace sendill_client
{
    public class SQLManager
    {
        OleDbDataAdapter odbAdapter;
        OleDbCommand objComm;
        string strConnection, strSQL, strAppConfig;

        public OleDbConnection CreateLocalDataConnection()
        {
            ConfigFile cf = new ConfigFile();
            OleDbConnection objConn = new OleDbConnection();
            strConnection = cf.GetDataConnection("Jet");
            objConn.ConnectionString = strConnection;
            return objConn;
        }

        public SqlConnection CreateSqlDapperConnection()
        {
            //string sqlconnectionstring = "Data Source=NOTANDI-PC\\SQLEXPRESS;Initial Catalog=Sendill;Integrated Security=True";
            string sqlconnectionstring = "Data Source=ONICE\\SQLEXPRESS;Initial Catalog=Sendill;Integrated Security=True";
            SqlConnection connsql = new SqlConnection(sqlconnectionstring);
            return connsql;
        }

        public int CommandSqlDapperSyncTour()
        {
            try
            {

                using (IDbConnection db = CreateSqlDapperConnection())
                {
                    int _reccount = 0;
                    DBManager dm = new DBManager();
                    var _tours = dm.GetAllToursFromFile();
                    foreach (var _tour in _tours)
                    {
                        db.Open();
                        //string sql = @"INSERT INTO `ADMINRRMessage` (`service_id`, `message`, `status`, `reqtime`, `resptime`) VALUES (@service_id, @message, @status, @reqtime, @resptime); SELECT CAST(SCOPE_IDENTITY() as int)";
                        //string sql = @"INSERT INTO `ADMINRRMessage` (`service_id`, `message`, `status`, `reqtime`, `resptime`) VALUES (@service_id, @message, @status, @reqtime, @resptime)";
                        string sql = @"INSERT INTO `dtoTour` (`idcustomer`, `idcar`, `idpin`, `tdatetime`, `time`, `taddress`, `tcontact`, `tphone`, `tnote`, `isdel`, `carsize`, `car1`, `car2`, `car3`, `car4`, `car5`) ";
                        sql = sql + "VALUES (@idcustomer,@idcar,@idpin,@tdatetime,@time,@tcustomer";
                        sql = sql + ",@taddress,@tcontact,@tphone,@tnote,@isdel,@carsize,@car1,@car,@car3,@car4,@car5)";


                        //db.Query(sql, _tour).SingleOrDefault();
                        db.Close();
                        _reccount++;
                    }
                    return _reccount;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Villa " + ex.ToString());
                return 0;
            }
        }



        
        public OleDbConnection CreateExcelCustomersLoadConnection()
        {
            string filePath = @"C:\Sendill\TOK\tbl_customeralla.xlsx";

            String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", filePath);
            //Create Connection to Excel work book 
            OleDbConnection objConn = new OleDbConnection(excelConnString);
            return objConn;
        }

        public IDbConnection CreateDapperConn()
        {
            strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\dbsendill\\dbsendill.mdb";
            IDbConnection _db = new OleDbConnection(strConnection);
            return _db;
        }

        

        public OleDbConnection CreateSmsServiceConnedtion()
        {
            OleDbConnection objConn = new OleDbConnection();
            //strConnection="Provider=SQLOLEDB;Data Source=DESKTOP-OKE8PD0\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=ozeki";
            //strConnection = "Provider=SQLOLEDB;Data Source=NOTANDI-PC\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=ozeki";
            //strConnection = "Provider=SQLOLEDB;Data Source=DESKTOP-SAPU2SG\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Sendill";
            objConn.ConnectionString = strConnection;
            return objConn;
        }

        public OleDbConnection CreateSendillServiceConnedtion()
        {
            OleDbConnection objConn = new OleDbConnection();
            strConnection = "Provider=SQLOLEDB;Data Source=NOTANDI-PC\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Sendill";
            //strConnection = "Provider=SQLOLEDB;Data Source=DESKTOP-OKE8PD0\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Sendill";
            //strConnection = "Provider=SQLOLEDB;Data Source=DESKTOP-SAPU2SG\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Sendill";
            //strConnection = "Provider=SQLOLEDB;Data Source=ONICE\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Sendill";
            //strConnection = "Provider=SQLOLEDB;Data Source=DESKTOP-PVGA44I;Integrated Security=SSPI;Initial Catalog=Sendill";

            objConn.ConnectionString = strConnection;
            return objConn;
        }

        public bool InportTokCustomers()
        {
            try
            {
                using (OleDbConnection excelconn = CreateExcelCustomersLoadConnection())
                {
                    string sclearsql = "delete from tbl_customer";
                    //string sqlconnectionstring = "Data Source=ONICE\\SQLEXPRESS;Initial Catalog=Sendill;Integrated Security=True";
                    string sqlconnectionstring = "Data Source=NOTANDI-PC\\SQLEXPRESS;Initial Catalog=Sendill;Integrated Security=True";
                    SqlConnection connsql = new SqlConnection(sqlconnectionstring);
                    SqlCommand comsql = new SqlCommand(sclearsql, connsql);
                    connsql.Open();
                    comsql.ExecuteNonQuery();
                    connsql.Close();
                    OleDbConnection sqlconn = CreateSendillServiceConnedtion();
                    string myexceldataquery = "select id,customer,address,phone from [Sheet11$]";
                    OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, excelconn);
                    excelconn.Open();
                    OleDbDataReader dr = oledbcmd.ExecuteReader();
                    SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlconnectionstring);
                    bulkcopy.DestinationTableName = "tbl_customer";
                    while (dr.Read())
                    {
                        bulkcopy.WriteToServer(dr);
                    }
                    dr.Close();
                    excelconn.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool SendSms(string strTo, string strMessage)
        {
            try
            {
                using (OleDbConnection smsconn = CreateSmsServiceConnedtion())
                {
                    smsconn.Open();
                    string sql = "INSERT INTO " +
                                 "ozekimessageout (receiver,msg,status) " +
                                 "VALUES " +
                                 "('" + strTo + "','" + strMessage + "','send')";
                    objComm = smsconn.CreateCommand();
                    objComm.CommandText = sql;
                    objComm.ExecuteNonQuery();
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Villa " + ex.ToString());
                return false;
            }

        }

        
        
        public bool CreateLogRec(string pstrLogMessage)
        {
            try
            {
                using (OleDbConnection conn = CreateSendillServiceConnedtion())
                {
                    string pdatetime = DateTime.Now.ToString();
                    conn.Open();
                    string sql = "INSERT INTO " +
                                 "tbl_log (logtimestamp,logtext) " +
                                 "VALUES " +
                                 "('" + pdatetime + "','" + pstrLogMessage + "')";
                    objComm = conn.CreateCommand();
                    objComm.CommandText = sql;
                    objComm.ExecuteNonQuery();
                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CreateCustomerRec(string pstrId,string pstrCustomer,string pstrAddress,string pstrPhone)
        {
            try
            {
                using (OleDbConnection conn = CreateSendillServiceConnedtion())
                {
                    string pdatetime = DateTime.Now.ToString();
                    conn.Open();
                    string sql = "INSERT INTO " +
                                 "tbl_customer (id,customer,address,phone) " +
                                 "VALUES " +
                                 "('" + pstrId + "','" + pstrCustomer + "','" + pstrAddress + "','" + pstrPhone + "')";
                    objComm = conn.CreateCommand();
                    objComm.CommandText = sql;
                    objComm.ExecuteNonQuery();
                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public int LogRecCount()
        {
            try
            {
                using (OleDbConnection uconn = CreateLocalDataConnection())
                {
                    uconn.Open();
                    string strSum = "SELECT COUNT(ID) FROM tbl_log";
                    objComm = uconn.CreateCommand();
                    objComm.CommandText = strSum;
                    return (int)objComm.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Villa " + ex.ToString());
                return 0;
            }
        }



        public void LogRecCreate(string pstrLogMessage)
        {


            try
            {
                using (OleDbConnection conn = CreateSendillServiceConnedtion())
                {
                    string pdatetime = DateTime.Now.ToString();
                    conn.Open();
                    string sql = "INSERT INTO " +
                                 "tbl_log (logtimestamp,logtext) " +
                                 "VALUES " +
                                 "('" + pdatetime + "','" + pstrLogMessage + "')";
                    objComm = conn.CreateCommand();
                    objComm.CommandText = sql;
                    objComm.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {



            }

            //uconn.Open();
            //string strSum = "SELECT COUNT(ID) FROM sendill_log";
            //objComm = uconn.CreateCommand();
            //objComm.CommandText = strSum;
            //int decCount = (int)objComm.ExecuteScalar();
            //decCount = decCount + 1;
            //dsSendillTableAdapters.sendill_logTableAdapter sta = new dsSendillTableAdapters.sendill_logTableAdapter();
            //dsSendill ds = new dsSendill();
            //ds.Tables[0].AcceptChanges();
            //sta.Insert(decCount, DateTime.Now, pstrLogMessage);
            //}
            //dsCloudConnTableAdapters.tbl_logTableAdapter lta = new dsCloudConnTableAdapters.tbl_logTableAdapter();

            //lta.Insert(DateTime.Now, pstrLogMessage);
        }

        //public IEnumerable<dtoTour> SqlTourSelect(string pstrDateFrom, string pstrDateTo)
        //{
        //    using(OleDbConnection uconn = CreateLocalDataConnection())
        //        {
        //            uconn.Open();
        //            OleDbDataReader tour_reader = new OleDbDataReader();
        //            strSQL = "SELECT * FROM tbl_tours";
        //            objComm = uconn.CreateCommand();
        //            objComm.CommandText = strSQL;
        //            tour_reader=objComm.ExecuteReader();

        //            while (tour_reader.Read())
        //            {

        //            }
        //        }

        //}
        //List<Person> personList = new List<Person>();
        //personList = DataReaderMapToList<Person>(dataReaderForPerson)
        public List<CustomerModel> GetAllTours()
        {

            using (OleDbConnection uconn = CreateSendillServiceConnedtion())
            {
                uconn.Open();

                strSQL = "SELECT * FROM tbl_customer";
                objComm = uconn.CreateCommand();
                objComm.CommandText = strSQL;
                OleDbDataReader customer_reader = objComm.ExecuteReader();
                List<CustomerModel> list_cust = new List<CustomerModel>();
                list_cust = DataReaderMapToList<CustomerModel>(customer_reader);

                return list_cust;

            }
        }

        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public void createsqltable(DataTable dt, string tablename)
        {
            string strconnection = "";
            string table = "";
            table += "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tablename + "]') AND type in (N'U'))";
            table += "BEGIN ";
            table += "create table " + tablename + "";
            table += "(";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i != dt.Columns.Count - 1)
                    table += dt.Columns[i].ColumnName + " " + "varchar(max)" + ",";
                else
                    table += dt.Columns[i].ColumnName + " " + "varchar(max)";
            }
            table += ") ";
            table += "END";
            InsertQuery(table, strconnection);
            CopyData(strconnection, dt, tablename);
        }
        public void InsertQuery(string qry, string connection)
        {


            SqlConnection _connection = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = qry;
            cmd.Connection = _connection;
            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
        public static void CopyData(string connStr, DataTable dt, string tablename)
        {
            using (SqlBulkCopy bulkCopy =
            new SqlBulkCopy(connStr, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = tablename;
                bulkCopy.WriteToServer(dt);
            }
        }



        public string CreateTABLEPablo(string connectionString, string tableName, System.Data.DataTable table)
        {
            string sqlsc;
            //using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                connection.Open();
                sqlsc = "CREATE TABLE " + tableName + "(";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sqlsc += "\n" + table.Columns[i].ColumnName;
                    if (table.Columns[i].DataType.ToString().Contains("System.Int32"))
                        sqlsc += " int ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.DateTime"))
                        sqlsc += " datetime ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.String"))
                        sqlsc += " nvarchar(" + table.Columns[i].MaxLength.ToString() + ") ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.Single"))
                        sqlsc += " single ";
                    else if (table.Columns[i].DataType.ToString().Contains("System.Double"))
                        sqlsc += " double ";
                    else
                        sqlsc += " nvarchar(" + table.Columns[i].MaxLength.ToString() + ") ";



                    if (table.Columns[i].AutoIncrement)
                        sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                    if (!table.Columns[i].AllowDBNull)
                        sqlsc += " NOT NULL ";
                    sqlsc += ",";
                }

                string pks = "\nCONSTRAINT PK_" + tableName + " PRIMARY KEY (";
                for (int i = 0; i < table.PrimaryKey.Length; i++)
                {
                    pks += table.PrimaryKey[i].ColumnName + ",";
                }
                pks = pks.Substring(0, pks.Length - 1) + ")";

                sqlsc += pks;
                connection.Close();

            }
            return sqlsc + ")";
        }
    }
}
//    public CustomerRepository()
//    {
//        this.db = new NorthwindEntities();
//    }
 
//    public CustomerRepository(NorthwindEntities db)
//    {
//        this.db = db;
//    }
 
//    public IEnumerable<Customer> SelectAll()
//    {
//        return db.Customers.ToList();
//    }
 
//    public Customer SelectByID(string id)
//    {
//        return db.Customers.Find(id);
//    }
 
//    public void Insert(Customer obj)
//    {
//        db.Customers.Add(obj);
//    }
 
//    public void Update(Customer obj)
//    {
//        db.Entry(obj).State = EntityState.Modified;
//    }
 
//    public void Delete(string id)
//    {
//        Customer existing = db.Customers.Find(id);
//        db.Customers.Remove(existing);
//    }
 
//    public void Save()
//    {
//        db.SaveChanges();
//    }
//}

//the connection string is : @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;"""
//And the DataSource is the path of the excel file
//public List<string> GetDataFromExcel()
//{
//    List<string> lst = new List<string>();
 
//    OleDbConnection conn = new OleDbConnection(ConnectionString);
 
//    string sql = "SELECT ID,City,State FROM [Cities$]";
 
//    OleDbCommand command = new OleDbCommand(sql, conn);
 
//    conn.Open();
 
//    using (OleDbDataReader reader = command.ExecuteReader())
//    {
//        while (reader.Read())
//        {
//            //Add Data to List<T>
//            lst.Add(reader["City"].ToString());
//        }
//        reader.Close();
//    }
 
//    if (conn != null)
//    {
//        conn.Close();
//        conn = null;
//    }
 
//    return lst;
//}