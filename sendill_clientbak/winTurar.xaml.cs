using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public CollectionView _cv;
        List<CustomerModel> _lcust = new List<CustomerModel>();        
        public List<dtoTour> _ltour = new List<dtoTour>();
        public List<TourModel> _mtour = new List<TourModel>();
        public List<dtoTour> _ltourfilter = new List<dtoTour>();
        public ObservableCollection<dtoTour> _otour = new ObservableCollection<dtoTour>();
        List<dtoCars> _car = new List<dtoCars>();
        string[] filtervalues =new string[4];
        DateTime _parDateFrom;
        DateTime _parDateTo;
        DateTime dtnow;
        public int p_carid;
        public bool p_caridfilter;
        public int p_repid;
        public dtoTour p_DtoTour;
        DataGridColumn dgcol_0;
        DataGridColumn dgcol_1;
        DataGridColumn dgcol_2;
        DataGridColumn dgcol_3;
        DataGridColumn dgcol_4;
        DataGridColumn dgcol_5;
        DataGridColumn dgcol_6;
        DataGridColumn dgcol_7;

 
        public winTurar()
        {
            InitializeComponent();


            try
            {


                DBManager dm = new DBManager();
                _lcust = dm.GetAllCustomers().ToList();
                    dtnow = DateTime.Now;
                    var isource = from c in _lcust
                                  orderby c.customer ascending
                                  select new
                                  {
                                      c.id,
                                      c.customer,
                                      c.address,
                                      c.phone
                                  };

                    this.DataContext = _lcust;
                    comboFilterCustomer.SelectedValuePath = "customer";
                    comboFilterCustomer.DisplayMemberPath = "customer";
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

        
        
        
        
        private void CollectionViewSource_Filter(object sender , FilterEventArgs e )
        {
            dtoTour t = e.Item as dtoTour;
            if (t !=null)
            {
                if (String.IsNullOrEmpty(t.idcar.ToString()))
                {
                    e.Accepted = false;
                }
                else
                {
                    if (t.idcar == 3)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                }
            }
        }

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
            var ival = Convert.ToString(cc.SelectedValue);
            string sval = ival;
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
            return dtour.tcustomer.Equals(sid);

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
            comboFilterName.Text = "Veldu viðskiptavin";
            comboFilterCustomer.Text = "Veldu bíl";
            
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
            ICollectionView dw = CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource);

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
                    _ltour = dm.GetToursPar_CustId(filtervalues[3]).ToList();
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
                    //_ltour = dm.GetToursPar_CarId_Date(Convert.ToInt32(filtervalues[2]), _parDateFrom, _parDateTo.AddDays(1)).ToList();
                    _ltour = dm.GetToursPar_CustId_Date(filtervalues[3], _parDateFrom, _parDateFrom.AddDays(1)).ToList();
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
                     _ltour = dm.GetToursPar_Car_CustId(Convert.ToInt32(filtervalues[2]), filtervalues[3]).ToList();
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

            DBManager dm = new DBManager();
            _ltour = dm.GetAllToursFromFile().ToList();
            _turarView = CollectionViewSource.GetDefaultView(_ltour);
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

        public void LoadToursToList()
        {
            // Breytt 27.01.207 By PS
            // Commented out begin
            // dtnow = DateTime.Now;
            // _parDateFrom = dtnow;
            //_parDateTo = dtnow.AddMonths(0).AddDays(1); 
            // Commented out ends
            DBManager dm = new DBManager();
            //_ltour = dm.GetAllToursFromFile().ToList();
            //_ltour = dm.GetToursPar_Default().ToList(); Change ID 100
            _ltour = dm.GetTourListFromDB();
            dataGridTurar.ItemsSource = _ltour;
            //OTours ot = (OTours)this.Resources["ktours"];
            //foreach(var otour in dm.GetAllToursFromFile())
            //{
            //    ot.Add(otour);
            //}
            //MessageBox.Show(ot.Count().ToString());
            //dataGridTurar.ItemsSource = ot;
            //_turarView = CollectionViewSource.GetDefaultView(_ltour);
            //_turarView.Filter = TurarSingelDateFilter;
            //dataGridTurar.ItemsSource = _ltour;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (p_caridfilter==true)
            {
                DBManager dm=new DBManager();
                //_ltour = dm.GetToursPar_CarId_Date(p_carid, DateTime.Now, DateTime.Now).ToList();
                //_ltour = dm.GetToursPar_CarId(p_carid).ToList();
                 _ltour = dm.GetToursByCarIdYearMonthDB(p_carid,DateTime.Now.Month,DateTime.Now.Year);
                 dataGridTurar.ItemsSource = _ltour;
                //_turarView = CollectionViewSource.GetDefaultView(_ltour);
                //dataGridTurar.ItemsSource = _turarView;
                //CollectionViewSource.GetDefaultView(_ltour).Refresh();
            }
            else
            {
                LoadToursToList();
            }
        }

        private void TourRibbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RibbonApplicationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtFilterCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageBox.Show("Viðskiptamannatexti breyttist.");
        }

        private void RibbonButton_Click_1(object sender, RoutedEventArgs e)
        {
            
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
                dtoTour tour = _ltour[i];
                winNewTour nt = new winNewTour();
                nt.globl_new_tour = false;
                nt.p_filtercarid = false;
                nt.global_update_tour = true;
                nt.DtoTour = tour;
                nt.Show();
            }

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

        private void txtFilterCarId_GotFocus(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = FilterCarId;
        }

        private bool FilterCarId(object item)
        {
            if (String.IsNullOrEmpty(txtFilterCarId.Text))
                return true;
            var tour = (dtoTour)item;

            return (tour.idcar.Equals(Convert.ToInt32(txtFilterCarId.Text)));
        }

        private void txtFilterCarId_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private void txtFilterCarId_LostFocus(object sender, RoutedEventArgs e)
        {
            txtFilterCarId.Text = string.Empty;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = null;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private bool FilterCustomer(object item)
        {

            if (String.IsNullOrEmpty(txtFillterCustomer.Text))
                return true;
            var tour = (dtoTour)item;
            if (String.IsNullOrEmpty(tour.tcustomer))
            {
                return false;
            }
            else
            {

                return (tour.tcustomer.StartsWith(txtFillterCustomer.Text.ToString(), StringComparison.OrdinalIgnoreCase));
            }
        }
        
        
        private void txtFilterCustomer_GotFocus(object sender, RoutedEventArgs e)
        {

            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();

        }

        private void txtFilterCustomer_LostFocus(object sender, RoutedEventArgs e)
        {
            txtFillterCustomer.Text = string.Empty;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = null;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private void txtFillterCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = FilterCustomer;
        }

        private void txtFillterCustomer_GotFocus(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private void txtFillterCustomer_LostFocus(object sender, RoutedEventArgs e)
        {
            txtFillterCustomer.Text = string.Empty;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = null;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private bool FilterAddress(Object item)
        {

            if (String.IsNullOrEmpty(txtFilterAddress.Text))
                return true;
            var tour = (dtoTour)item;
            if (String.IsNullOrEmpty(tour.taddress))
            {
                return false;
            }
            else
            {

                return (tour.taddress.StartsWith(txtFilterAddress.Text.ToString(), StringComparison.OrdinalIgnoreCase));
            }

        }

        private void txtFilterAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = FilterAddress;

        }

        private void txtFilterAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private void txtFilterAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            txtFilterAddress.Text = string.Empty;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = null;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();

        }

        private bool FilterContact(Object item)
        {

            if (String.IsNullOrEmpty(txtFilterContact.Text))
                return true;
            var tour = (dtoTour)item;
            if (String.IsNullOrEmpty(tour.tcontact))
            {
                return false;
            }
            else
            {
                return (tour.tcontact.StartsWith(txtFilterContact.Text.ToString(), StringComparison.OrdinalIgnoreCase));
            }

        }

        private void txtFilterContact_LostFocus(object sender, RoutedEventArgs e)
        {

            txtFilterContact.Text = string.Empty;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = null;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();

        }

        private void txtFilterContact_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = FilterContact;
        }

        private void txtFilterContact_GotFocus(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        
        private bool FilterPhone(Object item)
        {

            if (String.IsNullOrEmpty(txtFilterPhone.Text))
                return true;
            var tour = (dtoTour)item;
            if (String.IsNullOrEmpty(tour.tphone))
            {
                return false;
            }
            else
            {
                return (tour.tphone.StartsWith(txtFilterPhone.Text.ToString(), StringComparison.OrdinalIgnoreCase));
            }

        }
        
        
        private void txtFilterPhone_GotFocus(object sender, RoutedEventArgs e)
        {

            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();

        }

        private void txtFilterPhone_LostFocus(object sender, RoutedEventArgs e)
        {

            txtFilterPhone.Text = string.Empty;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = null;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();

        }

        private void txtFilterPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = FilterPhone;

        }

        private void txtFilterNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            _turarView = CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource);
            _turarView.Filter = FilterNote;
            //AllApplications.Where(x => x.Name.ToUpperInvariant().Contains(txtSearch.Text.ToUpperInvariant()))).ToList();
            var listtour = _ltour
                .Where(x => x != null)
                .Where(x => x.tnote != null)
                .Where(x => x.tnote.ToUpperInvariant().StartsWith(txtFilterNote.Text.ToString().ToUpperInvariant()));
            
            _ltourfilter = listtour.ToList();
            //_ltourfilter = _ltour.Where(x => x.tnote.ToUpperInvariant().Contains(txtFilterNote.Text.ToUpperInvariant())).ToList();

        }

        private void txtFilterNote_LostFocus(object sender, RoutedEventArgs e)
        {
            txtFilterNote.Text = string.Empty;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = null;
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private void txtFilterNote_GotFocus(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        }

        private bool FilterNote(object item)
        {

            if (String.IsNullOrEmpty(txtFilterNote.Text))
                return true;
            var tour = (dtoTour)item;
            if (String.IsNullOrEmpty(tour.tnote))
            {
                return false;
            }
            else
            {
                return (tour.tnote.StartsWith(txtFilterNote.Text.ToString(), StringComparison.OrdinalIgnoreCase));
            }

        }

        private void rcomColShow_Click(object sender, RoutedEventArgs e)
        {
            bool? bshow;
            bshow=chbColDate.IsChecked;
            if (Convert.ToBoolean(bshow))
            {
                dataGridTurar.Columns[0].Visibility=Visibility.Visible;
            }
            else
            {
                dataGridTurar.Columns[0].Visibility = Visibility.Hidden;
            }
            bshow = chbColTime.IsChecked;
            if (Convert.ToBoolean(bshow))
            {
                dataGridTurar.Columns[1].Visibility = Visibility.Visible;
            }
            else
            {
                dataGridTurar.Columns[1].Visibility = Visibility.Hidden;
            }
            bshow = chbColCarId.IsChecked;
            if (Convert.ToBoolean(bshow))
            {
                dataGridTurar.Columns[2].Visibility = Visibility.Visible;
            }
            else
            {
                dataGridTurar.Columns[2].Visibility = Visibility.Hidden;
            }
            bshow = chbColAddress.IsChecked;
            if (Convert.ToBoolean(bshow))
            {
                dataGridTurar.Columns[3].Visibility = Visibility.Visible;
            }
            else
            {
                dataGridTurar.Columns[3].Visibility = Visibility.Hidden;
            }
            bshow = chbColContact.IsChecked;
            if (Convert.ToBoolean(bshow))
            {
                dataGridTurar.Columns[4].Visibility = Visibility.Visible;
            }
            else
            {
                dataGridTurar.Columns[4].Visibility = Visibility.Hidden;
            }
            bshow = chbColPhone.IsChecked;
            if (Convert.ToBoolean(bshow))
            {
                dataGridTurar.Columns[5].Visibility = Visibility.Visible;
            }
            else
            {
                dataGridTurar.Columns[5].Visibility = Visibility.Hidden;
            }

            bshow = chbColCustomer.IsChecked;
            if (Convert.ToBoolean(bshow))
            {
                dataGridTurar.Columns[7].Visibility = Visibility.Visible;
            }
            else
            {
                dataGridTurar.Columns[7].Visibility = Visibility.Hidden;
            }

            bshow = chbColNote.IsChecked;
            if (Convert.ToBoolean(bshow))
            {
                dataGridTurar.Columns[6].Visibility = Visibility.Visible;
            }
            else
            {
                dataGridTurar.Columns[6].Visibility = Visibility.Hidden;
            }
        }

        private void chbColDate_Checked(object sender, RoutedEventArgs e)
        {
            //dataGridTurar.Columns.Add(dgcolDate);
        }

        private void chbColDate_Unchecked(object sender, RoutedEventArgs e)
        {
            //dataGridTurar.Columns.Remove(dgcolDate);
        }
    }

    public class OTours:ObservableCollection<dtoTour>
    {

    }
}

