using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper.Contrib;

namespace sendill_client
{
    public interface ICommand
    {
        void Execute(IDbConnection db);
    }
}
