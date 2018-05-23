using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper;
using System.Data;

namespace sendill_client
{
    public class MSSqlQuery
    {
        public class GetAllTours:IQuery<List<TourModel>>
        {
            public List<TourModel> Execute(IDbConnection db)
            {
                return db.GetAll<TourModel>().ToList();
            }
        }
        public class GetDefaultTours : IQuery<List<dtoTour>>
        {
            
            
            public List<dtoTour> Execute(IDbConnection db)
            {
                
                var p = new Dapper.DynamicParameters();
                p.Add("@pyear", DateTime.Now.Year);
                //p.Add("@pmon", DateTime.Now.Month);
                p.Add("@pmon", 1);
                var res=db.Query<dtoTour>("spGetDtoTourByCurrWeek",p,commandType: CommandType.StoredProcedure);
                return res.ToList();
            }
        }
        public class GetDtoToursByCarIdYearMonth : IQuery<List<dtoTour>>
        {
            public int pCarId;
            public int pYear;
            public int pMonth;
            
            public List<dtoTour> Execute(IDbConnection db)
            {

                var p = new Dapper.DynamicParameters();
                p.Add("@pyear", pYear);
                p.Add("@pmonth", pMonth);
                p.Add("@pcarid", pCarId);
                var res = db.Query<dtoTour>("spGetDtoTourByCarIdYearMonth", p, commandType: CommandType.StoredProcedure);
                return res.ToList();
            }
        }
    }
}
//var user = cnn.Query<User>("spGetUser", new {Id = 1}, 
//        commandType: CommandType.StoredProcedure).SingleOrDefault();