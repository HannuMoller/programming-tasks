using System.Collections;
using System.Linq;

namespace BankObjects
{
    class BankAccount
    {
        public string accountNumber;
        public string creationDate;
        public ArrayList activities;
        public float balance; // current balance

        public BankAccount(string accountNumber, string creationDate)
        {
            this.accountNumber = accountNumber;
            this.creationDate = creationDate;
            activities = new ArrayList();
            balance = 0.0f;
        }

        /// <summary>
        /// get current balance
        /// </summary>
        /// <returns> balance </returns>
        public float getBalance()
        {
            return balance;
        }

        /// <summary>
        /// get balance at specified date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public float getBalance(string date)
        {
            var balance2 = 0.0f;

            var activities2 = from BankAccountActivity activity in activities
                              where (activity.date.CompareTo(date) <= 0)
                              select activity;
            foreach (BankAccountActivity activity in activities2)
            {
                balance2 += activity.amount;
            }
            return balance2;
        }

        /// <summary>
        /// get all bank account activities
        /// </summary>
        /// <returns></returns>
        public ArrayList getActivities()
        {
            return activities;
        }

        /// <summary>
        /// get bank account activities for specified time range
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public BankAccountActivity[] getActivities(string firstDate, string lastDate)
        {
            var activities2 = from BankAccountActivity activity in activities
                             where (activity.date.CompareTo(firstDate) >= 0 && activity.date.CompareTo(lastDate) <= 0)
                             select activity;

            return activities2.ToArray();
        }

        /// <summary>
        /// Add new bank account activity
        /// </summary>
        /// <param name="activity"></param>
        public void AddBankAccountActivity(BankAccountActivity activity)
        {
            activities.Add(activity);
            balance += activity.amount;
        }

    }
}
