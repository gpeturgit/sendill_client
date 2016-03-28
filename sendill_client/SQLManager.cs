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
//using DapperExtensions;

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

        public IDbConnection CreateDapperConn()
        {
            strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\dbsendill\\dbsendill.mdb";
            IDbConnection _db = new OleDbConnection(strConnection);
            return _db;
        }

        public OleDbConnection CreateSmsServiceConnedtion()
        {
            OleDbConnection objConn= new OleDbConnection();
            //strConnection="Provider=SQLOLEDB;Data Source=DESKTOP-OKE8PD0\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=ozeki";
            //strConnection = "Provider=SQLOLEDB;Data Source=NOTANDI-PC\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=ozeki";
            strConnection = "Provider=SQLOLEDB;Data Source=DESKTOP-SAPU2SG\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Sendill";
            objConn.ConnectionString = strConnection;
            return objConn;
        }

        public OleDbConnection CreateSendillServiceConnedtion()
        {
            OleDbConnection objConn = new OleDbConnection();
            //strConnection = "Provider=SQLOLEDB;Data Source=NOTANDI-PC\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Sendill";
            //strConnection = "Provider=SQLOLEDB;Data Source=DESKTOP-OKE8PD0\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Sendill";
            strConnection = "Provider=SQLOLEDB;Data Source=DESKTOP-SAPU2SG\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Sendill";
            objConn.ConnectionString = strConnection;
            return objConn;
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

        
public static List<T> DataReaderMapToList<T>(IDataReader dr)
{
    List<T> list = new List<T>();
    T obj = default(T);
    while (dr.Read()) {
        obj = Activator.CreateInstance<T>();
        foreach (PropertyInfo prop in obj.GetType().GetProperties()) {
            if (!object.Equals(dr[prop.Name], DBNull.Value)) {
                prop.SetValue(obj, dr[prop.Name], null);
            }
        }
        list.Add(obj);
    }
    return list;
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

//        cmd.CommandText = "INSERT INTO Accountstbl (Username, [Password]) VALUES (@Username, @Password)";
//cmd.Parameters.AddWithValue("@Username", textBox1.Text);
//cmd.Parameters.AddWithValue("@Password", textBox2.Text);
//cmd.Connection = con;
//con.Open();
//cmd.ExecuteNonQuery();


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