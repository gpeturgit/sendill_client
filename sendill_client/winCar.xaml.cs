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
        public int global_id_car;
        public bool is_detail_form_call;
        List<dtoCars> ocar = new List<dtoCars>();
        public winCar()
        {

            InitializeComponent();
            var window2 = Application.Current.Windows
           .Cast<Window>()
           .FirstOrDefault(window => window is MainWindow) as MainWindow;
            ocar = window2.memListCar;
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.SortDescriptions.Add(new System.ComponentModel.SortDescription("stationid",System.ComponentModel.ListSortDirection.Ascending));
            masterviewsource.Source = ocar;
            //_rec = memListCar.FirstOrDefault(c => c.id == pid);
            mainGrid.DataContext = masterviewsource;

            comboCarType.ItemsSource = CreateCroup();
            comboCarType.DisplayMemberPath = "name";
            comboCarType.SelectedValuePath = "type";
            

        }

        private void LoadCarDetail()
        {
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.SortDescriptions.Add(new System.ComponentModel.SortDescription("stationid", System.ComponentModel.ListSortDirection.Ascending));


            int i = ocar.FindIndex(l => l.id == (Int16?)global_id_car);
            masterviewsource.Source = ocar;
            mainGrid.DataContext = masterviewsource;
            masterviewsource.View.MoveCurrentToFirst();
            //masterviewsource.View.MoveCurrentToPosition(i-1);
            childWinCar.Show();

        }

        private void comClose_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {

            FileStream fs = new FileStream(@"C:\dbsendill\list_carall.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, ocar);
            fs.Close();
            MessageBox.Show("Bílalisti uppfærður");


        }



        private void comSave_Click(object sender, RoutedEventArgs e)
        {

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

        private void carToolbarComRecNew_Click(object sender, RoutedEventArgs e)
        {
            dtoCars _car = new dtoCars();
            int rid = 1000 + ocar.Count();
                _car.id=rid;
            ocar.Add(_car);
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            masterviewsource.SortDescriptions.Add(new System.ComponentModel.SortDescription("id", System.ComponentModel.ListSortDirection.Ascending));
            masterviewsource.View.MoveCurrentToFirst();

        }

                public class CarGroups
        {
            public string name { get; set; }
            public int type { get; set; }
        }

        private List<CarGroups> CreateCroup()
        {
            List<CarGroups> _cargroup = new List<CarGroups>();
            CarGroups _ocg = new CarGroups();
            _ocg.type = 1;
            _ocg.name = "Lítill";
            _cargroup.Add(_ocg);
            _ocg = new CarGroups();
            _ocg.type = 2;
            _ocg.name = "Milli";
            _cargroup.Add(_ocg);
            _ocg = new CarGroups();
            _ocg.type = 3;
            _ocg.name = "Stór";
            _cargroup.Add(_ocg);

            return _cargroup;
        }

        private void carToolbarComRecSave_Click(object sender, RoutedEventArgs e)
        {
            DBManager dm = new DBManager();
            string spath = dm.GetAppConfigSetting();
            FileStream fs = new FileStream(spath+"list_carall.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, ocar);
            fs.Close();
            MessageBox.Show("Bílalisti uppfærður");
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.SortDescriptions.Add(new System.ComponentModel.SortDescription("stationid", System.ComponentModel.ListSortDirection.Ascending));

            masterviewsource.Source = ocar;
            mainGrid.DataContext = masterviewsource;

        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            winCarDetail wcd = new winCarDetail();
            wcd.pub_icar = 3;
            wcd.Show();
        }

        private void carToolbarComRecDelete_Click(object sender, RoutedEventArgs e)
        {
            int _idcar=Convert.ToInt32(txtId.Text);
            dtoCars _car = ocar.Find(x => x.id.Equals(_idcar));
            ocar.Remove(_car);
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.SortDescriptions.Add(new System.ComponentModel.SortDescription("stationid", System.ComponentModel.ListSortDirection.Ascending));
            masterviewsource.Source = ocar;
            mainGrid.DataContext = masterviewsource;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

     
            {
                LoadCarDetail();
            }
        }
    }




    }


