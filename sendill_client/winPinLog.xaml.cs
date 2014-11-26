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
            LoadToursToList();
            
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

    }
}
