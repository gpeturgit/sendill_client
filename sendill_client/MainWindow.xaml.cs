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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Threading;
using System.Runtime.Caching;
using System.Diagnostics;
using System.Timers;
using System.Threading;
using System.ComponentModel;
using System.ServiceProcess;
//using sendill_client.SmsCallbackReference;


namespace sendill_client
{
    
    //Version.2.3 Current dev.
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//, IWCFSmsCallbackCallback
    {
        public DataTable memTableCars;
        public DataTable memTableCustomers;
        public DataTable memTableTurar;
        public bool isStartup;
        public string extDataChange;
        public dtoTour global_dtotour;
        public List<dtoPin> memListPinBak1  =  new List<dtoPin>();
        public List<dtoPin> memListPinBak2 = new List<dtoPin>();
        public List<dtoPin> memListPinBak3 = new List<dtoPin>();
        public List<dtoPin> memListPinBak4 = new List<dtoPin>();
        public List<dtoPin> memListPinBak5 = new List<dtoPin>();
        public List<dtoPin> memListPinBak6 = new List<dtoPin>();
        public List<dtoPin>   memListPin1  =  new List<dtoPin>();
        public List<dtoPin>   memListPin2  =  new List<dtoPin>();
        public List<dtoPin>   memListPin3  =  new List<dtoPin>();
        public List<dtoPin>   memListPin4  =  new List<dtoPin>();
        public List<dtoPin>   memListPin5  =  new List<dtoPin>();
        public List<dtoCars>  memListCar   =  new List<dtoCars>();
        public List<dtoTour>  memListTour  =  new List<dtoTour>();
        public List<dtoTour> memListPin6 = new List<dtoTour>();
        public List<dtoPinStatus> memListPinStatus = new List<dtoPinStatus>();
        public List<dtoPinStatus> memListPinStatusBak = new List<dtoPinStatus>();
        public List<dtoPinStatus> memListPinStatusLog = new List<dtoPinStatus>();
        public List<appSysSettings> memAppSysSettings = new List<appSysSettings>();
        public System.Collections.IList selectedItem;
        public int pin1_id = 0;
        public int pin2_id = 0;
        public int pin3_id = 0;
        public int pin4_id = 0;
        public int pin5_id = 0;
        public int _pinid;
        public int _pinpos;
        public int carid;
        public dtoPinStatus DtoPinStatus;
        public Point startPoint;

        public List<dtoMessage> memListMessage = new List<dtoMessage>();

        // Version 2.4 new

        //private void btnPinOneTakeOff_Click(object sender, RoutedEventArgs e)
        //{
        //    (sender as Button).ContextMenu.IsEnabled = true;
        //    (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
        //    (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
        //    (sender as Button).ContextMenu.IsOpen = true;
        //}


   


        public BackgroundWorker MessageBackgroundWorker;

        public MainWindow()
        {
            //***************************************************************************************
            // VERSION 2.3 MainWindow.xaml.cs MainWindow()
            // Shows latest changes. 
            // 
            // 2.3 21.02.2018 - New
            //***************************************************************************************
            InitializeComponent();

            //LoadCarsToMem();
            //LoadCustomersToMem();
            LoadPropertiesFromConfig();
            System.Windows.Controls.ContextMenu mnuNightService = new System.Windows.Controls.ContextMenu();
            System.Windows.Controls.MenuItem mitem01 = new System.Windows.Controls.MenuItem();
            LoadAppSysSettingsToList();
            mitem01.Click += new RoutedEventHandler(mitem01_Click);
            mnuNightService.Items.Add(mitem01);
            isStartup = true;
            //LoadCarsToList();
            //LoadToursToList();

            global::sendill_client.Properties.Settings.Default.SendillAccConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\dbsendill\\dbsendill.mdb";

            AppStartSqlServerStatus();

            

            // ******* End  *******
        }

        public void LoadMessages()
        {
            DBManager dm = new DBManager();
            var _list = dm.GetAllMessagesFromAzure();
            //dtoMessage m = new dtoMessage();
            //m.id = 1;
            //m.from_id = 1002;
            //m.to_id = 1001;
            //m.message_type = 1;
            //m.send_from_date = DateTime.Now;
            //m.send_to_date = DateTime.Now;
            //m.delivered = false;
            //m.message_text ="Viðskiptamenn";
            //memListMessage.Add(m);
            listMessageOnPin.ItemsSource = _list;

        }

        private void btnMessageOnPin_Click(object sender, RoutedEventArgs e)
        {
            //(sender as Button).ContextMenu.IsEnabled = true;
            //(sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            //(sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            //(sender as Button).ContextMenu.IsOpen = true;
            Button b = sender as Button;
            int id = (int)b.CommandParameter;
            //ListBoxItem lbi = (ListBoxItem)listMessageOnPin.ItemContainerGenerator.ContainerFromIndex(0);
            //lbi.IsSelected = true;
            //int i = listMessageOnPin.SelectedIndex;
            //int ic = listMessageOnPin.Items.Count;
            //memListPinBak5 = (from lm in memListPin5 select lm).ToList();
            //memListPinStatusBak = (from lbak in memListPinStatus select lbak).ToList();

            var messageitem = (from lp in memListMessage
            where lp.id == id
            select lp).FirstOrDefault();
            MessageBox.Show("The message id is "+id.ToString());
            winDataUpdateMessage _win = new winDataUpdateMessage();
            _win.p_id = id;
            
            _win.ShowDialog();

        }

        public void QueueMessageCheckTimerStart()
        {
            DispatcherTimer dispatcherQueueMessageTimer = new DispatcherTimer();
            dispatcherQueueMessageTimer.Tick += DispatcherQueueMessageTimer_Tick;
            dispatcherQueueMessageTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherQueueMessageTimer.Start();


        }

        private void DispatcherQueueMessageTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>    

        /// Background Worker for checking for updates in the messages table on Azure.     

        /// Backgrond worker.    

        /// </summary>
        public void MessageWorkerCreate()
        {
            MessageBackgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,

                WorkerSupportsCancellation = true
            };

            //Event creation.    

            //For the performing operation in the background.    

            MessageBackgroundWorker.DoWork += MessageBackgroundWorker_DoWork1;

            //For the display of operation progress to UI.    

            MessageBackgroundWorker.ProgressChanged += MessageBackgroundWorker_ProgressChanged;

            //After the completation of operation.    

            MessageBackgroundWorker.RunWorkerCompleted += MessageBackgroundWorker_RunWorkerCompleted;

            MessageBackgroundWorker.RunWorkerAsync("Press Enter in the next 5 seconds to Cancel operation:");



        }

        private void MessageBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MessageBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>    

        /// Performs operation in the background.    

        /// </summary>    

        /// <param name="sender"></param>    

        /// <param name="e"></param>

        private void MessageBackgroundWorker_DoWork1(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MessageBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }



        //***************************************************************************************
        // VERSION 2.3 MainWindow.xaml.cs btnSystemServices_Click
        // Runs batch script to start sql server.
        // 
        // 2.3 21.02.2018 - New
        //***************************************************************************************

        private void btnSystemServices_Click(object sender, RoutedEventArgs e)
        {
            ServiceController sc = new ServiceController("MSSQLSERVER", "DESKTOP-7O4PAK0");
            if (sc.Status.Equals(ServiceControllerStatus.Running))
            {
                MessageBox.Show("Gagnagrunnurin er í gangi.");
            }
            else
            {
                AppStartSqlServerBatRun();
                image5.Source = new BitmapImage(new Uri("Images/dataconn_open.png", UriKind.Relative));
                AppStartLoadLists();
            }
            
            
        }
        // ******* End  *******


        //***************************************************************************************
        // VERSION 2.3 MainWindow.xaml.cs AppStartLoadLists()
        // Loads CarsList and ToursList from file into mem.
        // 
        // 2.3 21.02.2018 - New
        //***************************************************************************************
        public void AppStartLoadLists()
        {
            LoadCarsToList();
            LoadToursToList();
            LoadMessages();
        }
        // ******* End  *******


        //***************************************************************************************
        // VERSION 2.3 MainWindow.xaml.cs AppStartSqlServerStatus()
        // Check ef the SqlServir is running. Remembur to put parameters in a config file later. 
        // To Do - Set a alert image on message box.
        // 2.3 21.02.2018 - New
        //***************************************************************************************

        public void AppStartSqlServerStatus()
        {
            //Breyta parametrum við uppsettningu.
            ServiceController sc = new ServiceController("MSSQLSERVER", "DESKTOP-7O4PAK0");
            if (sc.Status.Equals(ServiceControllerStatus.Running))
            {
                sc.Close();
                AppStartLoadLists();
            }
            else
            {
                image5.Source = new BitmapImage(new Uri("Images/dataconn_off.png", UriKind.Relative));
                MessageBox.Show("Gagnagrunnurinn er ekki í gangi. Það þarf að ræsa hann.");
            }
        }

        // ******* End  *******


        //***************************************************************************************
        // VERSION 2.3 MainWindow.xaml.cs 
        // Start SqlServer instance from a batch file
        // To Do - Put parameters in a config file. 
        // To Do - Put exceptions in a config file. 
        // 2.3 21.02.2018 - New
        //***************************************************************************************

        public void AppStartSqlServerBatRun()
        {
            string bat = @"C:\sendill\sqlstart.bat";
            var psi = new ProcessStartInfo();
            psi.CreateNoWindow = true;
            psi.FileName = @"cmd.exe";
            psi.Verb = "runas";
            psi.Arguments = "/C " + bat;
            try
            {
                var process = new Process();
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception)
            {
                MessageBox.Show("Villa kom upp í ræsingu gagnagrunns. Hafið samband við þjónustuaðla.");
            }
        }
        // ******* Update Ends *******
        public void LoadPropertiesFromConfig()
        {
            //Load ListProperties.
            ucPin uc = new ucPin();
            DataTemplate dt_temp = this.FindResource("CarOnPin") as DataTemplate;
            DataTemplate dt_temp20 = this.FindResource("CarOnPin20") as DataTemplate;
            ConfigFile cf= new ConfigFile();
            TextBox[] arrlistheader = new TextBox[5];
            arrlistheader[0]=txtList1;
            arrlistheader[1]=txtList2;
            arrlistheader[2]=txtList3;
            arrlistheader[3]=txtList4;
            arrlistheader[4]=txtList5;
            ListBox[] arrListBox = new ListBox[5];
            arrListBox[0] = lboxPin1;
            arrListBox[1] = lboxPin2;
            arrListBox[2] = lboxPin3;
            arrListBox[3] = lboxPin4;
            arrListBox[4] = lboxPin5;
            var list = cf.GetAreaList();
            int i = 0;
            foreach (dtoListConfig dlc in list)
            {
                arrlistheader[i].Text = dlc.header;

                if (dlc.tmpldata=="CarOnPin")
                {
                    arrListBox[i].ItemTemplate = dt_temp;
                }
                else if (dlc.tmpldata=="CarOnPin20")
                {
                    arrListBox[i].ItemTemplate = dt_temp20;
                }
                i++;
            }            
        }

        public void SmsEvent(string Message)
        {
            string _carnum;

            _carnum = Message;

                try
                {
                    int tmp = Convert.ToInt32(_carnum);
                }
                catch (Exception h)
                {
                    txtArea01.Clear();
                    txtArea01.Text = "";
                }
                    int lcar = (from lc in memListCar
                                where lc.stationid == Convert.ToInt32(_carnum)
                                select lc).Count();
                    int caronpin = (from lps in memListPinStatus
                                    where lps.carid == Convert.ToInt32(_carnum)
                                    select lps).Count();
                    if (caronpin == 1)
                    {
                        int i = (from lps in memListPinStatus
                                 where lps.carid == Convert.ToInt32(_carnum)
                                 select lps).FirstOrDefault().pinid;
                        if (i == 5)
                        {
                            dtoPinStatus _status = (from lps in memListPinStatus
                                                    where lps.carid == Convert.ToInt32(_carnum)
                                                    select lps).FirstOrDefault();
                            memListPinStatus.Remove(_status);
                            dtoTour _plist = (from t in memListPin6
                                              where t.idcar == Convert.ToInt32(_carnum)
                                              select t).FirstOrDefault();
                            memListPin6.Remove(_plist);
                            listMessageOnPin.DataContext = memListPin6;
                            listMessageOnPin.Items.Refresh();
                            caronpin = 0;

                            SQLManager sm = new SQLManager();
                            string strLogMessage = SavePin6StatusToFile();
                            sm.LogRecCreate(strLogMessage);
                            strLogMessage = "Bíll númer " + _carnum + " tekinn af túralista.";
                            sm.LogRecCreate(strLogMessage);

                        }
                    }
                    if (caronpin == 0)
                    {
                        if (lcar == 1)
                        {
                            var carlist = from vlcar in memListCar
                                          where vlcar.stationid == Convert.ToInt32(_carnum)
                                          select vlcar;
                            pin1_id = pin1_id + 1;
                            dtoPin pin1 = new dtoPin();
                            pin1.id = pin1_id;
                            pin1.idcar = Convert.ToInt32(_carnum);
                            pin1.idpin = 0;
                            pin1.pbreak = false;
                            pin1.pcarcode = carlist.FirstOrDefault().code;
                            pin1.owner = carlist.FirstOrDefault().owner;
                            pin1.carsize = carlist.FirstOrDefault().size;
                            pin1.car1 = carlist.FirstOrDefault().car1;
                            pin1.car2 = carlist.FirstOrDefault().car2;
                            pin1.car3 = carlist.FirstOrDefault().car3;
                            pin1.car4 = carlist.FirstOrDefault().car4;
                            pin1.car5 = carlist.FirstOrDefault().car5;
                            pin1.car6 = carlist.FirstOrDefault().car6;
                            pin1.car7 = carlist.FirstOrDefault().car7;
                            memListPin1.Add(pin1);

                            //Settur status inní lista yfir status

                            dtoPinStatus pinStatus = new dtoPinStatus();
                            pinStatus.carid = pin1.idcar;
                            pinStatus.pinid = pin1.idpin;
                            pinStatus.pinpos = memListPin1.Count - 1;
                            memListPinStatus.Add(pinStatus);

                            SQLManager sm = new SQLManager();
                            string strLogMessage = SavePinStatusToFile();
                            sm.LogRecCreate(strLogMessage);
                            strLogMessage = "Bíll númer " + pinStatus.carid + " settur á stöðulista yfir nálar.";
                            sm.LogRecCreate(strLogMessage);
                            strLogMessage = SavePin1StatusToFile();
                            sm.LogRecCreate(strLogMessage);
                            strLogMessage = "Bíll númer " + pinStatus.carid + " settur á Stöðvarnálina.";
                            sm.LogRecCreate(strLogMessage);
                            lboxPin1.DataContext = memListPin1;
                            lboxPin1.Items.Refresh();

                        }
                        else
                        {
                            MessageBox.Show("Þeð er enginn bíll með þetta stöðvarnúmer. Reyndu aftur.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Þessi bíll er á nál.");
                    }
                
        }

        public void CallSmsService()
        {
            //System.ServiceModel.InstanceContext IC = new System.ServiceModel.InstanceContext(this);
            //WCFSmsCallbackClient client = new WCFSmsCallbackClient(IC);
            //client.OpenSession("3");

        }



        public void OnSettingExecute()
        {
            DBManager dm = new DBManager();
            appSysSettings settings = memAppSysSettings.Find(item => item.CODE == "APPSTATUS");
            var dialogResult = MessageBox.Show("Viltu fá geymda stöðu á nálum.", "Upphafsstaða á nálum.",MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LoadPinStatusToList();
                memListPin1 = dm.LoadPin1FromFile();
                lboxPin1.ItemsSource = memListPin1;
                lboxPin1.Items.Refresh();
                memListPin2 = dm.LoadPin2FromFile();
                lboxPin2.ItemsSource = memListPin2;
                memListPin3 = dm.LoadPin3FromFile();
                lboxPin3.ItemsSource = memListPin3;
                memListPin4 = dm.LoadPin4FromFile();
                lboxPin4.ItemsSource = memListPin4;
                memListPin5 = dm.LoadPin5FromFile();
                lboxPin5.ItemsSource = memListPin5;
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                SQLManager sm = new SQLManager();
                string message = SaveAppSysRunSettings();
                sm.LogRecCreate(message);
                //LoadPinStaPtusToList();
                //LoadPin1StatusToList();
                //lboxPin1.ItemsSource = memListPin1;
                memListPin1.Clear();
                memListPin2.Clear();
                memListPin3.Clear();
                memListPin4.Clear();
                memListPin5.Clear();
                memListPin6.Clear();
                memListPinStatus.Clear();
                SavePin1StatusToFile();
                SavePin2StatusToFile();
                SavePin3StatusToFile();
                SavePin4StatusToFile();
                SavePin5StatusToFile();
                SavePin6StatusToFile();
                SavePinStatusToFile();
            }
            //if(settings.VALUE=="SHUTDOWN")
            //{ 
            //    //settings.VALUE = "RUNNING";
            //    settings.VALUE = "RUNNING";
            //    string message = SaveAppSysRunSettings();
            //    SQLManager sm = new SQLManager();
            //    sm.LogRecCreate(message);
            //    //LoadPinStatusToList();
            //    //LoadPin1StatusToList();
            //    //lboxPin1.ItemsSource = memListPin1;
            //    memListPin1.Clear();
            //    memListPin2.Clear();
            //    memListPin3.Clear();
            //    memListPin4.Clear();
            //    memListPin5.Clear();
            //    memListPin6.Clear();
            //    memListPinStatus.Clear();
            //    SavePin1StatusToFile();
            //    SavePin2StatusToFile();
            //    SavePin3StatusToFile();
            //    SavePin4StatusToFile();
            //    SavePin5StatusToFile();
            //    SavePin6StatusToFile();
            //    SavePinStatusToFile();
            //}
            //else
            //{
            //    LoadPinStatusToList();

            //    //LoadPin1StatusToList();
            //    //lboxPin1.ItemsSource = memListPin1;
            //    memListPin1 = dm.LoadPin1FromFile();
            //    lboxPin1.ItemsSource = memListPin1;
            //    memListPin2 = dm.LoadPin2FromFile();
            //    lboxPin2.ItemsSource = memListPin2;
            //    memListPin3 = dm.LoadPin3FromFile();
            //    lboxPin3.ItemsSource = memListPin3;
            //    memListPin4 = dm.LoadPin4FromFile();
            //    lboxPin4.ItemsSource = memListPin4;
            //    memListPin5 = dm.LoadPin5FromFile();
            //    lboxPin5.ItemsSource = memListPin5;

            //}
            
        }

        public void OnTimer(Object source, ElapsedEventArgs e)
        {
            //DBManager dm = new DBManager();
            //dm.memListPin = memListPin1;
            //string rv1 = dm.SavePin1StatusToFile();
            //Debug.WriteLine(rv1);

            //FileStream fst2 = new FileStream(@"C:\dbsendill\list_pin2.bin", FileMode.Create);

            //BinaryFormatter bff2 = new BinaryFormatter();
            //bff2.Serialize(fst2, memListPin2);
            //fst2.Close();
            //string rvalue2 = " memListPin2 uppfærður";
            //Debug.WriteLine(rvalue2);


           // FileStream fst3 = new FileStream(@"C:\dbsendill\list_pin3.bin", FileMode.Create);

           // BinaryFormatter bff3 = new BinaryFormatter();
           // bff3.Serialize(fst3, memListPin3);
           // fst3.Close();
           // string rvalue3 = " memListPin3 uppfærður";
           // Debug.WriteLine(rvalue3);

           // FileStream fst4 = new FileStream(@"C:\dbsendill\list_pin4.bin", FileMode.Create);

           // BinaryFormatter bff4 = new BinaryFormatter();
           // bff4.Serialize(fst4, memListPin4);
           // fst4.Close();
           // string rvalue4 = " memListPin4 uppfærður";
           // Debug.WriteLine(rvalue4);

           // FileStream fst5 = new FileStream(@"C:\dbsendill\list_pin5.bin", FileMode.Create);

           // BinaryFormatter bff5 = new BinaryFormatter();
           // bff5.Serialize(fst5, memListPin5);
           // fst5.Close();
           // string rvalue5 = " memListPin5 uppfærður";
           // Debug.WriteLine(rvalue5);

           //FileStream fst6 = new FileStream(@"C:\dbsendill\list_PinStatus.bin", FileMode.Create);

           // BinaryFormatter bff6 = new BinaryFormatter();
           // bff6.Serialize(fst6, memListPinStatus);
           // fst6.Close();
           // string rvalue6 = " memListPinStatus uppfærður";
           // Debug.WriteLine(rvalue6);
        }

        #region local-Couch
        //private void image7_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    PopupLocalUpdateWindow.Show();
        //}
        #endregion
        #region leftmenu
        private void image2_MouseEnter(object sender, MouseEventArgs e)
        {

            Uri uri = new Uri("Images/tours_128_over.png", UriKind.Relative);
            ImageSource imgSource = new BitmapImage(uri);

            imgTours.Source = imgSource;

        }

        private void image2_MouseLeave(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Clipboard_blue_1281.png", UriKind.Relative);


            ImageSource imgSource = new BitmapImage(uri);

            imgTours.Source = imgSource;
        }

        private void image1_ImageFailed_1(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void image1_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/car_128_over.png", UriKind.Relative);


            ImageSource imgSource = new BitmapImage(uri);

            image1.Source = imgSource;
        }

        private void image1_MouseLeave(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/Truck128.png", UriKind.Relative);

            ImageSource imgSource = new BitmapImage(uri);

            image1.Source = imgSource;
        }

        private void limage1_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/customers_128_over.png", UriKind.Relative);


            ImageSource imgSource = new BitmapImage(uri);

            limage1.Source = imgSource;
        }

