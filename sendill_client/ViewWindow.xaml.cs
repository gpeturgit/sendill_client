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
using System.Windows.Threading;

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
                
        public ucPin[] arrUserControl;
        
        public ViewWindow()
        {
            InitializeComponent();
        }

        private void FourColumnLayout(int icol1, int icol2, int icol3, int icol4)
        {
            //ServiceManager sm = new ServiceManager();
           // sm.UpdateMemLisArray(4);

            var d_width = System.Windows.SystemParameters.PrimaryScreenWidth;
            var d_height = System.Windows.SystemParameters.PrimaryScreenHeight;

            int i_width = Convert.ToInt32(d_width / 4);
            int i_height = Convert.ToInt32(d_height);

            arrUserControl = new ucPin[4];

            ColumnDefinition coldef1 = new ColumnDefinition();
            coldef1.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef1);
            ucPin uc1 = new ucPin();
            arrUserControl[0] = uc1;
            arrUserControl[0].lboxPin.Height = i_height - 40;
            arrUserControl[0].PinHeader.Text = "Stöðin";
            //arrUserControl[0].lboxPin.ItemsSource = sm.arrMemListDtoPin[0];
            Grid.SetColumn(arrUserControl[0], 0);
            Grid.SetRow(arrUserControl[0], 0);
            gridMain.Children.Add(arrUserControl[0]);

            ColumnDefinition coldef2 = new ColumnDefinition();
            coldef2.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef2);
            ucPin uc2 = new ucPin();
            arrUserControl[1] = uc2;
            arrUserControl[1].lboxPin.Height = i_height - 40;
            arrUserControl[1].PinHeader.Text = "Miðbæjarsvæði";
            //arrUserControl[1].lboxPin.ItemsSource = sm.arrMemListDtoPin[1];
            Grid.SetColumn(arrUserControl[1], 1);
            Grid.SetRow(arrUserControl[1], 0);
            gridMain.Children.Add(arrUserControl[1]);

            ColumnDefinition coldef3 = new ColumnDefinition();
            coldef3.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef3);
            ucPin uc3 = new ucPin();
            arrUserControl[2] = uc3;
            arrUserControl[2].lboxPin.Height = i_height - 40;
            arrUserControl[2].PinHeader.Text = "Breiðholt - Ikea";
            //arrUserControl[2].lboxPin.ItemsSource = sm.arrMemListDtoPin[2];
            Grid.SetColumn(arrUserControl[2], 2);
            Grid.SetRow(arrUserControl[2], 0);
            gridMain.Children.Add(arrUserControl[2]);

            ColumnDefinition coldef4 = new ColumnDefinition();
            coldef4.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef4);
            ucPin uc4 = new ucPin();
            arrUserControl[3] = uc4;
            arrUserControl[3].lboxPin.Height = i_height - 40;
            arrUserControl[3].PinHeader.Text = "Árbær - Bauhaus.";
            //arrUserControl[3].lboxPin.ItemsSource = sm.arrMemListDtoPin[3];
            Grid.SetColumn(arrUserControl[3], 3);
            Grid.SetRow(arrUserControl[3], 0);
            gridMain.Children.Add(arrUserControl[3]);
        }

        private void FiveColumnLayout(int icol1, int icol2, int icol3, int icol4, int icol5)
        {
            //ServiceManager sm = new ServiceManager();
            //sm.UpdateMemLisArray(5);
            DataTemplate dt_temp = new DataTemplate(); 

            DBManager dm = new DBManager();

            ConfigFile conf = new ConfigFile();
            var d_width = conf.GetViewScreenWidth();
            var d_height = conf.GetViewScreenHeight();

            int i_width = Convert.ToInt32(d_width / 5);
            int i_height = Convert.ToInt32(d_height);

            arrUserControl = new ucPin[5];

            ColumnDefinition coldef1 = new ColumnDefinition();
            coldef1.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef1);
            ucPin uc1 = new ucPin();
            arrUserControl[0] = uc1;
            arrUserControl[0].lboxPin.Height = i_height - 40;
            arrUserControl[0].PinHeader.Text = "Stöðin";
            var s_ref = conf.GetListViewtTemlate("1");
            dt_temp = arrUserControl[0].FindResource(s_ref) as DataTemplate;
            arrUserControl[0].lboxPin.ItemTemplate = dt_temp;
            Grid.SetColumn(arrUserControl[0], 0);
            Grid.SetRow(arrUserControl[0], 0);
            gridMain.Children.Add(arrUserControl[0]);

            ColumnDefinition coldef2 = new ColumnDefinition();
            coldef2.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef2);
            ucPin uc2 = new ucPin();
            //UserControl1 uc2 = new UserControl1();
            arrUserControl[1] = uc2;
            arrUserControl[1].lboxPin.Height = i_height - 40;
            arrUserControl[1].PinHeader.Text = "Miðbæjarsvæði";
            s_ref = conf.GetListViewtTemlate("2");
            dt_temp = arrUserControl[1].FindResource(s_ref) as DataTemplate;
            arrUserControl[1].lboxPin.ItemTemplate = dt_temp;
           // arrUserControl[1].lboxPin.ItemTemplate = dt_temp_20;
            Grid.SetColumn(arrUserControl[1], 1);
            Grid.SetRow(arrUserControl[1], 0);
            gridMain.Children.Add(arrUserControl[1]);

            ColumnDefinition coldef3 = new ColumnDefinition();
            coldef3.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef3);
            ucPin uc3 = new ucPin();
            //UserControl1 uc3 = new UserControl1();
            arrUserControl[2] = uc3;
            arrUserControl[2].lboxPin.Height = i_height - 40;
            arrUserControl[2].PinHeader.Text = "Breiðholt - Ikea";
            s_ref = conf.GetListViewtTemlate("3");
            dt_temp = arrUserControl[2].FindResource(s_ref) as DataTemplate;
            arrUserControl[2].lboxPin.ItemTemplate = dt_temp;
            //arrUserControl[2].lboxPin.ItemTemplate = dt_temp_20;
            Grid.SetColumn(arrUserControl[2], 2);
            Grid.SetRow(arrUserControl[2], 0);
            gridMain.Children.Add(arrUserControl[2]);

            ColumnDefinition coldef4 = new ColumnDefinition();
            coldef4.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef4);
            ucPin uc4 = new ucPin();
            //UserControl1 uc4 = new UserControl1();
            arrUserControl[3] = uc4;
            arrUserControl[3].lboxPin.Height = i_height - 40;
            arrUserControl[3].PinHeader.Text = "Árbær - Bauhaus.";
            s_ref = conf.GetListViewtTemlate("4");
            dt_temp = arrUserControl[3].FindResource(s_ref) as DataTemplate;
            arrUserControl[3].lboxPin.ItemTemplate = dt_temp;
            //arrUserControl[3].lboxPin.ItemTemplate = dt_temp_20;
            Grid.SetColumn(arrUserControl[3], 3);
            Grid.SetRow(arrUserControl[3], 0);
            gridMain.Children.Add(arrUserControl[3]);

            ColumnDefinition coldef5 = new ColumnDefinition();
            coldef4.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef5);
            ucPin uc5 = new ucPin();
            //UserControl1 uc5 = new UserControl1();
            arrUserControl[4] = uc5;
            arrUserControl[4].lboxPin.Height = i_height - 40;
            arrUserControl[4].PinHeader.Text = "Renna.";
            s_ref = conf.GetListViewtTemlate("5");
            dt_temp = arrUserControl[4].FindResource(s_ref) as DataTemplate;
            arrUserControl[4].lboxPin.ItemTemplate = dt_temp;
            //arrUserControl[4].lboxPin.ItemTemplate = dt_temp_20;
            Grid.SetColumn(arrUserControl[4], 4);
            Grid.SetRow(arrUserControl[4], 0);
            gridMain.Children.Add(arrUserControl[4]);

        }

        private void SixColumnLayout(int icol1, int icol2, int icol3, int icol4, int icol5, int icol6)
        {
            //ServiceManager sm = new ServiceManager();
            //sm.UpdateMemLisArray(6);

            ConfigFile conf = new ConfigFile();
            var d_width = conf.GetViewScreenWidth();
            var d_height = conf.GetViewScreenHeight();

            int i_width = Convert.ToInt32(d_width / 5);
            int i_height = Convert.ToInt32(d_height);

            arrUserControl = new ucPin[5];

            ColumnDefinition coldef1 = new ColumnDefinition();
            coldef1.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef1);
            ucPin uc1 = new ucPin();
            arrUserControl[0] = uc1;
            arrUserControl[0].lboxPin.Height = i_height - 40;
            arrUserControl[0].PinHeader.Text = "Stöðin";
            //arrUserControl[0].lboxPin.ItemsSource = sm.arrMemListDtoPin[0];
            Grid.SetColumn(arrUserControl[0], 0);
            Grid.SetRow(arrUserControl[0], 0);
            gridMain.Children.Add(arrUserControl[0]);

            ColumnDefinition coldef2 = new ColumnDefinition();
            coldef2.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef2);
            ucPin uc2 = new ucPin();
            arrUserControl[1] = uc2;
            arrUserControl[1].lboxPin.Height = i_height - 40;
            arrUserControl[1].PinHeader.Text = "Miðbæjarsvæði";
            //arrUserControl[1].lboxPin.ItemsSource = sm.arrMemListDtoPin[1];
            Grid.SetColumn(arrUserControl[1], 1);
            Grid.SetRow(arrUserControl[1], 0);
            gridMain.Children.Add(arrUserControl[1]);

            ColumnDefinition coldef3 = new ColumnDefinition();
            coldef3.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef3);
            ucPin uc3 = new ucPin();
            arrUserControl[2] = uc3;
            arrUserControl[2].lboxPin.Height = i_height - 40;
            arrUserControl[2].PinHeader.Text = "Breiðholt - Ikea";
            //arrUserControl[2].lboxPin.ItemsSource = sm.arrMemListDtoPin[2];
            Grid.SetColumn(arrUserControl[2], 2);
            Grid.SetRow(arrUserControl[2], 0);
            gridMain.Children.Add(arrUserControl[2]);

            ColumnDefinition coldef4 = new ColumnDefinition();
            coldef4.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef4);
            ucPin uc4 = new ucPin();
            arrUserControl[3] = uc4;
            arrUserControl[3].lboxPin.Height = i_height - 40;
            arrUserControl[3].PinHeader.Text = "Árbær - Bauhaus.";
            //arrUserControl[3].lboxPin.ItemsSource = sm.arrMemListDtoPin[3];
            Grid.SetColumn(arrUserControl[3], 3);
            Grid.SetRow(arrUserControl[3], 0);
            gridMain.Children.Add(arrUserControl[3]);

            ColumnDefinition coldef5 = new ColumnDefinition();
            coldef5.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef5);
            ucPin uc5 = new ucPin();
            arrUserControl[4] = uc5;
            arrUserControl[4].lboxPin.Height = i_height - 40;
            arrUserControl[4].PinHeader.Text = "Renna.";
            //arrUserControl[4].lboxPin.ItemsSource = sm.arrMemListDtoPin[4];
            Grid.SetColumn(arrUserControl[4], 4);
            Grid.SetRow(arrUserControl[4], 0);
            gridMain.Children.Add(arrUserControl[4]);

            ColumnDefinition coldef6 = new ColumnDefinition();
            coldef4.Width = new GridLength(i_width);
            gridMain.ColumnDefinitions.Add(coldef5);
            ucPin uc6 = new ucPin();
            arrUserControl[5] = uc6;
            arrUserControl[5].lboxPin.Height = i_height - 40;
            arrUserControl[5].PinHeader.Text = "Svæði númer 6.";
            //arrUserControl[5].lboxPin.ItemsSource = sm.arrMemListDtoPin[5];
            Grid.SetColumn(arrUserControl[5], 5);
            Grid.SetRow(arrUserControl[5], 0);
            gridMain.Children.Add(arrUserControl[5]);

        }

        private void CreateLayout()
        {
            ConfigFile conf = new ConfigFile();
            var d_width = conf.GetViewScreenWidth();
            var d_height = conf.GetViewScreenHeight();
            this.Height = d_height;
            this.Width = d_width;
            gridMain.Width = d_width;
            gridMain.Height = d_height;
            gridMain.HorizontalAlignment = HorizontalAlignment.Left;
            gridMain.VerticalAlignment = VerticalAlignment.Top;
            gridMain.ShowGridLines = true;
            gridMain.Background = new SolidColorBrush(Colors.LightSteelBlue);
            int[] i_arr_layout = new int[6];
            i_arr_layout[0] = 5;
            i_arr_layout[1] = 1;
            i_arr_layout[2] = 1;
            i_arr_layout[3] = 1;
            i_arr_layout[4] = 1;
            i_arr_layout[5] = 1;

            //ServiceManager sm = new ServiceManager();

            switch (i_arr_layout[0])
            {
                case 4:
                    FourColumnLayout(i_arr_layout[1], i_arr_layout[2], i_arr_layout[3], i_arr_layout[4]);
                    Console.WriteLine("Fjórir dákar.");
                    break;
                case 5:
                    FiveColumnLayout(i_arr_layout[1], i_arr_layout[2], i_arr_layout[3], i_arr_layout[4], i_arr_layout[5]);
                    Console.WriteLine("Fimm dálkar.");
                    break;
                case 6:
                    SixColumnLayout(i_arr_layout[1], i_arr_layout[2], i_arr_layout[3], i_arr_layout[4], i_arr_layout[5], i_arr_layout[6]);
                    Console.WriteLine("Sex dálkar");
                    break;
                default:

                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateLayout();
            MessageBox.Show("Hæð er : " + this.Height.ToString() + " og breidd er : " + this.Width.ToString());
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            //this.WindowState = WindowState.Maximized;

        }

        private void UpdateLists()
        {
            DBManager dm = new DBManager();
            arrUserControl[0].lboxPin.ItemsSource = null;
            arrUserControl[0].lboxPin.ItemsSource = dm.LoadPin1FromFile();
            arrUserControl[0].lboxPin.Items.Refresh();

            arrUserControl[1].lboxPin.ItemsSource = null;
            arrUserControl[1].lboxPin.ItemsSource = dm.LoadPin2FromFile();
            arrUserControl[1].lboxPin.Items.Refresh();

            arrUserControl[2].lboxPin.ItemsSource = null;
            arrUserControl[2].lboxPin.ItemsSource = dm.LoadPin3FromFile();
            arrUserControl[2].lboxPin.Items.Refresh();

            arrUserControl[3].lboxPin.ItemsSource = null;
            arrUserControl[3].lboxPin.ItemsSource = dm.LoadPin4FromFile();
            arrUserControl[3].lboxPin.Items.Refresh();

            arrUserControl[4].lboxPin.ItemsSource = null;
            arrUserControl[4].lboxPin.ItemsSource = dm.LoadPin5FromFile();
            arrUserControl[4].lboxPin.Items.Refresh();



        }

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateLists();
        }
    }
}
