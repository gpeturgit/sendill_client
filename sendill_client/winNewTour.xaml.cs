﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winNewTour.xaml
    /// </summary>
    public partial class winNewTour : Window
    {
        List<dtoTour> ltour = new List<dtoTour>();
        public bool globl_new_tour;
        public int global_car_id;
        public int global_pin_id;
        bool isOrderedTour;

        public winNewTour()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource dtoTourViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dtoTourViewSource")));

            var window2 = Application.Current.Windows
            .Cast<Window>()
            .FirstOrDefault(window => window is MainWindow) as MainWindow;
            ltour = window2.memListTour;
            MessageBox.Show(globl_new_tour.ToString());
            if (globl_new_tour == true)
            {
                DBManager dm = new DBManager();
                var ocar = dm.GetSingleCarPar_Id(global_car_id);
                dtoTour _ltour = new dtoTour();
                _ltour.id = ltour.Count + 1;
                _ltour.idcar = (int)ocar.id;
                _ltour.idpin = global_pin_id;
                _ltour.carsize = ocar.size;
                ltour.Add(_ltour);
                dtoTourViewSource.Source = ltour;
                dtoTourViewSource.View.MoveCurrentToLast();
                
            }
            else
            {
                MessageBox.Show("Else");
                dtoTourViewSource.Source = ltour;
                this.DataContext = dtoTourViewSource;
                dtoTourViewSource.View.MoveCurrentToLast();
                
            }
            comboCarType.ItemsSource = CreateCarGroups();
            comboCarType.DisplayMemberPath = "name";
            comboCarType.SelectedValuePath = "type";
           
        }

        private void carToolbarComRecNew_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource dtoTourViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dtoTourViewSource")));
            dtoTourViewSource.Source = ltour;

            int int_id = ltour.FirstOrDefault().id+ltour.Count();
            //Debug.WriteLine(int_id.ToString());
            dtoTour obj_tour = new dtoTour();
            obj_tour.id = int_id;
            ltour.Add(obj_tour);
            dtoTourViewSource.View.MoveCurrentToLast();
            var window2 = Application.Current.Windows
            .Cast<Window>()
            .FirstOrDefault(window => window is MainWindow) as MainWindow;
            window2.memListTour = ltour;
        }

        private void carToolbarComRecFirst_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource dtoTourViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dtoTourViewSource")));
            dtoTourViewSource.Source = ltour;
            dtoTourViewSource.View.MoveCurrentToFirst();
        }

        private void carToolbarComRecLast_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource dtoTourViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dtoTourViewSource")));
            dtoTourViewSource.Source = ltour;
            dtoTourViewSource.View.MoveCurrentToLast();
        }

        private void carToolbarComRecNext_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource dtoTourViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dtoTourViewSource")));
            dtoTourViewSource.Source = ltour;
            dtoTourViewSource.View.MoveCurrentToNext();
            if (dtoTourViewSource.View.IsCurrentAfterLast)
            {
                dtoTourViewSource.View.MoveCurrentToFirst();
            }
        }

        private void carToolbarComRecPrev_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource dtoTourViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dtoTourViewSource")));
            dtoTourViewSource.Source = ltour;
            dtoTourViewSource.View.MoveCurrentToPrevious();
            if (dtoTourViewSource.View.IsCurrentBeforeFirst)
            {
                dtoTourViewSource.View.MoveCurrentToLast();
            }
        }

        public class CarGroups
        {
            public string name { get; set; }
            public int type { get; set; }
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
            _ocg.name = "Millistór";
            _cargroup.Add(_ocg);
            _ocg = new CarGroups();
            _ocg.type = 3;
            _ocg.name = "Stór";
            _cargroup.Add(_ocg);

            return _cargroup;
        }

        private void idTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboCarType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void carToolbarComRecSave_Click(object sender, RoutedEventArgs e)
        {

            var ptour = ltour.LastOrDefault();
            var idel = isdelCheckBox.IsChecked.Value;
            if (globl_new_tour == true)
            {
                var window2 = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;
                window2.funcAddNewTour(ptour);
                DBManager dm = new DBManager();
                var rmsg = dm.SaveToursToFile(ptour);
            }
            else
            {
                DBManager dm = new DBManager();
                var rmsg = dm.SaveToursToFile(ptour);
                winTurar wt = new winTurar();
                wt.Show();
            }


            this.Close();

        }



    }
}