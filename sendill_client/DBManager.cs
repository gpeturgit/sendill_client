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
using System.Data.Services.Client;
using sendill_client.srvRefSendillEnt;
using System.Windows.Forms;


namespace sendill_client
{
    [Serializable]
    public class DBManager
    {
        public List<dtoPin>[] arrMemListPin;
        List<dtoPin> memListPin = new List<dtoPin>();
        List<dtoTour> memListTour = new List<dtoTour>();
        List<dtoCars> memListCar = new List<dtoCars>();
        List<dtoCustomer> memListCustomer = new List<dtoCustomer>();
        List<dtoPinStatus> memListPinStatusLog = new List<dtoPinStatus>();
        List<dtoPinStatus> memListPinStatus = new List<dtoPinStatus>();
        private DataServiceCollection<tbl_log> trackedTblLog;
        public const string svcUri = "http//localhost:59387/WCFOData.svc";
        public string sappconfig;

        public string GetAppConfigSetting()
        {
            ConfigFile conf = new ConfigFile();
            string ssetting = conf.GetLocalBinFolder() + "\\DataFiles\\";
            return ssetting;
        }

        public string GetAppDataBackupConfigSetting()
        {
            ConfigFile conf = new ConfigFile();
            string ssetting = conf.GetLocalBinFolder() + "\\DataFiles\\bak\\";
            return ssetting;

        }
        
