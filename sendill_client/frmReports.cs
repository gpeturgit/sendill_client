using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sendill_client
{
    public partial class frmReports : Form
    {

        public frmReports()
        {
            InitializeComponent();
        }

        private void frmReports_Load(object sender, EventArgs e)
        {
            List<dtoTour> ltour = new List<dtoTour>();
            var exwin = System.Windows.Application.Current.Windows
            .Cast<System.Windows.Window>()
            .FirstOrDefault(window => window is winTurar) as winTurar;
            //string[] rpar = exwin.GetToursParameters();
            //MessageBox.Show(rpar[2]);
            //DBManager db = new DBManager();
            //dtoTourBindingSource.DataSource = db.GetToursPar_CarId(Convert.ToInt32(rpar[2]));
            if (exwin.p_repid==1)
            {
                this.reportViewer2.Dock = DockStyle.None;
                this.reportViewer2.Visible = false;
                this.reportViewer1.Dock = DockStyle.Fill;
                this.reportViewer1.Visible = true;
                dtoTourBindingSource.DataSource = exwin._ltour;
                this.reportViewer1.RefreshReport();
            }
            else
            {
                ltour.Add(exwin.p_DtoTour);
                this.reportViewer2.Dock = DockStyle.Fill;
                this.reportViewer2.Visible = true;
                this.reportViewer1.Dock = DockStyle.None;
                this.reportViewer1.Visible = false;
                dtoTourBindingSource1.DataSource = ltour;
                this.reportViewer2.RefreshReport();
                
            }
            
            
            
        }
    }
}
