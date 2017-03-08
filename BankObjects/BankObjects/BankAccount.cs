using System.Collections.Generic;
using System.Linq;

namespace BankObjects
{
    class BankAccount
    {
        private string _accountNumber;
        private string _creationDate;
        private List<BankAccountTransaction> _transactions;
        private float _balance; // current balance

        public BankAccount(string accountNumber, string creationDate)
        {
            this._accountNumber = accountNumber;
            this._creationDate = creationDate;
            _transactions = new List<BankAccountTransaction>();
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
            var balance = 0.0f;

            var transactions = _transactions.Where(activity => activity.Date.CompareTo(date) <= 0).ToList();
            transactions.ForEach (activity =>
            {
                balance += activity.Amount;
            });
            return balance;
        }

        /// <summary>
        /// get all bank account activities
        /// </summary>
        /// <returns></returns>
        public List<BankAccountTransaction> GetTransactions()
        {
            return _transactions;
        }

        /// <summary>
        /// get bank account activities for specified time range
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public BankAccountTransaction[] GetTransactions(string firstDate, string lastDate)
        {
            var transactions = from BankAccountTransaction transaction in _transactions
                             where (transaction.Date.CompareTo(firstDate) >= 0 && transaction.Date.CompareTo(lastDate) <= 0)
                             select transaction;

            return transactions.ToArray();
        }

        /// <summary>
        /// Add new bank account activity
        /// </summary>
        /// <param name="activity"></param>
        public void AddBankAccountTransaction(BankAccountTransaction activity)
        {
            _transactions.Add(activity);
            _balance += activity.Amount;
        }

    }
}
