using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;
using System.Windows.Threading;
using System.Runtime.Caching;


namespace sendill_client
{
    [Serializable]
    public class DBManager
    {
        
        List<dtoTour> memListTour = new List<dtoTour>();
        List<dtoCars> memListCar = new List<dtoCars>();
        List<dtoCustomer> memListCustomer = new List<dtoCustomer>();
        List<dtoPinStatus> memListPinStatusLog = new List<dtoPinStatus>();
        //dbsendillEntities ent = new dbsendillEntities();

        public void LoadListFromMem()
        {
            using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListTour = (List<dtoTour>)bin.Deserialize(stream);
            }
            using (Stream stream = File.Open(@"C:\dbsendill\list_carall.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListCar = (List<dtoCars>)bin.Deserialize(stream);
            }

        }
        public bool UpdateCarsFromFile(List<dtoCars> lcars)
        {
            //using (var db = new dbsendillEntities())
            //{
            //    foreach (var icar in lcars)
            //    {

            //    }

            return true;
            //}
        }



       public List<dtoCars> GetAllCars()
        {
           if(memListCar.Count==0)
           {
               LoadListFromMem();
           }
           return memListCar;
        }

        public IEnumerable<dtoCustomer> GetAllCustomers()
       {
           using (Stream stream = File.Open(@"C:\dbsendill\list_customers.bin", FileMode.Open))
           {
               BinaryFormatter bin = new BinaryFormatter();
               memListCustomer = (List<dtoCustomer>)bin.Deserialize(stream);
               var parlist = from fl in memListCustomer
                             select fl;
               return parlist;
               
           }
       }

        // Get Tour functions.


        
        public IEnumerable<dtoTour> GetToursPar_CarId(int pid)
        {
            IEnumerable<dtoTour> _select;
            if (memListTour.Count == 0)
            {
                using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bin.Deserialize(stream);
                }
                _select = from fl in memListTour
                          where fl.idcar == pid
                          select fl;
                return _select;
            }
            else
            {
                _select = from fl in memListTour
                          where fl.idcar == pid
                          select fl;
                return _select;
            }
        }

