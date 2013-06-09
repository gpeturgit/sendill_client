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
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winCustomer.xaml
    /// </summary>
    public partial class winCustomer : Window
    {
        DataTable memTableCustomers = new DataTable();
        List<dtoCustomer> _lcustomer = new List<dtoCustomer>();
        public winCustomer()
        {
            InitializeComponent();
            try
            {
                using (Stream stream = File.Open(@"C:\dbsendill\tbl_vidskiptamenn.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    memTableCustomers = (DataTable)bin.Deserialize(stream);
                    int rcount = memTableCustomers.Rows.Count;
                    MessageBox.Show(rcount.ToString());
                    MessageBox.Show(rcount.ToString());
                    MessageBox.Show("Viðskiptavinum");
                }
            }
            catch (IOException)
            {
            }
            MessageBox.Show("Bílar hlaðnir í minni");
            
            foreach (DataRow dr in memTableCustomers.Rows)
            {
                dtoCustomer _icust = new dtoCustomer();
                if (!dr.IsNull(0)) { _icust.id = (int)dr["ID"]; }
                if (!dr.IsNull(1)) { _icust.address = (string)dr["Vsk_heimili"]; }
                if (!dr.IsNull(2)) { _icust.name = (string)dr["Vsk_nafn"]; }
                if (!dr.IsNull(3)) { _icust.streetnr = (string)dr["Vsk_gata_nr"]; }
                if (!dr.IsNull(4)) { _icust.areacode = (double)dr["Vsk_postnr"]; }
                if (!dr.IsNull(5)) { _icust.phone1 = (string)dr["Vsk_simi"]; }
                if (!dr.IsNull(6)) { _icust.fax = (string)dr["Vsk_fax"]; }
                if (!dr.IsNull(7)) { _icust.phone2 = (string)dr["Vsk_simi2"];}
                if (!dr.IsNull(8)) { _icust.umbaf = (string)dr["Umb_af"];}
                if (!dr.IsNull(9)) { _icust.mobile = (string)dr["Vsk_gsm"];}
                if (!dr.IsNull(10)) { _icust.kt = (string)dr["Vsk_kt"];}
                if (!dr.IsNull(11)) { _icust.number = (string)dr["Vsk_nr"];}
                if (!dr.IsNull(12)) { _icust.area = (string)dr["Vsk_stadur"];}
                if (!dr.IsNull(13)) { _icust.email = (string)dr["email"];}
                if (!dr.IsNull(14)) { _icust.cumbaf = (string)dr["Vsk_umbeðið"];}
                if (!dr.IsNull(50)) { _icust.svaedi = (double)dr["Svaedi"]; }
                if (!dr.IsNull(51)) { _icust.url = (string)dr["URL"];}

                _lcustomer.Add(_icust);
                datagridvskm.ItemsSource = _lcustomer;
            }
        }

        private void comNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void comPrev_Click_1(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream(@"C:\dbsendill\list_customers.bin", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, _lcustomer);
            fs.Close();
            MessageBox.Show("Viðskiptamannalista hlaðið í minni");
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void comNew_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}