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
            // Muna þetta er réttur tengistrenagur fyrir Sql-server bara eitt skástrik.
            get { return @"Data Source=NOTANDI-PC\SQLEXPRESS;Initial Catalog=Sendill;Integrated Security=True"; }
            //get { return @"localhost;user id=root;password=dbaudkenni1912;persistsecurityinfo=True;database=sendill;allowuservariables=True"; }
        }

        public static SqlConnection SendillSqlServerConnection()
        {
            return new System.Data.SqlClient.SqlConnection(DbProperties);
        }
    }
}
