using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankBarCode
{
    /// <summary>
    /// Class ReferenceChecker: methods for checking and manipulating reference numbers
    /// </summary>
    class ReferenceChecker
    {

        /// <summary>
        /// check domestic reference
        /// </summary>
        /// <param name="reference"> domestic reference </param>
        /// <returns> true, if reference is ok </returns>
        public static bool Check(string reference)
        {
            var multipliers = new int[] { 7, 3, 1 };
            int mx = multipliers.Length - 1;

            try
            {
                int i = reference.Length-1;
                int chk = int.Parse(reference.Substring(i)); // original checksum
                int chk0 = 0; // calculated checksum
                int m = 0;
                while (i > 0)
                {
                    i--;
                    int c = int.Parse(reference.Substring(i, 1));
                    int mp = multipliers[m];
                    chk0 += c * mp;
                    m++;
                    if (m > mx)
                    {
                        m = 0;
                    }
                }

                chk0 = (10 - (chk0 % 10)) % 10;

                return (chk0 == chk); // return true, if calculated checksum matches with the original

            } catch (Exception ex)
            {
                // reference number did contain something else than just digits
                return false;
            }

        }

        /// <summary>
        /// convert domestic reference number to international notation
        /// </summary>
        /// <param name="reference"> domestic reference </param>
        /// <returns> international reference (or domestic reference, if it contains non digit characters) </returns>
        public static string ConvertToISO11649(string reference)
        {
            try
            {
                decimal drf = decimal.Parse(reference + "271500");
                int mod = 98 - (int) (drf % 97);
                string isoref = string.Format("RF{0:D2}{1}", mod, reference);
                Console.WriteLine("ISO11649: {0}", isoref);
                return isoref;
            }
            catch (Exception ex)
            {
                return reference;
            }
        }
    }
}
