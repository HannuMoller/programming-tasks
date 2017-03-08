using System;

namespace BankBarCode
{
    /// <summary>
    /// Generate bank barcode (either domestic or international format)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bank BarCode Generator v1.0");

            int version = 0;
            while (version < 4 || version > 5)
            {
                Console.Write("Which version to use (4 (= domestic) or 5 (=international)) : ");
                if (!int.TryParse(Console.ReadLine(), out version))
                {
                    Console.WriteLine("Invalid selection!");
                    version = 0;
                }
            }

            BarCode barcode = null;
            if (version == 4)
            {
                barcode = new BarCodeVersion4();
            } else
            {
                barcode = new BarCodeVersion5();
            }

            String account = null;
            do
            {
                Console.Write("Bank account : ");
                account = RemoveGaps(Console.ReadLine());
                if (!IBANChecker.Check(account))
                {
                    Console.WriteLine("Invalid bank account");
                    account = null;
                }
            } while (account == null);

            barcode.SetAccount(account);

            string reference = null;
            do
            {
                Console.Write("Bank reference number (in domestic format) : ");
                reference = RemoveGaps(Console.ReadLine());
                if (!ReferenceChecker.Check(reference))
                {
                    reference = null;
                }
            } while (reference == null);

            barcode.SetReference(reference);

            int euros = -1;
            while (euros < 0)
            {
                Console.Write("Enter euros (0..999999) : ");
                if (int.TryParse(Console.ReadLine(), out euros))
                {
                    if (euros < 0 || euros > 999999)
                    {
                        Console.WriteLine("Euro amount out of range!");
                        euros = -1;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid euro amount!");
                    euros = -1;
                }
            }
            barcode.SetEuros(euros);

            int cents = -1;
            while (cents < 0)
            {
                Console.Write("Enter cents (0..99) : ");
                if (int.TryParse(Console.ReadLine(), out cents))
                {
                    if (cents < 0 || cents > 99)
                    {
                        Console.WriteLine("Cent amount out of range!");
                        cents = -1;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid cent amount!");
                }
            }
            barcode.SetCents(cents);

            string dueDate = null;
            while (dueDate == null)
            {
                Console.Write("Enter due date (YYMMDD) : ");
                dueDate = Console.ReadLine();
                DateTime dt;
                if (!DateTime.TryParseExact(dueDate, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture,
                           System.Globalization.DateTimeStyles.None, out dt))
                {
                    Console.WriteLine("Invalid due date!");
                    dueDate = null;
                }
            }
            barcode.SetDueDate(dueDate);

            Console.WriteLine("IBAN = {0}", barcode.GetFormattedBarCode());
            Console.ReadKey();
        }


        private static string RemoveGaps(string x)
        {
            int i = x.IndexOf(' ');
            while (i > -1) {
                x = x.Remove(i, 1);
                i = x.IndexOf(' ');
            }
            return x;
        }

    }
}
