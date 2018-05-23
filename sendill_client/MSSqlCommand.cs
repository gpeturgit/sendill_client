using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Diagnostics;

namespace sendill_client
{
    public class MSSqlCommand
    {
        //*******************************************************************
        //
        // UV1 - Loads all tours from fiili into tbl_tours in SqlServer.
        //
        //*******************************************************************
        
        
        
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
        //*******************************************************************
        //
        // UV1 - Insert tour to tbl_tours table in SqlServer.
        //
        //*******************************************************************
        
        public class InsertTour : ICommand
        {
            public dtoTour out_tour = new dtoTour();
            public TourModel in_tour = new TourModel();
            public void Execute(IDbConnection db)
            {
                in_tour.car1 = out_tour.car1;
                in_tour.car2 = out_tour.car2;
                in_tour.car3 = out_tour.car3;
                in_tour.car4 = out_tour.car4;
                in_tour.car5 = out_tour.car5;
                in_tour.carsize = out_tour.carsize;
                in_tour.idcar = out_tour.idcar;
                in_tour.idcustomer = out_tour.idcustomer;
                in_tour.idpin = out_tour.idpin;
                in_tour.isdel = out_tour.isdel;
                in_tour.taddress = out_tour.taddress;
                in_tour.tcontact = out_tour.tcontact;
                in_tour.tcustomer = out_tour.tcustomer;
                in_tour.tyear = DateTime.Now.Year;
                in_tour.tmonth = DateTime.Now.Month;
                in_tour.tday = DateTime.Now.Day;
                in_tour.thour = DateTime.Now.Hour;
                in_tour.tmin = DateTime.Now.Minute;
                in_tour.tnote = out_tour.tnote;
                in_tour.tphone = out_tour.tphone;
                var _res=db.Insert(in_tour);
            }
        }

        //*******************************************************************
        // UV1 - Update tour to tbl_tours table in SqlServer.
        //*******************************************************************

        public class UpdateTour : ICommand
        {
            public dtoTour out_tour = new dtoTour();
            public TourModel in_tour = new TourModel();
            
            public void Execute(IDbConnection db)
            {
                in_tour.id = out_tour.id;
                in_tour.car1 = out_tour.car1;
                in_tour.car2 = out_tour.car2;
                in_tour.car3 = out_tour.car3;
                in_tour.car4 = out_tour.car4;
                in_tour.car5 = out_tour.car5;
                in_tour.carsize = out_tour.carsize;
                in_tour.idcar = out_tour.idcar;
                in_tour.idcustomer = out_tour.idcustomer;
                in_tour.idpin = out_tour.idpin;
                in_tour.isdel = out_tour.isdel;
                in_tour.taddress = out_tour.taddress;
                in_tour.tcontact = out_tour.tcontact;
                in_tour.tcustomer = out_tour.tcustomer;
                in_tour.tyear = out_tour.tdatetime.Year;
                in_tour.tmonth = out_tour.tdatetime.Month;
                in_tour.tday = out_tour.tdatetime.Day;
                in_tour.thour = out_tour.tdatetime.Hour;
                in_tour.tmin = out_tour.tdatetime.Minute;
                in_tour.tnote = out_tour.tnote;
                in_tour.tphone = out_tour.tphone;
                var out_res=db.Update(in_tour);
            }
        }

        //*******************************************************************
        // UV1 - Get tour frp, tbl_tours table in SqlServer.
        //*******************************************************************

        public class GetSingleTour : ICommand
        {
            public dtoTour out_tour = new dtoTour();
            public int id_tour;

            public void Execute(IDbConnection db)
            {
                var p = new Dapper.DynamicParameters();
                p.Add("@id", id_tour);
                out_tour = db.Query<dtoTour>("spGetSingleDtoTourById", p, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }
    }
}
