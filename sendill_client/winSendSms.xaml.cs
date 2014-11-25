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
using Twilio;

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

            //string AccountSid = "AC45b00a5504e242b8a486ebf4cad405c9";
            //string AuthToken = "2e0f568960bae6b8d21276001a52f315";
            //var twilio = new TwilioRestClient(AccountSid, AuthToken);

            //var message = twilio.SendMessage("+16082607366", "+3548235996", "Hringdu ef &#254;&#250; f&#230;r&#240; &#254;essi skilabo&#240;.");
            //txtBody.Text = message.Body;
            if(SendMessage("+15808237098","+3548235996","Þetta er sendill message"))
            {
                MessageBox.Show("Sending tókst");
            }
            else
            {
                MessageBox.Show("Sending tókst ekki.");
            }
            
        }

        public bool SendMessages(string from, string to, string message)
        {
            
            
            return true;
    //        $ curl -X POST https://api.twilio.com/2010-04-01/Accounts/ACf7f35d5369717d645bcc0631d1f4ebfa/SMS/Messages.json \
    //-u ACf7f35d5369717d645bcc0631d1f4ebfa:fa6d0a37d1ad482ba6280f5d37ebb8d5 \
    //--data-urlencode "From=+15808237098" \
    //--data-urlencode "To=+3548235996" \
    //--data-urlencode 'Body=Hello Pétur, welcome to Twilio!
        }

        private AuthenticationHeaderValue CreateBasicAuthenticationHeader(string username, string password)
        {
            return new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(String.Format("{0}:{1}", username, password))));
        }
        public bool SendMessage(string from, string to, string message)
        {
            var accountSid = "ACf7f35d5369717d645bcc0631d1f4ebfa";
            var authToken = "fa6d0a37d1ad482ba6280f5d37ebb8d5";
            var targeturi = "https://api.twilio.com/2010-04-01/Accounts/{0}/SMS/Messages";

            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Authorization = CreateBasicAuthenticationHeader(accountSid, authToken);

            var content = new StringContent(string.Format("From={0}&amp;To={1}&amp;Body={2}", from, to, message));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = client.PostAsync(string.Format(targeturi, accountSid), content);



            return response.IsCompleted;


           
        }
    }
}
