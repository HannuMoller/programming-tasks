using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBarCode
{
    /// <summary>
    /// Domestic bank barcode
    /// </summary>
    class BarCodeVersion4 : BarCode
    {

        /// <summary>
        /// set reference number
        /// </summary>
        /// <param name="reference"> reference number (in domestic format)</param>
        public override void SetReference(string reference)
        {
            while (reference.Length < 20)
            {
                // fill leading zeros
                reference = reference.Insert(0, "0");
            }
            this.reference = reference;
        }

        /// <summary>
        /// get bank barcode (domestic format)
        /// </summary>
        /// <returns> bank barcode </returns>
        protected override string GetBarCodeWithoutChecksum()
        {
            string barcode = string.Format("4{0}{1:D6}{2:D2}000{3}{4}", account, euros, cents, reference, duedate);
            return barcode;
        }

    }
}
