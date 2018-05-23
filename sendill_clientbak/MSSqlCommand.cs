using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper.Contrib.Extensions;

namespace sendill_client
{
    public class MSSqlCommand
    {
        public class LoadAllTours : ICommand
        {
            public void Execute(IDbConnection db)
            {

                DBManager dm = new DBManager();
                var ltour = dm.CreateTourModelListFile();
                //foreach(var _item in ltour)
                //{
                //    var res = db.Insert(_item);
                //}
                var res = db.Insert(ltour);
                

            }

        }
    }
}
