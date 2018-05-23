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

        public class GetAllMessages:IQuery<List<dtoMessage>>
        {
            public List<dtoMessage> Execute(IDbConnection db)
            {
                return db.GetAll<dtoMessage>().ToList();
            }
        }


        public class GetAllTours:IQuery<List<TourModel>>
        {
            public List<TourModel> Execute(IDbConnection db)
            {
                return db.GetAll<TourModel>().ToList();
            }
        }
        //***************************************************************************************
        // VERSION 2.4
        // Gets current mont
        // 2.2 04.04.2017 - Change SP to spGetDtoTourByCunnMonth and parameters removed.
        //***************************************************************************************
        public class GetDetailMessageById : IQuery<dtoViewMessage>
        {
            public int p_message_id;
            public dtoViewMessage Execute(IDbConnection db)
            {
                var p = new Dapper.DynamicParameters();
                p.Add("@p_message_id", p_message_id);
                var res = db.Query<dtoViewMessage>("spGetMessageById",p , commandType: CommandType.StoredProcedure);
                return res.ToList().FirstOrDefault();
            }
        }

        //***************************************************************************************
        // VERSION 2.4
        // Gets current month of tours from tbl_tours(TourModel) and loads into dtoTour list.
        // 2.2 04.04.2017 - Change SP to spGetDtoTourByCunnMonth and parameters removed.
        //***************************************************************************************
        public class GetToursByCurrMonth : IQuery<List<dtoTour>>
        {
            public List<dtoTour> Execute(IDbConnection db)
            {
                var res = db.Query<dtoTour>("spGetDtoTourByCurrMonth", commandType: CommandType.StoredProcedure);
                return res.ToList();
            }
        }

        //***************************************************************************************
        // VERSION 2.2
        // Gets last month of tours from tbl_tours(TourModel) and loads into dtoTour list.
        // 2.2 04.04.2017 - Cteated. Used for Last Month button on winTorar form. 
        //***************************************************************************************
        public class GetToursByLastMonth : IQuery<List<dtoTour>>
        {
            public List<dtoTour> Execute(IDbConnection db)
            {
                var res = db.Query<dtoTour>("spGetDtoTourByLastMonth", commandType: CommandType.StoredProcedure);
                return res.ToList();
            }
        }
        //***************************************************************************************
        // DBManager - GetTourListFromDB()
        // Gets all tours from tbl_tours(TourModel) and loads into dtoTour list
        //***************************************************************************************
        public class GetDefaultTours : IQuery<List<dtoTour>>
        {
            public List<dtoTour> Execute(IDbConnection db)
            {
                
                var p = new Dapper.DynamicParameters();
                p.Add("@pyear", DateTime.Now.Year);
                p.Add("@pmon", DateTime.Now.Month);
                //p.Add("@pmon", 1);
                var res=db.Query<dtoTour>("spGetDtoTourByCurrWeek",p,commandType: CommandType.StoredProcedure);
                return res.ToList();
            }
        }

        public class GetAllDtoTours : IQuery<List<dtoTour>>
        {
            public List<dtoTour> Execute(IDbConnection db)
            {
                var res = db.Query<dtoTour>("spGetAllDtoTour",commandType: CommandType.StoredProcedure);
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

        public class GetDtoToursByCarId : IQuery<List<dtoTour>>
        {
            public int pCarId;

            public List<dtoTour> Execute(IDbConnection db)
            {
                var p = new Dapper.DynamicParameters();
                p.Add("@pcarid", pCarId);
                var res = db.Query<dtoTour>("spGetDtoTourById", p, commandType: CommandType.StoredProcedure);
                return res.ToList();
            }
        }
    }
}
//var user = cnn.Query<User>("spGetUser", new {Id = 1}, 
//        commandType: CommandType.StoredProcedure).SingleOrDefault();