        public IEnumerable<dtoTour> GetToursPar_CarId_Date(int pid, DateTime? dfrom, DateTime? dto)
        {
            IEnumerable<dtoTour> _select;
            if (memListTour.Count == 0)
            {
                using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bin.Deserialize(stream);
                }
                _select = from fl in memListTour
                              where fl.idcar == pid && fl.tdatetime >= dfrom && fl.tdatetime <= dto
                              select fl;
                return _select;
            }
            else
            {
                _select = from fl in memListTour
                          where fl.idcar == pid && fl.tdatetime >= dfrom && fl.tdatetime <= dto
                          select fl;

                return _select;
            }
        }

        public IEnumerable<dtoTour> GetTourPar_Date(DateTime? dfrom, DateTime? dto)
        {
            IEnumerable<dtoTour> _select;
            if (memListTour.Count == 0)
            {
                using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bin.Deserialize(stream);
                }
                _select = from fl in memListTour
                          where fl.tdatetime >= dfrom && fl.tdatetime <= dto
                          select fl;
                return _select;
            }
            else
            {
                _select = from fl in memListTour
                          where fl.tdatetime >= dfrom && fl.tdatetime <= dto
                          select fl;

                return _select;
            }
        }

        public IEnumerable<dtoTour> GetTourPar_Year_Month(int dyear, int dmonth)
        {
            using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListTour = (List<dtoTour>)bin.Deserialize(stream);
                var parlist = from fl in memListTour
                              where fl.tdatetime.Year == 2014 && fl.tdatetime.Month == 6
                              select fl;
                return parlist;
            }
        }


        public dtoCars GetSingleCarPar_Id(int pid)
        {
                dtoCars _rec;
                if (memListCar.Count == 0)
                {
                    using (Stream stream = File.Open(@"C:\dbsendill\list_cars.bin", FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        memListCar = (List<dtoCars>)bin.Deserialize(stream);
                    }
                    _rec=memListCar.FirstOrDefault(c => c.id == pid);
                    return _rec;
                }
                else
                {
                    _rec=memListCar.FirstOrDefault(c => c.id == pid);
                    return _rec;
                }
        }


        public IEnumerable<dtoTour> GetAllToursFromFile()
        {
            IEnumerable<dtoTour> _select;
            if (memListTour.Count == 0)
            {
                using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bin.Deserialize(stream);
                }
                _select = from fl in memListTour
                          select fl;

                return _select;

            }
            else
            {
                _select = from fl in memListTour
                          select fl;
                return _select;
            }
        }

        //public string LoadToursFromDatabaseToFile()
        //{
        //    TourRepository reptour = new TourRepository();
        //    var lt = GetAllToursFromFile();
        //    foreach(dtoTour ot in lt)
        //    {
        //        tbl_tours t = new tbl_tours();
        //        t.car1=ot.car1;
        //        t.car2 = ot.car2;
        //        t.car3 = ot.car3;
        //        t.car4 = ot.car4;
        //        t.car5 = ot.car5;
        //        t.carsize = ot.carsize;
        //        t.id_cars =(short?)ot.idcar;
        //        t.id_customers = (short?)ot.idcustomer;
        //        t.id_owner = t.id_owner;
        //        t.isdel = ot.isdel;
        //        t.taddress = ot.taddress;
        //        t.tcontact = ot.tcontact;
        //        t.tcustomer = ot.tcustomer;
        //        t.tdatetime = ot.tdatetime;
        //        t.tnote = ot.tnote;
        //        t.tphione = ot.tphone;
        //        reptour.Insert(t);
        //        reptour.Save();
        //    }
        //    return"Tour taflan uppfæarð.";

            
        //}

        public string CreateMemFileFromDatabase(int fileid)
        {
            

            
            //if (fileid == 1)
            //{


            //    List<dtoCars> listcar = new List<dtoCars>();


            //    var context = from ccar in ent.tbl_cars
            //                  select ccar;

            //    foreach (var oc in context)
            //    {
            //        dtoCars car = new dtoCars();
            //        car.id = oc.ID;
            //        car.isdel = false;
            //        car.kt = oc.kt;
            //        car.length = oc.length;
            //        car.liftsize = oc.lift_size;
            //        car.address = oc.address;
            //        car.backdoorheight = oc.back_door_height;
            //        car.backdoorlength = oc.back_door_width;
            //        car.car1 = oc.car1;
            //        car.car2 = oc.car2;
            //        car.car3 = oc.car3;
            //        car.car4 = oc.car4;
            //        car.car5 = oc.car5;
            //        car.car6 = oc.car6;
            //        car.car7 = oc.car7;
            //        car.car8 = oc.car8;
            //        car.car9 = oc.car9;
            //        car.car10 = oc.car10;
            //        car.carname = oc.carname;
            //        car.carnumber = oc.carnumber;
            //        car.code = oc.code;
            //        car.daddress = oc.daddress;
            //        car.dkt = oc.dkt;
            //        car.dmobile = oc.dmobile;
            //        car.dphone = oc.dphone;
            //        car.dpostcode = oc.dpostcode;
            //        car.driver = oc.driver;
            //        car.dtown = oc.dtown;
            //        car.heightofbox = oc.heightofbox;
            //        car.maxcarry = oc.max_carry;
            //        car.mobile = oc.mobile;
            //        car.model = oc.model;
            //        car.owner = oc.owner;
            //        car.phone = car.phone;
            //        car.postcode = oc.phone;
            //        car.sidedoorheight = oc.side_door_height;
            //        car.sidedoorlength = oc.side_door_width;
            //        car.stationid = oc.stationid;
            //        car.size = Convert.ToInt32(oc.size);
            //        car.town = oc.town;
            //        car.volume = oc.Volume;
            //        car.weightlimit = oc.weight_limit;
            //        car.width = oc.width;
            //        listcar.Add(car);
            //    }
            //    FileStream fs = new FileStream(@"C:\dbsendill\list_carall.bin", FileMode.Create);
            //    BinaryFormatter bf = new BinaryFormatter();
            //    bf.Serialize(fs, listcar);
            //    fs.Close();
            //    return "Bílalisti uppfærður "+DateTime.Now.ToShortDateString();
            //}
            //else
            //{
             return "Villa kom fram við uppfærslu bílalistans.";
            //}
        }

        public string SaveToursToFile(dtoTour dt)
        {
            File.Delete(@"C:\dbsendill\bak\list_tours.bin");
            File.Copy(@"C:\dbsendill\list_tours.bin", @"C:\dbsendill\bak\list_tours.bin");
            if (memListTour.Count == 0)
            {
                using (FileStream fs = new FileStream(@"C:\dbsendill\list_tours.bin", FileMode.Open))
                {

                    BinaryFormatter bf = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bf.Deserialize(fs);

                }
            }
            FileStream fst = new FileStream(@"C:\dbsendill\list_tours.bin", FileMode.Create);

                memListTour.Add(dt);
                BinaryFormatter bff = new BinaryFormatter();
                bff.Serialize(fst, memListTour);
                fst.Close();
            string rvalue = " Túr geymdur";
            return rvalue;

        }

        public string SavePinStatusLogToFile()
        {
            FileStream fst = new FileStream(@"C:\dbsendill\list_pinstatus_log.bin", FileMode.Create);

            BinaryFormatter bff = new BinaryFormatter();
            bff.Serialize(fst, memListPinStatusLog);
            fst.Close();
            string rvalue = " Loggur geymdur";
            return rvalue;
        }

    }

    }