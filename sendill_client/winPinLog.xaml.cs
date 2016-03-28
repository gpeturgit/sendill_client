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
using System.IO;

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winPinLog.xaml
    /// </summary>
    public partial class winPinLog : Window
    {
        public winPinLog()
        {
            InitializeComponent();
            //LoadToursToList();
            //LoadCarsToList();
            //LoadLogFromMem();
        }


        private void LoadToursToList()
        {
            try
            {
                DBManager db = new DBManager();
                var memListTour = db.GetAllToursFromFile();
                
            }
            catch (IOException)
            {
            }
        }
        private void LoadCarsToList()
        {
            DBManager db = new DBManager();
            
 
        }

        private void LoadLogFromMem()
        {
             var window2 = Application.Current.Windows
            .Cast<Window>()
            .FirstOrDefault(window => window is MainWindow) as MainWindow;
             var memlist = window2.memListPinStatus;
             dgMain.ItemsSource = memlist;

        }



        private void comNew_Click(object sender, RoutedEventArgs e)
        {
            //LoadLogFromMem();
            SQLManager sm = new SQLManager();
            //sm.LogRecCreate("New row to log added");
            sm.LogRecCount();
            //int imtCount = sm.DapperLogGetAll().Count()+1;
            //dtoLog objLog = new dtoLog();
            //objLog.ID = imtCount;
            //objLog.logtimestamp = DateTime.Now;
            //objLog.logtext = "Búið að ýta á comNew_Click#";
            //string mssg=sm.DapperLogCreate(objLog);
            //MessageBox.Show("Tókst Dopper insert");
            //var mlist = sm.DapperLogGetAll();
            //dgMain.ItemsSource = mlist;


        }

        private void comPrev_Click(object sender, RoutedEventArgs e)
        {
            //SQLManager sm = new SQLManager();
            //var listLog = sm.DapperLogGetAll();
            //dgMain.ItemsSource = listLog;
        }

        private void comNext_Click(object sender, RoutedEventArgs e)
        {
            DBManager dm = new DBManager();
            dgMain.ItemsSource=dm.GetPinStatus();
        }



    }
}
