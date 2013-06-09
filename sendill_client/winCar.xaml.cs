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
using System.Data.SqlClient;
using System.Windows.Threading;
using System.Runtime.Caching;
using System.Collections.ObjectModel;

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winCar.xaml
    /// </summary>
    public partial class winCar : Window
    {

        
        //ObservableCollection<dtoCars> lcar = new ObservableCollection<dtoCars>();
        List<dtoCars> ocar = new List<dtoCars>();
        public winCar()
        {
            InitializeComponent();
            var window2 = Application.Current.Windows
           .Cast<Window>()
           .FirstOrDefault(window => window is MainWindow) as MainWindow;
            ocar = window2.memListCar;
            //lcar = lcar.ToObservableCollection();
            //foreach (DataRow dr in dtcars.Rows)
            //{
            //    dtoCars icar = new dtoCars();
            //    if (!dr.IsNull(0)) { icar.id = (int)dr["ID"]; }
            //    if (!dr.IsNull(1)) { icar.stationid = (Int16)dr["stationid"]; }
            //    if (!dr.IsNull(2)) { icar.carnumber = (string)dr["carnumber"]; }
            //    if (!dr.IsNull(3)) { icar.code = (string)dr["code"]; }
            //    if (!dr.IsNull(4)) { icar.listed = (bool)dr["listed"]; }
            //    if (!dr.IsNull(5)) { icar.carname = (string)dr["carname"]; }
            //    if (!dr.IsNull(6)) { icar.car1 = (bool)dr["car1"]; }
            //    if (!dr.IsNull(7)) { icar.car2 = (bool)dr["car2"]; }
            //    if (!dr.IsNull(8)) { icar.car3 = (bool)dr["car3"]; }
            //    if (!dr.IsNull(9)) { icar.car4 = (bool)dr["car4"]; }
            //    if (!dr.IsNull(10)) { icar.car5 = (bool)dr["car5"]; }
            //    if (!dr.IsNull(11)) { icar.car6 = (bool)dr["car6"]; }
            //    if (!dr.IsNull(12)) { icar.car7 = (bool)dr["car7"]; }
            //    if (!dr.IsNull(13)) { icar.car8 = (bool)dr["car8"]; }
            //    if (!dr.IsNull(14)) { icar.car9 = (bool)dr["car9"]; }
            //    if (!dr.IsNull(15)) { icar.car10 = (bool)dr["car10"]; }
            //    if (!dr.IsNull(16)) { icar.length = (double)dr["length"]; }
            //    if (!dr.IsNull(17)) { icar.backdoorlength = (double)dr["back_door_width"]; }
            //    if (!dr.IsNull(18)) { icar.backdoorheight = (double)dr["back_door_height"]; }
            //    if (!dr.IsNull(19)) { icar.sidedoorlength = (double)dr["side_door_width"]; }
            //    if (!dr.IsNull(20)) { icar.sidedoorheight = (double)dr["side_door_height"]; }
            //    if (!dr.IsNull(21)) { icar.weightlimit = (double)dr["weight_limit"]; }
            //    if (!dr.IsNull(22)) { icar.liftsize = (Int16)dr["lift_size"]; }
            //    if (!dr.IsNull(23)) { icar.volume = (double)dr["Volume"]; }
            //    if (!dr.IsNull(24)) { icar.width = (double)dr["width"]; }
            //    if (!dr.IsNull(25)) { icar.model = (string)dr["model"]; } else { icar.model = "na"; }
            //    if (!dr.IsNull(26)) { icar.maxcarry = (double)dr["max_carry"]; }
            //    if (!dr.IsNull(27)) { icar.owner = (string)dr["owner"]; }
            //    if (!dr.IsNull(28)) { icar.kt = (string)dr["kt"]; }
            //    if (!dr.IsNull(29)) { icar.address = (string)dr["address"]; }
            //    if (!dr.IsNull(30)) { icar.town = (string)dr["town"]; }
            //    if (!dr.IsNull(31)) { icar.postcode = (string)dr["postcode"]; }
            //    if (!dr.IsNull(32)) { icar.phone = (string)dr["phone"]; }
            //    if (!dr.IsNull(33)) { icar.mobile = (string)dr["mobile"]; }
            //    if (!dr.IsNull(34)) { icar.driver = (string)dr["driver"]; }
            //    if (!dr.IsNull(35)) { icar.dkt = (string)dr["dkt"]; }
            //    if (!dr.IsNull(36)) { icar.daddress = (string)dr["daddress"]; }
            //    if (!dr.IsNull(37)) { icar.dtown = (string)dr["dtown"]; }
            //    if (!dr.IsNull(38)) { icar.dpostcode = (string)dr["dpostcode"]; }
            //    if (!dr.IsNull(39)) { icar.dphone = (string)dr["dphone"]; }
            //    if (!dr.IsNull(40)) { icar.mobile = (string)dr["dmobile"]; }
            //    if (!dr.IsNull(41)) { icar.heightofbox = (double)dr["heightofbox"]; }
            //    lcar.Add(icar);

            //}
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            mainGrid.DataContext = masterviewsource;
            
        }
            //try
            //{
            //    //using (Stream stream = File.Open(@"C:\dbsendill\list_cars.bin", FileMode.Open))
            //    //{
            //    //    BinaryFormatter bin = new BinaryFormatter();

            //    //    lcar = (List<dtoCars>)bin.Deserialize(stream);
            //    //    dataGridCar.ItemsSource = lcar;


            //    //}
            //    //couchCars cc = new couchCars();
            //    //lcar = cc.LoadCarsFromCouch();
            //    //dataGridCar.ItemsSource = lcar;
            //}
            //catch (IOException)
            //{
            //}
                        
            //       }

        private void comClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream(@"C:\dbsendill\list_cars.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, ocar);
            fs.Close();
            MessageBox.Show("Bílalisti uppfærður");
        }

        private void comSave_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new dbsendillEntities() )
            {
                foreach(var ocars in ocar)
                {
                    var tblcars = new tbl_cars();
                    tblcars.ID = ocars.id;
                    tblcars.address = ocars.address;
                    tblcars.back_door_height = ocars.backdoorheight;
                    tblcars.back_door_width = ocars.backdoorlength;
                    tblcars.car1 = ocars.car1;
                    tblcars.car10 = ocars.car10;
                    tblcars.car2 = ocars.car2;
                    tblcars.car3 = ocars.car3;
                    tblcars.car4 = ocars.car4;
                    tblcars.car5 = ocars.car5;
                    tblcars.car6 = ocars.car6;
                    tblcars.car7 = ocars.car7;
                    tblcars.car8 = ocars.car8;
                    tblcars.car9 = ocars.car9;
                    tblcars.carname = ocars.carname;
                    tblcars.carnumber = ocars.carnumber;
                    tblcars.code = ocars.code;
                    tblcars.daddress = ocars.daddress;
                    tblcars.dkt = ocars.dkt;
                    tblcars.dmobile = ocars.dmobile;
                    tblcars.dphone = ocars.dphone;
                    tblcars.dpostcode = ocars.dpostcode;
                    tblcars.driver = ocars.driver;
                    tblcars.dtown = ocars.dtown;
                    tblcars.heightofbox = ocars.heightofbox;
                    tblcars.kt = ocars.kt;
                    tblcars.length = ocars.length;
                    tblcars.lift_size = ocars.liftsize;
                    tblcars.listed = ocars.listed;
                    tblcars.max_carry = ocars.maxcarry;
                    tblcars.mobile = ocars.mobile;
                    tblcars.model = ocars.model;
                    tblcars.owner = ocars.owner;
                    tblcars.phone = ocars.phone;
                    tblcars.postcode = ocars.postcode;
                    tblcars.side_door_height = ocars.sidedoorheight;
                    tblcars.side_door_width = ocars.sidedoorlength;
                    tblcars.stationid = Convert.ToInt16(ocars.stationid);
                    tblcars.town = ocars.town;
                    tblcars.Volume = ocars.volume;
                    tblcars.weight_limit = ocars.weightlimit;
                    tblcars.width = ocars.width;

                    db.tbl_cars.AddObject(tblcars);
                }
                db.SaveChanges();
                MessageBox.Show("Gagnagrunnur uppfærður");
            }

        }

        private void columnButtonEdit_Click(object sender, RoutedEventArgs e)
        {

            childWinCar.DataContext = dataGridCar.DataContext;
            
            childWinCar.Show();
        }

        private void carToolbarComRecFirst_Click(object sender, RoutedEventArgs e)
        {

            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            masterviewsource.View.MoveCurrentToFirst();
        
        }

        private void carToolbarComRecPrev_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            masterviewsource.View.MoveCurrentToPrevious();
        }

        private void carToolbarComRecNext_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            masterviewsource.View.MoveCurrentToNext();
        }

        private void carToolbarComRecLast_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            masterviewsource.View.MoveCurrentToLast();
        }






    }
    public static class ObservableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> items)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();

            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}

