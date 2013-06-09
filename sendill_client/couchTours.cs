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
    public class couchTours
    {
        string host = "localhost";
        //string host = "sendilldev.iriscouch.com";
        int port = 5984;

        public class jtour : CouchDocument
        {
            public jtour()
            {
            }
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
        }


        public bool UpdateTourToCouch(List<dtoTour> inpar)
        {
            try
            {

                var server = new CouchServer(host, port);
                var cdb = server.GetDatabase("dbsendill_newtours");
                foreach (dtoTour par in inpar)
                {
                    jcar jc = new jcar();


                    cdb.SaveDocument(jc);

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

    }
}
