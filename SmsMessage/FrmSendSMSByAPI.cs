using SmsIrRestful;
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
    public partial class FrmSendSMSByAPI : Form
    {
        string gettoken = null;
        string codevery;
        public FrmSendSMSByAPI()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtToken.Text))
            {
                MessageBox.Show("لطفا ابتدا بروی دکمه توکن کلیک کنید", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (string.IsNullOrEmpty(txtTo.Text))
                {
                    MessageBox.Show("لطفا شماره تلفن مقصد را وارد کنید", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrEmpty(txtMessage.Text))
                {
                    MessageBox.Show("لطفا متن پیام را جهت ارسال وارد کنید", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var messageSendObject = new MessageSendObject()
                    {
                        Messages = new List<string> { txtMessage.Text }.ToArray(),
                        MobileNumbers = new List<string> { txtTo.Text }.ToArray(),
                        LineNumber = txtFrom.Text,
                        SendDateTime = null,
                        CanContinueInCaseOfError = true
                    };

                    MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(gettoken, messageSendObject);

                    if (messageSendResponseObject.IsSuccessful)
                    {
                        MessageBox.Show("پیامک شما با موفقیت ارسال شد", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnToken_Click(object sender, EventArgs e)
        {
            var token = new Token().GetToken("8c239fb1b71d69d65d17a1c6", "!@#test!@#");
            txtToken.Text = token;
            gettoken = token;

            SmsLineNumber credit = new SmsLine().GetSmsLines(token);



            if (credit == null)
                throw new Exception($@"{nameof(credit) } is null");

            if (credit.IsSuccessful)
            {
                for (int i = 0; i < credit.SMSLines.Length; i++)
                {
                    txtFrom.Text = credit.SMSLines[i].LineNumber.ToString();
                }
                
            }
            else
            {
               
            }
        }
    }
}
