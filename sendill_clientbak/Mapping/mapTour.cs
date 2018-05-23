using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sendill_client.Mapping
{
    public class mapTour
    {
        public dtoTour InToDto(TourModel _inItem)
        {
            dtoTour _outItem = new dtoTour();
            _outItem.id = _inItem.id;
            _outItem.idcustomer = _inItem.idcustomer;
            _outItem.idcar = _inItem.idcar;
            _outItem.idpin = _inItem.idpin;
            _outItem.tdatetime = InToDateTime(_inItem.tyear, _inItem.tmonth, _inItem.tday, _inItem.thour, _inItem.tmin);
            _outItem.car1 = _inItem.car1;
            _outItem.car2 = _inItem.car2;
            _outItem.car3 = _inItem.car3;
            _outItem.car4 = _inItem.car4;
            _outItem.car5 = _inItem.car5;
            _outItem.carsize = _inItem.carsize;
            _outItem.isdel = _inItem.isdel;
            _outItem.taddress = _inItem.taddress;
            _outItem.tcontact = _inItem.tcontact;
            _outItem.tcustomer = _inItem.tcustomer;
            _outItem.tnote = _inItem.tnote;
            _outItem.tphone = _inItem.tphone;
            _outItem.time = _inItem.thour+":"+_inItem.tmin;
            return _outItem;
        }

        public TourModel InToModel(dtoTour _inItem)
        {
            TourModel _outItem = new TourModel();
            _outItem.id = _inItem.id;
            _outItem.idcustomer = _inItem.idcustomer;
            _outItem.idcar = _inItem.idcar;
            _outItem.idpin = _inItem.idpin;
            _outItem.tyear = _inItem.tdatetime.Year;
            _outItem.tmonth = _inItem.tdatetime.Month;
            _outItem.car1 = _inItem.car1;
            _outItem.car2 = _inItem.car2;
            _outItem.car3 = _inItem.car3;
            _outItem.car4 = _inItem.car4;
            _outItem.car5 = _inItem.car5;
            _outItem.carsize = _inItem.carsize;
            _outItem.isdel = _inItem.isdel;
            _outItem.taddress = _inItem.taddress;
            _outItem.tcontact = _inItem.tcontact;
            _outItem.tcustomer = _inItem.tcustomer;
            _outItem.tnote = _inItem.tnote;
            _outItem.tphone = _inItem.tphone;
            return _outItem;
        }

        private DateTime InToDateTime(int _yyyy,int _MM,int _dd,int _HH,int _mm)
        {
            string _sdate="";
            if (_dd < 10)
            {
                _sdate ="0" + _dd.ToString();
            }
            else
            {
                _sdate =_dd.ToString();
            }
            if (_MM<10)
            {
                _sdate=_sdate+"/0"+_MM.ToString();
            }
            else
            {
               _sdate=_sdate+"/"+_MM.ToString();
            }
            _sdate = _sdate + "/" + _yyyy.ToString();
            
            
            IFormatProvider culture = new System.Globalization.CultureInfo("is-IS", true);
            DateTime oDate = DateTime.Parse(_sdate, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            
            return oDate;
        }

    }
}