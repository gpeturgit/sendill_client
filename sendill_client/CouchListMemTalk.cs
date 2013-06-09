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
    public class CouchListMemTalk
    {
        string host = "localhost";
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
        }

        public class jarea : CouchDocument
        {
            public jarea()
            {
            }
            public int id { get; set; }
            public string name { get; set; }
            public string note { get; set; }
        }

        public class jpinitem : CouchDocument
        {
            public jpinitem()
            {
            }
            public int id { get; set; }
            public int idpin { get; set; }
            public int idcar { get; set; }
            public bool pbreak { get; set; }
            public string pcarcode { get; set; }
            public int pline { get; set; }
            public DateTime dtonpin { get; set; }
            public DateTime dtoffpinn { get; set; }
            public bool IsSelected { get; set; }
            public string phone { get; set; }
        }

        private class jcustomer : CouchDocument
        {

            public int id;
            public string address;
            public string name;
            public string streetnr;
            public double areacode;
            public string phone1;
            public string fax;
            public string phone2;
            public string umbaf;
            public string mobile;
            public string kt;
            public string number;
            public string area;
            public string email;
            public string cumbaf;
            public double svaedi;
            public string url;
            public bool isdel;

            public jcustomer()
            {
            }
        }

        private class jtour :CouchDocument
        {
             
            public int id;
            public int idcustomer;
            public int idcar;
            public DateTime tdatetime;
            public string tcustomer;
            public string taddress;
            public string tcontact;
            public string tphone;
            public string tnote;
            public bool isdel;

            public jtour()
            {
            }

            public jtour(int _id,
                        int _idcustomer,
                        int _idcar,
                        DateTime _tdatetime,
                        string _tcustomer,
                        string _taddress,
                        string _tcontact,
                        string _tphone,
                        string _tnote,
                        bool _isdel)
            {
                id=_id;
                idcustomer=_idcustomer;
                idcar=_idcar;
                tdatetime=_tdatetime;
                tcustomer=_tcustomer;
                taddress=_taddress;
                tcontact=_tcontact;
                tphone=_tphone;
                tnote=_tnote;
                isdel=_isdel;
            }
            #region CouchDocument Members

            public override void WriteJson(JsonWriter writer)
            {
                // This will write id and rev
                base.WriteJson(writer);

                writer.WritePropertyName("docType");
                writer.WriteValue("tour");
                writer.WritePropertyName("id");
                writer.WriteValue(id);
                writer.WritePropertyName("idcustomer");
                writer.WriteValue(idcustomer);
                writer.WritePropertyName("idcar");
                writer.WriteValue(idcar);
                writer.WritePropertyName("tdatetime");
                writer.WriteValue(tdatetime);
                writer.WritePropertyName("tcustomer");
                writer.WriteValue(tcustomer);
                writer.WritePropertyName("taddress");
                writer.WriteValue(taddress);
                writer.WritePropertyName("tcontact");
                writer.WriteValue(tcontact);
                writer.WritePropertyName("tphone");
                writer.WriteValue(tphone);
                writer.WritePropertyName("tnote");
                writer.WriteValue(tnote);
                writer.WritePropertyName("isdel");
                writer.WriteValue(isdel);
            }

            public override void ReadJson(JObject obj)
            {
                // This will read id and rev
                base.ReadJson(obj);

                id = obj["id"].Value<int>();
                idcustomer = obj["idcustomer"].Value<int>();
                idcar = obj["idcar"].Value<int>();
                tdatetime = obj["tdatetime"].Value<DateTime>();
                tcustomer = obj["tcustomer"].Value<string>();
                taddress = obj["taddress"].Value<string>();
                tcontact = obj["tcontact"].Value<string>();
                tphone = obj["tphone"].Value<string>();
                tnote = obj["tnote"].Value<string>();
                isdel = obj["isdel"].Value<bool>();
            }

            #endregion

        }

        public class jstatus
        {
            public jstatus()
            {
            }
            public int id { get; set; }
            public string message { get; set; }
            public string time { get; set; }
        }

        public bool UpdateTourToCouch(List<dtoTour> inpar)
        {
            try
            {

                var server = new CouchServer(host, port);
                var cdb = server.GetDatabase("dbsendill");
                foreach( dtoTour par in inpar)
                {
                jtour jt = new jtour();
                jt.id = par.id;
                jt.idcar = par.idcar;
                jt.idcustomer = par.idcustomer;
                jt.taddress = par.taddress;
                jt.tcontact = par.tcontact;
                jt.tdatetime = par.tdatetime;
                jt.tnote = par.tnote;
                jt.tphone = par.tphone;
                cdb.SaveDocument(jt);
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}

