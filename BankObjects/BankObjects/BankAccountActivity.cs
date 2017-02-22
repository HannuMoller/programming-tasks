namespace BankObjects
{
    /// <summary>
    /// One bank account activity
    /// </summary>
    public class BankAccountActivity
    {
        public string date;
        public float amount;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="date"> date of activity </param>
        /// <param name="amount"> amount of money </param>
        public BankAccountActivity(string date, float amount)
        {
            this.date = date;
            this.amount = amount;
        }

    }
}
