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

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winCarDetail.xaml
    /// </summary>
    public partial class winCarDetail : Window
    {
        public int pub_icar;
        List<dtoCars> ocar = new List<dtoCars>();
        public winCarDetail()
        {
            InitializeComponent();
            DBManager dbm = new DBManager();
            ocar = dbm.GetAllCars();
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            this.DataContext = masterviewsource;
            comboCarType.ItemsSource = CreateCarGroups();
            comboCarType.DisplayMemberPath = "name";
            comboCarType.SelectedValuePath = "type";

        }



        private void carToolbarComRecFirst_Click(object sender, RoutedEventArgs e)
        {

            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            masterviewsource.View.MoveCurrentToFirst();
        }



        private void carToolbarComRecLast_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            masterviewsource.View.MoveCurrentToLast();
        }



        private void carToolbarComRecPrev_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            masterviewsource.View.MoveCurrentToPrevious();
              if (masterviewsource.View.IsCurrentBeforeFirst)
              {
                  masterviewsource.View.MoveCurrentToLast();
              }
        }



        private void carToolbarComRecNext_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            masterviewsource.Source = ocar;
            masterviewsource.View.MoveCurrentToNext();
            if (masterviewsource.View.IsCurrentAfterLast)
            {
                masterviewsource.View.MoveCurrentToFirst();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource masterviewsource = (CollectionViewSource)this.FindResource("MasterView");
            int i = ocar.FindIndex(l => l.stationid == (Int16?)pub_icar);
            masterviewsource.Source = ocar;
            masterviewsource.View.MoveCurrentToFirst();
            masterviewsource.View.MoveCurrentToPosition(i);
        }

        public class CarGroups
        {
            public string name { get; set; }
            public int type { get; set;  }
        }

        private List<CarGroups> CreateCarGroups()
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void carToolbarComRecSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void carToolbarComRecDelete_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void carToolbarComRecNew_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hér er stofnaður nýr bíll.");
        }

        private void carToolbarComRecNew_Click_1(object sender, RoutedEventArgs e)
        {

        }







    }
}
