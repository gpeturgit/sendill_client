namespace sendill_client
{
    partial class frmReports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dtoTourBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtoCustomerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtoTourBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtoTourBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtoCustomerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtoTourBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "dsTtoTour";
            reportDataSource1.Value = this.dtoTourBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "sendill_client.Reports.repTourList.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(11, 11);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(411, 486);
            this.reportViewer1.TabIndex = 0;
            // 
            // reportViewer2
            // 
            reportDataSource2.Name = "TourDataSet";
            reportDataSource2.Value = this.dtoTourBindingSource1;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "sendill_client.Reports.repTourDetails.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(426, 2);
            this.reportViewer2.Margin = new System.Windows.Forms.Padding(2);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(432, 495);
            this.reportViewer2.TabIndex = 1;
            // 
            // dtoTourBindingSource
            // 
            this.dtoTourBindingSource.DataSource = typeof(sendill_client.dtoTour);
            // 
            // dtoCustomerBindingSource
            // 
            this.dtoCustomerBindingSource.DataSource = typeof(sendill_client.dtoCustomer);
            // 
            // dtoTourBindingSource1
            // 
            this.dtoTourBindingSource1.DataSource = typeof(sendill_client.dtoTour);
            // 
            // frmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 508);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.reportViewer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmReports";
            this.Text = "frmReports";
            this.Load += new System.EventHandler(this.frmReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtoTourBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtoCustomerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtoTourBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource dtoTourBindingSource;
        private System.Windows.Forms.BindingSource dtoCustomerBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.BindingSource dtoTourBindingSource1;
    }
}