        public void LoadListFromMem()
        {
            using (Stream stream = File.Open(GetAppConfigSetting()+"list_tours.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListTour = (List<dtoTour>)bin.Deserialize(stream);
            }
            using (Stream stream = File.Open(GetAppConfigSetting()+"list_carall.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListCar = (List<dtoCars>)bin.Deserialize(stream);
            }
            using (Stream stream = File.Open(GetAppConfigSetting() + "list_customers.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListCustomer = (List<dtoCustomer>)bin.Deserialize(stream);
            }

        }

        public List<dtoPin> LoadPin1FromFile()
        {
            string dfile = GetAppConfigSetting() + "list_pin1.bin";
            using (Stream stream = File.Open(dfile, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListPin = (List<dtoPin>)bin.Deserialize(stream);
            }
            return memListPin;
        }

        public List<dtoPin> LoadPin2FromFile()
        {
            string dfile = GetAppConfigSetting() + "list_pin2.bin";
            using (Stream stream = File.Open(dfile, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListPin = (List<dtoPin>)bin.Deserialize(stream);
            }
            return memListPin;
        }

        public List<dtoPin> LoadPin3FromFile()
        {
            string dfile = GetAppConfigSetting() + "list_pin3.bin";
            using (Stream stream = File.Open(dfile, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListPin = (List<dtoPin>)bin.Deserialize(stream);
            }
            return memListPin;
        }

        public List<dtoPin> LoadPin4FromFile()
        {
            string dfile = GetAppConfigSetting() + "list_pin4.bin";
            using (Stream stream = File.Open(dfile, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListPin = (List<dtoPin>)bin.Deserialize(stream);
            }
            return memListPin;
        }

        public List<dtoPin> LoadPin5FromFile()
        {
            string dfile = GetAppConfigSetting() + "list_pin5.bin";
            using (Stream stream = File.Open(dfile, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListPin = (List<dtoPin>)bin.Deserialize(stream);
            }
            return memListPin;
        }

        public List<dtoPin> LoadPin6FromFile()
        {
            string dfile = GetAppConfigSetting() + "list_pin6.bin";
            using (Stream stream = File.Open(dfile, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListPin = (List<dtoPin>)bin.Deserialize(stream);
            }
            return memListPin;
        }




        public string SavePinStatusToFile()
        {
            using (FileStream fst = new FileStream(GetAppConfigSetting()+"list_pinstatus.bin", FileMode.Create))
            {
                BinaryFormatter bff = new BinaryFormatter();
                bff.Serialize(fst, memListPinStatus);
                fst.Close();
                string rvalue = " memListStatus uppfærður";
                return rvalue;
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

        public IEnumerable<dtoPinStatus> GetPinStatus()
       {
                   using (Stream stream = File.Open(GetAppConfigSetting()+"list_pinstatus.bin", FileMode.OpenOrCreate))
                   {
                       BinaryFormatter bin = new BinaryFormatter();
                       memListPinStatusLog = (List<dtoPinStatus>)bin.Deserialize(stream);
                   }
                   return memListPinStatusLog.ToList(); ;
       }
        
        
        
        
        
        public IEnumerable<dtoCustomer> GetAllCustomers()
       {
               if (memListCustomer.Count == 0)
               {
                   using (Stream stream = File.Open(GetAppConfigSetting()+"list_customers.bin", FileMode.Open))
                   {
                       BinaryFormatter bin = new BinaryFormatter();
                       memListCustomer = (List<dtoCustomer>)bin.Deserialize(stream);
                   }
                   return memListCustomer.ToList(); ;
               }
               else
               {
                   return memListCustomer.ToList();
               }
               
           }

        public dtoCustomer GetCustomerById(int id)
        {
            if (memListCustomer.Count==0)
            {
                   using (Stream stream = File.Open(GetAppConfigSetting()+"list_customers.bin", FileMode.Open))
                   {
                       BinaryFormatter bin = new BinaryFormatter();
                       memListCustomer = (List<dtoCustomer>)bin.Deserialize(stream);
                       
                   }
                   return memListCustomer.Find(p => p.id == id);
            }
            else
            {
                return memListCustomer.Find(p => p.id == id);
            }
        }

        // Get Tour functions.


        
        public IEnumerable<dtoTour> GetToursPar_CarId(int pid)
        {
            IEnumerable<dtoTour> _select;
            if (memListTour.Count == 0)
            {
                using (Stream stream = File.Open(GetAppConfigSetting()+"list_tours.bin", FileMode.Open))
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
                using (Stream stream = File.Open(GetAppConfigSetting()+"list_tours.bin", FileMode.Open))
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
                using (Stream stream = File.Open(GetAppConfigSetting()+"list_tours.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bin.Deserialize(stream);
                }
                if (dfrom == dto)
                {
                    _select = from fl in memListTour
                              where fl.tdatetime == dfrom && fl.tdatetime == dto
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
            else
            {
                if (dfrom == dto)
                {
                    _select = from fl in memListTour
                              where fl.tdatetime == dfrom && fl.tdatetime == dto
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
        }

        public IEnumerable<dtoTour> GetToursPar_CustId(int pCustId)
        {
            IEnumerable<dtoTour> _select;
            if (memListTour.Count == 0)
            {
                using (Stream stream = File.Open(GetAppConfigSetting() + "list_tours.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bin.Deserialize(stream);
                }
                _select = from fl in memListTour
                          where fl.idcustomer == pCustId
                          orderby fl.tdatetime descending
                          select fl;
                return _select;
            }
            else
            {
                _select = from fl in memListTour
                          where fl.idcustomer == pCustId
                          select fl;
                return _select;
            }
        }

        public IEnumerable<dtoTour> GetToursPar_CustId_Date(int pCustId, DateTime? dfrom, DateTime? dto)
        {
            IEnumerable<dtoTour> _select;
            if (memListTour.Count == 0)
            {
                using (Stream stream = File.Open(GetAppConfigSetting() + "list_tours.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bin.Deserialize(stream);
                }
                _select = from fl in memListTour
                          where fl.idcustomer == pCustId
                          select fl;
                return _select;
            }
            else
            {
                _select = from fl in memListTour
                          where fl.idcustomer == pCustId
                          select fl;
                return _select;
            }
        }
        
        public IEnumerable<dtoTour> GetTourPar_Year_Month(int dyear, int dmonth)
        {
            using (Stream stream = File.Open(GetAppConfigSetting()+"list_tours.bin", FileMode.Open))
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
                    using (Stream stream = File.Open(GetAppConfigSetting()+"list_carall.bin", FileMode.Open))
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
                using (Stream stream = File.Open(GetAppConfigSetting()+"list_tours.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bin.Deserialize(stream);
                }
                _select = from fl in memListTour
                          orderby fl.tdatetime descending
                          select fl;

                return _select;

            }
            else
            {
                _select = from fl in memListTour
                          orderby fl.tdatetime descending
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
               //List<dtooCars> c = new List<dtooCars>();

                //using (FileStream stream = new FileStream(@"C:\dbsendill\list_carall.bin", FileMode.Open))
                //{

                //    BinaryFormatter bf = new BinaryFormatter();
                //    memListCar = (List<dtoCars>)bf.Deserialize(stream);


                //    foreach (var oc in memListCar)
                //    {
                //        dtooCars car = new dtooCars();
                //        car.id = oc.id;
                //        car.isdel = oc.isdel;
                //        car.kt = oc.kt;
                //        car.length = oc.length;
                //        car.liftsize = Convert.ToDouble(oc.liftsize);
                //        car.address = oc.address;
                //        car.backdoorheight = oc.backdoorheight;
                //        car.backdoorlength = oc.backdoorlength;
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
                //        car.maxcarry = oc.maxcarry;
                //        car.mobile = oc.mobile;
                //        car.model = oc.model;
                //        car.owner = oc.owner;
                //        car.phone = car.phone;
                //        car.postcode = oc.phone;
                //        car.sidedoorheight = oc.sidedoorheight;
                //        car.sidedoorlength = oc.sidedoorlength;
                //        car.stationid = oc.stationid;
                //        car.size = oc.size;
                //        car.town = oc.town;
                //        car.volume = oc.width;
                //        car.weightlimit = oc.weightlimit;
                //        car.width = oc.width;
                //        c.Add(car);
                //    }

                //    FileStream fs = new FileStream(@"C:\dbsendill\newlist_carall.bin", FileMode.Create);
                //    BinaryFormatter bbf = new BinaryFormatter();
                //    bf.Serialize(fs, c);
                //    fs.Close();
                    return "Bílalisti uppfærður " + DateTime.Now.ToShortDateString();
            //}
        }
           

        public string SaveToursToFile(dtoTour dt)
        {
            string sTimeStamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string fname = "list_tours" + sTimeStamp + ".bin";
            File.Copy(GetAppConfigSetting() + "list_tours.bin", GetAppDataBackupConfigSetting() + fname);
            if (memListTour.Count == 0)
            {
                using (FileStream fs = new FileStream(GetAppConfigSetting() + "list_tours.bin", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    memListTour = (List<dtoTour>)bf.Deserialize(fs);
                }
            }
            FileStream fst = new FileStream(GetAppConfigSetting()+"list_tours.bin", FileMode.Create);
                memListTour.Add(dt);
                BinaryFormatter bff = new BinaryFormatter();
                bff.Serialize(fst, memListTour);
                fst.Close();
            string rvalue = " Túr geymdur";
            return rvalue;
        }

        public string SavePinStatusLogToFile()
        {
            FileStream fst = new FileStream(GetAppConfigSetting()+"list_pinstatus_log.bin", FileMode.Create);

            BinaryFormatter bff = new BinaryFormatter();
            bff.Serialize(fst, memListPinStatusLog);
            fst.Close();
            string rvalue = " Loggur geymdur";
            return rvalue;
        }



        public string SavePin1StatusToFile(List<dtoPin> memListPin)
        {
            using (FileStream fst = new FileStream(GetAppConfigSetting()+"list_pin1.bin", FileMode.Create))
            {

                if (memListPin.Count > 0)
                {
                    BinaryFormatter bff = new BinaryFormatter();
                    bff.Serialize(fst, memListPin);
                    fst.Close();
                    string rvalue = " memListPin1 uppfærður";
                    return rvalue;
                }
                return "Listi tómur";
            }
        }

        public string SavePin2StatusToFile(List<dtoPin> memListPin)
        {
            using (FileStream fst = new FileStream(GetAppConfigSetting()+"list_pin2.bin", FileMode.Create))
            {

                if (memListPin.Count > 0)
                {
                    BinaryFormatter bff = new BinaryFormatter();
                    bff.Serialize(fst, memListPin);
                    fst.Close();
                    string rvalue = " memListPin2 uppfærður";
                    return rvalue;
                }
                return "Listi tómur";
            }
        }

        public string ZinkLogTable()
        {
            string suri = @"http://localhost:59387/WCFOData.svc";
            Uri muri = new Uri(suri);
            var context = new sendill_client.srvRefSendillEnt.SendillEntities(muri);
            tbl_log newlog = new tbl_log();
            newlog.ID = 99999999;
            newlog.logtimestamp = DateTime.Now;
            newlog.logtext = "Þetta er texte frá service.";
            try
            {
                context.AddTotbl_log(newlog);
                DataServiceResponse responce = context.SaveChanges();
                return "log_tbl uppfærður.";
            }
            catch (DataServiceRequestException ex)
            {
                return "Villa kom upp við uppfærslu " + ex.ToString();
            }
        }

        public bool ReplicateCarsTable()
        {
            return true;
        }




    }

    }