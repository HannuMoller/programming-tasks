using System;
using System.Linq;
using System.Globalization;

namespace BankObjects
{
    class BankUtils
    {
        /// <summary>
        /// validate date (in format YYYYMMDD)
        /// </summary>
        /// <param name="date"> date to be validated </param>
        /// <returns> true, if date is ok </returns>
        public static bool ValidateDate(string date)
        {
            DateTime dt;
            return DateTime.TryParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out dt);
            }

        /// <summary>
        /// Get preceding date
        /// </summary>
        /// <param name="date"> date </param>
        /// <returns> date for previous day </returns>
        public static string PreviousDate(string date)
        {
            var dt = DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            dt = dt.AddDays(-1);
            return dt.ToString("yyyyMMdd");
        }

        /// <summary>
        /// generate checksum for bank account numbers in domestic format
        /// </summary>
        /// <param name="bban"> account number </param>
        /// <returns> checksum </returns>
        private static char GenerateBbanChecksum(string bban)
        {
            var digits = bban.ToArray();
            var checksum = 0;
            var multipliers = 2;
            var i = digits.Length; // stepper for bban
            while (i > 0)
            {
                i--;
                if (!char.IsDigit(digits[i]))
                    return '!'; // BBAN contained non-digit!
                var n = multipliers * (digits[i] - '0');
                n = n / 10 + n % 10; // if n was greater than 9, sum the digits (eg. 12 -> 1+2 -> 3)
                checksum += n;
                multipliers = 3 - multipliers; // 2 -> 1 -> 2 -> 1 ...
            }

            checksum = (10 - (checksum % 10)) % 10;

            return (char) ('0' + checksum);
        }

        /// <summary>
        /// "normalize" bban
        /// </summary>
        /// <param name="bban"> bank account number in domestic format (either with or without intermediate '-') </param>
        /// <returns> bank account number as 14 digits </returns>
        public static string NormalizeBban(string bban)
        {
            bban = bban.Replace("-", "");
            var fillPos = 0;
            switch (bban.ElementAt(0))
            {
               case '4': // Aktia Pankki, Säästöpankit, Paikallisosuuspankit
               case '5': // OP-Pohjola, Helsingin OP, Pohjola Pankki
                    fillPos = 7;
                    break;
               default: // all the others
                    fillPos = 6;
                    break;
            }
            int requiredZeros = 14 - bban.Length;
            return bban.Insert(fillPos, "00000000000000".Substring(0, requiredZeros));
        }

