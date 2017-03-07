using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBarCode
{
    /// <summary>
    /// Base class for bank barcodes
    /// </summary>
    abstract class BarCode
    {
        protected string account; // 16 digits
        protected int euros; // 6 digits
        protected int cents; // 2 digits
        protected string reference; // 20 digits (in version 4 (domestic), 23 digits in version 5 (international))
        protected string duedate; // YYMMDD = 6 digits

        /// <summary>
        /// set bank account number
        /// </summary>
        /// <param name="account"> bank account number </param>
        public void SetAccount(string account)
        {
            this.account = account.Substring(2); // remove leading two letters
        }

        /// <summary>
        /// set reference number
        /// </summary>
        /// <param name="reference"> reference number </param>
        public abstract void SetReference(string reference);

        /// <summary>
        /// set euro count
        /// </summary>
        /// <param name="euros"> euro count </param>
        public void SetEuros(int euros)
        {
            this.euros = euros;
        }

        /// <summary>
        /// set euro cent count
        /// </summary>
        /// <param name="cents"> euro cent count </param>
        public void SetCents(int cents)
        {
            this.cents = cents;
        }

        /// <summary>
        /// set due date
        /// </summary>
        /// <param name="date"> due date </param>
        public void SetDueDate(string date)
        {
            this.duedate = date;
        }

        /// <summary>
        /// get bank barcode
        /// </summary>
        /// <returns> bank barcode </returns>
        protected abstract string GetBarCodeWithoutChecksum();

        /// <summary>
        /// get bank barcode
        /// </summary>
        /// <returns> bank barcode </returns>
        public string GetBarCode()
        {
            string barcode = GetBarCodeWithoutChecksum();
            string checksum = CalculateChecksum(barcode);
            return barcode + checksum;
        }

        /// <summary>
        /// calculate checksum using modulo 103 algorithm
        /// </summary>
        /// <param name="ban"></param>
        /// <returns>checksum</returns>
        private string CalculateChecksum(string ban)
        {
            int checksum = 105; // Code128C START CODE C
            int multiplier = 1;
            int i = 0; // stepper
            while (i < ban.Length)
            {
                int b = int.Parse(ban.Substring(i, 2));
                // Console.WriteLine("{0} * {1} = {2}", multiplier, b, multiplier*b);
                checksum += multiplier * b;
                multiplier++;
                i += 2;
            }

            checksum %= 103;

            // Console.WriteLine("checksum = {0}", checksum);

            return string.Format("{0:D2}", checksum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFormattedBarCode()
        {
            var s = new StringBuilder("[105]");
            string barcode = GetBarCodeWithoutChecksum();
            int i = 0;
            while ((i < barcode.Length))
            {
                s.Append(String.Format(" {0}", barcode.Substring(i, 2)));
                i += 2;
            }
            s.Append(string.Format(" [{0}] [stop]", CalculateChecksum(barcode)));

            return s.ToString();
        }
    }
}
