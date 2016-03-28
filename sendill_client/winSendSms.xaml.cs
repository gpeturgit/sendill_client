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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace sendill_client
{
    /// <summary>
    /// Interaction logic for winSendSms.xaml
    /// </summary>
    public partial class winSendSms : Window
    {
        public winSendSms()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Find your Account Sid and Auth Token at twilio.com/user/account 
            //string AccountSid = "ACac6aa42f83f298b1f85b8470239a1766";
            //string AuthToken = "2e0f568960bae6b8d21276001a52f315";
            //TwilioRestClient mytwilio = new TwilioRestClient(AccountSid, AuthToken);

            //var send = mytwilio.SendMessage("+12014318341","+3548605801","HALLO FRA SENDLI", "");
            //txtBody.Text = send.Sid;
            //string AccountSid = "AC45b00a5504e242b8a486ebf4cad405c9";
            //string AuthToken = "2e0f568960bae6b8d21276001a52f315";
            //var twilio = new TwilioRestClient(AccountSid, AuthToken);

            //var message = twilio.SendMessage("+16082607366", "+3548235996", "Hringdu ef &#254;&#250; f&#230;r&#240; &#254;essi skilabo&#240;.");
            //txtBody.Text = message.Body;
            //sendill_client.SmsServiceReference.WCFSmsClient smsclient = new SmsServiceReference.WCFSmsClient();
            //txtBody.Text = smsclient.NewTourSms();
            
            SQLManager sm = new SQLManager();
            if (sm.SendSms(txtTo.Text,txtBody.Text))
            {
                MessageBox.Show("Sms sent");
            }
            else
            {
                MessageBox.Show("Sms ekki sent.");
            }







        }
    }
}