        /// <summary>
        /// validate bank account number
        /// </summary>
        /// <param name="bban"> account number in domestic format </param>
        /// <returns> true, if account number is correct </returns>
        public static bool ValidateBban(string bban)
        {
            var parts = bban.Split('-');
            switch (parts.Count())
            {
                case 2:
                    if (parts[0].Length != 6)
                        return false; // bank part must be 6 digits!!!
                    break;
                case 1:
                    // no '-' -> ok
                    if (bban.Length < 7)
                        return false; // not long enough
                    parts = new string[] { bban.Substring(0, 6), bban.Substring(6) };
                    break;
                default:
                    return false; // too many '-'
            }

            if (parts[1].Length > 8)
                return false; // too long

            switch (bban.ElementAt(0))
            {
                case '1': // Nordea Pankki
                case '2': // Nordea Pankki
                case '6': // Ålandsbanken
                case '8': // Sampo Pankki
                    break;
                case '4': 
                    switch (bban.ElementAt(1))
                    {
                        case '0':
                            switch (bban.ElementAt(2))
                            {
                                case '0': // 400 = Säästöpankkien Keskuspankki
                                case '2': // 401 -"-
                                case '3': // 402 -"-
                                case '5': // 405 Aktia Pankki
                                case '6': // 406 Säästöpankkien Keskuspankki
                                case '7': // 407 -"-
                                case '8': // 408 -"-
                                    break;
                                default:
                                    return false;
                            }
                            break;
                        case '1':
                            switch (bban.ElementAt(2))
                            {
                                case '3':
                                    return false; // unknown bank
                                default:
                                    // 410-412, 414-419 = Säästöpankkien Keskuspankki
                                    break;
                            }
                            break;
                        case '2':
                            switch (bban.ElementAt(2))
                            {
                                case '2':
                                    return false; // unknown bank
                                default:
                                    // 420-421, 423-429 = Säästöpankkien Keskuspankki
                                    break;
                            }
                            break;
                        case '3':
                            switch (bban.ElementAt(2))
                            {
                                case '3':
                                case '4':
                                    return false; // unknown bank
                                default:
                                    // 430-432, 435-439 = Säästöpankkien Keskuspankki
                                    break;
                            }
                            break;
                        case '4':
                            // 440-449 = Säästöpankkien Keskuspankki
                            break;
                        case '5':
                            switch (bban.ElementAt(2))
                            {
                                case '3':
                                    return false; // unknown bank
                                default:
                                    // 450-452, 454-459 = Säästöpankkien Keskuspankki
                                    break;
                            }
                            break;
                        case '6':
                            switch (bban.ElementAt(2))
                            {
                                case '0':
                                case '1':
                                case '2':
                                case '3':
                                case '4':
                                    // 460-464 = Säästöpankkien Keskuspankki
                                    break;
                                default:
                                    return false; // unknown bank
                            }
                            break;
                        case '7':
                            switch (bban.ElementAt(2))
                            {
                                case '9':
                                    // 479 = Bonum Pankki
                                    break;
                                default:
                                    // 470-478 = POP Pankit
                                    break;
                            }
                            break;
                        case '8':
                            switch (bban.ElementAt(2))
                            {
                                case '0':
                                case '1':
                                case '2':
                                    return false; // unknown bank
                                default:
                                    // 483-489 = Säästöpankkien Keskuspankki
                                    break;
                            }
                            break;
                        case '9':
                            switch (bban.ElementAt(2))
                            {
                                case '7':
                                    // 497 = Aktia Pankki
                                    break;
                                case '4':
                                case '8':
                                case '9':
                                    return false; // unknown bank
                                default:
                                    // 491-493, 495-496 = Säästöpankkien Keskuspankki
                                    break;
                            }
                            break;

                    }
                    break;
                case '5': // OP-Pohjola, Helsingin OP, Pohjola Pankki
                    // ok
                    break;
                case '3':
                    switch (bban.ElementAt(1))
                    {
                        case '1': // Handelsbanken
                        case '3': // Skandinaviska Enskilda Banken
                        case '4': // Danske Bank
                        case '6': // Tapiola Pankki
                        case '7': // DnB NOR Bank
                        case '8': // Swedbank
                        case '9': // S-Pankki
                            break;
                        default:
                            return false; // unknown bank
                    }
                    break;
                case '7':
                    switch (bban.ElementAt(1))
                    {
                        case '1':
                            switch (bban.ElementAt(2))
                            {
                                case '3':
                                    // 713 = Citibank
                                    break;
                                case '5':
                                    // 715 = Säästöpankkien Keskuspankki
                                    break;
                                default:
                                    return false; // unknown bank
                            }
                            break;
                        case '9':
                            switch (bban.ElementAt(2))
                            {
                                case '9':
                                    // 799 = Holvi
                                    break;
                                default:
                                    return false; // unknown bank
                            }
                            break;
                        default:
                            return false; // unknown bank
                    }
                    break;
                default:
                    return false; // unknown bank
            }

            bban = NormalizeBban(bban);

            var checksum = GenerateBbanChecksum(bban.Substring(0, 13));
            if (!char.IsDigit(checksum))
                return false; // bban contained something which prevented checksum calculation

            return (checksum == bban.ElementAt(13));
        }

        /// <summary>
        /// generate IBAN from BBAN
        /// </summary>
        /// <param name="countryCode"> ISO-code for country </param>
        /// <param name="bban"> bank account number in domestic format (assume it is already validated)</param>
        /// <returns> IBAN, or null, if illegal country code is given </returns>
        public static string CreateIban(string countryCode, string bban)
        {
            try
            {
                var culture = CultureInfo.GetCultureInfo(countryCode);
                bban = NormalizeBban(bban);
                var ph1 = (bban + countryCode + "00").ToArray();
                var iban = "";
                foreach (char t in ph1)
                {
                    if (char.IsDigit(t))
                    {
                        iban += t;
                    }
                    else
                    {
                        iban += $"{(t - 'A' + 10):D2}";
                    }
                }
                int chk;
                while (iban.Length > 7)
                {
                    chk = int.Parse(iban.Substring(0, 7)) % 97;
                    iban = $"{chk}{iban.Substring(7)}";
                }
                chk = (98 - int.Parse(iban) % 97) % 97;
                
                return $"{countryCode}{chk:D2}{bban}";
            }
            catch (CultureNotFoundException cnfe)
            {
                // unable to use specified country code
                return null;
            }
        }

        /// <summary>
        /// generate random IBAN
        /// 
        /// </summary>
        /// <returns> IBAN </returns>
        public static string GenerateIban()
        {
            var allowedBankGroups = new char[] { '1', '2', '6', '8' }; // keep it simple, use only these bank groups

            var rnd = new Random();
            var bankGroup = allowedBankGroups[rnd.Next(allowedBankGroups.Length)];
            var bankBody = rnd.Next(100000);
            var accountBody = rnd.Next(10000000);
            var bban = $"{bankGroup}{bankBody:D5}{accountBody:D7}";
            var checksum = GenerateBbanChecksum(bban);

            return CreateIban("FI", bban + checksum);
        }

    }
}
