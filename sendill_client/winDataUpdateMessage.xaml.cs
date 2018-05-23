using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for winDataUpdateMessage.xaml
    /// </summary>
    public partial class winDataUpdateMessage : Window
    {
        public int p_id;

        public winDataUpdateMessage()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DBManager dm = new DBManager();
            var _item = dm.GetDetailMessageFromQueue(p_id);
            txtMessageText.Text = _item.message_text.ToString();
            
        }
    }
}

