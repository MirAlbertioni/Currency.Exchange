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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            try
            {
                numericUpDown1.Value = Commission.GlobalCommission;
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Commission.GlobalCommission = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
            Start form = new Start();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Start form = new Start();
            form.ShowDialog();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
