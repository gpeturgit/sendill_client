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

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winCustomer.xaml
    /// </summary>
    public partial class winCustomer : Window
    {
        ICollectionView _CustomerView;
        DataTable memTableCustomers = new DataTable();
        List<CustomerModel> _cm;
        List<CustomerModel> _lcustomer = new List<CustomerModel>();
        public winCustomer()
        {
            InitializeComponent();
            DBManager dm = new DBManager();
            _lcustomer = dm.GetAllCustomers().ToList();
            datagridvskm.ItemsSource = _lcustomer;
            //try
            //{
            //    using (Stream stream = File.Open(@"C:\dbsendill\tbl_vidskiptamenn.bin", FileMode.Open))
            //    {
            //        BinaryFormatter bin = new BinaryFormatter();

            //        memTableCustomers = (DataTable)bin.Deserialize(stream);
            //        int rcount = memTableCustomers.Rows.Count;
            //        MessageBox.Show(rcount.ToString());
            //        MessageBox.Show(rcount.ToString());
            //        MessageBox.Show("Viðskiptavinum");
            //    }
            //}
            //catch (IOException)
            //{
            //}
            //MessageBox.Show("Bílar hlaðnir í minni");
            
            //foreach (DataRow dr in memTableCustomers.Rows)
            //{
            //    dtoCustomer _icust = new dtoCustomer();
            //    if (!dr.IsNull(0)) { _icust.id = (int)dr["ID"]; }
            //    if (!dr.IsNull(1)) { _icust.address = (string)dr["Vsk_heimili"]; }
            //    if (!dr.IsNull(2)) { _icust.name = (string)dr["Vsk_nafn"]; }
            //    if (!dr.IsNull(3)) { _icust.streetnr = (string)dr["Vsk_gata_nr"]; }
            //    if (!dr.IsNull(4)) { _icust.areacode = (double)dr["Vsk_postnr"]; }
            //    if (!dr.IsNull(5)) { _icust.phone1 = (string)dr["Vsk_simi"]; }
            //    if (!dr.IsNull(6)) { _icust.fax = (string)dr["Vsk_fax"]; }
            //    if (!dr.IsNull(7)) { _icust.phone2 = (string)dr["Vsk_simi2"];}
            //    if (!dr.IsNull(8)) { _icust.umbaf = (string)dr["Umb_af"];}
            //    if (!dr.IsNull(9)) { _icust.mobile = (string)dr["Vsk_gsm"];}
            //    if (!dr.IsNull(10)) { _icust.kt = (string)dr["Vsk_kt"];}
            //    if (!dr.IsNull(11)) { _icust.number = (string)dr["Vsk_nr"];}
            //    if (!dr.IsNull(12)) { _icust.area = (string)dr["Vsk_stadur"];}
            //    if (!dr.IsNull(13)) { _icust.email = (string)dr["email"];}
            //    if (!dr.IsNull(14)) { _icust.cumbaf = (string)dr["Vsk_umbeðið"];}
            //    if (!dr.IsNull(50)) { _icust.svaedi = (double)dr["Svaedi"]; }
            //    if (!dr.IsNull(51)) { _icust.url = (string)dr["URL"];}

            //    _lcustomer.Add(_icust);
            //    datagridvskm.ItemsSource = _lcustomer;
            //}
        }

        private void comNext_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Þetta er eitt.");
        }

        private void comPrev_Click_1(object sender, RoutedEventArgs e)
        {
            SQLManager sm = new SQLManager();
            CustomerModel cm = _cm[0];
            if (sm.CreateCustomerRec(cm.id, cm.customer, cm.address, cm.phone))
            {
                
            datagridvskm.ItemsSource = null;
            DBManager dm = new DBManager();
            _lcustomer = dm.GetAllCustomers().ToList();
            datagridvskm.ItemsSource = _lcustomer;

                MessageBox.Show("Nýr viðskiptavinur búin til.");

            }
            else
            {
                MessageBox.Show("Villa kom upp við uppfærslu viskiptamannaskrár. Hafðu samband við upsjónarmann.");
            }

            
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void comNew_Click(object sender, RoutedEventArgs e)
        {
            SQLManager sm = new SQLManager();
            _cm = new List<CustomerModel>();
            datagridvskm.ItemsSource = null;
            datagridvskm.ItemsSource = _cm;
            datagridvskm.CanUserAddRows = true;
            datagridvskm.Items.Refresh();
            //datagridvskm.ItemsSource = _lcustomer;
            //datagridvskm.Items.Refresh();
            //if (sm.InportTokCustomers())
            //{
            //    DBManager dm = new DBManager();
            //    _lcustomer = dm.GetAllCustomers().ToList();
            //    datagridvskm.ItemsSource=null;
            //    atagridvskm.ItemsSource = _lcustomer;
            //    datagridvskm.Items.Refresh();
            //    string strLogMessage="Viðskiptamanalisti með "+_lcustomer.Count.ToString()+"viðskiptamönnum uppfærður.";
            //    sm.LogRecCreate(strLogMessage);
            //    MessageBox.Show(strLogMessage);
            //}
            //else
            //{
            //    string strLogMessage="Villa kom fram við innkeyrslu á viðskiptavinum.";
            //    MessageBox.Show(strLogMessage);
            //}
            
        }

        private void txtFilterName_GotFocus(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(datagridvskm.ItemsSource).Refresh();
        }

        private void txtFilterName_LostFocus(object sender, RoutedEventArgs e)
        {
            txtFilterName.Text = string.Empty;
            CollectionViewSource.GetDefaultView(datagridvskm.ItemsSource).Filter = null;
            CollectionViewSource.GetDefaultView(datagridvskm.ItemsSource).Refresh();
            //    CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = null;
            //    CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
            
        }

        private void txtFilterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _CustomerView = CollectionViewSource.GetDefaultView(datagridvskm.ItemsSource);
            _CustomerView.Filter = FilterCustomerName;
        }
        private bool FilterCustomerName(object item)
        {

            if (String.IsNullOrEmpty(txtFilterName.Text))
                return true;
            var cust = (CustomerModel)item;
            if (String.IsNullOrEmpty(cust.customer))
            {
                return false;
            }
            else
            {

                return (cust.customer.StartsWith(txtFilterName.Text.ToString(), StringComparison.OrdinalIgnoreCase));
            }
        }

        private void txtFilterId_GotFocus(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(datagridvskm.ItemsSource).Refresh();
        }

        private void txtFilterId_LostFocus(object sender, RoutedEventArgs e)
        {
            txtFilterId.Text = string.Empty;
            CollectionViewSource.GetDefaultView(datagridvskm.ItemsSource).Filter = null;
            CollectionViewSource.GetDefaultView(datagridvskm.ItemsSource).Refresh();
        }

        private void txtFilterId_TextChanged(object sender, TextChangedEventArgs e)
        {
            _CustomerView = CollectionViewSource.GetDefaultView(datagridvskm.ItemsSource);
            _CustomerView.Filter = FilterCustomerId;
        }

        private bool FilterCustomerId(object item)
        {

            if (String.IsNullOrEmpty(txtFilterId.Text))
                return true;
            var cust = (CustomerModel)item;
            if (String.IsNullOrEmpty(cust.id))
            {
                return false;
            }
            else
            {

                return (cust.id.StartsWith(txtFilterId.Text.ToString(), StringComparison.OrdinalIgnoreCase));
            }
        }

        private void comInportCustomers_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Þessi aðgerð skrifar yfir allan viðskiptamannalistann. Viltu halda áfram.", "Delete Confirmation", System.Windows.MessageBoxButton.OKCancel);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                MessageBox.Show("Viðskiptamannalisti keyrður inn.");
                //SQLManager sm = new SQLManager();
                //if (sm.InportTokCustomers())
                //{
                //    DBManager dm = new DBManager();
                //    _lcustomer = dm.GetAllCustomers().ToList();
                //    datagridvskm.ItemsSource = null;
                //    datagridvskm.ItemsSource = _lcustomer;
                //    datagridvskm.Items.Refresh();
                //    string strLogMessage = "Viðskiptamanalisti með " + _lcustomer.Count.ToString() + "viðskiptamönnum uppfærður.";
                //    sm.LogRecCreate(strLogMessage);
                //    MessageBox.Show(strLogMessage);
                //}
                //else
                //{
                //    string strLogMessage = "Villa kom fram við innkeyrslu á viðskiptavinum.";
                //    MessageBox.Show(strLogMessage);
                //}
            }
        }
        //private void txtFilterNote_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    _turarView = CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource);
        //    _turarView.Filter = FilterNote;
        //    //AllApplications.Where(x => x.Name.ToUpperInvariant().Contains(txtSearch.Text.ToUpperInvariant()))).ToList();
        //    var listtour = _ltour
        //        .Where(x => x != null)
        //        .Where(x => x.tnote != null)
        //        .Where(x => x.tnote.ToUpperInvariant().StartsWith(txtFilterNote.Text.ToString().ToUpperInvariant()));

        //    _ltourfilter = listtour.ToList();
        //    //_ltourfilter = _ltour.Where(x => x.tnote.ToUpperInvariant().Contains(txtFilterNote.Text.ToUpperInvariant())).ToList();

        //}

        //private void txtFilterNote_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    txtFilterNote.Text = string.Empty;
        //    CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Filter = null;
        //    CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        //}

        //private void txtFilterNote_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    CollectionViewSource.GetDefaultView(dataGridTurar.ItemsSource).Refresh();
        //}

        //private bool FilterNote(object item)
        //{

        //    if (String.IsNullOrEmpty(txtFilterNote.Text))
        //        return true;
        //    var tour = (dtoTour)item;
        //    if (String.IsNullOrEmpty(tour.tnote))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return (tour.tnote.StartsWith(txtFilterNote.Text.ToString(), StringComparison.OrdinalIgnoreCase));
        //    }

        //}

    }
}