        private void limage1_MouseLeave(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/UserDarkBlue1281.png", UriKind.Relative);


            ImageSource imgSource = new BitmapImage(uri);

            limage1.Source = imgSource;
        }

        private void limage2_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/settings_128_over.png", UriKind.Relative);


            ImageSource imgSource = new BitmapImage(uri);

            limage2.Source = imgSource;
        }

        private void limage2_MouseLeave(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/SettingsDarkBlue.png", UriKind.Relative);


            ImageSource imgSource = new BitmapImage(uri);

            limage2.Source = imgSource;
        }

        private void image6_MouseEnter(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/print_128_over.png", UriKind.Relative);
            ImageSource imgSource = new BitmapImage(uri);
            image6.Source = imgSource;
        }

        private void image6_MouseLeave(object sender, MouseEventArgs e)
        {
            Uri uri = new Uri("Images/print_128.png", UriKind.Relative);
            ImageSource imgSource = new BitmapImage(uri);
            image6.Source = imgSource;
        }

        private void limage2_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            winCar _wincar = new winCar();
            _wincar.ShowDialog();
        }

        private void imgTours_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            winTurar _winturar = new winTurar();
            _winturar.p_caridfilter = false;
            _winturar.ShowDialog();
        }

        private void limage1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            winCustomer _wincustomer = new winCustomer();
            _wincustomer.ShowDialog();
        }

        private void imgTours_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            winTurar _winturar = new winTurar();
            _winturar.ShowDialog();
        }

        #endregion

 //Nálar Main forrit

        #region Nálar - Stöðvarlistar fyrir bíla

