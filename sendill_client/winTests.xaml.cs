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
    /// Interaction logic for winTests.xaml
    /// </summary>
    public partial class winTests : Window
    {
        public winTests()
        {
            InitializeComponent();
        }

        private void comPinLogOpen_Click(object sender, RoutedEventArgs e)
        {
            winPinLog _wplog = new winPinLog();
            _wplog.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DBManager dm = new DBManager();
            string smessage=dm.CreateMemFileFromDatabase(1);
            MessageBox.Show(smessage);
        }

        private void comEndurrada_Click(object sender, RoutedEventArgs e)
        {
            frmPinReorder winPinReorder = new frmPinReorder();
            winPinReorder.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            winNewTour wt= new winNewTour();
            wt.Show();
        }
    }
}