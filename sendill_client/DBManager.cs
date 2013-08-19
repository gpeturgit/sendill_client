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
        dbsendillEntities ent = new dbsendillEntities();

        public void LoadListFromMem()
        {
            using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListTour = (List<dtoTour>)bin.Deserialize(stream);
            }
            using (Stream stream = File.Open(@"C:\dbsendill\list_cars.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListCar = (List<dtoCars>)bin.Deserialize(stream);
            }

        }
        public bool UpdateCarsFromFile(List<dtoCars> lcars)
        {
            using (var db = new dbsendillEntities())
            {
                foreach (var icar in lcars)
                {

                }

                return true;
            }
        }

        public IEnumerable<dtoTour> GetToursPar_CarId(int pid)
        {
            using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListTour = (List<dtoTour>)bin.Deserialize(stream);
                var parlist = from fl in memListTour
                              where fl.idcar == pid
                              select fl;
                return parlist;
            }
        }



        public IEnumerable<dtoTour> GetToursPar_CarId_Date(int pid, DateTime dfrom, DateTime dto)
        {
            using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListTour = (List<dtoTour>)bin.Deserialize(stream);
                var parlist = from fl in memListTour
                              where fl.idcar == pid || fl.tdatetime >= dfrom || fl.tdatetime <= dto
                              select fl;
                return parlist;
            }
        }



        public dtoCars GetSingleCarPar_CarId(int pid)
        {
            using (Stream stream = File.Open(@"C:\dbsendill\list_cars.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListCar = (List<dtoCars>)bin.Deserialize(stream);
                var rcar = memListCar.FirstOrDefault(c => c.id == pid);
                return rcar;
            }

        }


        public List<dtoTour> GetAllToursFromFile()
        {
            using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                memListTour = (List<dtoTour>)bin.Deserialize(stream);
                var parlist = from fl in memListTour
                              select fl;
                return parlist.ToList();
            }
        }

        public string CreateMemFileFromDatabase(int fileid)
        {

            List<dtoCars> listcar = new List<dtoCars>();


            var context = from ccar in ent.tbl_cars
                          select ccar;

            foreach (var oc in context)
            {
                dtoCars car = new dtoCars();
                car.id = oc.ID;
                car.isdel = false;
                car.kt = oc.kt;
                car.length = oc.length;
                car.liftsize = oc.lift_size;
                car.address = oc.address;
                car.backdoorheight = oc.back_door_height;
                car.backdoorlength = oc.back_door_width;
                car.car1 = oc.car1;
                car.car2 = oc.car2;
                car.car3 = oc.car3;
                car.car4 = oc.car4;
                car.car5 = oc.car5;
                car.car6 = oc.car6;
                car.car7 = oc.car7;
                car.car8 = oc.car8;
                car.car9 = oc.car9;
                car.car10 = oc.car10;
                car.carname = oc.carname;
                car.carnumber = oc.carnumber;
                car.code = oc.code;
                car.daddress = oc.daddress;
                car.dkt = oc.dkt;
                car.dmobile = oc.dmobile;
                car.dphone = oc.dphone;
                car.dpostcode = oc.dpostcode;
                car.driver = oc.driver;
                car.dtown = oc.dtown;
                car.heightofbox = oc.heightofbox;
                car.maxcarry = oc.max_carry;
                car.mobile = oc.mobile;
                car.model = oc.model;
                car.owner = oc.owner;
                car.phone = car.phone;
                car.postcode = oc.phone;
                car.sidedoorheight = oc.side_door_height;
                car.sidedoorlength = oc.side_door_width;
                car.stationid = oc.stationid;
                car.size = 1;
                car.town = oc.town;
                car.volume = oc.Volume;
                car.weightlimit = oc.weight_limit;
                car.width = oc.width;
                listcar.Add(car);
            }
            FileStream fs = new FileStream(@"C:\dbsendill\carfile.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, listcar);
            fs.Close();
            return "Bílalisti uppfærður 21.05.2013-1230";
        }


    }

    }