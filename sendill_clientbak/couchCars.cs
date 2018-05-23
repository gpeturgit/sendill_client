using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Divan;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sendill_client
{
    public class couchCars
    {
        string host = "localhost";
        //string host = "sendilldev.iriscouch.com";
        int port = 5984;

        public class jcar : CouchDocument
        {
            public jcar()
            {
            }
            
            public int id;
            public int stationid;
            public string carnumber;
            public string code;
            public bool listed;
            public string carname;
            public bool car1;
            public bool car2;
            public bool car3;
            public bool car4;
            public bool car5;
            public bool car6;
            public bool car7;
            public bool car8;
            public bool car9;
            public bool car10;
            public double length;
            public double backdoorlength;
            public double backdoorheight;
            public double sidedoorlength;
            public double sidedoorheight;
            public double weightlimit;
            public Int16 liftsize;
            public double volume;
            public double width;
            public string model;
            public double maxcarry;
            public string owner;
            public string kt;
            public string address;
            public string town;
            public string postcode;
            public string phone;
            public string mobile;
            public string driver;
            public string dkt;
            public string daddress;
            public string dtown;
            public string dpostcode;
            public string dphone;
            public string dmobile;
            public double heightofbox;
            public bool isdel;
            public int size;

            public jcar(int _id,
                        int _stationid,
                        string _carnumber,
                        string _code,
                        bool _listed,
                        string _carname,
                        bool _car1,
                        bool _car2,
                        bool _car3,
                        bool _car4,
                        bool _car5,
                        bool _car6,
                        bool _car7,
                        bool _car8,
                        bool _car9,
                        bool _car10,
                        double _length,
                        double _backdoorlength,
                        double _backdoorheight,
                        double _sidedoorlength,
                        double _sidedoorheight,
                        double _weightlimit,
                        Int16 _liftsize,
                        double _volume,
                        double _width,
                        string _model,
                        double _maxcarry,
                        string _owner,
                        string _kt,
                        string _address,
                        string _town,
                        string _postcode,
                        string _phone,
                        string _mobile,
                        string _driver,
                        string _dkt,
                        string _daddress,
                        string _dtown,
                        string _dpostcode,
                        string _dphone,
                        string _dmobile,
                        double _heightofbox,
                        bool _isdel,
                        int _size)
            {
                id = _id ;
                stationid = _stationid;
                carnumber = _carnumber;
                code = _code;
                listed = _listed;
                carname=_carname;
                car1= _car1;
                car2= _car2;
                car3= _car3;
                car4= _car4;
                car5= _car5;
                car6= _car6;
                car7= _car7;
                car8= _car8;
                car9= _car9;
                car10= _car10;
                length=_length;
                backdoorlength=_backdoorlength;
                backdoorheight=_backdoorheight;
                sidedoorlength=_sidedoorlength;
                sidedoorheight=_sidedoorheight;
                weightlimit=_weightlimit;
                liftsize=_liftsize;
                volume=_volume;
                width= _width;
                model=_model;
                maxcarry=_maxcarry;
                owner=_owner;
                kt=_kt;
                address=_address;
                town=_town;
                postcode=_postcode;
                phone=_phone;
                mobile=_mobile;
                driver=_driver;
                dkt=_dkt;
                daddress=_daddress;
                dtown=_dtown;
                dpostcode=_dpostcode;
                dphone=_dphone;
                dmobile=_dmobile;
                heightofbox=_heightofbox;
                isdel=_isdel;
                size=_size;
            
            }
            public override void WriteJson(JsonWriter writer)
            {
                // This will write id and rev
                base.WriteJson(writer);

                writer.WritePropertyName("docType");
                writer.WriteValue("car");
                writer.WritePropertyName("id");
                writer.WriteValue(id);
                writer.WritePropertyName("stationid");
                writer.WriteValue(stationid);
                writer.WritePropertyName("carnumber");
                writer.WriteValue(carnumber);
                writer.WritePropertyName("code");
                writer.WriteValue(code);
                writer.WritePropertyName("listed");
                writer.WriteValue(listed);
                writer.WritePropertyName("carname");
                writer.WriteValue(carname);
                writer.WritePropertyName("car1");
                writer.WriteValue(car1);
                writer.WritePropertyName("car2");
                writer.WriteValue(car2);
                writer.WritePropertyName("car3");
                writer.WriteValue(car3);
                writer.WritePropertyName("car4");
                writer.WriteValue(car4);
                writer.WritePropertyName("car5");
                writer.WriteValue(car5);
                writer.WritePropertyName("car6");
                writer.WriteValue(car6);
                writer.WritePropertyName("car7");
                writer.WriteValue(car7);
                writer.WritePropertyName("car8");
                writer.WriteValue(car8);
                writer.WritePropertyName("car9");
                writer.WriteValue(car9);
                writer.WritePropertyName("car10");
                writer.WriteValue(car10);
                writer.WritePropertyName("length");
                writer.WriteValue(length);
                writer.WritePropertyName("backdoorlength");
                writer.WriteValue(backdoorlength);
                writer.WritePropertyName("backdoorheight");
                writer.WriteValue(backdoorheight);
                writer.WritePropertyName("sidedoorlength");
                writer.WriteValue(sidedoorlength);
                writer.WritePropertyName("sidedoorheight");
                writer.WriteValue(sidedoorheight);
                writer.WritePropertyName("weightlimit");
                writer.WriteValue(weightlimit);
                writer.WritePropertyName("liftsize");
                writer.WriteValue(liftsize);
                writer.WritePropertyName("volume");
                writer.WriteValue(volume);
                writer.WritePropertyName("width");
                writer.WriteValue(width);
                writer.WritePropertyName("model");
                writer.WriteValue(model);
                writer.WritePropertyName("maxcarry");
                writer.WriteValue(maxcarry);
                writer.WritePropertyName("owner");
                writer.WriteValue(owner);
                writer.WritePropertyName("kt");
                writer.WriteValue(kt);
                writer.WritePropertyName("address");
                writer.WriteValue(address);
                writer.WritePropertyName("town");
                writer.WriteValue(town);
                writer.WritePropertyName("postcode");
                writer.WriteValue(postcode);
                writer.WritePropertyName("phone");
                writer.WriteValue(phone);
                writer.WritePropertyName("mobile");
                writer.WriteValue(mobile);
                writer.WritePropertyName("driver");
                writer.WriteValue(driver);
                writer.WritePropertyName("dkt");
                writer.WriteValue(dkt);
                writer.WritePropertyName("daddress");
                writer.WriteValue(daddress);
                writer.WritePropertyName("dtown");
                writer.WriteValue(dtown);
                writer.WritePropertyName("dpostcode");
                writer.WriteValue(dpostcode);
                writer.WritePropertyName("dphone");
                writer.WriteValue(dphone);
                writer.WritePropertyName("dmobile");
                writer.WriteValue(dmobile);
                writer.WritePropertyName("heightofbox");
                writer.WriteValue(heightofbox);
                writer.WritePropertyName("isdel");
                writer.WriteValue(isdel);
                writer.WritePropertyName("size");
                writer.WriteValue(size);

            }

            public override void ReadJson(JObject obj)
            {
                // This will read id and rev
                base.ReadJson(obj);
                
                //if( obj["id"]==null)
                id = obj["id"].Value<int>();
                stationid = obj["stationid"].Value<int>();
                carnumber = obj["carnumber"].Value<string>(); ;
                code = obj["code"].Value<string>();
                listed = obj["listed"].Value<bool>();
                carname = obj["carname"].Value<string>();
                //if (obj["car1"].Value<bool>() == null)
                //{
                //    car1 = false;
                //}
                //else
                //{
                //    car1 = obj["car1"].Value<bool>();
                //}
                car1 = obj["car1"].Value<bool>();
                car2 = obj["car2"].Value<bool>();
                car3 = obj["car3"].Value<bool>();
                car4 = obj["car4"].Value<bool>();
                car5 = obj["car5"].Value<bool>();
                car6 = obj["car6"].Value<bool>();
                car7 = obj["car7"].Value<bool>();
                car8 = obj["car8"].Value<bool>();
                car9 = obj["car9"].Value<bool>();
                car10 = obj["car10"].Value<bool>();

                //length = obj["length"].Value<int>();
                if (obj["backdoorlength"] == null) { backdoorlength = 9999; } else { backdoorlength = obj["backdoorlength"].Value<double>(); }
                //backdoorlength = obj["backdoorlength"].Value<double>();
                if (obj["backdoorheight"] == null) { backdoorheight = 9999; } else { backdoorheight = obj["backdoorheight"].Value<double>(); }
                //backdoorheight = obj["backdoorheight"].Value<double>();
                if (obj["sidedoorlength"] == null) { sidedoorlength = 9999; } else { sidedoorlength = obj["sidedoorlength"].Value<double>(); }
                //sidedoorlength = obj["sidedoorlength"].Value<double>();
                if (obj["sidedoorheight"] == null) { sidedoorheight = 9999; } else { sidedoorlength = obj["sidedoorlength"].Value<double>(); }
                //sidedoorheight = obj["sidedoorheight"].Value<double>();

                weightlimit = obj["weightlimit"].Value<double>();
                liftsize = obj["liftsize"].Value<Int16>();
                volume = obj["volume"].Value<double>();
                width = obj["width"].Value<double>();
                model = obj["model"].Value<string>();

                maxcarry = obj["maxcarry"].Value<double>();
                owner = obj["owner"].Value<string>();
                kt = obj["kt"].Value<string>();
                address = obj["address"].Value<string>();
                town = obj["town"].Value<string>();

                postcode = obj["postcode"].Value<string>();
                phone = obj["phone"].Value<string>();
                mobile = obj["mobile"].Value<string>();
                driver = obj["driver"].Value<string>();
                dkt = obj["dkt"].Value<string>();

                daddress = obj["daddress"].Value<string>();
                dtown = obj["dtown"].Value<string>();
                dpostcode = obj["dpostcode"].Value<string>();
                dphone = obj["dphone"].Value<string>();
                dmobile = obj["dmobile"].Value<string>();

                heightofbox = obj["heightofbox"].Value<double>();
                isdel = obj["isdel"].Value<bool>();
                size = obj["size"].Value<int>();

            }


        }
        public bool UpdataCarsToCouch(List<dtoCars> inpar)
        {
            try
            {

                var server = new CouchServer(host, port);
                var cdb = server.GetDatabase("dbsendill_newcars");
                foreach (dtoCars par in inpar)
                {
                    jcar jc = new jcar();
                    if (par.id == null) { jc.id = 9999; } else { jc.id = par.id; };
                    if (par.stationid == null) { jc.stationid = 9999; } else { jc.stationid = par.stationid; }
                    if (par.carnumber == null) { jc.carnumber = "null"; } else { jc.carnumber = par.carnumber; }
                    if (par.code == null) { jc.code = "null"; } else { jc.code = par.code; }
                    if (par.listed == null) { jc.listed = false; } else { jc.listed = par.listed; }
                    if (par.carname == null) { jc.carname = "null"; } else { jc.carname = par.carname; }
                    if (par.car1 == null) { jc.car1 = false; } else { jc.car1 = par.car1; }
                    if (par.car2 == null) { jc.car2 = false; } else { jc.car2 = par.car2; }
                    if (par.car3 == null) { jc.car3 = false; } else { jc.car3 = par.car3; }
                    if (par.car4 == null) { jc.car4 = false; } else { jc.car4 = par.car4; }
                    if (par.car5 == null) { jc.car5 = false; } else { jc.car5 = par.car5; }
                    if (par.car6 == null) { jc.car6 = false; } else { jc.car6 = par.car6; }
                    if (par.car7 == null) { jc.car7 = false; } else { jc.car7 = par.car7; }
                    if (par.car8 == null) { jc.car8 = false; } else { jc.car8 = par.car8; }
                    if (par.car9 == null) { jc.car9 = false; } else { jc.car9 = par.car9; }
                    if (par.car10 == null) { jc.car10 = false; } else { jc.car10 = par.car10; }
                    if (par.length == null) { jc.length = 9999; } else { jc.length = par.length; }
                    if (par.backdoorheight == null) { jc.backdoorheight = 9999; } else { jc.backdoorheight = par.backdoorheight; }
                    if (par.backdoorlength == null) { jc.backdoorlength = 9999; } else { jc.backdoorlength = par.backdoorlength; }
                    if (par.sidedoorheight == null) { jc.sidedoorheight = 9999; } else { jc.sidedoorheight = par.sidedoorheight; }
                    if (par.sidedoorlength == null) { jc.sidedoorlength = 9999; } else { jc.sidedoorlength = par.sidedoorlength; }
                    if (par.weightlimit == null) { jc.weightlimit = 9999; } else { jc.weightlimit = par.weightlimit; }
                    if (par.liftsize == null) { jc.liftsize = 9999; } else { jc.liftsize = par.liftsize; }
                    if (par.volume == null) { jc.volume = 9999; } else { jc.volume = par.volume; }
                    if (par.width == null) { jc.width = 9999; } else { jc.width = par.width; }
                    if (par.model == null) { jc.model = "null"; } else { jc.model = par.model; }
                    if (par.maxcarry == null) { jc.maxcarry = 9999; } else { jc.maxcarry = par.maxcarry; }
                    if (par.owner == null) { jc.owner = "null"; } else { jc.owner = par.owner; }
                    if (par.kt == null) { jc.kt = "null"; } else { jc.kt = par.kt; }
                    if (par.address == null) { jc.address = "null"; } else { jc.address = par.address; }
                    if (par.town == null) { jc.town = "null"; } else { jc.town = par.town; }
                    if (par.postcode == null) { jc.postcode = "null"; } else { jc.postcode = par.postcode; }
                    if (par.phone == null) { jc.phone = "null"; } else { jc.phone = par.phone; }
                    if (par.mobile == null) { jc.mobile = "null"; } else { jc.mobile = par.mobile; }
                    if (par.driver == null) { jc.driver = "null"; } else { jc.driver = par.driver; }
                    if (par.dkt == null) { jc.dkt = "null"; } else { jc.dkt = par.dkt; }
                    if (par.daddress == null) { jc.daddress = "null"; } else { jc.daddress = par.daddress; }
                    if (par.dtown == null) { jc.dtown = "null"; } else { jc.dtown = par.town; }
                    if (par.postcode == null) { jc.postcode = "null"; } else { jc.postcode = par.postcode; }
                    if (par.dphone == null) { jc.dphone = "null"; } else { jc.dphone = par.dphone; }
                    if (par.dmobile == null) { jc.dmobile = "null"; } else { jc.dmobile = par.dmobile; }
                    if (par.heightofbox == null) { jc.heightofbox = 9999; } else { jc.heightofbox = par.heightofbox; }
                    if (par.isdel == null) { jc.isdel = false; } jc.isdel = par.isdel;
                    if (par.size == null) { jc.size = 9999; } else { jc.size = par.size; }

                    cdb.SaveDocument(jc);
                
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public List<dtoCars> LoadCarsFromCouch()
        {

            List<dtoCars> lcar = new List<dtoCars>();
            
            var server = new CouchServer(host, port);
            var cdb = server.GetDatabase("dbsendill_newcars");
            var tempView = cdb.NewTempView("dbsendill_cars", "dbsendill_cars", "if (doc.docType && doc.docType == 'car') emit(doc.carnumber, doc);");
            var linqCars = tempView.LinqQuery<jcar>();
            var mycar = from c in linqCars
                        select c;
            foreach (var par in mycar)
            {
                dtoCars jc = new dtoCars();
                jc.id = par.id;
                jc.stationid = par.stationid;
                jc.carnumber = par.carnumber;
                jc.code = par.code;
                jc.listed = par.listed;
                jc.carname = par.carname;
                jc.car1 = par.car1;
                jc.car2 = par.car2;
                jc.car3 = par.car3;
                jc.car4 = par.car4;
                jc.car5 = par.car5;
                jc.car6 = par.car6;
                jc.car7 = par.car7;
                jc.car8 = par.car8;
                jc.car9 = par.car9;
                jc.car10 = par.car10;
                jc.length = par.length;
                jc.backdoorheight = par.backdoorheight;
                jc.backdoorlength = par.backdoorlength;
                jc.sidedoorheight = par.sidedoorheight;
                jc.sidedoorlength = par.sidedoorlength;
                jc.weightlimit = par.weightlimit;
                jc.liftsize = par.liftsize;
                jc.volume = par.volume;
                jc.width = par.width;
                jc.model = par.model;
                jc.maxcarry = par.maxcarry;
                jc.owner = par.owner;
                jc.kt = par.kt;
                jc.address = par.address;
                jc.town = par.town;
                jc.postcode = par.postcode;
                jc.phone = par.phone;
                jc.mobile = par.mobile;
                jc.driver = par.driver;
                jc.dkt = par.dkt;
                jc.daddress = par.daddress;
                jc.dtown = par.town;
                jc.postcode = par.postcode;
                jc.dphone = par.dphone;
                jc.dmobile = par.dmobile;
                jc.heightofbox = par.heightofbox;
                jc.isdel = par.isdel;
                jc.size = par.size;
                lcar.Add(jc);

            }
            return lcar;
            //var tempView = cdb.NewTempView("test", "test", "if (doc.docType && doc.docType == 'car') emit(doc.Hps, doc);");
            //var linqCars = tempView.LinqQuery<Car>();
        }
    }
}
