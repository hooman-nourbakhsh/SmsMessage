using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmsMessage
{
    public partial class FrmSendSMSByHTTPPost : Form
    {
        public FrmSendSMSByHTTPPost()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                Stream s = client.OpenRead(string.Format("http://ip.sms.ir/SendMessage.ashx?user=9333446698&pass=d590d6&text={0}&to={1}&lineNo=50002015520443", txtMessage.Text, txtTo.Text));
                //Stream s = client.OpenRead(string.Format("https://platform.clickatell.com/messages/http/send?apiKey=tUaSht9MRnS5ek3KH2v_mQ==&to={0}&content={1}", txtTo.Text, txtMessage.Text));
                StreamReader reader = new StreamReader(s);
                string result = reader.ReadToEnd();
                MessageBox.Show("Send Message Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
