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

        public winNewTour()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource dtoTourViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dtoTourViewSource")));
            //DBManager dm = new DBManager();
            //ltour = dm.GetAllToursFromFile();
            var window2 = Application.Current.Windows
            .Cast<Window>()
            .FirstOrDefault(window => window is MainWindow) as MainWindow;
            ltour = window2.memListTour;
            dtoTourViewSource.Source = ltour;
           
        }

        private void carToolbarComRecNew_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource dtoTourViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dtoTourViewSource")));
            dtoTourViewSource.Source = ltour;

            int int_id = ltour.FirstOrDefault().id+ltour.Count();
            Debug.WriteLine(int_id.ToString());
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



    }
}