//Senda bíl í túr.

        private void RemoveCarFrmArea01(string sidcar)
        {

        }

        #region Allar nálar - ListItem events

        public void MemListRestore(int pin_id)
        {
            try
            {
                DBManager dm = new DBManager();
                if (pin_id == 0)
                {
                    memListPin1 = memListPinBak1;
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();
                    SavePin1StatusToFile();
                    var listsel = from c in memListPinStatus
                                  where c.pinid == 0
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos >= DtoPinStatus.pinpos)
                        {
                            item.pinpos = item.pinpos + 1;
                        }
                    }

                    memListPinStatus.Add(DtoPinStatus);
                }
                if (pin_id == 1)
                {
                    memListPin2 = memListPinBak2;
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();

                    var listsel = from c in memListPinStatus
                                  where c.pinid == 1
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos >= DtoPinStatus.pinpos)
                        {
                            item.pinpos = item.pinpos + 1;
                        }
                    }

                    memListPinStatus.Add(DtoPinStatus);

                }
                if (pin_id == 2)
                {
                    memListPin3 = memListPinBak3;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();

                    var listsel = from c in memListPinStatus
                                  where c.pinid == 2
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos >= DtoPinStatus.pinpos)
                        {
                            item.pinpos = item.pinpos + 1;
                        }
                    }
                    memListPinStatus.Add(DtoPinStatus);

                }
                if (pin_id == 3)
                {
                    memListPin4 = memListPinBak4;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();

                    var listsel = from c in memListPinStatus
                                  where c.pinid == 3
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos >= DtoPinStatus.pinpos)
                        {
                            item.pinpos = item.pinpos + 1;
                        }
                    }
                    memListPinStatus.Add(DtoPinStatus);

                }
                if (pin_id == 4)
                {

                    memListPin5 = memListPinBak5;
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();
                    var listsel = from c in memListPinStatus
                                  where c.pinid == 4
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos >= DtoPinStatus.pinpos)
                        {
                            item.pinpos = item.pinpos + 1;
                        }
                    }
                    memListPinStatus.Add(DtoPinStatus);
                }
            }
            catch
            {

            }
        }


        private void btnSendToTour_Click(object sender, RoutedEventArgs e)
        {
        try
            {
                string logmsg;
                DBManager dm = new DBManager();
                SQLManager sqm = new SQLManager();
                dtoPin sitem = new dtoPin();
                Button b = sender as Button;
                int id = (int)b.CommandParameter;
                
                var ops = (from mpin in memListPinStatus
                              where mpin.carid == id
                              select mpin).FirstOrDefault();
                var pinnum = ops.pinid;
                DtoPinStatus = ops;
                _pinpos = ops.pinpos;
                if (pinnum==0)
                {
                  ListBoxItem  lbi = (ListBoxItem)lboxPin1.ItemContainerGenerator.ContainerFromIndex(0);
                  lbi.IsSelected = true;
                  int i = lboxPin1.SelectedIndex;
                  int ic = lboxPin1.Items.Count;
                  memListPinBak1 = (from lm in memListPin1 select lm).ToList();
                  memListPinStatusBak = (from lbak in memListPinStatus select lbak).ToList();
                  sitem = (from lp in memListPin1
                          where lp.idcar==id
                          select lp).FirstOrDefault();
                  memListPin1.Remove(sitem);
                  lboxPin1.DataContext = memListPin1;
                  lboxPin1.Items.Refresh();
                  memListPinStatus.Remove(ops);
                  //ops.pinid = 5;
                  //ops.pinpos = memListPin6.Count;
                  //memListPinStatus.Add(ops);
                  //logmsg = SavePinStatusToFile();
                  //sqm.LogRecCreate(logmsg);
                  //memListPinStatusLog.Add(ops);
                  logmsg = "Bíll númer " + ops.carid.ToString() + " sendur i túr.";
                  sqm.LogRecCreate(logmsg);
                  logmsg = SavePin1StatusToFile();
                  sqm.LogRecCreate(logmsg);
                  var listsel = from c in memListPinStatus
                               where c.pinid ==0
                               select c;
                  foreach (var item in listsel)
                  {
                      if(item.pinpos>ops.pinpos)
                      {
                          item.pinpos = item.pinpos - 1;
                      }
                  }
                  logmsg = SavePinStatusToFile();
                  sqm.LogRecCreate(logmsg);
                }
                if (pinnum==1)
                {
                    ListBoxItem lbi = (ListBoxItem)lboxPin2.ItemContainerGenerator.ContainerFromIndex(0);
                    lbi.IsSelected = true;
                    int i = lboxPin2.SelectedIndex;
                    int ic = lboxPin2.Items.Count;
                    memListPinBak2 = (from lm in memListPin2 select lm).ToList();
                    memListPinStatusBak = (from lbak in memListPinStatus select lbak).ToList();
                    sitem = (from lp in memListPin2
                             where lp.idcar == id
                             select lp).FirstOrDefault();
                    memListPin2.Remove(sitem);
                    lboxPin2.DataContext = memListPin2;
                    lboxPin2.Items.Refresh();
                    memListPinStatus.Remove(ops);
                    //ops.pinid = 5;
                    //ops.pinpos = memListPin6.Count;
                    //memListPinStatus.Add(ops);
                    //memListPinStatusLog.Add(ops);
                    //logmsg = SavePinStatusToFile();
                    //sqm.LogRecCreate(logmsg);
                    //memListPinStatusLog.Add(ops);
                    logmsg = "Bíll númer " + ops.carid.ToString() + " sendur i túr.";
                    sqm.LogRecCreate(logmsg);
                    logmsg = SavePin2StatusToFile();
                    sqm.LogRecCreate(logmsg);
                    var listsel = from c in memListPinStatus
                                  where c.pinid == 1
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos > ops.pinpos)
                        {
                            item.pinpos = item.pinpos - 1;
                        }
                    }
                    logmsg = SavePinStatusToFile();
                    sqm.LogRecCreate(logmsg);

                }
                if (pinnum == 2)
                {

                    ListBoxItem lbi = (ListBoxItem)lboxPin3.ItemContainerGenerator.ContainerFromIndex(0);
                    lbi.IsSelected = true;
                    int i = lboxPin3.SelectedIndex;
                    int ic = lboxPin3.Items.Count;
                    memListPinBak3 = (from lm in memListPin3 select lm).ToList();
                    memListPinStatusBak = (from lbak in memListPinStatus select lbak).ToList();
                    sitem = (from lp in memListPin3
                             where lp.idcar == id
                             select lp).FirstOrDefault();


                    memListPin3.Remove(sitem);
                    lboxPin3.DataContext = memListPin3;
                    lboxPin3.Items.Refresh();
                    memListPinStatus.Remove(ops);
                    //ops.pinid = 5;
                    //ops.pinpos = memListPin6.Count;
                    //memListPinStatus.Add(ops);
                    //memListPinStatusLog.Add(ops);
                    //logmsg = SavePinStatusToFile();
                    //sqm.LogRecCreate(logmsg);
                    //memListPinStatusLog.Add(ops);

                    logmsg = "Bíll númer " + ops.carid.ToString() + " sendur i túr.";
                    sqm.LogRecCreate(logmsg);
                    logmsg = SavePin3StatusToFile();
                    sqm.LogRecCreate(logmsg);
                    var listsel = from c in memListPinStatus
                                  where c.pinid == 2
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos > ops.pinpos)
                        {
                            item.pinpos = item.pinpos - 1;
                        }
                    }
                    logmsg = SavePinStatusToFile();
                    sqm.LogRecCreate(logmsg);

                }
                if (pinnum == 3)
                {
                    ListBoxItem lbi = (ListBoxItem)lboxPin4.ItemContainerGenerator.ContainerFromIndex(0);
                    lbi.IsSelected = true;
                    int i = lboxPin4.SelectedIndex;
                    int ic = lboxPin4.Items.Count;
                    memListPinBak4 = (from lm in memListPin4 select lm).ToList();
                    memListPinStatusBak = (from lbak in memListPinStatus select lbak).ToList();
                    
                    
                    sitem = (from lp in memListPin4
                             where lp.idcar == id
                             select lp).FirstOrDefault();
                    memListPin4.Remove(sitem);
                    lboxPin4.DataContext = memListPin4;
                    lboxPin4.Items.Refresh();
                    memListPinStatus.Remove(ops);
                    //ops.pinid = 5;
                    //ops.pinpos = memListPin6.Count;
                    //memListPinStatus.Add(ops);
                    //memListPinStatusLog.Add(ops);
                    //logmsg = SavePinStatusToFile();
                    //sqm.LogRecCreate(logmsg);
                    //memListPinStatusLog.Add(ops);

                    logmsg = "Bíll númer " + ops.carid.ToString() + " sendur i túr.";
                    sqm.LogRecCreate(logmsg);
                    logmsg = SavePin4StatusToFile();
                    sqm.LogRecCreate(logmsg);
                    var listsel = from c in memListPinStatus
                                  where c.pinid == 3
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos > ops.pinpos)
                        {
                            item.pinpos = item.pinpos - 1;
                        }
                    }
                    logmsg = SavePinStatusToFile();
                    sqm.LogRecCreate(logmsg);

                }
                if (pinnum == 4)
                {
                    ListBoxItem lbi = (ListBoxItem)lboxPin5.ItemContainerGenerator.ContainerFromIndex(0);
                    lbi.IsSelected = true;
                    int i = lboxPin5.SelectedIndex;
                    int ic = lboxPin5.Items.Count;
                    memListPinBak5 = (from lm in memListPin5 select lm).ToList();
                    memListPinStatusBak = (from lbak in memListPinStatus select lbak).ToList();

                    sitem = (from lp in memListPin5
                             where lp.idcar == id
                             select lp).FirstOrDefault();
                    memListPin5.Remove(sitem);
                    lboxPin5.DataContext = memListPin5;
                    lboxPin5.Items.Refresh();
                    memListPinStatus.Remove(ops);
                    //ops.pinid = 5;
                    //ops.pinpos = memListPin6.Count;
                    // memListPinStatus.Add(ops);
                    //memListPinStatusLog.Add(ops);
                    //logmsg = SavePinStatusToFile();
                    //sqm.LogRecCreate(logmsg);
                    //memListPinStatusLog.Add(ops);

                    logmsg = "Bíll númer " + ops.carid.ToString() + " sendur i túr.";
                    sqm.LogRecCreate(logmsg);
                    logmsg = SavePin5StatusToFile();
                    sqm.LogRecCreate(logmsg);
                    var listsel = from c in memListPinStatus
                                  where c.pinid == 4
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos > ops.pinpos)
                        {
                            item.pinpos = item.pinpos - 1;
                        }
                    }
                    logmsg = SavePinStatusToFile();
                    sqm.LogRecCreate(logmsg);
                }
                
                dtoTour _ltour = new dtoTour();
                _ltour.id = memListTour.Count + 1;
                _ltour.idcar = sitem.idcar;
                _ltour.idpin = sitem.idpin;
                _ltour.carsize = sitem.carsize;
                memListTour.Add(_ltour);

                winNewTour _editTour = new winNewTour();
                _editTour.global_car_id = id;
                _editTour.globl_new_tour = true;
                _editTour.global_pin_id = pinnum;
                _editTour.DtoTour = _ltour;
                _editTour.ShowDialog();
            }

            catch (Exception ex)
            {
                string strEx = ex.ToString();
                SQLManager sm = new SQLManager();
                dtoLog obj_log = new dtoLog();
                obj_log.ID = sm.LogRecCount();
                obj_log.logtimestamp = DateTime.Now;
                obj_log.logtext = "ERROR þegar verið er að senda bíl í túr.  " + ex.ToString();
                sm.LogRecCreate(obj_log.logtext.ToString());

            }
        }


        private void btnCoffeebreak_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string logmsg;
                int intPinId;
                Button b = sender as Button;
                int p_id = (int)b.CommandParameter;
                //U04-1 Eftirfarandi línur teknar út - private void btnCoffeebreak_Click(object sender, RoutedEventArgs e)
                //var pinnum = (from mpin in memListPinStatus
                //              where mpin.carid == id
                //              select mpin).FirstOrDefault().pinid;
                //var pinpos = (from mpin in memListPinStatus
                //              where mpin.carid == id
                //              select mpin).FirstOrDefault().pinpos;
                //var pinitem = (from mpin in memListPinStatus
                //               where mpin.carid == id
                //               select mpin).FirstOrDefault();
                //U04-2 Eftirfarandi línum bætt við - private void btnCoffeebreak_Click(object sender, RoutedEventArgs e)
                var pinList1 = (from m1 in memListPin1
                                where m1.idcar == p_id
                                select m1);
                if (pinList1.Count() == 0)
                {
                    var pinList2 = (from m2 in memListPin2
                                    where m2.idcar == p_id
                                    select m2);
                    if (pinList2.Count() == 0)
                    {
                        var pinList3 = (from m3 in memListPin3
                                        where m3.idcar == p_id
                                        select m3);
                        if (pinList3.Count() == 0)
                        {
                            var pinList4 = (from m4 in memListPin4
                                            where m4.idcar == p_id
                                            select m4);
                            if (pinList4.Count() == 0)
                            {
                                var pinList5 = (from m5 in memListPin5
                                                where m5.idcar == p_id
                                                select m5);
                                var _pin5 = pinList5.FirstOrDefault();
                                if (_pin5.pbreak == true)
                                {
                                    _pin5.pbreak = false;
                                }
                                else
                                {
                                    _pin5.pbreak = true;
                                }
                                lboxPin5.Items.Refresh();
                            }
                            else
                            {
                                var _pin4 = pinList4.FirstOrDefault();
                                if (_pin4.pbreak == true)
                                {
                                    _pin4.pbreak = false;
                                }
                                else
                                {
                                    _pin4.pbreak = true;
                                }
                                lboxPin4.Items.Refresh();
                            }
                        }
                        else
                        {
                            var _pin3 = pinList3.FirstOrDefault();
                            if (_pin3.pbreak == true)
                            {
                                _pin3.pbreak = false;
                            }
                            else
                            {
                                _pin3.pbreak = true;
                            }
                            lboxPin3.Items.Refresh();
                        }
                    }
                    else
                    {
                        var _pin2 = pinList2.FirstOrDefault();
                        if (_pin2.pbreak == true)
                        {
                            _pin2.pbreak = false;
                        }
                        else
                        {
                            _pin2.pbreak = true;
                        }
                        lboxPin2.Items.Refresh();
                    }
                }
                else
                {
                    var _pin1 = pinList1.FirstOrDefault();
                    if (_pin1.pbreak == true)
                    {
                        _pin1.pbreak = false;
                    }
                    else
                    {
                        _pin1.pbreak = true;
                    }
                    lboxPin1.Items.Refresh();
                }

                //U04-3 Eftirfarandi línur kverfa úr - private void btnCoffeebreak_Click(object sender, RoutedEventArgs e)

                //if (pinnum == 0)
                //{
                //    var _pin = lboxPin1.Items[pinitem.pinpos] as dtoPin;
                //    if (_pin.pbreak == true)
                //    {
                //        _pin.pbreak = false;
                //        pinitem.onbreak = false;
                //    }
                //    else
                //    {
                //        _pin.pbreak = true;
                //        pinitem.onbreak = true;
                //    }
                //    lboxPin1.Items.Refresh();
                //    logmsg = SavePin1StatusToFile();
                //}

                //if (pinnum == 1)
                //{
                //    var _pin = lboxPin2.Items[pinpos] as dtoPin;
                //    if (_pin.pbreak == true)
                //    {
                //        _pin.pbreak = false;
                //        pinitem.onbreak = false;
                //    }
                //    else
                //    {
                //        _pin.pbreak = true;
                //        pinitem.onbreak = true;
                //    }
                //    lboxPin2.Items.Refresh();
                //    logmsg = SavePin2StatusToFile();
                //}

                //if (pinnum == 2)
                //{
                //    var _pin = lboxPin3.Items[pinpos] as dtoPin;
                //    if (_pin.pbreak == true)
                //    {
                //        _pin.pbreak = false;
                //        pinitem.onbreak = false;
                //    }
                //    else
                //    {
                //        _pin.pbreak = true;
                //        pinitem.onbreak = true;
                //    }
                //    lboxPin3.Items.Refresh();
                //    logmsg = SavePin3StatusToFile();
                //}

                //if (pinnum == 3)
                //{
                //    var _pin = lboxPin4.Items[pinpos] as dtoPin;
                //    if (_pin.pbreak == true)
                //    {
                //        _pin.pbreak = false;
                //        pinitem.onbreak = false;
                //    }
                //    else
                //    {
                //        _pin.pbreak = true;
                //        pinitem.onbreak = true;
                //    }
                //    lboxPin4.Items.Refresh();
                //    logmsg = SavePin4StatusToFile();
                //}

                //if (pinnum == 4)
                //{
                //    var _pin = lboxPin5.Items[pinpos] as dtoPin;
                //    if (_pin.pbreak == true)
                //    {
                //        _pin.pbreak = false;
                //        pinitem.onbreak = false;
                //    }
                //    else
                //    {
                //        _pin.pbreak = true;
                //        pinitem.onbreak = true;
                //    }
                //    lboxPin5.Items.Refresh();
                //    logmsg = SavePin5StatusToFile();
                //}
                SQLManager sm = new SQLManager();
                logmsg = SavePinStatusToFile();
                sm.LogRecCreate(logmsg);
            }
            catch (Exception ex)
            {
                //string strEx = ex.ToString();
                //SQLManager sm = new SQLManager();
                //dtoLog obj_log = new dtoLog();
                //obj_log.ID = sm.LogRecCount() + 1;
                //obj_log.logtimestamp=DateTime.Now;
                //obj_log.logtext="ERROR þegar verið er að merkja bíl á nál.  "+ ex.ToString();
                //sm.LogRecCreate(obj_log.logtext.ToString());
            }
        }

        #endregion

        # region txtAreaKeyDown

        private void txtArea01_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string _carnum;

                if (e.Key == Key.Enter)
                {
                    _carnum = txtArea01.Text;
                    try
                    {
                        int tmp = Convert.ToInt32(_carnum);
                    }
                    catch (Exception h)
                    {
                        txtArea01.Clear();
                        txtArea01.Text = "";
                    }

                    if (txtArea01.Text.ToString().Length == 0)
                    {
                        MessageBox.Show("Það var villa í innslættinum. Sláðu inn númer til að setja á nálina.", "Villa við innslátt.", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                    else
                    {
                        txtArea01.Clear();
                        txtArea01.Text = "";
                        int lcar = (from lc in memListCar
                                    where lc.stationid == Convert.ToInt32(_carnum)
                                    select lc).Count();
                        int caronpin = (from lps in memListPinStatus
                                        where lps.carid == Convert.ToInt32(_carnum)
                                        select lps).Count();
                        if (caronpin == 1)
                        {
                            int i = (from lps in memListPinStatus
                                     where lps.carid == Convert.ToInt32(_carnum)
                                     select lps).FirstOrDefault().pinid;

                            if (i == 5)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoTour _plist = (from t in memListPin6
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin6.Remove(_plist);
                                listMessageOnPin.DataContext = memListPin6;
                                listMessageOnPin.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin6StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af túralista.";
                                sm.LogRecCreate(strLogMessage);
                            }
                            //Þetta er ekki á nál 2 .
                            if(i == 1)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin2
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin2.Remove(_plist);
                                lboxPin2.DataContext = memListPin2;
                                lboxPin2.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin2StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 2";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki nál 3.
                            if(i == 2)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin3
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin3.Remove(_plist);
                                lboxPin3.DataContext = memListPin3;
                                lboxPin3.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin3StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 3";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            
                        }
                        // Þetta er ekki á nál 4
                        if(i == 3)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin4
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin4.Remove(_plist);
                                lboxPin4.DataContext = memListPin4;
                                lboxPin4.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin4StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 4";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                        }
                        // þetta er ekki á nál 5
                        if(i == 4)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin5
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin5.Remove(_plist);
                                lboxPin5.DataContext = memListPin5;
                                lboxPin5.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin5StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 5";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                        }
                        if (caronpin==0)
                        {
                            if (lcar == 1)
                            {
                                var carlist = from vlcar in memListCar
                                              where vlcar.stationid == Convert.ToInt32(_carnum)
                                              select vlcar;
                                pin1_id = pin1_id + 1;
                                dtoPin pin1 = new dtoPin();
                                pin1.id = pin1_id;
                                pin1.idcar = Convert.ToInt32(_carnum);
                                pin1.idpin = 0;
                                pin1.pline = memListPin1.Count - 1;
                                pin1.pbreak = false;
                                pin1.pcarcode = carlist.FirstOrDefault().code;
                                pin1.owner = carlist.FirstOrDefault().owner;
                                pin1.carsize = carlist.FirstOrDefault().size;
                                pin1.car1 = carlist.FirstOrDefault().car1;
                                pin1.car2 = carlist.FirstOrDefault().car2;
                                pin1.car3 = carlist.FirstOrDefault().car3;
                                pin1.car4 = carlist.FirstOrDefault().car4;
                                pin1.car5 = carlist.FirstOrDefault().car5;
                                pin1.car6 = carlist.FirstOrDefault().car6;
                                pin1.car7 = carlist.FirstOrDefault().car7;
                                pin1.dtonpin = DateTime.Now;
                                pin1.listed = carlist.FirstOrDefault().listed;
                                //Settur status inní lista yfir status
                                memListPin1.Add(pin1);
                                dtoPinStatus pinStatus = new dtoPinStatus();
                                pinStatus.carid = pin1.idcar;
                                pinStatus.pinid = pin1.idpin;
                                pinStatus.pinpos = memListPin1.Count - 1;
                                memListPinStatus.Add(pinStatus);
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePinStatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á stöðulista yfir nálar.";
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = SavePin1StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á Stöðvarnálina.";
                                sm.LogRecCreate(strLogMessage);
                                lboxPin1.DataContext = memListPin1;
                                lboxPin1.Items.Refresh();

                            }
                            else
                            {
                                MessageBox.Show("Þeð er enginn bíll með þetta stöðvarnúmer. Reyndu aftur.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Þessi bíll er á nál.");
                        }
                    }
                }
                }
                
            catch
            {

            }
        }

        private void txtArea02_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string _carnum;
                if (e.Key == Key.Enter)
                {
                    _carnum = txtArea02.Text;
                    try
                    {
                        int tmp = Convert.ToInt32(_carnum);
                    }
                    catch (Exception h)
                    {
                        txtArea02.Clear();
                        txtArea02.Text = "";
                    }

                    if (txtArea02.Text.ToString().Length == 0)
                    {
                        MessageBox.Show("Það var villa í innslættinum. Sláðu inn númer til að setja á nálina.");
                    }
                    else
                    {
                        txtArea02.Clear();
                        txtArea02.Text = "";
                        int lcar = (from lc in memListCar
                                    where lc.stationid == Convert.ToInt32(_carnum)
                                    select lc).Count();
                        int caronpin = (from lps in memListPinStatus
                                        where lps.carid == Convert.ToInt32(_carnum)
                                        select lps).Count();
                        if (caronpin == 1)
                        {
                            int i = (from lps in memListPinStatus
                                     where lps.carid == Convert.ToInt32(_carnum)
                                     select lps).FirstOrDefault().pinid;
                            if (i == 5)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoTour _plist = (from t in memListPin6
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin6.Remove(_plist);
                                listMessageOnPin.DataContext = memListPin6;
                                listMessageOnPin.Items.Refresh();
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin6StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af túralista.";
                                sm.LogRecCreate(strLogMessage);


                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 1
                            if (i == 0)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin1
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin1.Remove(_plist);
                                lboxPin1.DataContext = memListPin1;
                                lboxPin1.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin1StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 1";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 2 .
                            if (i == 1)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin2
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin2.Remove(_plist);
                                lboxPin2.DataContext = memListPin2;
                                lboxPin2.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin2StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 2";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki nál 3.
                            if (i == 2)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin3
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin3.Remove(_plist);
                                lboxPin3.DataContext = memListPin3;
                                lboxPin3.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin3StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 3";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;

                            }
                            // Þetta er ekki á nál 4
                            if (i == 3)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin4
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin4.Remove(_plist);
                                lboxPin4.DataContext = memListPin4;
                                lboxPin4.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin4StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 4";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            // þetta er ekki á nál 5
                            if (i == 4)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin5
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin5.Remove(_plist);
                                lboxPin5.DataContext = memListPin5;
                                lboxPin5.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin5StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 5";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                        }
                        if (caronpin == 0)
                        {
                            if (lcar == 1)
                            {
                                var carlist = from vlcar in memListCar
                                              where vlcar.stationid == Convert.ToInt32(_carnum)
                                              select vlcar;
                                pin2_id = pin2_id + 1;
                                dtoPin pin2 = new dtoPin();
                                pin2.id = pin2_id;
                                pin2.idcar = Convert.ToInt32(_carnum);
                                pin2.idpin = 1;
                                pin2.pbreak = false;
                                pin2.pcarcode = carlist.FirstOrDefault().code;
                                pin2.owner = carlist.FirstOrDefault().owner;
                                pin2.carsize = carlist.FirstOrDefault().size;
                                pin2.car1 = carlist.FirstOrDefault().car1;
                                pin2.car2 = carlist.FirstOrDefault().car2;
                                pin2.car3 = carlist.FirstOrDefault().car3;
                                pin2.car4 = carlist.FirstOrDefault().car4;
                                pin2.car5 = carlist.FirstOrDefault().car5;
                                pin2.car6 = carlist.FirstOrDefault().car6;
                                pin2.car7 = carlist.FirstOrDefault().car7;
                                pin2.dtonpin = DateTime.Now;
                                pin2.listed = carlist.FirstOrDefault().listed;
                                memListPin2.Add(pin2);
                                dtoPinStatus pinStatus = new dtoPinStatus();
                                pinStatus.carid = pin2.idcar;
                                pinStatus.pinid = pin2.idpin;
                                pinStatus.pinpos = memListPin2.Count - 1;
                                memListPinStatus.Add(pinStatus);

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePinStatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á stöðulista yfir nálar.";
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = SavePin2StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á Miðbæjarnálina.";
                                sm.LogRecCreate(strLogMessage);

                                lboxPin2.DataContext = memListPin2;
                                lboxPin2.Items.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("Þeð er enginn bíll með þetta stöðvarnúmer. Reyndu aftur.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bíllinn er á nál.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Villa í innslættinum", "Sláðu inn númer stöðvarnúmer bíls.", MessageBoxButton.OK, MessageBoxImage.Error);
                string strEx = ex.ToString();
                SQLManager sm = new SQLManager();
                sm.LogRecCreate(strEx);

            }
        }

        private void txtArea03_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    string _carnum;
                    _carnum = txtArea03.Text;
                    try
                    {
                        int tmp = Convert.ToInt32(_carnum);
                    }
                    catch (Exception h)
                    {
                        txtArea03.Clear();
                        txtArea03.Text = "";
                    }

                    if (txtArea03.Text.ToString().Length == 0)
                    {
                        MessageBox.Show("Það var villa í innslættinum. Sláðu inn númer til að setja á nálina.");
                    }
                    else
                    {
                        txtArea03.Clear();
                        txtArea03.Text = "";
                        int lcar = (from lc in memListCar
                                    where lc.stationid == Convert.ToInt32(_carnum)
                                    select lc).Count();

                        int caronpin = (from lps in memListPinStatus
                                        where lps.carid == Convert.ToInt32(_carnum)
                                        select lps).Count();

                        if (caronpin == 1)
                        {
                            int i = (from lps in memListPinStatus
                                     where lps.carid == Convert.ToInt32(_carnum)
                                     select lps).FirstOrDefault().pinid;
                            if (i == 5)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoTour _plist = (from t in memListPin6
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin6.Remove(_plist);
                                listMessageOnPin.DataContext = memListPin6;
                                listMessageOnPin.Items.Refresh();

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin6StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af túralista.";
                                sm.LogRecCreate(strLogMessage);

                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 1
                            if (i == 0)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin1
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin1.Remove(_plist);
                                lboxPin1.DataContext = memListPin1;
                                lboxPin1.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin1StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 1";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 2 .
                            if (i == 1)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin2
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin2.Remove(_plist);
                                lboxPin2.DataContext = memListPin2;
                                lboxPin2.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin2StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 2";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki nál 3.
                            if (i == 2)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin3
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin3.Remove(_plist);
                                lboxPin3.DataContext = memListPin3;
                                lboxPin3.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin3StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 3";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;

                            }
                            // Þetta er ekki á nál 4
                            if (i == 3)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin4
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin4.Remove(_plist);
                                lboxPin4.DataContext = memListPin4;
                                lboxPin4.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin4StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 4";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            // þetta er ekki á nál 5
                            if (i == 4)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin5
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin5.Remove(_plist);
                                lboxPin5.DataContext = memListPin5;
                                lboxPin5.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin5StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 5";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                        }

                        if (caronpin == 0)
                        {
                            if (lcar == 1)
                            {
                                var carlist = from vlcar in memListCar
                                              where vlcar.stationid == Convert.ToInt32(_carnum)
                                              select vlcar;
                                pin3_id = pin3_id + 1;
                                dtoPin pin3 = new dtoPin();
                                pin3.id = pin3_id;
                                pin3.idcar = Convert.ToInt32(_carnum);
                                pin3.idpin = 2;
                                pin3.pbreak = false;
                                pin3.pcarcode = carlist.FirstOrDefault().code;
                                pin3.owner = carlist.FirstOrDefault().owner;
                                pin3.carsize = carlist.FirstOrDefault().size;
                                pin3.car1 = carlist.FirstOrDefault().car1;
                                pin3.car2 = carlist.FirstOrDefault().car2;
                                pin3.car3 = carlist.FirstOrDefault().car3;
                                pin3.car4 = carlist.FirstOrDefault().car4;
                                pin3.car5 = carlist.FirstOrDefault().car5;
                                pin3.dtonpin = DateTime.Now;
                                pin3.listed = carlist.FirstOrDefault().listed;
                                memListPin3.Add(pin3);
                                Debug.Write(pin3.id.ToString());
                                dtoPinStatus pinStatus = new dtoPinStatus();
                                pinStatus.carid = pin3.idcar;
                                pinStatus.pinid = pin3.idpin;
                                pinStatus.pinpos = memListPin3.Count - 1;
                                memListPinStatus.Add(pinStatus);
                                lboxPin3.DataContext = memListPin3;
                                lboxPin3.Items.Refresh();

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePinStatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á stöðulista yfir nálar.";
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = SavePin3StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á Breiðholtslnál.";
                                sm.LogRecCreate(strLogMessage);

                            }
                            else
                            {
                                MessageBox.Show("Þeð er enginn bíll með þetta stöðvarnúmer. Reyndu aftur.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bílinn er þegar á nál.");
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void txtArea04_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    string _carnum;
                    _carnum = txtArea04.Text;
                    try
                    {
                        int tmp = Convert.ToInt32(_carnum);
                    }
                    catch (Exception h)
                    {
                        txtArea04.Clear();
                        txtArea04.Text = "";
                    }

                    if (txtArea04.Text.ToString().Length == 0)
                    {
                        MessageBox.Show("Það var villa í innslættinum. Sláðu inn númer til að setja á nálina.");
                    }
                    else
                    {

                        txtArea04.Clear();
                        txtArea04.Text = "";

                        int lcar = (from lc in memListCar
                                    where lc.stationid == Convert.ToInt32(_carnum)
                                    select lc).Count();
                        int caronpin = (from lps in memListPinStatus
                                        where lps.carid == Convert.ToInt32(_carnum)
                                        select lps).Count();
                        if (caronpin == 1)
                        {
                            int i = (from lps in memListPinStatus
                                     where lps.carid == Convert.ToInt32(_carnum)
                                     select lps).FirstOrDefault().pinid;
                            if (i == 5)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoTour _plist = (from t in memListPin6
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin6.Remove(_plist);
                                listMessageOnPin.DataContext = memListPin6;
                                listMessageOnPin.Items.Refresh();


                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 1
                            if (i == 0)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin1
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin1.Remove(_plist);
                                lboxPin1.DataContext = memListPin1;
                                lboxPin1.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin1StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 1";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 2 .
                            if (i == 1)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin2
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin2.Remove(_plist);
                                lboxPin2.DataContext = memListPin2;
                                lboxPin2.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin2StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 2";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki nál 3.
                            if (i == 2)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin3
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin3.Remove(_plist);
                                lboxPin3.DataContext = memListPin3;
                                lboxPin3.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin3StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 3";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;

                            }
                            // Þetta er ekki á nál 4
                            if (i == 3)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin4
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin4.Remove(_plist);
                                lboxPin4.DataContext = memListPin4;
                                lboxPin4.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin4StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 4";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            // þetta er ekki á nál 5
                            if (i == 4)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin5
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin5.Remove(_plist);
                                lboxPin5.DataContext = memListPin5;
                                lboxPin5.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin5StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 5";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                        }

                        if (caronpin == 0)
                        {

                            if (lcar == 1)
                            {
                                var carlist = from vlcar in memListCar
                                              where vlcar.stationid == Convert.ToInt32(_carnum)
                                              select vlcar;

                                pin4_id = pin4_id + 1;
                                dtoPin pin4 = new dtoPin();
                                pin4.id = pin4_id;
                                pin4.idcar = Convert.ToInt32(_carnum);
                                pin4.idpin = 3;
                                pin4.pbreak = false;
                                pin4.pcarcode = carlist.FirstOrDefault().code;
                                pin4.owner = carlist.FirstOrDefault().owner;
                                pin4.carsize = carlist.FirstOrDefault().size;
                                pin4.car1 = carlist.FirstOrDefault().car1;
                                pin4.car2 = carlist.FirstOrDefault().car2;
                                pin4.car3 = carlist.FirstOrDefault().car3;
                                pin4.car4 = carlist.FirstOrDefault().car4;
                                pin4.car5 = carlist.FirstOrDefault().car5;
                                pin4.dtonpin = DateTime.Now;
                                pin4.listed = carlist.FirstOrDefault().listed;

                                memListPin4.Add(pin4);
                                dtoPinStatus pinStatus = new dtoPinStatus();
                                pinStatus.carid = pin4.idcar;
                                pinStatus.pinid = pin4.idpin;
                                pinStatus.pinpos = memListPin4.Count - 1;
                                memListPinStatus.Add(pinStatus);

                                lboxPin4.DataContext = memListPin4;
                                lboxPin4.Items.Refresh();

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePinStatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á stöðulista yfir nálar.";
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = SavePin4StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á Árbæjarnál.";
                                sm.LogRecCreate(strLogMessage);
                            }
                            else
                            {
                                MessageBox.Show("Þeð er enginn bíll með þetta stöðvarnúmer. Reyndu aftur.");
                            }
                        }

                        else
                        {
                            MessageBox.Show("Bílinn er þegar á nál");
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void txtArea04_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Key == Key.Enter)
                {
                    string _carnum;
                    _carnum = txtArea04.Text;
                    try
                    {
                        int tmp = Convert.ToInt32(_carnum);
                    }
                    catch (Exception h)
                    {
                        txtArea04.Clear();
                        txtArea04.Text = "";
                    }

                    if (txtArea04.Text.ToString().Length == 0)
                    {
                        MessageBox.Show("Það var villa í innslættinum. Sláðu inn númer til að setja á nálina.");
                    }
                    else
                    {

                        txtArea04.Clear();
                        txtArea04.Text = "";

                        int lcar = (from lc in memListCar
                                    where lc.stationid == Convert.ToInt32(_carnum)
                                    select lc).Count();
                        int caronpin = (from lps in memListPinStatus
                                        where lps.carid == Convert.ToInt32(_carnum)
                                        select lps).Count();
                        if (caronpin == 1)
                        {
                            int i = (from lps in memListPinStatus
                                     where lps.carid == Convert.ToInt32(_carnum)
                                     select lps).FirstOrDefault().pinid;
                            if (i == 5)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoTour _plist = (from t in memListPin6
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin6.Remove(_plist);
                                listMessageOnPin.DataContext = memListPin6;
                                listMessageOnPin.Items.Refresh();

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin6StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af túralista.";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 1
                            if (i == 0)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin1
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin1.Remove(_plist);
                                lboxPin1.DataContext = memListPin1;
                                lboxPin1.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin1StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 1";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 2 .
                            if (i == 1)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin2
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin2.Remove(_plist);
                                lboxPin2.DataContext = memListPin2;
                                lboxPin2.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin2StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 2";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki nál 3.
                            if (i == 2)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin3
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin3.Remove(_plist);
                                lboxPin3.DataContext = memListPin3;
                                lboxPin3.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin3StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 3";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;

                            }
                            // Þetta er ekki á nál 4
                            if (i == 3)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin4
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin4.Remove(_plist);
                                lboxPin4.DataContext = memListPin4;
                                lboxPin4.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin4StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 4";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            // þetta er ekki á nál 5
                            if (i == 4)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin5
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin5.Remove(_plist);
                                lboxPin5.DataContext = memListPin5;
                                lboxPin5.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin5StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 5";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                        }

                        if (caronpin == 0)
                        {

                            if (lcar == 1)
                            {
                                var carlist = from vlcar in memListCar
                                              where vlcar.stationid == Convert.ToInt32(_carnum)
                                              select vlcar;

                                pin4_id = pin4_id + 1;
                                dtoPin pin4 = new dtoPin();
                                pin4.id = pin4_id;
                                pin4.idcar = Convert.ToInt32(_carnum);
                                pin4.idpin = 3;
                                pin4.pbreak = false;
                                pin4.pcarcode = carlist.FirstOrDefault().code;
                                pin4.owner = carlist.FirstOrDefault().owner;
                                pin4.carsize = carlist.FirstOrDefault().size;
                                pin4.car1 = carlist.FirstOrDefault().car1;
                                pin4.car2 = carlist.FirstOrDefault().car2;
                                pin4.car3 = carlist.FirstOrDefault().car3;
                                pin4.car4 = carlist.FirstOrDefault().car4;
                                pin4.car5 = carlist.FirstOrDefault().car5;
                                pin4.dtonpin = DateTime.Now;
                                pin4.listed = carlist.FirstOrDefault().listed;
                                memListPin4.Add(pin4);
                                dtoPinStatus pinStatus = new dtoPinStatus();
                                pinStatus.carid = pin4.idcar;
                                pinStatus.pinid = pin4.idpin;
                                pinStatus.pinpos = memListPin4.Count - 1;
                                memListPinStatus.Add(pinStatus);

                                lboxPin4.DataContext = memListPin4;
                                lboxPin4.Items.Refresh();

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePinStatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á stöðulista yfir nálar.";
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = SavePin4StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á Breiðholtslnál.";
                                sm.LogRecCreate(strLogMessage);
                            }
                            else
                            {
                                MessageBox.Show("Þeð er enginn bíll með þetta stöðvarnúmer. Reyndu aftur.");
                            }
                        }

                        else
                        {
                            MessageBox.Show("Bílinn er þegar á nál");
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void txtArea05_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string _carnum;
                if (e.Key == Key.Enter)
                {
                    _carnum = txtArea05.Text;
                    //MessageBox.Show(_carnum);
                    try
                    {
                        int tmp = Convert.ToInt32(_carnum);
                    }
                    catch (Exception h)
                    {
                        txtArea05.Clear();
                        txtArea05.Text = "";
                    }

                    if (txtArea05.Text.ToString().Length == 0)
                    {
                        MessageBox.Show("Það var villa í innslættinum. Sláðu inn númer til að setja á nálina.");
                    }
                    else
                    {
                        txtArea05.Clear();
                        txtArea05.Text = "";
                        int lcar = (from lc in memListCar
                                    where lc.stationid == Convert.ToInt32(_carnum)
                                    select lc).Count();

                        int caronpin = (from lps in memListPinStatus
                                        where lps.carid == Convert.ToInt32(_carnum)
                                        select lps).Count();

                        if (caronpin == 1)
                        {
                            int i = (from lps in memListPinStatus
                                     where lps.carid == Convert.ToInt32(_carnum)
                                     select lps).FirstOrDefault().pinid;
                            if (i == 5)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoTour _plist = (from t in memListPin6
                                                  where t.idcar == Convert.ToInt32(_carnum)
                                                  select t).FirstOrDefault();
                                memListPin6.Remove(_plist);
                                listMessageOnPin.DataContext = memListPin6;
                                listMessageOnPin.Items.Refresh();

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin6StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af túralista.";
                                sm.LogRecCreate(strLogMessage);

                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 1
                            if (i == 0)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin1
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin1.Remove(_plist);
                                lboxPin1.DataContext = memListPin1;
                                lboxPin1.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin1StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 1";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki á nál 2 .
                            if (i == 1)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin2
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin2.Remove(_plist);
                                lboxPin2.DataContext = memListPin2;
                                lboxPin2.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin2StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 2";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            //Þetta er ekki nál 3.
                            if (i == 2)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin3
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin3.Remove(_plist);
                                lboxPin3.DataContext = memListPin3;
                                lboxPin3.Items.Refresh();
                                caronpin = 0;

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin3StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 3";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;

                            }
                            // Þetta er ekki á nál 4
                            if (i == 3)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin4
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin4.Remove(_plist);
                                lboxPin4.DataContext = memListPin4;
                                lboxPin4.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin4StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 4";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                            // þetta er ekki á nál 5
                            if (i == 4)
                            {
                                dtoPinStatus _status = (from lps in memListPinStatus
                                                        where lps.carid == Convert.ToInt32(_carnum)
                                                        select lps).FirstOrDefault();
                                memListPinStatus.Remove(_status);
                                dtoPin _plist = (from t in memListPin5
                                                 where t.idcar == Convert.ToInt32(_carnum)
                                                 select t).FirstOrDefault();
                                memListPin5.Remove(_plist);
                                lboxPin5.DataContext = memListPin5;
                                lboxPin5.Items.Refresh();
                                caronpin = 0;
                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePin5StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + _carnum + " tekinn af nál 5";
                                sm.LogRecCreate(strLogMessage);
                                caronpin = 0;
                            }
                        }


                        if (caronpin == 0)
                        {
                            if (lcar == 1)
                            {
                                var carlist = from vlcar in memListCar
                                              where vlcar.stationid == Convert.ToInt32(_carnum)
                                              select vlcar;

                                pin5_id = pin5_id + 1;
                                dtoPin pin5 = new dtoPin();
                                pin5.id = pin5_id;
                                pin5.idcar = Convert.ToInt32(_carnum);
                                pin5.idpin = 4;
                                pin5.pbreak = false;
                                pin5.pcarcode = carlist.FirstOrDefault().code;
                                pin5.owner = carlist.FirstOrDefault().owner;
                                pin5.carsize = carlist.FirstOrDefault().size;
                                pin5.car1 = carlist.FirstOrDefault().car1;
                                pin5.car2 = carlist.FirstOrDefault().car2;
                                pin5.car3 = carlist.FirstOrDefault().car3;
                                pin5.car4 = carlist.FirstOrDefault().car4;
                                pin5.car5 = carlist.FirstOrDefault().car5;
                                pin5.dtonpin = DateTime.Now;
                                pin5.pline = memListPin5.Count() + 1;
                                pin5.listed = carlist.FirstOrDefault().listed;
                                // Bæta við status.

                                memListPin5.Add(pin5);
                                dtoPinStatus pinStatus = new dtoPinStatus();
                                pinStatus.carid = pin5.idcar;
                                pinStatus.pinid = pin5.idpin;
                                pinStatus.pinpos = memListPin5.Count - 1;
                                memListPinStatus.Add(pinStatus);

                                // Uppfæra lista.

                                lboxPin5.DataContext = memListPin5;
                                lboxPin5.Items.Refresh();

                                SQLManager sm = new SQLManager();
                                string strLogMessage = SavePinStatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur á stöðulista yfir nálar.";
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = SavePin5StatusToFile();
                                sm.LogRecCreate(strLogMessage);
                                strLogMessage = "Bíll númer " + pinStatus.carid + " settur í rennu.";
                                sm.LogRecCreate(strLogMessage);
                            }
                            else
                            {
                                MessageBox.Show("Þeð er enginn bíll með þetta stöðvarnúmer. Reyndu aftur.");
                            }
                        }
                        else
                        {
                            MessageBox.Show(" Bíllinn er á nál. ");
                        }
                    }
                }
            }
            catch
            {

            }
        }

        #endregion


        #region btnPinMooveDownClick - 25.05.2015 - Lokið

        private void btnPinOneMooveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin1.Count > 1)
                {
                    if (lboxPin1.SelectedIndex > -1) //#1
                    {
                        int i = lboxPin1.SelectedIndex;
                        int ic = lboxPin1.Items.Count;
                        dtoPin sitem = memListPin1[i];

                        if (i == memListPin1.Count - 1)
                        {
                            MessageBox.Show("Þetta er fyrsti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            memListPin1.RemoveAt(i);
                            memListPin1.Insert(i + 1, sitem);
                            lboxPin1.DataContext = memListPin1;
                            lboxPin1.Items.Refresh();
                            string strLogMessage = SavePin1StatusToFile();
                            SQLManager sm = new SQLManager();
                            sm.LogRecCreate(strLogMessage);
                            strLogMessage = "Car number " + sitem.idcar.ToString() + " færður niður á nál 1.";
                            sm.LogRecCreate(strLogMessage);
                            var ipinpos = (from mpin in memListPinStatus
                                           where mpin.carid == sitem.idcar
                                           select mpin).FirstOrDefault().pinpos;
                            var pinitem = (from mpin in memListPinStatus
                                           where mpin.carid == sitem.idcar
                                           select mpin).FirstOrDefault();
                            memListPinStatus.Remove(pinitem);
                            ipinpos = ipinpos + 1;
                            pinitem.pinpos = ipinpos;
                            var listsel = from c in memListPinStatus
                                          where c.pinid == 0
                                          select c;
                            foreach (var item in listsel)
                            {
                                if (item.pinpos == ipinpos)
                                {
                                    item.pinpos = ipinpos - 1;
                                }
                            }
                            memListPinStatus.Add(pinitem);
                            strLogMessage = SavePinStatusToFile();
                            sm.LogRecCreate(strLogMessage);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Það er enginn bíll valinn.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                string strEx = ex.ToString();
                SQLManager sm = new SQLManager();
                sm.LogRecCreate(strEx);
            }
        }

        private void btnPinTwoMooveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin2.Count > 1)
                {
                    int i = lboxPin2.SelectedIndex;
                    int ic = lboxPin2.Items.Count;
                    dtoPin sitem = memListPin2[i];
                    if (i == memListPin2.Count - 1)
                    {
                        MessageBox.Show("Þetta er fyrsti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        memListPin2.RemoveAt(i);
                        memListPin2.Insert(i + 1, sitem);
                        lboxPin2.DataContext = memListPin2;
                        lboxPin2.Items.Refresh();
                        string strLogMessage = SavePin2StatusToFile();
                        SQLManager sm = new SQLManager();
                        sm.LogRecCreate(strLogMessage);
                        strLogMessage = "Car number " + sitem.idcar.ToString() + " færður niður á nál 2.";
                        sm.LogRecCreate(strLogMessage);
                        var ipinpos = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault().pinpos;
                        var pinitem = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault();
                        memListPinStatus.Remove(pinitem);
                        ipinpos = ipinpos + 1;
                        pinitem.pinpos = ipinpos;
                        var listsel = from c in memListPinStatus
                                      where c.pinid == 1
                                      select c;
                        foreach (var item in listsel)
                        {
                            if (item.pinpos == ipinpos)
                            {
                                item.pinpos = ipinpos - 1;
                            }
                        }
                        memListPinStatus.Add(pinitem);
                        strLogMessage = SavePinStatusToFile();
                        sm.LogRecCreate(strLogMessage);
                    }
                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                string strEx = ex.ToString();
                SQLManager sm = new SQLManager();
                sm.LogRecCreate(strEx);
            }
        }

        private void btnPinThreeMooveDown_Click(object sender, RoutedEventArgs e)
        {
            if (memListPin3.Count > 1)
            {
                //txtReorderHeader.Text = "  Endurraða Stöðin";
                //childWinReorderPin.Show();
                //childWinReorderPin_DataGrid.ItemsSource = memListPin1;
                //_pinid = 0;
                int i = lboxPin3.SelectedIndex;
                int ic = lboxPin3.Items.Count;
                dtoPin sitem = memListPin3[i];

                if (i == memListPin3.Count - 1)
                {
                    MessageBox.Show("Þetta er síðasti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    memListPin3.RemoveAt(i);
                    memListPin3.Insert(i + 1, sitem);
                    lboxPin3.DataContext = memListPin3;
                    lboxPin3.Items.Refresh();
                    string strLogMessage = SavePin3StatusToFile();
                    SQLManager sm = new SQLManager();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " færður niður á nál 3.";
                    sm.LogRecCreate(strLogMessage);

                    var ipinpos = (from mpin in memListPinStatus
                                   where mpin.carid == sitem.idcar
                                   select mpin).FirstOrDefault().pinpos;
                    var pinitem = (from mpin in memListPinStatus
                                   where mpin.carid == sitem.idcar
                                   select mpin).FirstOrDefault();
                    memListPinStatus.Remove(pinitem);
                    ipinpos = ipinpos + 1;
                    pinitem.pinpos = ipinpos;
                    var listsel = from c in memListPinStatus
                                  where c.pinid == 2
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos == ipinpos)
                        {
                            item.pinpos = ipinpos - 1;
                        }
                    }
                    memListPinStatus.Add(pinitem);
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                }
            }
            else
            {
                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPinFourMooveDown_Click(object sender, RoutedEventArgs e)
        {
            if (memListPin4.Count > 1)
            {
                //txtReorderHeader.Text = "  Endurraða Stöðin";
                //childWinReorderPin.Show();
                //childWinReorderPin_DataGrid.ItemsSource = memListPin1;
                //_pinid = 0;
                int i = lboxPin4.SelectedIndex;
                int ic = lboxPin4.Items.Count;
                dtoPin sitem = memListPin4[i];

                if (i == memListPin4.Count - 1)
                {
                    MessageBox.Show("Þetta er síðasti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    memListPin4.RemoveAt(i);
                    memListPin4.Insert(i + 1, sitem);
                    lboxPin4.DataContext = memListPin4;
                    lboxPin4.Items.Refresh();

                    string strLogMessage = SavePin4StatusToFile();
                    SQLManager sm = new SQLManager();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " færður niður á nál 4.";
                    sm.LogRecCreate(strLogMessage);

                    var ipinpos = (from mpin in memListPinStatus
                                   where mpin.carid == sitem.idcar
                                   select mpin).FirstOrDefault().pinpos;
                    var pinitem = (from mpin in memListPinStatus
                                   where mpin.carid == sitem.idcar
                                   select mpin).FirstOrDefault();
                    memListPinStatus.Remove(pinitem);
                    ipinpos = ipinpos + 1;
                    pinitem.pinpos = ipinpos;
                    var listsel = from c in memListPinStatus
                                  where c.pinid == 3
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos == ipinpos)
                        {
                            item.pinpos = ipinpos - 1;
                        }
                    }
                    memListPinStatus.Add(pinitem);
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                }
            }
            else
            {
                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPinFiveMooveDown_Click(object sender, RoutedEventArgs e)
        {
            if (memListPin5.Count > 1)
            {
                //txtReorderHeader.Text = "  Endurraða Stöðin";
                //childWinReorderPin.Show();
                //childWinReorderPin_DataGrid.ItemsSource = memListPin1;
                //_pinid = 0;
                int i = lboxPin5.SelectedIndex;
                int ic = lboxPin5.Items.Count;
                dtoPin sitem = memListPin5[i];

                if (i == memListPin5.Count - 1)
                {
                    MessageBox.Show("Þetta er síðasti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    memListPin5.RemoveAt(i);
                    memListPin5.Insert(i + 1, sitem);
                    lboxPin5.DataContext = memListPin5;
                    lboxPin5.Items.Refresh();

                    string strLogMessage = SavePin5StatusToFile();
                    SQLManager sm = new SQLManager();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " færður niður á nál 3.";
                    sm.LogRecCreate(strLogMessage);

                    var ipinpos = (from mpin in memListPinStatus
                                   where mpin.carid == sitem.idcar
                                   select mpin).FirstOrDefault().pinpos;
                    var pinitem = (from mpin in memListPinStatus
                                   where mpin.carid == sitem.idcar
                                   select mpin).FirstOrDefault();
                    memListPinStatus.Remove(pinitem);
                    ipinpos = ipinpos + 1;
                    pinitem.pinpos = ipinpos;
                    var listsel = from c in memListPinStatus
                                  where c.pinid == 4
                                  select c;
                    foreach (var item in listsel)
                    {
                        if (item.pinpos == ipinpos)
                        {
                            item.pinpos = ipinpos - 1;
                        }
                    }
                    memListPinStatus.Add(pinitem);
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);

                }
            }
            else
            {
                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        #endregion


        #region btnPinMoveUpClick - 25.05.2015 - Lokið

        private void btnPinOneMooveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin1.Count > 1)
                {
                    if (lboxPin1.SelectedIndex > -1) //#2
                    {
                        int i = lboxPin1.SelectedIndex;
                        int ic = lboxPin1.Items.Count;
                        dtoPin sitem = memListPin1[i];

                        if (i == 0)
                        {
                            MessageBox.Show("Þetta er fyrsti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            memListPin1.RemoveAt(i);
                            memListPin1.Insert(i - 1, sitem);
                            lboxPin1.DataContext = memListPin1;
                            lboxPin1.Items.Refresh();
                            string strLogMessage = SavePin1StatusToFile();
                            SQLManager sm = new SQLManager();
                            sm.LogRecCreate(strLogMessage);
                            strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " færður upp á nál 1.";
                            sm.LogRecCreate(strLogMessage);
                            var ipinpos = (from mpin in memListPinStatus
                                           where mpin.carid == sitem.idcar
                                           select mpin).FirstOrDefault().pinpos;
                            var pinitem = (from mpin in memListPinStatus
                                           where mpin.carid == sitem.idcar
                                           select mpin).FirstOrDefault();
                            memListPinStatus.Remove(pinitem);
                            ipinpos = ipinpos - 1;
                            pinitem.pinpos = ipinpos;
                            var listsel = from c in memListPinStatus
                                          where c.pinid == 0
                                          select c;
                            foreach (var item in listsel)
                            {
                                if (item.pinpos == ipinpos)
                                {
                                    item.pinpos = ipinpos + 1;
                                }
                            }
                            memListPinStatus.Add(pinitem);
                            strLogMessage = SavePinStatusToFile();
                            sm.LogRecCreate(strLogMessage);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Það er enginn bíll valinn.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {

            }
        }

        private void btnPinTwoMooveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin2.Count > 1)
                {
                    int i = lboxPin2.SelectedIndex;
                    int ic = lboxPin2.Items.Count;
                    dtoPin sitem = memListPin2[i];
                    if (i == 0)
                    {
                        MessageBox.Show("Þetta er fyrsti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        memListPin2.RemoveAt(i);
                        memListPin2.Insert(i - 1, sitem);
                        lboxPin2.DataContext = memListPin2;
                        lboxPin2.Items.Refresh();
                        string strLogMessage = SavePin2StatusToFile();
                        SQLManager sm = new SQLManager();
                        sm.LogRecCreate(strLogMessage);
                        strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " færður upp á nál 2.";
                        sm.LogRecCreate(strLogMessage);
                        var ipinpos = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault().pinpos;
                        var pinitem = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault();
                        memListPinStatus.Remove(pinitem);
                        ipinpos = ipinpos - 1;
                        pinitem.pinpos = ipinpos;
                        var listsel = from c in memListPinStatus
                                      where c.pinid == 1
                                      select c;
                        foreach (var item in listsel)
                        {
                            if (item.pinpos == ipinpos)
                            {
                                item.pinpos = ipinpos + 1;
                            }
                        }
                        memListPinStatus.Add(pinitem);
                        strLogMessage = SavePinStatusToFile();
                        sm.LogRecCreate(strLogMessage);
                    }
                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                string strEx = ex.ToString();
                SQLManager sm = new SQLManager();
                sm.LogRecCreate(strEx);

            }
        }

        private void btnPinThreeMooveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (memListPin3.Count > 1)
                {
                    //txtReorderHeader.Text = "  Endurraða Stöðin";
                    //childWinReorderPin.Show();
                    //childWinReorderPin_DataGrid.ItemsSource = memListPin1;
                    //_pinid = 0;
                    int i = lboxPin3.SelectedIndex;
                    int ic = lboxPin3.Items.Count;
                    dtoPin sitem = memListPin3[i];

                    if (i == 0)
                    {
                        MessageBox.Show("Þetta er fyrsti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        memListPin3.RemoveAt(i);
                        memListPin3.Insert(i - 1, sitem);
                        lboxPin3.DataContext = memListPin3;
                        lboxPin3.Items.Refresh();

                        string strLogMessage = SavePin3StatusToFile();
                        SQLManager sm = new SQLManager();
                        sm.LogRecCreate(strLogMessage);
                        strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " færður upp á nál 3.";
                        sm.LogRecCreate(strLogMessage);
                        var ipinpos = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault().pinpos;
                        var pinitem = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault();
                        memListPinStatus.Remove(pinitem);
                        ipinpos = ipinpos - 1;
                        pinitem.pinpos = ipinpos;
                        var listsel = from c in memListPinStatus
                                      where c.pinid == 2
                                      select c;
                        foreach (var item in listsel)
                        {
                            if (item.pinpos == ipinpos)
                            {
                                item.pinpos = ipinpos + 1;
                            }
                        }
                        memListPinStatus.Add(pinitem);
                        strLogMessage = SavePinStatusToFile();
                        sm.LogRecCreate(strLogMessage);
                    }
                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                string strEx = ex.ToString();
                SQLManager sm = new SQLManager();
                sm.LogRecCreate(strEx);
            }
        }

        private void btnPinFourMooveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (memListPin4.Count > 1)
                {
                    //txtReorderHeader.Text = "  Endurraða Stöðin";
                    //childWinReorderPin.Show();
                    //childWinReorderPin_DataGrid.ItemsSource = memListPin1;
                    //_pinid = 0;
                    int i = lboxPin4.SelectedIndex;
                    int ic = lboxPin4.Items.Count;
                    dtoPin sitem = memListPin4[i];

                    if (i == 0)
                    {
                        MessageBox.Show("Þetta er fyrsti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        memListPin4.RemoveAt(i);
                        memListPin4.Insert(i - 1, sitem);
                        lboxPin4.DataContext = memListPin4;
                        lboxPin4.Items.Refresh();

                        string strLogMessage = SavePin4StatusToFile();
                        SQLManager sm = new SQLManager();
                        sm.LogRecCreate(strLogMessage);
                        strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " færður upp á nál 4.";
                        sm.LogRecCreate(strLogMessage);
                        var ipinpos = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault().pinpos;
                        var pinitem = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault();
                        memListPinStatus.Remove(pinitem);
                        ipinpos = ipinpos - 1;
                        pinitem.pinpos = ipinpos;
                        var listsel = from c in memListPinStatus
                                      where c.pinid == 3
                                      select c;
                        foreach (var item in listsel)
                        {
                            if (item.pinpos == ipinpos)
                            {
                                item.pinpos = ipinpos + 1;
                            }
                        }
                        memListPinStatus.Add(pinitem);
                        strLogMessage = SavePinStatusToFile();
                        sm.LogRecCreate(strLogMessage);
                    }
                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                string strEx = ex.ToString();
                SQLManager sm = new SQLManager();
                sm.LogRecCreate(strEx);
            }
        }

        private void btnPinFiveMooveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin5.Count > 1)
                {
                    //txtReorderHeader.Text = "  Endurraða Stöðin";
                    //childWinReorderPin.Show();
                    //childWinReorderPin_DataGrid.ItemsSource = memListPin1;
                    //_pinid = 0;
                    int i = lboxPin5.SelectedIndex;
                    int ic = lboxPin5.Items.Count;
                    dtoPin sitem = memListPin5[i];

                    if (i == 0)
                    {
                        MessageBox.Show("Þetta er fyrsti bíllinn á nálinni.", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        memListPin5.RemoveAt(i);
                        memListPin5.Insert(i - 1, sitem);
                        lboxPin5.DataContext = memListPin5;
                        lboxPin5.Items.Refresh();

                        string strLogMessage = SavePin5StatusToFile();
                        SQLManager sm = new SQLManager();
                        sm.LogRecCreate(strLogMessage);
                        strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " færður upp á nál 5.";
                        sm.LogRecCreate(strLogMessage);
                        var ipinpos = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault().pinpos;
                        var pinitem = (from mpin in memListPinStatus
                                       where mpin.carid == sitem.idcar
                                       select mpin).FirstOrDefault();
                        memListPinStatus.Remove(pinitem);
                        ipinpos = ipinpos - 1;
                        pinitem.pinpos = ipinpos;
                        var listsel = from c in memListPinStatus
                                      where c.pinid == 4
                                      select c;
                        foreach (var item in listsel)
                        {
                            if (item.pinpos == ipinpos)
                            {
                                item.pinpos = ipinpos + 1;
                            }
                        }
                        memListPinStatus.Add(pinitem);
                        strLogMessage = SavePinStatusToFile();
                        sm.LogRecCreate(strLogMessage);
                    }
                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                string strEx = ex.ToString();
                SQLManager sm = new SQLManager();
                sm.LogRecCreate(strEx);
            }
        }

        #endregion


        #region PinMenuActions


        // Opnar glugga yfir upplýsingar um bílinn og bílstjórann.
        
        #region mnuPinCar_click

        private void mnuItemPin1Car_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin1.Count() == 0)
                {

                    MessageBox.Show("Það er enginn bíll á nálinni. ");

                }
                else
                {
                    if (lboxPin1.SelectedItems.Count > 0)
                    {
                        int i = lboxPin1.SelectedIndex;
                        int ic = lboxPin1.Items.Count;
                        dtoPin sitem = memListPin1[i];
                        winCarDetail WinCarDetail = new winCarDetail();
                        WinCarDetail.pub_icar = sitem.idcar;
                        WinCarDetail.Show();
                    }
                    else
                    {
                        MessageBox.Show("Það er enginn bíll valinn. ");
                    }
                }
            }
            catch
            {

            }
        }

        private void mnuItemPin2Car_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lboxPin2.SelectedItems.Count > 0)
                {
                    int i = lboxPin2.SelectedIndex;
                    int ic = lboxPin2.Items.Count;
                    dtoPin sitem = memListPin2[i];
                    winCarDetail WinCarDetail = new winCarDetail();
                    WinCarDetail.pub_icar = sitem.idcar;
                    WinCarDetail.Show();
                }
                else
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
            }
            catch
            {

            }

        }

        private void mnuItemPin3Car_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lboxPin3.SelectedItems.Count > 0)
                {
                    int i = lboxPin3.SelectedIndex;
                    int ic = lboxPin3.Items.Count;
                    dtoPin sitem = memListPin3[i];
                    winCarDetail WinCarDetail = new winCarDetail();
                    WinCarDetail.pub_icar = sitem.idcar;
                    WinCarDetail.Show();
                }
                else
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
            }
            catch
            {

            }
        }

        private void mnuItemPin4Car_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lboxPin4.SelectedItems.Count > 0)
                {
                    int i = lboxPin4.SelectedIndex;
                    int ic = lboxPin4.Items.Count;
                    dtoPin sitem = memListPin4[i];
                    winCarDetail WinCarDetail = new winCarDetail();
                    WinCarDetail.pub_icar = sitem.idcar;
                    WinCarDetail.Show();
                }
                else
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
            }
            catch
            {
            }
        }

        private void mnuItemPin5Car_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (lboxPin5.SelectedItems.Count > 0)
                {
                    int i = lboxPin5.SelectedIndex;
                    int ic = lboxPin5.Items.Count;
                    dtoPin sitem = memListPin5[i];
                    winCarDetail WinCarDetail = new winCarDetail();
                    WinCarDetail.pub_icar = sitem.idcar;
                    WinCarDetail.Show();
                }
                else
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
            }
            catch
            {

            }

        }

        #endregion


        // Sýnir menu fyrir aðgerðir á völdum bíl.

        #region btnPinTakeOff_Click
        private void btnPinOneTakeOff_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void btnPinTwoTakeOff_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void btnPinThreeTakeOff_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void btnPinFiveTakeOff_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void btnPinFourTakeOff_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        #endregion


        #region mnuItemPinCar01_Click - Opnar glugga yfir túra sem bíllinn hefur farið.

        // Opnar glugga yfyr túra sem bíll hefur farið.


        
        private void mnuItemPin1Car01_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin1.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                    int i = lboxPin1.SelectedIndex;
                    int ic = lboxPin1.Items.Count;
                    dtoPin sitem = memListPin1[i];                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
                    DBManager dbm = new DBManager();
                    winTurar WinTurar = new winTurar();
                    WinTurar.p_carid = sitem.idcar;
                    WinTurar.p_caridfilter = true;
                    WinTurar.Show();
            }
            catch
            {
                MessageBox.Show("Reyndu aftur.");
            }

            //dataGridTur.ItemsSource = dbm.GetToursPar_CarId(sitem.idcar);
            //childWinTours.Show();
        }

        private void mnuItemPin2Car01_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin2.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                int i = lboxPin2.SelectedIndex;
                int ic = lboxPin2.Items.Count;
                dtoPin sitem = memListPin2[i];
                DBManager dbm = new DBManager();
                winTurar WinTurar = new winTurar();
                WinTurar.p_carid = sitem.idcar;
                WinTurar.p_caridfilter = true;
                WinTurar.Show();
            }
            catch
            {

            }

            //dataGridTur.ItemsSource = dbm.GetToursPar_CarId(sitem.idcar);
            //childWinTours.Show();

        }

        private void mnuItemPin3Car01_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin3.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                int i = lboxPin3.SelectedIndex;
                int ic = lboxPin3.Items.Count;
                dtoPin sitem = memListPin3[i];
                DBManager dbm = new DBManager();
                winTurar WinTurar = new winTurar();
                WinTurar.p_carid = sitem.idcar;
                WinTurar.p_caridfilter = true;
                WinTurar.Show();
            }
            catch
            {

            }

            //dataGridTur.ItemsSource = dbm.GetToursPar_CarId(sitem.idcar);
            //childWinTours.Show();
        }

        private void mnuItemPin4Car01_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin4.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                int i = lboxPin4.SelectedIndex;
                int ic = lboxPin4.Items.Count;
                dtoPin sitem = memListPin4[i];
                DBManager dbm = new DBManager();
                winTurar WinTurar = new winTurar();
                WinTurar.p_carid = sitem.idcar;
                WinTurar.p_caridfilter = true;
                WinTurar.Show();
            }
            catch
            {

            }
        }
        
        private void mnuItemPin5Car01_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin5.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                int i = lboxPin5.SelectedIndex;
                int ic = lboxPin5.Items.Count;
                dtoPin sitem = memListPin5[i];
                DBManager dbm = new DBManager();
                winTurar WinTurar = new winTurar();
                WinTurar.p_carid = sitem.idcar;
                WinTurar.p_caridfilter = true;
                WinTurar.Show();
            }
            catch
            {
            }
        }
        #endregion


        #region mnuItemPinCar02_Click - Opna glugga yfir upplýsingar um bíl og bílstjóra.

        //Tekur valinn bíl af nálinni.
        private void mnuItemPin1Car02_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin1.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin1.SelectedIndex;
                    int ic = lboxPin1.Items.Count;
                    dtoPin sitem = memListPin1[i];
                    memListPin1.Remove(sitem);
                    lboxPin1.ItemsSource = null;
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();

                    SQLManager sm = new SQLManager();
                    string strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 1.";
                    sm.LogRecCreate(strLogMessage);

                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 0 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    memListPinStatus.Remove(itemPinStatus);
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + itemPinStatus.carid.ToString() + " tekinn af stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                    
                }
            }
            catch
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
            }
        }

        private void mnuItemPin2Car02_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin2.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin2.SelectedIndex;
                    int ic = lboxPin2.Items.Count;
                    dtoPin sitem = memListPin2[i];
                    memListPin2.Remove(sitem);
                    lboxPin2.ItemsSource = null;
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();

                    SQLManager sm = new SQLManager();
                    string strLogMessage = SavePin2StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 2.";
                    sm.LogRecCreate(strLogMessage);

                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 1 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    memListPinStatus.Remove(itemPinStatus);

                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + itemPinStatus.carid.ToString() + " tekinn af stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
            }
        }

        private void mnuItemPin3Car02_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin3.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin3.SelectedIndex;
                    int ic = lboxPin3.Items.Count;
                    dtoPin sitem = memListPin3[i];
                    memListPin3.Remove(sitem);
                    lboxPin3.ItemsSource = null;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();

                    SQLManager sm = new SQLManager();
                    string strLogMessage = SavePin3StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 3.";
                    sm.LogRecCreate(strLogMessage);

                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 2 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    memListPinStatus.Remove(itemPinStatus);

                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + itemPinStatus.carid.ToString() + " tekinn af stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
            }

        }

        private void mnuItemPin4Car02_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin4.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin4.SelectedIndex;
                    int ic = lboxPin4.Items.Count;
                    dtoPin sitem = memListPin4[i];
                    memListPin4.Remove(sitem);
                    lboxPin4.ItemsSource = null;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();

                    SQLManager sm = new SQLManager();
                    string strLogMessage = SavePin4StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 4.";
                    sm.LogRecCreate(strLogMessage);

                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 3 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    memListPinStatus.Remove(itemPinStatus);

                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + itemPinStatus.carid.ToString() + " tekinn af stöðulista.";
                    sm.LogRecCreate(strLogMessage);

                }
            }
            catch
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
            }

        }
        private void mnuItemPin5Car02_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (memListPin5.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin5.SelectedIndex;
                    int ic = lboxPin5.Items.Count;
                    dtoPin sitem = memListPin5[i];
                    memListPin5.Remove(sitem);
                    lboxPin5.ItemsSource = null;
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();

                    SQLManager sm = new SQLManager();
                    string strLogMessage = SavePin5StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 5.";
                    sm.LogRecCreate(strLogMessage);

                    int scarid = sitem.idcar;

                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 4 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }

                    memListPinStatus.Remove(itemPinStatus);

                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + itemPinStatus.carid.ToString() + " tekinn af stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
            }
        }



        
        #endregion


        #region mnuItemPin_Car03_Click - Færsla yfir á næstu nál.

        //Færir bíl af nál eitt yfir á nál tvö.
        private void mnuItemPin1Car03_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin1.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin1.SelectedIndex;
                    int ic = lboxPin1.Items.Count;
                    dtoPin sitem = memListPin1[i];
                    memListPin1.Remove(sitem);
                    //Uppfæra skrána memListPin1.bin minnislita fyrir nál 1.
                    string strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 1.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin2.Add(sitem);
                    //Uppfæra skrána memListPin2.bin minnislita fyri nál 2.
                    strLogMessage = SavePin2StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 2.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin1.ItemsSource = null;
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();
                    lboxPin2.ItemsSource = null;
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 1;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 0 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin2.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál tvö í stöðulista.";
                    sm.LogRecCreate(strLogMessage);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());

            }
        }

        //Færir bíl af nál tvö yfir á nál þrjú.
        private void mnuItemPin2Car03_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (lboxPin2.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin2.SelectedIndex;
                    int ic = lboxPin2.Items.Count;
                    dtoPin sitem = memListPin2[i];
                    memListPin2.Remove(sitem);
                    //Uppfæra skrána memListPin2.bin minnislita fyrir nál 2.
                    string strLogMessage = SavePin2StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 2.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin3.Add(sitem);
                    //Uppfæra skrána memListPin3.bin minnislita fyri nál 3.
                    strLogMessage = SavePin3StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 3.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin2.ItemsSource = null;
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();
                    //lboxPin2.ItemsSource = null;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 2;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 1 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin3.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 1 í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }

        }

        //Færir bíl af nál þrjú yfir á nál fjögur.
        private void mnuItemPin3Car03_Click(object sender, RoutedEventArgs e)
        {
            SQLManager sm = new SQLManager();
            try
            {
                if (lboxPin3.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin3.SelectedIndex;
                    int ic = lboxPin3.Items.Count;
                    dtoPin sitem = memListPin3[i];
                    memListPin3.Remove(sitem);
                    //Uppfæra skrána memListPin3.bin minnislita fyrir nál 3.
                    string strLogMessage = SavePin3StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 3.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin4.Add(sitem);
                    //Uppfæra skrána memListPin2.bin minnislita fyri nál 2.
                    strLogMessage = SavePin4StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 4.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin3.ItemsSource = null;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();
                    lboxPin4.ItemsSource = null;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 3;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 2 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin4.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál fjögur í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }
        }

        //Færir bíl af nál fjögur yfir á nál fimm.      
        private void mnuItemPin4Car03_Click(object sender, RoutedEventArgs e)
        {
            
            SQLManager sm = new SQLManager();
            try
            {
                if (lboxPin4.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin4.SelectedIndex;
                    int ic = lboxPin4.Items.Count;
                    dtoPin sitem = memListPin4[i];
                    memListPin4.Remove(sitem);
                    //Uppfæra skrána memListPin4.bin minnislita fyrir nál 4.
                    string strLogMessage = SavePin4StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 4.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin5.Add(sitem);
                    //Uppfæra skrána memListPin5.bin minnislita fyri nál 5.
                    strLogMessage = SavePin5StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 5.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin4.ItemsSource = null;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();
                    //lboxPin2.ItemsSource = null;
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 4;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 3 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin5.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 5 í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }

        }

        private void mnuItemPin5Car03_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (lboxPin5.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin5.SelectedIndex;
                    int ic = lboxPin5.Items.Count;
                    dtoPin sitem = memListPin5[i];
                    memListPin5.Remove(sitem);
                    //Uppfæra skrána memListPin5.bin minnislita fyrir nál 5.
                    string strLogMessage = SavePin5StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 5.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin1.Add(sitem);
                    //Uppfæra skrána memListPin5.bin minnislita fyri nál 1.
                    strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 1.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin5.ItemsSource = null;
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();
                    lboxPin1.ItemsSource = null;
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 0;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 4 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin1.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 1 í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }

        }

        #endregion


        #region mnuItemPin_Car04_Click

        //*** Færa bíl af nál eitt yfir á nál þrjú.
        private void mnuItemPin1Car04_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin1.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin1.SelectedIndex;
                    int ic = lboxPin1.Items.Count;
                    dtoPin sitem = memListPin1[i];
                    memListPin1.Remove(sitem);
                    //Uppfæra skrána memListPin1.bin minnislita fyrir nál 1.
                    string strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 1.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin3.Add(sitem);
                    //Uppfæra skrána memListPin3.bin minnislista fyri nál 3.
                    strLogMessage = SavePin3StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 3.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin1.ItemsSource = null;
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();
                    //lboxPin2.ItemsSource = null;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 2;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 0 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin3.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál þrjú í stöðulista.";
                    sm.LogRecCreate(strLogMessage);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());

            }

        }

        //*** Færa bíl af nál tvö yfir á nál fjögur
        private void mnuItemPin2Car04_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin2.Count() == 0)
                {
                    MessageBox.Show(" Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin2.SelectedIndex;
                    int ic = lboxPin2.Items.Count;
                    dtoPin sitem = memListPin2[i];
                    memListPin2.Remove(sitem);
                    //Uppfæra skrána memListPin1.bin minnislita fyrir nál 1.
                    string strLogMessage = SavePin2StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 2.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin4.Add(sitem);
                    //Uppfæra skrána memListPin3.bin minnislista fyri nál 3.
                    strLogMessage = SavePin3StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 4.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin2.ItemsSource = null;
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();
                    //lboxPin2.ItemsSource = null;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 3;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 1 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin3.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál fjögur í stöðulista.";
                    sm.LogRecCreate(strLogMessage);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());

            }

        }

        //*** Færa bíl af nál þrjú yfir á nál fimm

        private void mnuItemPin3Car04_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin3.Count() == 0)
                {
                    MessageBox.Show(" Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin3.SelectedIndex;
                    int ic = lboxPin3.Items.Count;
                    dtoPin sitem = memListPin3[i];
                    memListPin3.Remove(sitem);
                    //Uppfæra skrána memListPin3.bin minnislita fyrir nál 3.
                    string strLogMessage = SavePin3StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 3.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin5.Add(sitem);
                    //Uppfæra skrána memListPin5.bin minnislista fyri nál 5.
                    strLogMessage = SavePin5StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 5.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin3.ItemsSource = null;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();
                    //lboxPin2.ItemsSource = null;
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 4;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 2 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin4.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál fimm í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }
        }


        //*** Færa bíl af nál fjögur yfir á nál eitt

        private void mnuItemPin4Car04_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin4.Count() == 0)
                {
                    MessageBox.Show(" Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin4.SelectedIndex;
                    int ic = lboxPin4.Items.Count;
                    dtoPin sitem = memListPin4[i];
                    memListPin4.Remove(sitem);
                    //Uppfæra skrána memListPin4.bin minnislita fyrir nál 4.
                    string strLogMessage = SavePin4StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 4.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin1.Add(sitem);
                    //Uppfæra skrána memListPin5.bin minnislista fyri nál 5.
                    strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 1.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin4.ItemsSource = null;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();
                    //lboxPin2.ItemsSource = null;
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 0;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 3 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin1.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál eitt í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }

        }

        //*** Færa bíl af nál fimm yfir á nál tvö

        private void mnuItemPin5Car04_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin5.Count() == 0)
                {
                    MessageBox.Show(" Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin5.SelectedIndex;
                    int ic = lboxPin5.Items.Count;
                    dtoPin sitem = memListPin5[i];
                    memListPin5.Remove(sitem);
                    //Uppfæra skrána memListPin5.bin minnislita fyrir nál 5.
                    string strLogMessage = SavePin5StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 5.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin2.Add(sitem);
                    //Uppfæra skrána memListPin2.bin minnislista fyri nál 2.
                    strLogMessage = SavePin2StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 2.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin5.ItemsSource = null;
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();
                    lboxPin2.ItemsSource = null;
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 1;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 4 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin2.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál eitt í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }

        }
       

        #endregion


        #region mnuItemPin_Car05_Click
        //*** Færa bíl af nál eitt yfir á nál fjögur.
        private void mnuItemPin1Car05_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin1.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin1.SelectedIndex;
                    int ic = lboxPin1.Items.Count;
                    dtoPin sitem = memListPin1[i];
                    memListPin1.Remove(sitem);
                    //Uppfæra skrána memListPin1.bin minnislita fyrir nál 1.
                    string strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 1.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin4.Add(sitem);
                    //Uppfæra skrána memListPin3.bin minnislista fyri nál 4.
                    strLogMessage = SavePin4StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 4.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    //lboxPin1.ItemsSource = null;
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();
                    //lboxPin2.ItemsSource = null;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 3;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 0 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin4.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál fjögur í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }
        }
        //*** Færa bíl af nál tvö yfir á nál fimm.
        private void mnuItemPin2Car05_Click(object sender, RoutedEventArgs e)
        {
            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin2.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin2.SelectedIndex;
                    int ic = lboxPin2.Items.Count;
                    dtoPin sitem = memListPin2[i];
                    memListPin2.Remove(sitem);
                    //Uppfæra skrána memListPin1.bin minnislita fyrir nál 1.
                    string strLogMessage = SavePin2StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 2.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin5.Add(sitem);
                    //Uppfæra skrána memListPin3.bin minnislista fyri nál 4.
                    strLogMessage = SavePin5StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 5.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    //lboxPin1.ItemsSource = null;
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();
                    //lboxPin2.ItemsSource = null;
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 4;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 0 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin5.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál fjögur í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }
        }
        //*** Færa bíl af nál þrjú yfir á nál eitt.
        private void mnuItemPin3Car05_Click(object sender, RoutedEventArgs e)
        {
            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin3.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin3.SelectedIndex;
                    int ic = lboxPin3.Items.Count;
                    dtoPin sitem = memListPin3[i];
                    memListPin3.Remove(sitem);
                    //Uppfæra skrána memListPin3.bin minnislita fyrir nál 3.
                    string strLogMessage = SavePin3StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 3.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin1.Add(sitem);
                    //Uppfæra skrána memListPin3.bin minnislista fyri nál 4.
                    strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 1.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    //lboxPin1.ItemsSource = null;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();
                    //lboxPin2.ItemsSource = null;
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 0;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 2 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin1.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál eitt í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }
        }
         
        private void mnuItemPin4Car05_Click(object sender, RoutedEventArgs e)
        {
            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin4.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll á nálinni. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin4.SelectedIndex;
                    int ic = lboxPin4.Items.Count;
                    dtoPin sitem = memListPin4[i];
                    memListPin4.Remove(sitem);
                    //Uppfæra skrána memListPin3.bin minnislita fyrir nál 4.
                    string strLogMessage = SavePin4StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 4.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin2.Add(sitem);
                    //Uppfæra skrána memListPin1.bin minnislista fyri nál 2.
                    strLogMessage = SavePin2StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 2.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin4.ItemsSource = null;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();
                    lboxPin2.ItemsSource = null;
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();
                    //lboxPin2.SelectedItems.Clear();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 1;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 3 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin4.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál tvö í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }
        }

        private void mnuItemPin5Car05_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin5.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll á nálinni. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin5.SelectedIndex;
                    int ic = lboxPin5.Items.Count;
                    dtoPin sitem = memListPin5[i];
                    memListPin5.Remove(sitem);
                    //Uppfæra skrána memListPin3.bin minnislita fyrir nál 5.
                    string strLogMessage = SavePin5StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 5.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin3.Add(sitem);
                    //Uppfæra skrána memListPin1.bin minnislista fyri nál 3.
                    strLogMessage = SavePin3StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 3.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin5.ItemsSource = null;
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();
                    lboxPin3.ItemsSource = null;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();
                    //lboxPin2.SelectedItems.Clear();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 2;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 4 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin5.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál þrjú í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }

        }
        
        #endregion


        #region mnuItemPin_Car06_Click
        //*** Færa bíl af nál eitt yfir á nál fimm.
        private void mnuItemPin1Car06_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin1.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin1.SelectedIndex;
                    int ic = lboxPin1.Items.Count;
                    dtoPin sitem = memListPin1[i];
                    memListPin1.Remove(sitem);
                    //Uppfæra skrána memListPin1.bin minnislita fyrir nál 1.
                    string strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 1.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin5.Add(sitem);
                    //Uppfæra skrána memListPin3.bin minnislista fyri nál 4.
                    strLogMessage = SavePin5StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 5.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 4;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 0 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin5.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál fimm í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }
        }

        //*** Færa bíl af nál tvö yfir á nál eitt.
        private void mnuItemPin2Car06_Click(object sender, RoutedEventArgs e)
        {
            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin2.Count() == 0)
                {
                    MessageBox.Show(" Það er enginn bíll valinn. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin2.SelectedIndex;
                    int ic = lboxPin2.Items.Count;
                    dtoPin sitem = memListPin2[i];
                    memListPin2.Remove(sitem);
                    //Uppfæra skrána memListPin2.bin minnislita fyrir nál 2.
                    string strLogMessage = SavePin2StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 2.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin1.Add(sitem);
                    //Uppfæra skrána memListPin1.bin minnislista fyri nál 1.
                    strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 1.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin1.ItemsSource = memListPin1;
                    lboxPin1.Items.Refresh();
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 0;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 1 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin1.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál fjögur í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }
        }

        //*** Færa bíl af nál þrjú yfir á nál tvö.
        private void mnuItemPin3Car06_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin3.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll á nálinni. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin3.SelectedIndex;
                    int ic = lboxPin3.Items.Count;
                    dtoPin sitem = memListPin3[i];
                    memListPin3.Remove(sitem);
                    //Uppfæra skrána memListPin3.bin minnislita fyrir nál 3.
                    string strLogMessage = SavePin3StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 3.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin2.Add(sitem);
                    //Uppfæra skrána memListPin1.bin minnislista fyri nál 2.
                    strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 2.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin3.ItemsSource = null;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();
                    lboxPin2.ItemsSource = null;
                    lboxPin2.ItemsSource = memListPin2;
                    lboxPin2.Items.Refresh();
                    //lboxPin2.SelectedItems.Clear();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 1;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 2 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin2.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál tvö í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }

        }


        private void mnuItemPin4Car06_Click(object sender, RoutedEventArgs e)
        {
            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin4.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll á nálinni. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin4.SelectedIndex;
                    int ic = lboxPin4.Items.Count;
                    dtoPin sitem = memListPin4[i];
                    memListPin4.Remove(sitem);
                    //Uppfæra skrána memListPin3.bin minnislita fyrir nál 4.
                    string strLogMessage = SavePin4StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 4.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin3.Add(sitem);
                    //Uppfæra skrána memListPin1.bin minnislista fyri nál 3.
                    strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 3.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin4.ItemsSource = null;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();
                    lboxPin3.ItemsSource = null;
                    lboxPin3.ItemsSource = memListPin3;
                    lboxPin3.Items.Refresh();
                    //lboxPin2.SelectedItems.Clear();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 2;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 2 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin3.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál tvö í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }
        }

        private void mnuItemPin5Car06_Click(object sender, RoutedEventArgs e)
        {

            SQLManager sm = new SQLManager();
            try
            {
                if (memListPin5.Count() == 0)
                {
                    MessageBox.Show("Það er enginn bíll á nálinni. ");
                }
                else
                {
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    int i = lboxPin5.SelectedIndex;
                    int ic = lboxPin5.Items.Count;
                    dtoPin sitem = memListPin5[i];
                    memListPin5.Remove(sitem);
                    //Uppfæra skrána memListPin5.bin minnislita fyrir nál 5.
                    string strLogMessage = SavePin5StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " tekinn af nál 5.";
                    sm.LogRecCreate(strLogMessage);
                    memListPin4.Add(sitem);
                    //Uppfæra skrána memListPin4.bin minnislista fyri nál 4.
                    strLogMessage = SavePin1StatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál 4.";
                    sm.LogRecCreate(strLogMessage);
                    //Uppfæra listbox.
                    lboxPin5.ItemsSource = null;
                    lboxPin5.ItemsSource = memListPin5;
                    lboxPin5.Items.Refresh();
                    lboxPin4.ItemsSource = null;
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();
                    //lboxPin2.SelectedItems.Clear();
                    int scarid = sitem.idcar;
                    itemPinStatus = (from t in memListPinStatus
                                     where t.carid == scarid
                                     select t).FirstOrDefault();
                    itemPinStatus.pinid = 3;

                    var listPinStatus = from ls in memListPinStatus
                                        where ls.pinid == 4 && ls.pinpos > itemPinStatus.pinpos
                                        select ls;
                    foreach (var stat in listPinStatus)
                    {
                        stat.pinpos = stat.pinpos - 1;
                    }
                    itemPinStatus.pinpos = memListPin4.Count - 1;
                    strLogMessage = SavePinStatusToFile();
                    sm.LogRecCreate(strLogMessage);
                    strLogMessage = "Bíll númer " + sitem.idcar.ToString() + " settur á nál fjögur í stöðulista.";
                    sm.LogRecCreate(strLogMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
                sm.LogRecCreate("Kerfisvilla " + ex.ToString());
            }

        }
        
        #endregion





        #region 1 Nálar - Svæði1 - Stöðin - kóði sem er eingöngu á stöðinni. 

        //#1** Búð að lagfæra þegar enginn bíll er valinn. **//
        


        //#2** Búð að lagfæra þegar enginn bíll er valinn. **//


        private void comA1_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (memListPin1.Count > 1)
                {
                    int i = lboxPin1.SelectedIndex;
                    int ic = lboxPin1.Items.Count;
                    dtoPin sitem = memListPin1[i];
                    memListPin1.RemoveAt(i);
                    memListPin1.Insert(i - 1, sitem);
                    lboxPin1.DataContext = memListPin1;
                    lboxPin1.Items.Refresh();


                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {

            }
        }

        //private void lboxPin1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    int i = lboxPin1.SelectedIndex;
        //    MessageBox.Show("Bíll með index númer " + i.ToString());
        //}

       
        // Setja bíl á nál

       

        // Svæði1 - ListItem events

        private void btnCarOnPin_Click(object sender, RoutedEventArgs e)
        {
            //Button b = sender as Button;
            //int id = (int)b.CommandParameter;
            //ListBoxItem lbi = (ListBoxItem)lboxPin1.ItemContainerGenerator.ContainerFromIndex(0);
            //lbi.IsSelected = true;
            //winNewTour _editTour = new winNewTour();
            //_editTour.Show();
            //winCar _viewCar = new winCar();
            //_viewCar.Show();
        }

        private void cmdSendToTour_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bíll sendur í túr");
        }

        // ListItem events

        // Hreynsa textbox

        private void txtArea01_GotFocus(object sender, RoutedEventArgs e)
        {
            txtArea01.Clear();
        }

        private void txtArea01_LostFocus(object sender, RoutedEventArgs e)
        {
            txtArea01.Clear();
        }
        
        // Taka bíl af Nál1
        private void com_A1_Off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lboxPin1.SelectedItems.Count > 0)
            {
                var selectedcars = lboxPin1.SelectedItems;
                int scarid;
                dtoPinStatus itemPinStatus = new dtoPinStatus();
                foreach (dtoPin selcar in selectedcars)
                {
                    foreach(var car in memListPin1)
                    {
                        if (car.pline > selcar.pline)
                        {
                            car.pline = car.pline - 1;
                        }
                    }
                    memListPin1.Remove(selcar);
                    lboxPin1.Items.Refresh();
                    scarid = selcar.idcar;
                    var list = from t in memListPinStatus
                            where t.carid == scarid
                            select t;
                    foreach(var i in list)
                    {
                        itemPinStatus=i;
                    }
                    memListPinStatus.Remove(itemPinStatus);                 
                }
            }
            else
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
            }
        }


        #endregion


        #region 2 Nálar - Svæði2 - Miðbær - kóði sem er eingöngu á þessu svæði.

        //Svæði 2 Byrjar 

                private void txtArea02_GotFocus(object sender, RoutedEventArgs e)
                {
                    txtArea02.Clear();
                }

                private void txtArea02_LostFocus(object sender, RoutedEventArgs e)
                {
                    txtArea02.Text = "";
                }

                #region Ónotaður kódi
                //Þetta er fyrir endurröðun
                private void comA2_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {

                    //if (memListPin5.Count > 1)
                    //{
                    //    txtReorderHeader.Text = "  Endurraða miðbæjarstæði";
                    //    childWinReorderPin.Show();
                    //    childWinReorderPin_DataGrid.ItemsSource = memListPin2;
                    //    _pinid = 1;

                    //}
                    //else
                    //{
                    //    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}

                }
                // Taka bíl af nál
                private void com_A2_Pause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {
                    if (lboxPin2.SelectedItems.Count > 0)
                    {
                        var selectedcars = lboxPin2.SelectedItems;
                        int scarid;
                        dtoPinStatus itemPinStatus = new dtoPinStatus();
                        foreach (dtoPin selcar in selectedcars)
                        {
                            
                            foreach (var car in memListPin2)
                            {
                                if (car.pline > selcar.pline)
                                {
                                    car.pline = car.pline - 1;
                                }
                            }
                            memListPin2.Remove(selcar);
                            lboxPin2.Items.Refresh();
                            scarid = selcar.idcar;
                            var list = from t in memListPinStatus
                                       where t.carid == scarid
                                       select t;
                            foreach (var i in list)
                            {
                                itemPinStatus = i;
                            }
                            memListPinStatus.Remove(itemPinStatus);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Það er enginn bíll valinn. ");
                    }
                }
                #endregion

                // Svæði 2 endar
            #endregion


        #region Nálar Svæði3 - Breiðholt - Ikea

            // Svæði 3 byrjar




            private void txtArea03_GotFocus(object sender, RoutedEventArgs e)
            {
                txtArea03.Clear();
            }

            private void txtArea03_LostFocus(object sender, RoutedEventArgs e)
            {
                txtArea03.Text = "";
            }


            private void txtArea04_KeyUp(object sender, KeyEventArgs e)
            {
                try
                {
                    if (e.Key == Key.Enter)
                    {
                        string _carnum;
                        _carnum = txtArea04.Text;
                        try
                        {
                            int tmp = Convert.ToInt32(_carnum);
                        }
                        catch (Exception h)
                        {
                            txtArea04.Clear();
                            txtArea04.Text = "";
                        }

                        if (txtArea04.Text.ToString().Length == 0)
                        {
                            MessageBox.Show("Það var villa í innslættinum. Sláðu inn númer til að setja á nálina.");
                        }
                        else
                        {
                            txtArea04.Clear();
                            txtArea04.Text = "";
                            int lcar = (from lc in memListCar
                                        where lc.stationid == Convert.ToInt32(_carnum)
                                        select lc).Count();

                            int caronpin = (from lps in memListPinStatus
                                            where lps.carid == Convert.ToInt32(_carnum)
                                            select lps).Count();

                            if (caronpin == 1)
                            {
                                int i = (from lps in memListPinStatus
                                         where lps.carid == Convert.ToInt32(_carnum)
                                         select lps).FirstOrDefault().pinid;
                                if (i == 5)
                                {
                                    dtoPinStatus _status = (from lps in memListPinStatus
                                                            where lps.carid == Convert.ToInt32(_carnum)
                                                            select lps).FirstOrDefault();
                                    memListPinStatus.Remove(_status);
                                    dtoTour _plist = (from t in memListPin6
                                                      where t.idcar == Convert.ToInt32(_carnum)
                                                      select t).FirstOrDefault();
                                    memListPin6.Remove(_plist);
                                    listMessageOnPin.DataContext = memListPin6;
                                    listMessageOnPin.Items.Refresh();


                                    caronpin = 0;
                                }
                            }

                            if (caronpin == 0)
                            {
                                if (lcar == 1)
                                {
                                    var carlist = from vlcar in memListCar
                                                  where vlcar.stationid == Convert.ToInt32(_carnum)
                                                  select vlcar;
                                    pin3_id = pin3_id + 1;
                                    dtoPin pin3 = new dtoPin();
                                    pin3.id = pin3_id;
                                    pin3.idcar = Convert.ToInt32(_carnum);
                                    pin3.idpin = 2;
                                    pin3.pbreak = false;
                                    pin3.pcarcode = carlist.FirstOrDefault().code;
                                    pin3.owner = carlist.FirstOrDefault().owner;
                                    pin3.carsize = carlist.FirstOrDefault().size;
                                    pin3.car1 = carlist.FirstOrDefault().car1;
                                    pin3.car2 = carlist.FirstOrDefault().car2;
                                    pin3.car3 = carlist.FirstOrDefault().car3;
                                    pin3.car4 = carlist.FirstOrDefault().car4;
                                    pin3.car5 = carlist.FirstOrDefault().car5;
                                    memListPin4.Add(pin3);
                                    dtoPinStatus pinStatus = new dtoPinStatus();
                                    pinStatus.carid = pin3.idcar;
                                    pinStatus.pinid = pin3.idpin;
                                    pinStatus.pinpos = memListPin4.Count - 1;
                                    memListPinStatus.Add(pinStatus);
                                    lboxPin4.DataContext = memListPin4;
                                    lboxPin4.Items.Refresh();

                                }
                                else
                                {
                                    MessageBox.Show("Þeð er enginn bíll með þetta stöðvarnúmer. Reyndu aftur.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Bílinn er þegar á nál.");
                            }
                        }

                    }
                }
                catch
                {

                }
            }

            //Þetta er fyrir endurröðun
            private void comA3_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {

                //if (memListPin5.Count > 1)
                //{
                //    txtReorderHeader.Text = "  Endurraða stæði Breiðholt - IKEA ";
                //    childWinReorderPin.Show();
                //    childWinReorderPin_DataGrid.ItemsSource = memListPin3;
                //    _pinid = 2;

                //}
                //else
                //{
                //    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                //}

            }



        // Taka bíl af nál 3



            private void com_A3_Pause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (lboxPin3.SelectedItems.Count > 0)
                {

                    string slog;
                    var selectedcars = lboxPin3.SelectedItems;
                    int scarid;
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    //dtoPin itemDtoPin = new dtoPin();
                    foreach (dtoPin selcar in selectedcars)
                    {
                        //itemDtoPin = selcar;
                        foreach (var car in memListPin3)
                        {
                            if (car.pline > selcar.pline)
                            {
                                car.pline = car.pline - 1;
                            }
                        }
                        //    memListPin2.Remove(selcar);
                        Debug.WriteLine("===================================");
                        Debug.Write(selcar.idcar.ToString());
                        memListPin3.Remove(selcar);
                        //        lboxPin2.Items.Refresh()
                        lboxPin3.Items.Refresh();
                        scarid = selcar.idcar;
                        var list = from t in memListPinStatus
                                   where t.carid == scarid
                                   select t;
                        foreach (var i in list)
                        {
                            itemPinStatus = i;
                        }
                        memListPinStatus.Remove(itemPinStatus);
                        SQLManager sm = new SQLManager();
                        slog = SavePin3StatusToFile();
                        sm.LogRecCreate(slog);
                        slog = SavePinStatusToFile();
                        sm.LogRecCreate(slog);
                        
                    }
                }
                else
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
            }
        // Svæði 3 endar

            #endregion


        #region Nálar - Svæði4 - Árbær / Bauhaus

            //Svæði 4 byrjar



            
        
             private void txtArea04_LostFocus(object sender, RoutedEventArgs e)
            {
                txtArea04.Text = "";
            }

            
        
            private void txtArea04_GotFocus_1(object sender, RoutedEventArgs e)
            {
                txtArea04.Clear();
            }

            
        
            private void txtArea04_GotFocus(object sender, RoutedEventArgs e)
            {
                //txtArea04.Clear();
            }

            //Þetta er fyrir endurröðun
            
            private void comA4_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {


                //if (memListPin5.Count > 1)
                //{
                //    txtReorderHeader.Text = "  Endurraða stæði Árbær - Bauhaus ";
                //    childWinReorderPin.Show();
                //    childWinReorderPin_DataGrid.ItemsSource = memListPin4;
                //    _pinid = 3;

                //}
                //else
                //{
                //    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
            }



            private void com_A4_Pause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (lboxPin4.SelectedItems.Count > 0)
                {
                    var selectedcars = lboxPin4.SelectedItems;
                    int scarid;
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    dtoPin itemDtoPin4 = new dtoPin();
                    Debug.WriteLine("+++++ Fjöldi raða" + lboxPin4.SelectedItems.Count.ToString());
                    foreach (dtoPin selcar in selectedcars)
                    {
                        MessageBox.Show("Þetta er bíll á pin 4 með stöðvarnúmer " + selcar.idcar.ToString());
                        itemDtoPin4 = selcar;
                        scarid = selcar.idcar;

                    }
                    memListPin4.Remove(itemDtoPin4);

                    foreach (var car in memListPin4)

                    {
                        if (car.pline > itemDtoPin4.pline)
                        {
                            car.pline = car.pline - 1;
                        }
                    }
                    lboxPin4.ItemsSource = memListPin4;
                    lboxPin4.Items.Refresh();
                    Debug.WriteLine("**** Þetta er stöðuslistinn **********");
                    foreach (var a in memListPinStatus)
                    {
                        Debug.WriteLine(a.pinid + " - " + a.carid);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Það er enginn bíll valinn. ");
                }
            }

            #endregion


        #region Nálar - Svæði5 - Renna



            //Þetta er fyrir endurröðun
        private void comA5_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {

                //if (memListPin5.Count > 1)
                //{
                //    txtReorderHeader.Text = "  Endurraða Rennu ";
                //    childWinReorderPin.Show(); 
                //    childWinReorderPin_DataGrid.ItemsSource = memListPin5;
                //    _pinid = 4;

                //}
                //else
                //{
                //    MessageBox.Show("Það eru einungis einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                //}

            }

        
        #endregion


        #region Nálar - Túrar í bið

        



        #endregion


        #region Nálar - Bílar í túr

        #endregion


        #endregion

        #endregion

 // End nálar

         #region popup Windows

            #region TourPopUp
            private void tourToolbarcomFilterOn_Click(object sender, RoutedEventArgs e)
            {
                //tourToolbarComFilter.Visibility = Visibility.Visible;
                //tourToolbarComFilterOff.Visibility = Visibility.Visible;
                //tourToolbarDatePickerFrom.Visibility = Visibility.Visible;
                //tourToolbarDatePickerTo.Visibility = Visibility.Visible;
                //tourToolbarLblDateFrom.Visibility = Visibility.Visible;
                //tourToolbarLblDateTo.Visibility = Visibility.Visible;
                //tourToolbarcomFilterOn.Visibility = Visibility.Collapsed;
            }

            private void tourToolbarComFilter_Click(object sender, RoutedEventArgs e)
            {


            }

            private void tourToolbarComFilterOff_Click(object sender, RoutedEventArgs e)
            {
                //tourToolbarComFilter.Visibility = Visibility.Collapsed;
                //tourToolbarComFilterOff.Visibility = Visibility.Collapsed;
                //tourToolbarDatePickerFrom.Visibility = Visibility.Collapsed;
                //tourToolbarDatePickerTo.Visibility = Visibility.Collapsed;
                //tourToolbarLblDateFrom.Visibility = Visibility.Collapsed;
                //tourToolbarLblDateTo.Visibility = Visibility.Collapsed;
                //tourToolbarcomFilterOn.Visibility = Visibility.Visible;
            }


            #endregion

            #region Reorder PopUp
            private void ChildWinReorderPin_ToolbarComSave_Click(object sender, RoutedEventArgs e)
            {
                //switch (_pinid)
                //{
                //    case 0:
                //        {
                //            childWinReorderPin.Close();
                //            memListPin1 = memListPin1.OrderBy(x => x.pline).ToList();
                //            lboxPin1.DataContext = memListPin5;
                //            lboxPin1.Items.Refresh();
                //            break;
                //        }

                //    case 1:
                //        {
                //            childWinReorderPin.Close();
                //            memListPin2 = memListPin2.OrderBy(x => x.pline).ToList();
                //            lboxPin5.DataContext = memListPin2;
                //            lboxPin2.Items.Refresh();
                //            break;
                //        }
                //    case 2:
                //        {
                //            childWinReorderPin.Close();
                //            memListPin3 = memListPin3.OrderBy(x => x.pline).ToList();
                //            lboxPin3.DataContext = memListPin3;
                //            lboxPin3.Items.Refresh();
                //            break;
                //        }
                //    case 3:
                //        {
                //            childWinReorderPin.Close();
                //            memListPin4 = memListPin4.OrderBy(x => x.pline).ToList();
                //            lboxPin4.DataContext = memListPin4;
                //            lboxPin4.Items.Refresh();
                //            break;
                //        }
                //    case 4:
                //        {
                //            childWinReorderPin.Close();
                //            memListPin5 = memListPin5.OrderBy(x => x.pline).ToList();
                //            lboxPin5.DataContext = memListPin5;
                //            lboxPin5.Items.Refresh();
                //            break;
                //        }
                //    default:
                //        MessageBox.Show("Númer nálarinnar er ekki rétt");
                //        break;


                //}

            }

            #endregion


            #endregion

 //Hlaða Töflum í minni.

        #region Hlaða töflum í minni

        private void LoadTurarToMem()
        {
            using (Stream stream = File.Open(@"C:\dbsendill\tbl_turar.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                memTableTurar = (DataTable)bin.Deserialize(stream);
                int rcount = memTableTurar.Rows.Count;
            }
        }
            
            
            private void LoadCustomersToMem()
            {
                try
                {
                    using (Stream stream = File.Open(@"C:\dbsendill\tbl_vidskiptamenn.bin", FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();

                        memTableCustomers = (DataTable)bin.Deserialize(stream);
                        int rcount = memTableCustomers.Rows.Count;
                    }
                }
                catch (IOException)
                {
                }
            }
            
            
            private void LoadCarsToMem()
            {
                try
                {
                    using (Stream stream = File.Open(@"C:\dbsendill\tbl_car.bin", FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();

                        memTableCars = (DataTable)bin.Deserialize(stream);

                    }
                }
                catch (IOException)
                {
                }
            }

        #endregion

 //Hlaða listum í minni

        #region Hlaða listum í minni

            private void LoadToursToList()
            {
                try
                {
                    DBManager db = new DBManager();
                    memListTour = db.GetAllToursFromFile().ToList();
                }
                catch (IOException)
                {
                }
            }
            
            private void LoadCarsToList()
            {
                try
                {
                    //using (Stream stream = File.Open(@"C:\dbsendill\list_carall.bin", FileMode.Open))
                    //{
                    //    BinaryFormatter bin = new BinaryFormatter();
                    //    memListCar = (List<dtoCars>)bin.Deserialize(stream);
                    //}

                    DBManager dm = new DBManager();
                    memListCar=dm.GetAllCars().ToList();

                }
                catch (IOException)
                {
                }
            }

            private void LoadAppSysSettingsToList()
            {
                try
                {
                    using (Stream stream = File.Open(@"C:\Sendill\DataFiles\appsyssettings.bin", FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        memAppSysSettings = (List<appSysSettings>)bin.Deserialize(stream);
                    }
                }
                catch (IOException)
                {
                }
            }

            private void LoadPin1StatusToList()
            {
                try
                {
                    using (Stream stream = File.Open(@"C:\Sendill\DataFiles\list_pin1.bin", FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        memListPin1 = (List<dtoPin>)bin.Deserialize(stream);
                    }
                }
                catch (IOException)
                {
                }
            }

            private void LoadPinStatusToList()
            {
                try
                {
                    using (Stream stream = File.Open(@"C:\Sendill\DataFiles\list_pinstatus.bin", FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        memListPinStatus = (List<dtoPinStatus>)bin.Deserialize(stream);
                    }
                }
                catch (IOException)
                {
                }
            }


            


        #endregion

        #region Functions Save to File - og ónotað.

        public void funcFillToursOnPin()
            {
                List<dtoTour> ltour = new List<dtoTour>();
                dtoTour objtour = new dtoTour();
                objtour.id = 1;
                objtour.idpin=1;
                objtour.taddress="Gtundarstíg 38";
                objtour.tcontact="Jón Sigurbjörnsson";
                objtour.idcar = 3;
                ltour.Add(objtour);
                listMessageOnPin.ItemsSource=ltour;
            }

        public void funcCreatePinItemDataTemlate(int listboxindex)
            {
                var btrigger = new DataTrigger();
                var bstyle = new Style();
            }

        public void funcAddNewTour(dtoTour par_tour)
        {
            memListPin6.Add(par_tour);
            listMessageOnPin.ItemsSource = memListPin6;
            listMessageOnPin.Items.Refresh();
        }

        public string SavePin1StatusToFile()
        {
            ConfigFile cf= new ConfigFile();
            string spath = cf.GetLocalBinFolder() + "\\DataFiles\\";
            using (FileStream fst = new FileStream(spath+"list_pin1.bin", FileMode.Create))
            {
                    BinaryFormatter bff = new BinaryFormatter();
                    bff.Serialize(fst, memListPin1);
                    fst.Close();
                    string rvalue = " memListPin1 uppfærður";
                    return rvalue;
            }
        }

        public string SavePin2StatusToFile()
        {
            ConfigFile cf = new ConfigFile();
            string spath = cf.GetLocalBinFolder() + "\\DataFiles\\";
            using (FileStream fst = new FileStream(spath+"list_pin2.bin", FileMode.Create))
            {

                BinaryFormatter bff = new BinaryFormatter();
                bff.Serialize(fst, memListPin2);
                fst.Close();
                string rvalue = " memListPin2 uppfærður";
                return rvalue;
            }
        }

        public string SavePin3StatusToFile()
        {
            ConfigFile cf = new ConfigFile();
            string spath = cf.GetLocalBinFolder() + "\\DataFiles\\";
            using (FileStream fst = new FileStream(spath+"list_pin3.bin", FileMode.Create))
            {

                BinaryFormatter bff = new BinaryFormatter();
                bff.Serialize(fst, memListPin3);
                fst.Close();
                string rvalue = " memListPin3 uppfærður";
                return rvalue;
            }
        }

        public string SavePin4StatusToFile()
        {
            ConfigFile cf = new ConfigFile();
            string spath = cf.GetLocalBinFolder() + "\\DataFiles\\";
            using (FileStream fst = new FileStream(spath+"list_pin4.bin", FileMode.Create))
            {
                
                BinaryFormatter bff = new BinaryFormatter();
                bff.Serialize(fst, memListPin4);
                fst.Close();
                string rvalue = " memListPin4 uppfærður";
                return rvalue;
            }
        }
 
        public string SavePin5StatusToFile()
        {
            ConfigFile cf = new ConfigFile();
            string spath = cf.GetLocalBinFolder() + "\\DataFiles\\";
            using (FileStream fst = new FileStream(spath+"list_pin5.bin", FileMode.Create))
            {
                BinaryFormatter bff = new BinaryFormatter();
                bff.Serialize(fst, memListPin5);
                fst.Close();
                string rvalue = " memListPin5 uppfærður";
                return rvalue;
            }
        }

        public string SavePin6StatusToFile()
        {
            ConfigFile cf = new ConfigFile();
            string spath = cf.GetLocalBinFolder() + "\\DataFiles\\";
            using (FileStream fst = new FileStream(spath+"list_pin6.bin", FileMode.Create))
            {
                BinaryFormatter bff = new BinaryFormatter();
                bff.Serialize(fst, memListPin5);
                fst.Close();
                string rvalue = " memListPin6 uppfærður";
                return rvalue;
            }
        }

        public string SavePinStatusToFile()
        {
            using (FileStream fst = new FileStream(@"C:\Sendill\DataFiles\list_pinstatus.bin", FileMode.Create))
            {
                BinaryFormatter bff = new BinaryFormatter();
                bff.Serialize(fst, memListPinStatus);
                fst.Close();
                string rvalue = " memListStatus uppfærður";
                return rvalue;
            }
            //DBManager dm = new DBManager();
            //return dm.SavePinStatusToFile();
        }

        public string SaveAppSysRunSettings()
        {
            FileStream fst = new FileStream(@"C:\Sendill\DataFiles\appsyssettings.bin", FileMode.Create);

            BinaryFormatter bff = new BinaryFormatter();
            bff.Serialize(fst, memAppSysSettings);
            fst.Close();
            string rvalue = " Appsyssettings.bin uppfærð";
            return rvalue;

        }
        #endregion
     
        #region Ónotaður kóði

        void mitem01_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Þetta er Nightservice menuitem1");
            //throw new NotImplementedException();
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void image6_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
        }

        private void limage2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //winDesighn wt = new winDesighn();
            //wt.ShowDialog();
            //winTests wt = new winTests();
            //wt.Show();
            frmAdmin fadm = new frmAdmin();
            fadm.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

            private void TextBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
            {
            }
            private void txtArea01_TextChanged(object sender, TextChangedEventArgs e)
            {
            }
            private void txtArea01_TextInput(object sender, TextCompositionEventArgs e)
            {
            }
        
            private void comNightService_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                //comNightService.ContextMenu
            }

            private void image1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
            {

            }

            private void image6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                //frmReports rep = new frmReports();
                //rep.ShowDialog();
                //MessageBox.Show("Skýrslur í núverandi útgæfu eru í túralista.");
                ViewWindow ww = new ViewWindow();
                //System.Windows.Forms.Screen s2 = System.Windows.Forms.Screen.AllScreens[0];
                //System.Drawing.Rectangle r2 = s2.WorkingArea;
                //MessageBox.Show(r2.Height.ToString()+" x " + r2.Width.ToString());
                //ww.WindowState = System.Windows.WindowState.Normal;
                //ww.Top = r2.Top+10;
                //ww.Left = r2.Left+10;
                //ww.WindowState = System.Windows.WindowState.Maximized;
                ww.Show();
                
            }

            private void txtArea03_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

        //private void comLoadDataToLocalCouch_Click(object sender, RoutedEventArgs e)
        //{
        //    CouchListMemTalk couchtalk = new CouchListMemTalk();
        //   bool tf= couchtalk.UpdateTourToCouch(memListTour);
        //   if (tf == true)
        //   {
        //       MessageBox.Show("Túrar  uppfærðir í local gagnagrunn");
        //   }
        //   else
        //   {
        //       MessageBox.Show("Uppfærsla tókst ekki");
        //   }
        //}

        //private void comLoadCarsToLocalCouch_Click(object sender, RoutedEventArgs e)
        //{
        //    couchCars ccars = new couchCars();
        //    bool cf = ccars.UpdataCarsToCouch(memListCar);
        //    if (cf == true)
        //    {
        //        MessageBox.Show("Bílar  uppfærðir í local gagnagrunn");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Uppfærsla tókst ekki");
        //    }
        //}

        // Svæði 4 endar


    ////        {
    ////    public int id { get; set; }
    ////    public int stationid { get; set; }
    ////    public string carnumber { get; set; }
    ////    public string code { get; set; }
    ////    public bool listed { get; set; }
    ////    public string carname { get; set; }
    ////    public bool car1 { get; set; }
    ////    public bool car2 { get; set; }
    ////    public bool car3 { get; set; }
    ////    public bool car4 { get; set; }
    ////    public bool car5 { get; set; }
    ////    public bool car6 { get; set; }
    ////    public bool car7 { get; set; }
    ////    public bool car8 { get; set; }
    ////    public bool car9 { get; set; }
    ////    public bool car10 { get; set; }
    ////    public float backdoorlength { get; set; }
    ////    public float backdoorheight { get; set; }
    ////    public float sidedoorlength { get; set; }
    ////    public float sidedoorheight { get; set; }
    ////    public float weightlimit { get; set; }
    ////    public float liftsize { get; set; }
    ////    public float volume { get; set; }
    ////    public float width { get; set; }
    ////    public string model { get; set; }
    ////    public float maxcarry { get; set; }
    ////    public string owner { get; set; }
    ////    public string kt { get; set; }
    ////    public string address { get; set; }
    ////    public string town { get; set; }
    ////    public string postcode { get; set; }
    ////    public string phone { get; set; }
    ////    public string mobile { get; set; }
    ////    public string driver { get; set; }
    ////    public string dkt { get; set; }
    ////    public string daddress { get; set; }
    ////    public string dtown { get; set; }
    ////    public string dpostcode { get; set; }
    ////    public string dphone { get; set; }
    ////    public string dmobile { get; set; }
    ////    public float heightofbox { get; set; }

        ////}
        #endregion

        #region Debug

            private void comTest_Click(object sender, RoutedEventArgs e)
            {
                
                Debug.WriteLine("Númer nálar - Númer bíls");
                Debug.WriteLine("------------------------");
                
                foreach (var i in memListPinStatus)
                {

                    Debug.WriteLine("    "+i.pinid.ToString() + "      -      " + i.carid.ToString());

                }
            }

        #endregion

            private void btnNewTour_Click(object sender, RoutedEventArgs e)
            {

            }

            private void lboxPin1_SizeChanged(object sender, SizeChangedEventArgs e)
            {

            }

            private void lboxPin1_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                //int i = lboxPin1.SelectedIndex;
                //ListBoxItem lbi = (ListBoxItem)lboxPin1.ItemContainerGenerator.ContainerFromIndex(i);
                //lbi.SetValue(Name,"nn1");
                //Object obj = nn1.FindName("btnSendToTour");
                //if (obj is Button)
                //{
                //    Button btn = (Button)obj;
                //    MessageBox.Show(btn.Name+" Fundinn");
                //}
                selectedItem = lboxPin1.SelectedItems;
            }

            private void btnSendToTour_Loaded(object sender, RoutedEventArgs e)
            {
                //Button b = sender as Button;
                //b.Name = "newButton";
                //MessageBox.Show(b.Name);
                //MessageBox.Show(b.Parent.GetType().Name);
                //b.IsEnabled = false;
            }

            private void lboxPin2_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                selectedItem = lboxPin2.SelectedItems;
            }

            private void Window_Closed(object sender, EventArgs e)
            {
                var mysettings = memAppSysSettings.Find(item => item.CODE == "APPSTATUS");
                mysettings.VALUE = "SHUTDOWN";
                //appSysSettings apps = new appSysSettings();
                //apps.TYPE = "RUN";
                //apps.CODE = "APPSTATUS";
                //apps.VALUE = "SHUTDOWN";
                //memAppSysSettings.Add(apps);
                string strLog = SaveAppSysRunSettings();
                SQLManager sm = new SQLManager();
                sm.LogRecCreate("AppSysSettings APPSTATUS=SHUTDOWN");
                
            }
            #region Drag Drop

            private void lboxPin1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {

                //startPoint = e.GetPosition(null);


            }

            private void lboxPin1_PreviewMouseMove(object sender, MouseEventArgs e)
            {

                //Point mousePos = e.GetPosition(null);
                //Vector diff = startPoint - mousePos;

                //if(e.LeftButton==MouseButtonState.Pressed &&
                //    Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || 
                //    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance )
                //{

                //    ListBox listBox = sender as ListBox;
                //    var listBoxItem = FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);

                //    dtoPin Pin = (dtoPin)listBox.ItemContainerGenerator.ItemFromContainer(listBoxItem);

                //    DataObject dragData = new DataObject(DataFormats.Text, Pin);
                //    DragDrop.DoDragDrop(listBoxItem, dragData, DragDropEffects.Move);


                //}

            }

            private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
            {
                do
                {
                    if (current is T)
                    {
                        return (T)current;
                    }
                    current = VisualTreeHelper.GetParent(current);
                }
                while (current != null);
                return null;
            }

            private void lboxPin2_Drop(object sender, DragEventArgs e)
            {

                //if (e.Data.GetDataPresent("myFormat"))
                //{
                //    dtoPin Pin = e.Data.GetData("myFormat") as dtoPin;
                //    ListBox listBox = sender as ListBox;
                //    listBox.Items.Add(Pin);
                //}



            }

            private void lboxPin2_DragEnter(object sender, DragEventArgs e)
            {

                //if (e.Data.GetDataPresent("myFormat") || sender == e.Source)
                //{
                //    e.Effects = DragDropEffects.None;
                //}

            }

            #endregion

            private void image5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                winTests ws = new winTests();
                ws.Show();
            }

            private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            {
                var openwin = Application.Current.Windows;
                var count = openwin.Count;
                if (count>1)
                {
                    for (int i = 1; i <= count; i++)
                    {
                        openwin[1].Close();
                    }
                }
            }

            private void lboxPin2_LostFocus(object sender, RoutedEventArgs e)
            {
                //lboxPin2.UnselectAll();
            }

            private void lboxPin1_LostFocus(object sender, RoutedEventArgs e)
            {
                //
            }

        public void funcItemSelectList1True()
            {
                lboxPin2.UnselectAll();
                lboxPin3.UnselectAll();
                lboxPin4.UnselectAll();
                lboxPin5.UnselectAll();
            }

        public void funcItemSelectList2True()
        {
            lboxPin1.UnselectAll();
            lboxPin3.UnselectAll();
            lboxPin4.UnselectAll();
            lboxPin5.UnselectAll();
        }

        public void funcItemSelectList3True()
        {
            lboxPin1.UnselectAll();
            lboxPin2.UnselectAll();
            lboxPin4.UnselectAll();
            lboxPin5.UnselectAll();
        }

        public void funcItemSelectList4True()
        {
            lboxPin1.UnselectAll();
            lboxPin2.UnselectAll();
            lboxPin3.UnselectAll();
            lboxPin5.UnselectAll();
        }

        public void funcItemSelectList5True()
        {
            lboxPin1.UnselectAll();
            lboxPin2.UnselectAll();
            lboxPin3.UnselectAll();
            lboxPin4.UnselectAll();
        }

        private void lboxPin1_GotFocus(object sender, RoutedEventArgs e)
        {
            funcItemSelectList1True();
        }

        private void lboxPin2_GotFocus(object sender, RoutedEventArgs e)
        {
            funcItemSelectList2True();
        }

        private void lboxPin4_GotFocus(object sender, RoutedEventArgs e)
        {
            funcItemSelectList4True();
        }

        private void lboxPin3_GotFocus(object sender, RoutedEventArgs e)
        {
            funcItemSelectList3True();
        }

        private void lboxPin5_GotFocus(object sender, RoutedEventArgs e)
        {
            funcItemSelectList5True();
        }

        private void btnMessagesRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadMessages();
        }
    }
}