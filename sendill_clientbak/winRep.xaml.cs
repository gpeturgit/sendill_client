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
using System.Windows.Forms;



namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winRep.xaml
    /// </summary>
    public partial class winRep : Window
    {
        public winRep()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReportsApplication2.Form1 wrep = new ReportsApplication2.Form1();
            wrep.Show();
        }
    }
}
