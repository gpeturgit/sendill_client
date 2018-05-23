using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace sendill_client
{
    public interface IQuery<T>
    {
        T Execute(IDbConnection db);
    }
}
