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
    /// Interaction logic for frmAdmin.xaml
    /// </summary>
    public partial class frmAdmin : Window
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //SQLManager sm = new SQLManager();
            //var res = sm.CommandSqlDapperSyncTour();
            //MessageBox.Show(res.ToString());
            //MSSqlCommand.LoadAllTours _dbcommand = new MSSqlCommand.LoadAllTours();
            //_dbcommand.Execute(SqlServerBaseConn.SendillSqlServerConnection());
            DBManager dm = new DBManager();
            var res = dm.CreateTourModelListFile();
            var sres = dm.SaveTourModelToFile(res);
            MessageBox.Show(sres);


        }
    }
}
