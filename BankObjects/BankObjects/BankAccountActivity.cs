namespace BankObjects
{
    /// <summary>
    /// One bank account activity
    /// </summary>
    public class BankAccountActivity
    {
        private string _date;
        private float _amount;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="date"> date of activity </param>
        /// <param name="amount"> amount of money </param>
        public BankAccountActivity(string date, float amount)
        {
            this._date = date;
            this._amount = amount;
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public float Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}
