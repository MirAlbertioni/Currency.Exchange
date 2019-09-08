using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centric.Currency.Exchange
{
    public class CustomerReceipt
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public double CommissionAmount { get; set; }
        public double Disbursed { get; set; }
        public double ValueFrom { get; set; }
        public double ValueTo { get; set; }
        public string ValueFromText { get; set; }
        public string ValueToText { get; set; }
    }
}
