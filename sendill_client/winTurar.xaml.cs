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
        public List<dtoTour> _ltour = new List<dtoTour>();
        List<dtoCars> _car = new List<dtoCars>();
        string[] filtervalues =new string[4];
        DateTime _parDateFrom;
        DateTime _parDateTo;
        DateTime dtnow;
        public int p_carid;
        public bool p_caridfilter;
        public int p_repid;
        public dtoTour p_DtoTour;
 
        public winTurar()
        {
            InitializeComponent();


            try
            {


                DBManager dm = new DBManager();
                _lcust = dm.GetAllCustomers().ToList();
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

                    this.DataContext = _lcust;
                    comboFilterCustomer.SelectedValuePath = "id";
                    comboFilterCustomer.DisplayMemberPath = "name";


                
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
                //List<dtoTour> ltour = new List<dtoTour>();
                //DBManager dm = new DBManager();
                //dataGridTurar.ItemsSource = dm.GetAllToursFromFile();
                //_ltour = dm.GetAllToursFromFile().ToList();
                //_turarView = CollectionViewSource.GetDefaultView(_ltour);
                //_turarView.Filter = TurarSingelDateFilter;
                //dataGridTurar.ItemsSource = _turarView;
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
            
        }

        private void comChildSave_Click(object sender, RoutedEventArgs e)
        {
            //dtoTour itour = new dtoTour();
            //itour.idcar =  Convert.ToInt32(txtIdCar.Text);
            //itour.tcontact = txtContact.Text;
            //itour.taddress = txtAddress.Text;
            //itour.tphone = txtPhone.Text;
            //itour.tcustomer = txtCustomer.Text;
            //itour.tnote = txtNote.Text;
            //_ltour.Add(itour);
            //PopupNewTourWindow.Close();
            //dtnow = DateTime.Now;
            //var isource = from c in _ltour
            //              select c;
            //dataGridTurar.ItemsSource = isource;
            
        }

        private void comChildCancel_Click(object sender, RoutedEventArgs e)
        {
            //txtIdCar.Clear();
            //txtAddress.Clear();
            //txtContact.Clear();
            //txtPhone.Clear();
            //txtCustomer.Clear();
            //txtNote.Clear();
            //PopupNewTourWindow.Close();
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
            //try
            //{
            //    string strid = comboCustomer.SelectedValue.ToString();
            //    int id = Convert.ToInt32(strid);
            //    var isource = from c in _lcust
            //                  where c.id == id
            //                  select new
            //                  {
            //                      c.address,
            //                      c.streetnr
            //                  };
            //    foreach (var row in isource)
            //    {
            //        string str = row.address + " " + row.streetnr;
            //        txtAddress.Text = str;
            //    }
            //}
            //catch(IOException)
            //{

            //}
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

        private void comboFilterCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cc = sender as ComboBox;
            var ival = Convert.ToInt32(cc.SelectedValue);
            string sval = ival.ToString();
            filtervalues[3] = sval;
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
            //if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty") && !(filtervalues[2] == "emty") && !(filtervalues[3] == "emty"))
            //{
            //    MessageBox.Show("Filter fyrir viðskiptavini og stöðvarnúmer virka ekki.");
            //    _turarView.Filter = TurarDateFromFilter;
            //    _turarView.Filter = TurarIdFilter;
            //    _turarView.Filter = TurarCustomerFilter;
            //    CollectionViewSource.GetDefaultView(_ltour).Refresh();

            //}
            //else if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty") && !(filtervalues[2] == "emty") && (filtervalues[3] == "emty"))
            //{
            //    MessageBox.Show("Filter fyrir stöðvarnúmer virkar ekki.");
            //    _turarView.Filter = TurarDateFromFilter;
            //    _turarView.Filter = TurarIdFilter;
            //    CollectionViewSource.GetDefaultView(_ltour).Refresh();
            //}
            //else if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty") && (filtervalues[2] == "emty") && (filtervalues[3]== "emty"))
            //{
            //    _turarView.Filter = TurarDateFromFilter;
            //    CollectionViewSource.GetDefaultView(_ltour).Refresh();
            //}
            if ((chbDate.IsChecked == true) && (chbCustomer.IsChecked == false) && (chbStation.IsChecked == false))
            {
                if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty"))
                {
                    DBManager dm = new DBManager();
                    _ltour = dm.GetTourPar_Date(_parDateFrom, _parDateTo.AddDays(1)).ToList();
                    _turarView = CollectionViewSource.GetDefaultView(_ltour);
                    dataGridTurar.ItemsSource = _turarView;
                    CollectionViewSource.GetDefaultView(_ltour).Refresh();

                }
                else
                {
                    MessageBox.Show("Það vantar gildi í afmörkunina");
                }
            }
            if ((chbDate.IsChecked == false) && (chbCustomer.IsChecked == true) && (chbStation.IsChecked == false))
            {
                if (!(filtervalues[3] == "emty"))
                {
                    DBManager dm = new DBManager();
                    _ltour = dm.GetToursPar_CustId(Convert.ToInt32(filtervalues[3])).ToList();
                    _turarView = CollectionViewSource.GetDefaultView(_ltour);
                    dataGridTurar.ItemsSource = _turarView;
                    CollectionViewSource.GetDefaultView(_ltour).Refresh();
                }
                else
                {
                    MessageBox.Show("Það vantar gildi í afmörkunina.");
                }
            }
            if ((chbDate.IsChecked == false) && (chbCustomer.IsChecked == false) && (chbStation.IsChecked == true))
            {
                if (!(filtervalues[2] == "emty"))
                {
                    DBManager dm = new DBManager();
                    _ltour = dm.GetToursPar_CarId(Convert.ToInt32(filtervalues[2])).ToList();
                    _turarView = CollectionViewSource.GetDefaultView(_ltour);
                    dataGridTurar.ItemsSource = _turarView;
                    CollectionViewSource.GetDefaultView(_ltour).Refresh();
                }
                else
                {
                    MessageBox.Show("Það vantar gildi í afmörkunina.");
                }
            }
            //if (!(filtervalues[0] == "emty") && !(filtervalues[1]=="emty") &&)
            if ((chbDate.IsChecked == true) && (chbStation.IsChecked == true) && (chbCustomer.IsChecked == false))
            {
                MessageBox.Show("Dagsettningr og stöðvarafmörkur.");

                if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty") && !(filtervalues[2] == "emty"))
                {
                    DBManager dm = new DBManager();
                    _ltour = dm.GetToursPar_CarId_Date(Convert.ToInt32(filtervalues[2]), _parDateFrom, _parDateTo.AddDays(1)).ToList();
                    _turarView = CollectionViewSource.GetDefaultView(_ltour);
                    dataGridTurar.ItemsSource = _turarView;
                    CollectionViewSource.GetDefaultView(_ltour).Refresh();
                }
                else
                {
                    MessageBox.Show("Það vantar gildi í afmörkunina.");
                }
            }

            if ((chbDate.IsChecked == true) && (chbCustomer.IsChecked == true) && (chbStation.IsChecked == false))
            {
             
                if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty") && !(filtervalues[3] == "emty"))
                {
                    DBManager dm = new DBManager();
                    _ltour = dm.GetToursPar_CarId_Date(Convert.ToInt32(filtervalues[2]), _parDateFrom, _parDateTo.AddDays(1)).ToList();
                    _turarView = CollectionViewSource.GetDefaultView(_ltour);
                    dataGridTurar.ItemsSource = _turarView;
                    CollectionViewSource.GetDefaultView(_ltour).Refresh();
                }
                else
                {
                    MessageBox.Show("Það vantar gildi í afmörkunina.");
                }

            }
            if ((chbDate.IsChecked == false) && (chbCustomer.IsChecked == true) && (chbStation.IsChecked == true))
            {
                if (!(filtervalues[2] == "emty") && !(filtervalues[3] == "emty"))
                {
                    DBManager dm = new DBManager();
                    _ltour = dm.GetToursPar_CarId_Date(Convert.ToInt32(filtervalues[3]), _parDateFrom, _parDateTo.AddDays(1)).ToList();
                    _turarView = CollectionViewSource.GetDefaultView(_ltour);
                    dataGridTurar.ItemsSource = _turarView;
                    CollectionViewSource.GetDefaultView(_ltour).Refresh();
                }
                else
                {
                    MessageBox.Show("Það vantar gildi í afmörkunina.");
                }
            }
            if ((chbDate.IsChecked == true) && (chbCustomer.IsChecked == true) && (chbStation.IsChecked == true))
            {
                MessageBox.Show("Stöðvar, viðskiptamanna og dagsettningar afmörkun.");
                if (!(filtervalues[0] == "emty") && !(filtervalues[1] == "emty") && !(filtervalues[2] == "emty") && !(filtervalues[3] == "emty"))
                {
                    MessageBox.Show("Þú hefur valið afmörkun sem ekki er gild. Hafið samband við kerfesstjóra til að virkja þessa afmörkun.");
                }
                else
                {
                    MessageBox.Show("Það vantar gildi í afmörkunina.");
                }
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

            
            p_repid = 1;
            frmReports frm = new frmReports();
            frm.Show();

        }

        private void rComReportRec_Click(object sender, RoutedEventArgs e)
        {
            p_repid = 2;
            int i = dataGridTurar.SelectedIndex;
            if (i == -1)
            {
                MessageBox.Show(" Það er enginn túr valinn. ",
                    "Aðvörun .",
                    MessageBoxButton.OK,
                    MessageBoxImage.Hand);
            }
            else
            {
                
                p_DtoTour = _ltour[i];
                frmReports frm = new frmReports();
                frm.Show();
            }






        }

        private void txtSearchByText_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private bool CusomerTextFilter(object itour)
        {
            return true;
        }



        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            
            int i = dataGridTurar.SelectedIndex;
            if (i == -1)
            {
                //MessageBox.Show(" Það er enginn túr valinn. ",
                //    "Aðvörun .",
                //    MessageBoxButton.OK,
                //    MessageBoxImage.Hand);
                dtoTour tour = _ltour[0];
                winNewTour nt = new winNewTour();
                nt.globl_new_tour = false;
                nt.p_filtercarid = false;
                nt.DtoTour = tour;
                string sm = tour.id.ToString();
                nt.Show();

            }
            else
            {
                dtoTour tour = _ltour[i];
                winNewTour nt = new winNewTour();
                nt.globl_new_tour = false;
                nt.p_filtercarid = true;
                nt.DtoTour = tour;
                string sm = tour.id.ToString();
                nt.Show();
            }
        }
        public void LoadToursToList()
        {
             dtnow = DateTime.Now;
             _parDateFrom = dtnow;
            _parDateTo = dtnow.AddMonths(0).AddDays(1); 
            
            List<dtoTour> ltour = new List<dtoTour>();
            DBManager dm = new DBManager();
            //dataGridTurar.ItemsSource = dm.GetAllToursFromFile();
            _ltour = dm.GetAllToursFromFile().ToList();
            _turarView = CollectionViewSource.GetDefaultView(_ltour);
            //_turarView.Filter = TurarSingelDateFilter;
            dataGridTurar.ItemsSource = _turarView;
            CollectionViewSource.GetDefaultView(_ltour).Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (p_caridfilter==true)
            {
                DBManager dm=new DBManager();
                //_ltour = dm.GetToursPar_CarId_Date(p_carid, DateTime.Now, DateTime.Now).ToList();
                 _ltour = dm.GetToursPar_CarId(p_carid).ToList();
                _turarView = CollectionViewSource.GetDefaultView(_ltour);
                dataGridTurar.ItemsSource = _turarView;
                CollectionViewSource.GetDefaultView(_ltour).Refresh();
            }
            else
            {
                LoadToursToList();
            }
            this.WindowState = WindowState.Maximized;
        }

        private void TourRibbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RibbonApplicationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

