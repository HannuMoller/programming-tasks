using System.Collections;
using System.Linq;

namespace BankObjects
{
    class BankAccount
    {
        private string _accountNumber;
        private string _creationDate;
        private ArrayList _activities;
        private float _balance; // current balance

        public BankAccount(string accountNumber, string creationDate)
        {
            this._accountNumber = accountNumber;
            this._creationDate = creationDate;
            _activities = new ArrayList();
            _balance = 0.0f;
        }

        public string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        /// <summary>
        /// get current balance
        /// </summary>
        /// <returns> balance </returns>
        public float Balance
        {
            get { return _balance; }
        }

        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        /// <summary>
        /// get balance at specified date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public float GetBalance(string date)
        {
            var balance2 = 0.0f;

            var activities2 = from BankAccountActivity activity in _activities
                              where (activity.Date.CompareTo(date) <= 0)
                              select activity;
            foreach (BankAccountActivity activity in activities2)
            {
                balance2 += activity.Amount;
            }
            return balance2;
        }

        /// <summary>
        /// get all bank account activities
        /// </summary>
        /// <returns></returns>
        public ArrayList GetActivities()
        {
            return _activities;
        }

        /// <summary>
        /// get bank account activities for specified time range
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public BankAccountActivity[] GetActivities(string firstDate, string lastDate)
        {
            var activities2 = from BankAccountActivity activity in _activities
                             where (activity.Date.CompareTo(firstDate) >= 0 && activity.Date.CompareTo(lastDate) <= 0)
                             select activity;

            return activities2.ToArray();
        }

        /// <summary>
        /// Add new bank account activity
        /// </summary>
        /// <param name="activity"></param>
        public void AddBankAccountActivity(BankAccountActivity activity)
        {
            _activities.Add(activity);
            _balance += activity.Amount;
        }

    }
}
