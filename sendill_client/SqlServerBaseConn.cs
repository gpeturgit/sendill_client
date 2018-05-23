using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace sendill_client
{
    public class SqlServerBaseConn
    {
        public static string DbProperties
        {
            get { return @"Data Source=localhost;Initial Catalog=Sendill;Integrated Security=True"; }
            //get { return @"Data Source=srvsendill.database.windows.net; Initial Catalog=dbsendill; Integrated Security=False; User ID=dbadmin; Password=audkenni1912; Connect Timeout=60; Encrypt=False; TrustServerCertificate=Falsee"; }
            //Data Source=srvsendill.database.windows.net; Initial Catalog=dbsendill; Integrated Security=False; User ID=dbadmin; Password=audkenni1912; Connect Timeout=60; Encrypt=False; TrustServerCertificate=False;
            //get { return @"localhost;user id=root;password=dbaudkenni1912;persistsecurityinfo=True;database=sendill;allowuservariables=True"; }
        }

        public static SqlConnection SendillSqlServerConnection()
        {
            return new System.Data.SqlClient.SqlConnection(DbProperties);
        }
    }
}
