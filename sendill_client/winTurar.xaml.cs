using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Data.Linq;

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winTurar.xaml
    /// </summary>

    public partial class winTurar : Window
    {
        ICollectionView _turarView;
        List<dtoCustomer> _lcust = new List<dtoCustomer>();        
        List<dtoTour> _ltour = new List<dtoTour>();
        List<dtoCars> _car = new List<dtoCars>();
        string[] filtervalues =new string[4];
        DateTime _parDateFrom;
        DateTime _parDateTo;
        DateTime dtnow;
 
        public winTurar()
        {
            InitializeComponent();


           // var window2 = Application.Current.Windows
           //.Cast<Window>()
           //.FirstOrDefault(window => window is MainWindow) as MainWindow;
           // var dtturar = window2.memTableTurar;
           // dtoTour _tour = new dtoTour();
           // foreach (DataRow dr in dtturar.Rows)
           // {
           //     dtoTour _itour = new dtoTour();
           //     if (!dr.IsNull(0)) { _itour.id = (int)dr["ID"]; }
           //     if (!dr.IsNull(1)) { _itour.idcustomer = (int)dr["id_customers"]; }
           //     if (!dr.IsNull(2)) { _itour.idcar = (int)dr["id_cars"]; }
           //     if (!dr.IsNull(4)) { _itour.tdatetime = (DateTime)dr["timedate"]; }
           //     if (!dr.IsNull(5)) { _itour.tcustomer = (string)dr["customer"]; }
           //     if (!dr.IsNull(6)) { _itour.tcontact = (string)dr["contact"]; }
           //     if (!dr.IsNull(7)) { _itour.tphone = (string)dr["phione"]; }
           //     if (!dr.IsNull(8)) { _itour.tnote = (string)dr["note"]; }
           //     _ltour.Add(_itour);
           // }
            try
            {
                using (Stream stream = File.Open(@"C:\dbsendill\list_customers.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    _lcust = (List<dtoCustomer>)bin.Deserialize(stream);
                    dtnow = DateTime.Now;
                    var isource = from c in _lcust
                                  orderby c.name ascending
                                  select new
                                  {
                                      c.id,
                                      c.name,
                                      c.address,
                                      c.number
                                  };

                    comboCustomer.ItemsSource =_lcust;
                    comboCustomer.SelectedValuePath = "id";
                    this.DataContext = _lcust;
                    comboFilterCustomer.SelectedValuePath = "id";
                    comboFilterCustomer.DisplayMemberPath = "name";


                }
            }
            catch (IOException)
            {
            }
            try
            {
                DBManager dm = new DBManager();
                _car= dm.GetAllCars();
                var scar=from c in _car
                         orderby c.stationid ascending
                         select new
                         {
                            ID= c.stationid,
                            VALUE=c.stationid.ToString()
                         };
                comboFilterName.ItemsSource = scar;
                comboFilterName.SelectedValuePath = "ID";
                comboFilterName.DisplayMemberPath = "VALUE";



            }
            catch
            {

            }


            try
            {
                //using (Stream stream = File.Open(@"C:\dbsendill\list_tours.bin", FileMode.Open))
                //{
                //    BinaryFormatter bin = new BinaryFormatter();
                //    _ltour = (List<dtoTour>)bin.Deserialize(stream);
                //    dtnow = DateTime.Now;
                //    var isource = from c in _ltour
                //                  orderby c.tdatetime
                //                  select c; 
                //    dataGridTurar.ItemsSource = isource;
                //}
                //var window2 = Application.Current.Windows
                //.Cast<Window>()
                //.FirstOrDefault(window => window is MainWindow) as MainWindow;
                ArrayDefaultValues();
                List<dtoTour> ltour = new List<dtoTour>();
                DBManager dm = new DBManager();
                //dataGridTurar.ItemsSource = dm.GetAllToursFromFile();
                _ltour = dm.GetAllToursFromFile().ToList();
                _turarView = CollectionViewSource.GetDefaultView(_ltour);
                //_turarView.Filter = TurarSingelDateFilter;
                dataGridTurar.ItemsSource = _turarView;
                //_turarView.Filter = TurarSingelDateFilter;
               // CollectionViewSource.GetDefaultView(_ltour).Refresh();
                //dataGridTurar.ItemsSource = dm.GetTourPar_Year_Month(DateTime.Now.Year-1, DateTime.Now.Month);
                //MessageBox.Show(_ltour.Count.ToString());
                
            }
            catch (IOException)
            {
            }
        }


        //private void comSave_Click(object sender, RoutedEventArgs e)
        //{
        //    //dtoTour _itour = new dtoTour();
        //    //_itour.id = 1;
        //    //_itour.idcar = 1;
        //    //_itour.idcustomer = 1;
        //    //_itour.tdatetime = DateTime.Now;
        //    //_itour.tcustomer = "Pétur Pan";
        //    //_itour.taddress = "Snekkjuvogi 12";
        //    //_itour.tcontact = "Guðmundur Sigurjónsson";
        //    //_itour.tnote = "Þetta er testfærsla í grunninn";
        //    //_ltour.Add(_itour);
        //    FileStream fs = new FileStream(@"C:\dbsendill\list_tours.bin", FileMode.Create);
        //    BinaryFormatter bf = new BinaryFormatter();
        //    bf.Serialize(fs, _ltour);
        //    fs.Close();
        //    MessageBox.Show("Túralista hlaðið í minni");
        //}

        private void comTBarNew_Click(object sender, RoutedEventArgs e)
        {
            PopupNewTourWindow.Show();
        }

        private void comChildSave_Click(object sender, RoutedEventArgs e)
        {
            dtoTour itour = new dtoTour();
            itour.idcar =  Convert.ToInt32(txtIdCar.Text);
            itour.tcontact = txtContact.Text;
            itour.taddress = txtAddress.Text;
            itour.tphone = txtPhone.Text;
            itour.tcustomer = txtCustomer.Text;
            itour.tnote = txtNote.Text;
            _ltour.Add(itour);
            PopupNewTourWindow.Close();
            dtnow = DateTime.Now;
            var isource = from c in _ltour
                          select c;
            dataGridTurar.ItemsSource = isource;
            
        }

        private void comChildCancel_Click(object sender, RoutedEventArgs e)
        {
            txtIdCar.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtPhone.Clear();
            txtCustomer.Clear();
            txtNote.Clear();
            PopupNewTourWindow.Close();
        }

        private void comTBarSave_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream(@"C:\dbsendill\list_tours.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, _ltour);
            fs.Close();
        }

        private void comboCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string strid = comboCustomer.SelectedValue.ToString();
            //MessageBox.Show(strid);
            //int id = Convert.ToInt32(strid);
            //var isource = from c in _lcust
            //              where c.id == id
            //              select new
            //              {
            //                  c.address,
            //                  c.streetnr
            //              };
            //foreach (var row in isource)
            //{
            //    string str = row.address+" "+row.streetnr;
            //    MessageBox.Show(str);
            //    txtAddress.Text = str;
            //}

            
        }

        private void comboCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string strid = comboCustomer.SelectedValue.ToString();
                int id = Convert.ToInt32(strid);
                var isource = from c in _lcust
                              where c.id == id
                              select new
                              {
                                  c.address,
                                  c.streetnr
                              };
                foreach (var row in isource)
                {
                    string str = row.address + " " + row.streetnr;
                    txtAddress.Text = str;
                }
            }
            catch(IOException)
            {

            }
        }

        private void tourToolbarComFilter_Click(object sender, RoutedEventArgs e)
        {

            //if (!(filtervalues[0] == "emty") && (filtervalues[1] == "emty" && filtervalues[2] == "emty" && filtervalues[3] == "emty"))
            //{
                
            //    ICollectionView _turarView = CollectionViewSource.GetDefaultView(_ltour);

            //    _turarView.Filter=TurarSingelDateFilter;

            //    dataGridTurar.ItemsSource = _turarView;
            //}

            //_turarView.Filter = TurarSingelDateFilter;
            // CollectionViewSource.GetDefaultView(_ltour).Refresh();
            //_turarView = CollectionViewSource.GetDefaultView(_ltour);

            _turarView.Filter = TurarSingelDateFilter;
            
            //dataGridTurar.ItemsSource = _turarView;

            CollectionViewSource.GetDefaultView(_ltour).Refresh();

        }

        private void comboFilterCusRtomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //try
            //{
            //    var cc = sender as ComboBox;
            //    string sval = cc.SelectedValue.ToString();
            //    filtervalues[3] = sval;
            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}

        }

        private void ArrayDefaultValues()
        {
            filtervalues[0] = "emty";
            filtervalues[1] = "emty";
            filtervalues[2] = "emty";
            filtervalues[3] = "emty";
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var dp = sender as DatePicker;
            string sdate = dp.Text.ToString();
            _parDateFrom = Convert.ToDateTime(dp.SelectedDate);
            filtervalues[0] = sdate;
        }

        private void datePicker2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var dp = sender as DatePicker;
            string sdate = dp.Text.ToString();
            _parDateTo = Convert.ToDateTime(dp.SelectedDate);
            filtervalues[1] = sdate;
        }

        private void comboFilterName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                var cc = sender as ComboBox;
                var ival = Convert.ToInt32(cc.SelectedValue);
                string sval = ival.ToString();
                filtervalues[2] = sval;
        }

        #region Filters

        private bool TurarSingelDateFilter(object itour)
        {

            dtoTour _tour = itour as dtoTour;
            if (_tour.tdatetime.ToShortDateString() == filtervalues[0])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        
        private bool TurarDateFromFilter(object itour)
        {
            dtoTour _tour = itour as dtoTour;
            if (_tour.tdatetime <= _parDateTo && _tour.tdatetime >= _parDateFrom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TurarIdFilter(object itour)
        {
            dtoTour dtour = itour as dtoTour;
            string sid = filtervalues[2];
            int iid = Convert.ToInt32(sid);
            return dtour.idcar.Equals(iid);
        }

        private bool TurarCustomerFilter(object itour)
        {

            dtoTour dtour = itour as dtoTour;
            string sid = filtervalues[3];
            int iid = Convert.ToInt32(sid);
            return dtour.idcustomer.Equals(iid);

        }

        //private bool TurarAddressFilter(object itour)
        //{

        //    ////dtoTour dtour = itour as dtoTour;
        //    ////string 

        //}
        #endregion

        private void tourToolbarComFilterClear_Click(object sender, RoutedEventArgs e)
        {

            this.DataContext = _lcust;
            comboFilterCustomer.SelectedValuePath = "id";
            comboFilterCustomer.DisplayMemberPath = "name";
            comboFilterName.Text = "";
            
            _turarView.Filter = null;
            
            dataGridTurar.ItemsSource = _turarView;

            CollectionViewSource.GetDefaultView(_ltour).Refresh();

        }

        public string[] GetToursParameters()
        {
            return filtervalues;
        }

        private void comT2BarPrint_Click(object sender, RoutedEventArgs e)
        {
            frmReports frm = new frmReports();
            frm.Show();
        }

        private void rComFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty") && !(filtervalues[2] == "emty") && !(filtervalues[3] == "emty"))
            {
                MessageBox.Show("Filter fyrir viðskiptavini og stöðvarnúmer virka ekki.");
                _turarView.Filter = TurarDateFromFilter;
                _turarView.Filter = TurarIdFilter;
                _turarView.Filter = TurarCustomerFilter;
                CollectionViewSource.GetDefaultView(_ltour).Refresh();

            }
            else if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty") && !(filtervalues[2] == "emty") && (filtervalues[3] == "emty"))
            {
                MessageBox.Show("Filter fyrir stöðvarnúmer virkar ekki.");
                _turarView.Filter = TurarDateFromFilter;
                _turarView.Filter = TurarIdFilter;
                CollectionViewSource.GetDefaultView(_ltour).Refresh();
            }
            else if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty") && (filtervalues[2] == "emty") && (filtervalues[3]== "emty"))
            {
                _turarView.Filter = TurarDateFromFilter;
                CollectionViewSource.GetDefaultView(_ltour).Refresh();
            }



        }

        private void rComFilterOff_Click(object sender, RoutedEventArgs e)
        {
            comboFilterCustomer.Text = "";
            comboFilterName.Text = "";
            datePicker1.SelectedDate = null;
            datePicker2.SelectedDate = null;

            _turarView.Filter = null;

            dataGridTurar.ItemsSource = _turarView;

            CollectionViewSource.GetDefaultView(_ltour).Refresh();
            ArrayDefaultValues();

        }

        private void rComReportList_Click(object sender, RoutedEventArgs e)
        {
            frmReports frm = new frmReports();
            frm.Show();

        }

        private void rComReportRec_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearchByText_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private bool CusomerTextFilter(object itour)
        {
            
            
            
            
            return true;
        }

        private void comboFilterCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                var cc = sender as ComboBox;
                var ival = Convert.ToInt32(cc.SelectedValue);
                string sval = ival.ToString();
                filtervalues[3] = sval;
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            //PopupNewTourWindow.Show();
            winNewTour nt = new winNewTour();
            nt.globl_new_tour = false;
            nt.Show();
            this.Close();
        }
        public void LoadToursToList()
        {
            List<dtoTour> ltour = new List<dtoTour>();
            DBManager dm = new DBManager();
            //dataGridTurar.ItemsSource = dm.GetAllToursFromFile();
            _ltour = dm.GetAllToursFromFile().ToList();
            _turarView = CollectionViewSource.GetDefaultView(_ltour);
            //_turarView.Filter = TurarSingelDateFilter;
            dataGridTurar.ItemsSource = _turarView;
            CollectionViewSource.GetDefaultView(_ltour).Refresh();
        }

        //private void PiratesFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    CollectionViewSource.GetDefaultView(PiratesListView.ItemsSource).Refresh();
        //}

        //private bool UserFilter(object item)
        //{
        //    if (String.IsNullOrEmpty(PiratesFilter.Text))
        //        return true;

        //    var pirate = (Pirate)item;

        //    return (pirate.FirstName.StartsWith(PiratesFilter.Text, StringComparison.OrdinalIgnoreCase)
        //            || pirate.LastName.StartsWith(PiratesFilter.Text, StringComparison.OrdinalIgnoreCase));
        //}

    }
}

