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
    public class couchCustomers
    {
        string host = "sendill.iriscouch.com";
        int port = 5984;

        public class jcustomer : CouchDocument
        {
            public jcustomer()
            {
            }

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

            public jcustomer(
            int _id,
            string _address,
            string _name,
            string _streetnr,
            double _areacode,
            string _phone1,
            string _fax,
            string _phone2,
            string _umbaf,
            string _mobile,
            string _kt,
            string _number,
            string _area,
            string _email,
            string _cumbaf,
            double _svaedi,
            string _url,
            bool _isdel)



            {
            id=_id;
            address=_address;
            name=_name;
            streetnr=_streetnr;
            areacode=_areacode;
            phone1=_phone1;
            fax=_fax;
            phone2=_phone2;
            umbaf=_umbaf;
            mobile=_mobile;
            kt=_kt;
            number=_number;
            area=_area;
            email=_email;
            cumbaf=_cumbaf;
            svaedi=_svaedi;
            url=_url;
            isdel=_isdel;
            }

            public override void WriteJson(JsonWriter writer)
            {
                // This will write id and rev
                base.WriteJson(writer);

                writer.WritePropertyName("docType");
                writer.WriteValue("customer");
                writer.WritePropertyName("id");
                writer.WriteValue(id);
                writer.WritePropertyName("name");
                writer.WriteValue(name);
                writer.WritePropertyName("streetnr");
                writer.WriteValue(streetnr);
                writer.WritePropertyName("areacode");
                writer.WriteValue(areacode);
                writer.WritePropertyName("phone1");
                writer.WriteValue(phone1);
                writer.WritePropertyName("fax");
                writer.WriteValue(fax);
                writer.WritePropertyName("phone2");
                writer.WriteValue(phone2);
                writer.WritePropertyName("umbaf");
                writer.WriteValue(umbaf);
                writer.WritePropertyName("mobile");
                writer.WriteValue(mobile);
                writer.WritePropertyName("kt");
                writer.WriteValue(kt);
                writer.WritePropertyName("number");
                writer.WriteValue(number);
                writer.WritePropertyName(" area");
                writer.WriteValue(area);
                writer.WritePropertyName("email");
                writer.WriteValue(email);
                writer.WritePropertyName("cumbaf");
                writer.WriteValue(cumbaf);
                writer.WritePropertyName("svaedi");
                writer.WriteValue(svaedi);
                writer.WritePropertyName("url");
                writer.WriteValue(url);
                writer.WritePropertyName("isdel");
                writer.WriteValue(isdel);
            }
            public override void ReadJson(JObject obj)
            {
                // This will read id and rev
                base.ReadJson(obj);
                
                id = obj["id"].Value<int>();
                address = obj["address"].Value<string>();
                name = obj["name"].Value<string>(); ;
                streetnr= obj["steetnr"].Value<string>();
                areacode = obj["areacode"].Value<double>();
                phone1 = obj["phone1"].Value<string>();
                fax = obj["fax"].Value<string>();
                phone2 = obj["phone2"].Value<string>();
                umbaf = obj["umbaf"].Value<string>();
                mobile = obj["mobile"].Value<string>();
                kt = obj["kt"].Value<string>();
                area = obj["area"].Value<string>();
                email = obj["email"].Value<string>();
                cumbaf = obj["cumbaf"].Value<string>();
                svaedi = obj["svaedi"].Value<double>();
                url = obj["url"].Value<string>();
                isdel = obj["isdel"].Value<bool>();
            
            }



        }
        public bool UpdateCustomersToCouch(List<dtoCustomer> inpar)
        {
            try
            {

                var server = new CouchServer(host, port);
                var cdb = server.GetDatabase("dbsendill_customers");
                foreach (dtoCustomer par in inpar)
                {
                    jcustomer jc = new jcustomer();
                    jc.id = par.id;
                    jc.address = par.address;
                    jc.area = par.area;
                    jc.areacode = par.areacode;
                    jc.cumbaf = par.cumbaf;
                    jc.email = par.email;
                    jc.fax = par.fax;
                    jc.isdel = par.isdel;
                    jc.kt = par.kt;
                    jc.mobile = par.mobile;
                    jc.name = par.name;
                    jc.number = par.number;
                    jc.phone1 = par.phone1;
                    jc.phone2 = par.phone2;
                    jc.streetnr = par.streetnr;
                    jc.svaedi = par.svaedi;
                    jc.umbaf = par.umbaf;
                    jc.url = par.url;
                    
 

                    cdb.SaveDocument(jc);

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public List<dtoCustomer> LoadCustomersFromCouch()
        {

            List<dtoCustomer> lcustomer = new List<dtoCustomer>();

            var server = new CouchServer(host, port);
            var cdb = server.GetDatabase("dbsendill_newcustomers");
            var tempView = cdb.NewTempView("dbsendill_newcustomerss", "dbsendill_newcustomer", "if (doc.docType && doc.docType == 'customer') emit(doc.carnumber, doc);");
            var linqCustomer = tempView.LinqQuery<jcustomer>();
            var qalldoc = cdb.QueryAllDocuments().View.LinqQuery<jcustomer>();
            var mycustomer = from c in linqCustomer
                        select c;
            foreach (var par in mycustomer)
            {
                dtoCustomer jc = new dtoCustomer();
                jc.id = par.id;
                jc.address = par.address;
                jc.area = par.area;
                jc.areacode = par.areacode;
                jc.cumbaf = par.cumbaf;
                jc.email = par.email;
                jc.fax = par.fax;
                jc.isdel = par.isdel;
                jc.kt = par.kt;
                jc.mobile = par.mobile;
                jc.name = par.name;
                jc.number = par.number;
                jc.phone1 = par.phone1;
                jc.phone2 = par.phone2;
                jc.streetnr = par.streetnr;
                jc.svaedi = par.svaedi;
                jc.umbaf = par.umbaf;
                jc.url = par.url;

                lcustomer.Add(jc);

            }
            return lcustomer;
            //var tempView = cdb.NewTempView("test", "test", "if (doc.docType && doc.docType == 'car') emit(doc.Hps, doc);");
            //var linqCars = tempView.LinqQuery<Car>();
        }
    }
}
