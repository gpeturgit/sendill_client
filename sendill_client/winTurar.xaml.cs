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

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winTurar.xaml
    /// </summary>
    public partial class winTurar : Window
    {
        List<dtoCustomer> _lcust = new List<dtoCustomer>();        
        List<dtoTour> _ltour = new List<dtoTour>();
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
                                  select new
                                  {
                                      c.id,
                                      c.name,
                                      c.address,
                                      c.number
                                  };

                    comboCustomer.ItemsSource =_lcust;
                    comboCustomer.SelectedValuePath = "id";


                }
            }
            catch (IOException)
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

                var window2 = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;
                List<dtoTour> ltour = new List<dtoTour>();
                ltour = window2.memListTour;
                dataGridTurar.ItemsSource = ltour;
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
            MessageBox.Show("Túralista hlaðið í minni");
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





        

    }
}

