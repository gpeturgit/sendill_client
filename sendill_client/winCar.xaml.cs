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

        List<dtoCars> ocar = new List<dtoCars>();
        public winCar()
        {

            InitializeComponent();
            var window2 = Application.Current.Windows
           .Cast<Window>()
           .FirstOrDefault(window => window is MainWindow) as MainWindow;
            ocar = window2.memListCar;
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            mainGrid.DataContext = masterviewsource;

        }



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
            using (var db = new dbsendillEntities())
            {
                foreach(var ocars in ocar)
                {
                    var tblcars = new tbl_cars();
                    tblcars.ID = ocars.id;
                    tblcars.address = ocars.address;
                    tblcars.back_door_height = ocars.backdoorheight;
                    tblcars.back_door_width = ocars.backdoorlength;
                    tblcars.car1 = ocars.car1;
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

