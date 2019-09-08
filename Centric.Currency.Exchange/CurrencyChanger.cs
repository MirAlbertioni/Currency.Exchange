using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centric.Currency.Exchange
{
    public partial class CurrencyChanger : Form
    {
        static HttpClient client = new HttpClient();
        public Rate FirstRate { get; set; }
        public Rate SecondRate { get; set; }
        private readonly string exchangeRateUrl = "https://api.exchangeratesapi.io/latest?base=";
        public float CommissionAmount { get; set; }

        public CurrencyChanger()
        {
            InitializeComponent();
            AddItemsToCombobox();
            label7.Text = "";
        }

        private void AddItemsToCombobox()
        {
            comboBox1.Items.Add(new ComboboxItem { Text = "SEK" });
            comboBox1.Items.Add(new ComboboxItem { Text = "USD" });
            comboBox1.Items.Add(new ComboboxItem { Text = "GBP" });
            comboBox1.Items.Add(new ComboboxItem { Text = "CNY" });

            comboBox2.Items.Add(new ComboboxItem { Text = "SEK" });
            comboBox2.Items.Add(new ComboboxItem { Text = "USD" });
            comboBox2.Items.Add(new ComboboxItem { Text = "GBP" });
            comboBox2.Items.Add(new ComboboxItem { Text = "CNY" });

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cB1SelectedItem = comboBox1.GetItemText(this.comboBox1.SelectedItem);
            var cB2SelectedItem = comboBox2.GetItemText(this.comboBox2.SelectedItem);

            if (!string.IsNullOrEmpty(cB1SelectedItem))
            {
                var url = $"{exchangeRateUrl}{cB1SelectedItem}";

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        FirstRate = await response.Content.ReadAsAsync<Rate>();
                        UpdateCurrency();
                    }
                    catch (Exception ex)
                    {
                        label5.Text = ex.Message;
                    }
                }
            }
        }

        private async void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cB1SelectedItem = comboBox1.GetItemText(this.comboBox1.SelectedItem);
            var cB2SelectedItem = comboBox2.GetItemText(this.comboBox2.SelectedItem);

            if (!string.IsNullOrEmpty(cB2SelectedItem))
            {
                var url = $"{exchangeRateUrl}{cB2SelectedItem}";

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        SecondRate = await response.Content.ReadAsAsync<Rate>();
                        UpdateCurrency();
                    }
                    catch (Exception ex)
                    {
                        label5.Text = ex.Message;
                    }
                }
            }
        }

        private double GetValue(string selectedItem, Rate rate)
        {
            var value = selectedItem == "SEK" ? rate.Rates.SEK :
                selectedItem == "USD" ? rate.Rates.USD :
                selectedItem == "GBP" ? rate.Rates.GBP :
                selectedItem == "CNY" ? rate.Rates.CNY :
                0;
            return Math.Round(value, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cB1SelectedItem = comboBox1.GetItemText(this.comboBox1.SelectedItem);
            var firstValue = GetValue(cB1SelectedItem, FirstRate);

            var cB2SelectedItem = comboBox2.GetItemText(this.comboBox2.SelectedItem);
            var secondValue = GetValue(cB2SelectedItem, FirstRate);

            var convert = float.TryParse(textBox1.Text, out float txtBox);
            label7.Text = "";
            if (!convert)
            {
                label7.Text = "Använd endast siffror och ett decimal tecken ";
            }

            float com = (float)Commission.GlobalCommission / (float)100;
            CommissionAmount = txtBox * com;

            var totalValue = (txtBox * secondValue) - CommissionAmount;

            label4.Text = $"Att utbetala ({cB2SelectedItem})";
            label3.Text = $"Att betala ({cB1SelectedItem})";
            textBox2.Text = Math.Round(totalValue, 2).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var cb2 = comboBox1.SelectedIndex;

            comboBox1.SelectedIndex = comboBox2.SelectedIndex;
            comboBox2.SelectedIndex = cb2;
            UpdateCurrency();
        }

        private void UpdateCurrency()
        {
            if(FirstRate == null)
            {
                return;
            }
            var cB1SelectedItem = comboBox1.GetItemText(this.comboBox1.SelectedItem);
            var cB2SelectedItem = comboBox2.GetItemText(this.comboBox2.SelectedItem);
            label5.Text = 
                $"{GetValue(cB1SelectedItem, FirstRate).ToString()} {cB1SelectedItem}" +
                $" = " +
                $"{GetValue(cB2SelectedItem, FirstRate).ToString()} {cB2SelectedItem}";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            float.TryParse(textBox1.Text, out float txtBoxAmount);
            float.TryParse(textBox2.Text, out float txtBoxDisbursed);

            var cB1SelectedItem = comboBox1.GetItemText(this.comboBox1.SelectedItem);
            var firstValue = GetValue(cB1SelectedItem, FirstRate);

            var cB2SelectedItem = comboBox2.GetItemText(this.comboBox2.SelectedItem);
            var secondValue = GetValue(cB2SelectedItem, FirstRate);

            var receipt = new CustomerReceipt
            {
                Amount = Math.Round(txtBoxAmount, 2),
                CommissionAmount = Math.Round(CommissionAmount, 2),
                Date = DateTime.Now,
                Disbursed = Math.Round(txtBoxDisbursed, 2),
                ValueFrom = Math.Round(firstValue, 2),
                ValueTo = Math.Round(secondValue, 2),
                ValueFromText = cB1SelectedItem,
                ValueToText = cB2SelectedItem
            };

            this.Hide();
            Receipt form = new Receipt(receipt);
            form.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
