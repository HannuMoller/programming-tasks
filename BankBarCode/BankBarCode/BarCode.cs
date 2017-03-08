using System;
using System.Text;

namespace BankBarCode
{
    /// <summary>
    /// Base class for bank barcodes
    /// </summary>
    abstract class BarCode
    {
        protected string _account; // 16 digits
        protected int _euros; // 6 digits
        protected int _cents; // 2 digits
        protected string _reference; // 20 digits (in version 4 (domestic), 23 digits in version 5 (international))
        protected string _duedate; // YYMMDD = 6 digits

        /// <summary>
        /// set bank account number
        /// </summary>
        /// <param name="account"> bank account number </param>
        public void SetAccount(string account)
        {
            this._account = account.Substring(2); // remove leading two letters
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
            this._euros = euros;
        }

        /// <summary>
        /// set euro cent count
        /// </summary>
        /// <param name="cents"> euro cent count </param>
        public void SetCents(int cents)
        {
            this._cents = cents;
        }

        /// <summary>
        /// set due date
        /// </summary>
        /// <param name="date"> due date </param>
        public void SetDueDate(string date)
        {
            this._duedate = date;
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
                checksum += multiplier * b;
                multiplier++;
                i += 2;
            }

            checksum %= 103;

            return $"{checksum:D2}";
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
                s.Append($" {barcode.Substring(i, 2)}");
                i += 2;
            }
            s.Append($" [{CalculateChecksum(barcode)}] [stop]");

            return s.ToString();
        }
    }
}
