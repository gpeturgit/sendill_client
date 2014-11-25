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

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DataTable memTableCars;
        public DataTable memTableCustomers;
        public DataTable memTableTurar;
        public bool isStartup;
        public string extDataChange;
        public List<dtoPin>   memListPin1  =  new List<dtoPin>();
        public List<dtoPin>   memListPin2  =  new List<dtoPin>();
        public List<dtoPin>   memListPin3  =  new List<dtoPin>();
        public List<dtoPin>   memListPin4  =  new List<dtoPin>();
        public List<dtoPin>   memListPin5  =  new List<dtoPin>();
        public List<dtoCars>  memListCar   =  new List<dtoCars>();
        public List<dtoTour>  memListTour  =  new List<dtoTour>();
        public List<dtoPinStatus> memListPinStatus = new List<dtoPinStatus>();
        public int pin1_id = 0;
        public int pin2_id = 0;
        public int pin3_id = 0;
        public int pin4_id = 0;
        public int pin5_id = 0;
        public int _pinid;
        public int carid;

        public MainWindow()
        {
            InitializeComponent();
            
            LoadCarsToMem();
            LoadCustomersToMem();
            LoadCarsToList();
                                                                  
            ContextMenu mnuNightService = new ContextMenu();
            MenuItem mitem01 = new MenuItem();
            
            mitem01.Click += new RoutedEventHandler(mitem01_Click);
            mnuNightService.Items.Add(mitem01);            
            isStartup=true;
            LoadToursToList();
            funcFillToursOnPin();
        }


        // Stillingar fyrir local-Couch

        #region local-Couch
        private void image7_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            PopupLocalUpdateWindow.Show();
        }
        #endregion

        // End stillingar fyrir local-Couch


        // Program navigation

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


        // End programm navigation


        //Nálar

        #region Nálar - Stöðvarlistar fyrir bíla



            #region Nálar - Svæði1 - Stöðin

        private void comA1_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (memListPin5.Count > 1)
            {
                txtReorderHeader.Text = "  Endurraða Stöðin";
                childWinReorderPin.Show();
                childWinReorderPin_DataGrid.ItemsSource = memListPin1;
                _pinid = 0;

            }
            else
            {
                MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       
        // Setja bíl á nál

        private void txtArea01_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                if (txtArea01.Text.ToString().Length == 0)
                {
                    MessageBox.Show("Reyturinn fyrir stöðvarnúmerið er tómur. Sláðu inn númer til að setja á nálina.");
                }
                else
                {
                    string _carnum;
                    _carnum = txtArea01.Text;
                    txtArea01.Clear();
                    txtArea01.Text = "";
                    int lcar = (from lc in memListCar
                                where lc.stationid == Convert.ToInt32(_carnum)
                                select lc).Count();
                    int caronpin=(from lps in memListPinStatus
                                where lps.carid == Convert.ToInt32(_carnum)
                                select lps).Count();
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
                            memListPin1.Add(pin1);

                            //Settur status inní lista yfir status

                            dtoPinStatus pinStatus = new dtoPinStatus();
                            pinStatus.carid = pin1.idcar;
                            pinStatus.pinid = pin1.idpin;
                            memListPinStatus.Add(pinStatus);
                            //var list = from c in memListPin1
                            //           select c;
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

        
        
        // Svæði1 - ListItem events


        private void btnSendToTour_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int id = (int)b.CommandParameter;
            MessageBox.Show("Senda þetta bílnúmer í túr " + id.ToString());
        }




        private void btnCoffeebreak_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Klikkað á kaffitíma.");

        }

        private void btnCarOnPin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSendToTour_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bíll sendur í túr");
        }

        // ListItem events

        //Context menu
        // Upplýsingar um bíl
        private void mnuItemPin1Car_Click(object sender, RoutedEventArgs e)
        {

        }
        // Opnar glugga yfyr túra sem bíll hefur farið.
        private void mnuItemPin1Car01_Click(object sender, RoutedEventArgs e)
        {
            MenuItem b = sender as MenuItem;
            int id = (int)b.CommandParameter;
            DBManager dbm = new DBManager();
            dataGridTur.ItemsSource = dbm.GetToursPar_CarId(id);       
            childWinTours.Show();
        }

        private void mnuItemPin1Car02_Click(object sender, RoutedEventArgs e)
        {

        }

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
                    MessageBox.Show("Þetta er bíll með stöðvarnúmer " + selcar.idcar.ToString());
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
                    Debug.WriteLine("Þetta er línan sem á að stroka út úr stöðulistanum.");
                    foreach(var i in list)
                    {
                        itemPinStatus=i;
                        Debug.WriteLine(i.pinid + " - " + i.carid);
                    }
                    memListPinStatus.Remove(itemPinStatus);
                    
                    Debug.WriteLine("Þetta er stöðuslistinn");
                    foreach(var a in memListPinStatus)
                    {
                        Debug.WriteLine(a.pinid + " - " + a.carid);
                    }
                    
                    
                }
            }
            else
            {
                MessageBox.Show("Það er enginn bíll valinn. ");
            }
        }
            
            
            #endregion


            #region Nálar - Svæði2 - Miðbær

                //Svæði 2 Byrjar 
                private void txtArea02_KeyDown(object sender, KeyEventArgs e)
                {
                    if (e.Key == Key.Enter)
                    {
                        string _carnum;
                        string _value = "";
                        bool _istrue;
                        _carnum = txtArea02.Text;
                        if (_carnum==_value)
                        {
                            _istrue = true;
                        }
                        else
                        {
                            _istrue = false;
                        }

                        if (_istrue == false)
                        {
                            txtArea02.Clear();
                            txtArea02.Text = "";
                            int lcar = (from lc in memListCar
                                        where lc.stationid == Convert.ToInt32(_carnum)
                                        select lc).Count();
                            int caronpin = (from lps in memListPinStatus
                                            where lps.carid == Convert.ToInt32(_carnum)
                                            select lps).Count();
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
                                    memListPin2.Add(pin2);
                                    dtoPinStatus pinStatus = new dtoPinStatus();
                                    pinStatus.carid = pin2.idcar;
                                    pinStatus.pinid = pin2.idpin;
                                    memListPinStatus.Add(pinStatus);

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
                        else
                        {
                            MessageBox.Show("Engin tala var sleginn inn");
                        }
                    }
                }
                //    if (e.Key == Key.Enter)
                //    {
                //        string _carnum;
                //        _carnum = txtArea01.Text;
                //        txtArea01.Clear();
                //        txtArea01.Text = "";
                //        int lcar = (from lc in memListCar
                //                    where lc.stationid == Convert.ToInt32(_carnum)
                //                    select lc).Count();
                //        if (lcar == 1)
                //        {
                //            var carlist = from vlcar in memListCar
                //                          where vlcar.stationid == Convert.ToInt32(_carnum)
                //                          select vlcar;
                //            pin1_id = pin1_id + 1;
                //            dtoPin pin1 = new dtoPin();
                //            pin1.id = pin1_id;
                //            pin1.idcar = Convert.ToInt32(_carnum);
                //            pin1.idpin = pin1_id;
                //            pin1.pbreak = true;
                //            pin1.pcarcode = carlist.FirstOrDefault().code;
                //            memListPin1.Add(pin1);

                //            //var list = from c in memListPin1
                //            //           select c;
                //            lboxPin1.DataContext = memListPin1;
                //            lboxPin1.Items.Refresh();
                //        }
                //        else
                //        {
                //            MessageBox.Show("Þeð er enginn bíll með þetta stöðvarnúmer. Reyndu aftur.");
                //        }
                //}

                private void txtArea02_GotFocus(object sender, RoutedEventArgs e)
                {
                    txtArea02.Clear();
                }

                private void txtArea02_LostFocus(object sender, RoutedEventArgs e)
                {
                    txtArea02.Text = "";
                }

                //Þetta er fyrir endurröðun
                private void comA2_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {

                    if (memListPin5.Count > 1)
                    {
                        txtReorderHeader.Text = "  Endurraða miðbæjarstæði";
                        childWinReorderPin.Show();
                        childWinReorderPin_DataGrid.ItemsSource = memListPin2;
                        _pinid = 1;

                    }
                    else
                    {
                        MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

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
                            MessageBox.Show("Þetta er bíll með stöðvarnúmer " + selcar.idcar.ToString());
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
                            Debug.WriteLine("Þetta er línan sem á að stroka út úr stöðulistanum.");
                            foreach (var i in list)
                            {
                                itemPinStatus = i;
                                Debug.WriteLine(i.pinid + " - " + i.carid);
                            }
                            memListPinStatus.Remove(itemPinStatus);

                            Debug.WriteLine("Þetta er stöðuslistinn");
                            foreach (var a in memListPinStatus)
                            {
                                Debug.WriteLine(a.pinid + " - " + a.carid);
                            }


                        }
                    }
                    else
                    {
                        MessageBox.Show("Það er enginn bíll valinn. ");
                    }
                }

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

            private void txtArea03_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Enter)
                {
                    string _carnum;
                    _carnum = txtArea03.Text;

                    txtArea03.Clear();
                    txtArea03.Text = "";
                    int lcar = (from lc in memListCar
                                where lc.stationid == Convert.ToInt32(_carnum)
                                select lc).Count();

                    int caronpin = (from lps in memListPinStatus
                                    where lps.carid == Convert.ToInt32(_carnum)
                                    select lps).Count();
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
                        memListPin3.Add(pin3);
                        Debug.Write(pin3.id.ToString());
                        dtoPinStatus pinStatus = new dtoPinStatus();
                        pinStatus.carid = pin3.idcar;
                        pinStatus.pinid = pin3.idpin;
                        memListPinStatus.Add(pinStatus);
                        lboxPin3.DataContext = memListPin3;
                        lboxPin3.Items.Refresh();

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

            //Þetta er fyrir endurröðun
            private void comA3_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {

                if (memListPin5.Count > 1)
                {
                    txtReorderHeader.Text = "  Endurraða stæði Breiðholt - IKEA ";
                    childWinReorderPin.Show();
                    childWinReorderPin_DataGrid.ItemsSource = memListPin3;
                    _pinid = 2;

                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }



        // Taka bíl af nál 3



            private void com_A3_Pause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                Debug.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Debug.WriteLine("++++++++ Taka valinn bíl af nál 3 +++++++++++++++++++++");
                if (lboxPin3.SelectedItems.Count > 0)
                {
                    var selectedcars = lboxPin3.SelectedItems;
                    
                    dtoPinStatus itemPinStatus = new dtoPinStatus();
                    dtoPin itemDtoPin = new dtoPin();
                    Debug.WriteLine("+++++ Fjöldi raða"+ lboxPin3.SelectedItems.Count.ToString() );
                    foreach(dtoPin selcar in selectedcars)
                    {
                       MessageBox.Show("Þetta er bíll með stöðvarnúmer " + selcar.idcar.ToString());
                       itemDtoPin = selcar;
                        //foreach (var car in memListPin3)
                        //{
                        //    if (car.pline > selcar.pline)
                        //    {
                        //        car.pline = car.pline - 1;
                        //    }
                        //}
                        //foreach (var i in memListPin3)
                        //{
                        //    Debug.WriteLine("-------- " + i.idcar);
                        //}
                        //memListPin3.Remove(selcar);
                        //lboxPin3.Items.Refresh();
            //            scarid = selcar.idcar;
            //            var list = from t in memListPinStatus
            //                       where t.carid == scarid
            //                       select t;
            //            Debug.WriteLine("Þetta er línan sem á að stroka út úr stöðulistanum.");
            //            foreach (var i in list)
            //            {
            //                itemPinStatus = i;
            //                Debug.WriteLine(i.pinid + " - " + i.carid);
            //            }
            //            memListPinStatus.Remove(itemPinStatus);

            //            Debug.WriteLine("Þetta er stöðuslistinn");
            //            foreach (var a in memListPinStatus)
            //            {
            //                Debug.WriteLine(a.pinid + " - " + a.carid);
            //            }


                    }
                    memListPin3.Remove(itemDtoPin);
                    lboxPin3.Items.Refresh();
                    foreach (var i in memListPin3)
                    {
                        if (i.pline > itemDtoPin.pline)
                        {
                            i.pline = i.pline - 1;
                        }
                    }
                    itemDtoPin = null;
                    //var list = from t in memListPinStatus
                    //           where t.carid == itemDtoPin.idcar
                    //           select t;
                    //foreach (var i in list)
                    //{
                    //    itemPinStatus = i;
                    //}
                    memListPinStatus.Remove(itemPinStatus);

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

            private void txtArea04_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Enter)
                {
                    string _carnum;
                    _carnum = txtArea04.Text;
                    txtArea04.Clear();
                    txtArea04.Text = "";
                    int lcar = (from lc in memListCar
                                where lc.stationid == Convert.ToInt32(_carnum)
                                select lc).Count();
                    int caronpin = (from lps in memListPinStatus
                                    where lps.carid == Convert.ToInt32(_carnum)
                                    select lps).Count();
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

                        memListPin4.Add(pin4);
                        dtoPinStatus pinStatus = new dtoPinStatus();
                        pinStatus.carid = pin4.idcar;
                        pinStatus.pinid = pin4.idpin;
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
                        MessageBox.Show("Bílinn er þegar á nál");
                    }
                }
           
            }

            private void txtArea04_LostFocus(object sender, RoutedEventArgs e)
            {
                txtArea04.Text = "";
            }

            private void txtArea04_GotFocus(object sender, RoutedEventArgs e)
            {
                txtArea04.Clear();
            }

            //Þetta er fyrir endurröðun
            private void comA4_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {


                if (memListPin5.Count > 1)
                {
                    txtReorderHeader.Text = "  Endurraða stæði Árbær - Bauhaus ";
                    childWinReorderPin.Show();
                    childWinReorderPin_DataGrid.ItemsSource = memListPin4;
                    _pinid = 3;

                }
                else
                {
                    MessageBox.Show("Það er einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

                        //Debug.WriteLine("Þetta er línan sem á að stroka út úr stöðulistanum.");
                        //foreach (var i in list)
                        //{
                        //    itemPinStatus = i;
                        //    Debug.WriteLine(i.pinid + " - " + i.carid);
                        //}
                        




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



            private void txtArea05_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Enter)
                {
                    string _carnum;
                    _carnum = txtArea05.Text;
                    txtArea05.Clear();
                    txtArea05.Text = "";
                    int lcar = (from lc in memListCar
                                where lc.stationid == Convert.ToInt32(_carnum)
                                select lc).Count();

                    int caronpin = (from lps in memListPinStatus
                                where lps.carid == Convert.ToInt32(_carnum)
                                select lps).Count();
                    if (caronpin == 0)
                    {


                        if (lcar == 1)
                        {
                            var carlist = from vlcar in memListCar
                                          where vlcar.stationid == Convert.ToInt32(_carnum)
                                          select vlcar;

                            pin5_id = pin5_id + 1;
                            dtoPin pin5 = new dtoPin();
                            pin5.id = pin4_id;
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
                            pin5.pline = memListPin5.Count() + 1;

                            // Bæta við status.

                            memListPin5.Add(pin5);
                            dtoPinStatus pinStatus = new dtoPinStatus();
                            pinStatus.carid = pin5.idcar;
                            pinStatus.pinid = pin5.idpin;
                            memListPinStatus.Add(pinStatus);

                            // Uppfæra lista.
                            
                            lboxPin5.DataContext = memListPin5;
                            lboxPin5.Items.Refresh();
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

            //Þetta er fyrir endurröðun
        private void comA5_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {

                if (memListPin5.Count > 1)
                {
                    txtReorderHeader.Text = "  Endurraða Rennu ";
                    childWinReorderPin.Show(); 
                    childWinReorderPin_DataGrid.ItemsSource = memListPin5;
                    _pinid = 4;

                }
                else
                {
                    MessageBox.Show("Það eru einungis einn eða enginn bíll á nálinni", "Villa vegna endurröðunnar", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        
            #endregion


        #region Nálar - Túrar í bið

        



        #endregion


            #region Nálar - Bílar í túr

        #endregion


            #endregion

            // End nálar

            #region popup Windows

            #region TourPopUp
            private void tourToolbarcomFilterOn_Click(object sender, RoutedEventArgs e)
            {
                tourToolbarComFilter.Visibility = Visibility.Visible;
                tourToolbarComFilterOff.Visibility = Visibility.Visible;
                tourToolbarDatePickerFrom.Visibility = Visibility.Visible;
                tourToolbarDatePickerTo.Visibility = Visibility.Visible;
                tourToolbarLblDateFrom.Visibility = Visibility.Visible;
                tourToolbarLblDateTo.Visibility = Visibility.Visible;
                tourToolbarcomFilterOn.Visibility = Visibility.Collapsed;
            }

            private void tourToolbarComFilter_Click(object sender, RoutedEventArgs e)
            {


            }

            private void tourToolbarComFilterOff_Click(object sender, RoutedEventArgs e)
            {
                tourToolbarComFilter.Visibility = Visibility.Collapsed;
                tourToolbarComFilterOff.Visibility = Visibility.Collapsed;
                tourToolbarDatePickerFrom.Visibility = Visibility.Collapsed;
                tourToolbarDatePickerTo.Visibility = Visibility.Collapsed;
                tourToolbarLblDateFrom.Visibility = Visibility.Collapsed;
                tourToolbarLblDateTo.Visibility = Visibility.Collapsed;
                tourToolbarcomFilterOn.Visibility = Visibility.Visible;
            }


            #endregion

            #region Reorder PopUp
            private void ChildWinReorderPin_ToolbarComSave_Click(object sender, RoutedEventArgs e)
            {
                switch (_pinid)
                {
                    case 0:
                        {
                            childWinReorderPin.Close();
                            memListPin1 = memListPin1.OrderBy(x => x.pline).ToList();
                            lboxPin1.DataContext = memListPin5;
                            lboxPin1.Items.Refresh();
                            break;
                        }

                    case 1:
                        {
                            childWinReorderPin.Close();
                            memListPin2 = memListPin2.OrderBy(x => x.pline).ToList();
                            lboxPin5.DataContext = memListPin2;
                            lboxPin2.Items.Refresh();
                            break;
                        }
                    case 2:
                        {
                            childWinReorderPin.Close();
                            memListPin3 = memListPin3.OrderBy(x => x.pline).ToList();
                            lboxPin3.DataContext = memListPin3;
                            lboxPin3.Items.Refresh();
                            break;
                        }
                    case 3:
                        {
                            childWinReorderPin.Close();
                            memListPin4 = memListPin4.OrderBy(x => x.pline).ToList();
                            lboxPin4.DataContext = memListPin4;
                            lboxPin4.Items.Refresh();
                            break;
                        }
                    case 4:
                        {
                            childWinReorderPin.Close();
                            memListPin5 = memListPin5.OrderBy(x => x.pline).ToList();
                            lboxPin5.DataContext = memListPin5;
                            lboxPin5.Items.Refresh();
                            break;
                        }
                    default:
                        MessageBox.Show("Númer nálarinnar er ekki rétt");
                        break;


                }

            }

            #endregion


            #endregion


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
            winTests wt = new winTests();
            wt.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Hlaða Töflum í minni.
        #region Hlaða töflum í minni

            private void LoadTurarToMem()
        {
            using (Stream stream = File.Open(@"C:\dbsendill\tbl_turar.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                memTableTurar = (DataTable)bin.Deserialize(stream);
                int rcount = memTableTurar.Rows.Count;
                MessageBox.Show(rcount.ToString());
                MessageBox.Show("Túrar hlaðnir í minni");
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
                MessageBox.Show("Bílar hlaðnir");
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
                MessageBox.Show("Bílar hlaðnir í minni");
            }

        #endregion

        //Hlaða listum í minni
        #region Hlaða listum í minni

            private void LoadToursToList()
            {
                try
                {
                    DBManager db = new DBManager();
                    memListTour = db.GetAllToursFromFile();
                }
                catch (IOException)
                {
                }
            }
            private void LoadCarsToList()
            {
                try
                {
                    using (Stream stream = File.Open(@"C:\dbsendill\carfile.bin", FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        MessageBox.Show("Binary formatter lokið");
                        memListCar = (List<dtoCars>)bin.Deserialize(stream);
                    }
                }
                catch (IOException)
                {
                }
                MessageBox.Show("Bílar hlaðnir í minni");
            }
            


        #endregion

            #region Functions

            public void funcFillToursOnPin()
            {
                List<dtoTour> ltour = new List<dtoTour>();
                dtoTour objtour = new dtoTour();
                objtour.id = 1;
                objtour.idpin=1;
                objtour.taddress="Gtundarstíg 38";
                objtour.tcontact="Jón Sigurbjörnsson";
                ltour.Add(objtour);
                lbox6.ItemsSource=ltour;


            }

            #endregion
     

            #region Ónotaður kóði

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
                winRep repwindow = new winRep();
                repwindow.ShowDialog();
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





















    }
}
