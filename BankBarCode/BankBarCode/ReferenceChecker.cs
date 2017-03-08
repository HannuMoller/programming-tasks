using System;

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
            var mx = multipliers.Length;
            var cc = reference.ToCharArray();

            try
            {
                var i = cc.Length-1;
                var chk = cc[i] - '0'; // original checksum
                var chk0 = 0; // calculated checksum
                var m = 0;
                while (i > 0)
                {
                    i--;
                    var c = cc[i] - '0';
                    var mp = multipliers[m];
                    chk0 += c * mp;
                    m = (m + 1) % mx;
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
                string isoref = $"RF{mod:D2}{reference}";
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
