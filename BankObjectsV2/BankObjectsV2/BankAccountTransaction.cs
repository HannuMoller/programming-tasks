namespace BankObjects
{
    /// <summary>
    /// One bank account activity
    /// </summary>
    public class BankAccountTransaction
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="date"> date of activity </param>
        /// <param name="amount"> amount of money </param>
        public BankAccountTransaction(string date, float amount)
        {
            Date = date;
            Amount = amount;
        }

        public string Date { get; set; }

        public float Amount { get; set; }
    }
}
