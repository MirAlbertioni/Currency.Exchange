using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centric.Currency.Exchange
{
    public partial class Receipt : Form
    {
        public Receipt(CustomerReceipt receipt)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            try
            {
                label2.Text = receipt.Date.ToString();
                label4.Text = $"{receipt.Amount.ToString()} {receipt.ValueFromText}";
                label6.Text = $"{receipt.CommissionAmount.ToString()} {receipt.ValueFromText}";
                label8.Text = $"{receipt.Disbursed.ToString()} { receipt.ValueToText}";
                label9.Text = $"{receipt.ValueFrom} {receipt.ValueFromText} - {receipt.ValueTo} {receipt.ValueToText}";

            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Start form = new Start();
            form.ShowDialog();
        }
    }
}
