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
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            label1.Text = $"Kommission : {Commission.GlobalCommission.ToString()}%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            CurrencyChanger form = new CurrencyChanger();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Settings form = new Settings();
            form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
