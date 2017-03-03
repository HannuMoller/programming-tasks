using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBarCode
{
    /// <summary>
    /// Class IBANChecker: methods for checking IBAN account numbers
    /// </summary>
    class IBANChecker
    {

        /// <summary>
        /// Check if IBAN account number is valid
        /// </summary>
        /// <param name="account"> IBAN account number (without gaps) </param>
        /// <returns> true, if IBAN account number is valid </returns>
        public static bool Check(string account)
        {
            try
            {
                if (account.Length != 18)
                {
                    // valid IBAN account number is always 18 characters long!
                    return false;
                }
                string acc2 = account.Substring(4);
                int ch1 = account.ElementAt(0) - 'A' + 10;
                int ch2 = account.ElementAt(1) - 'A' + 10;
                int chk = int.Parse(account.Substring(2, 2));
                acc2 += string.Format("{0:D2}{1:D2}{2}", ch1, ch2, account.Substring(2,2));
                decimal all = decimal.Parse(acc2);
                if (all % 97 == 1)
                {
                    return true;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                // IBAN account number probably contained illegal characters
                return false;
            }
        }
    }
}
