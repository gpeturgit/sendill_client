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
    /// Interaction logic for winRep.xaml
    /// </summary>
    public partial class winRep : Window
    {
        public winRep()
        {
            InitializeComponent();
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hlaða inn skýrslu með viðskiptamannaskrá.");

        }
    }
}
