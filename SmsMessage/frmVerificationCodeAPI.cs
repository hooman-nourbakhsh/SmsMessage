using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmsIrRestful;

namespace SmsMessage
{
    public partial class frmVerificationCodeAPI : Form
    {
        string gettoken = null;
        string codevery;
        public frmVerificationCodeAPI()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
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

                Random r = new Random();

                //Generate 10 random numbers
                for (int i = 1; i <=10; i++)
                {
                    //Console.WriteLine(r.Next());
                    string co = r.Next().ToString();
                    codevery = co;
                }


                var restVerificationCode = new RestVerificationCode()

                {
                    Code = codevery,
                    MobileNumber = txtTo.Text
                };

                var restVerificationCodeRespone = new VerificationCode().Send(gettoken, restVerificationCode);

                if (restVerificationCodeRespone.IsSuccessful)
                {
                    MessageBox.Show("پیامک شما با موفقیت ارسال شد", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
              }
            else
            {
                MessageBox.Show("ERROR", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
