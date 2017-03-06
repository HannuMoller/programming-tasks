using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BankObjects
{
    public class BankAccount
    {
        private string _accountNumber;
        private List<BankAccountTransaction> _transactions;
        private float _balance; // current balance

        public BankAccount(string accountNumber)
        {
            this._accountNumber = accountNumber;
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


        /// <summary>
        /// get balance at specified date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public float GetBalance(string date)
        {
            var balance = 0.0f;

            var transactions = _transactions.Where(transaction => transaction.Date.CompareTo(date) <= 0).ToList();
            transactions.ForEach(transaction =>
            {
                balance += transaction.Amount;
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
        /// <param name="transaction"></param>
        public void AddBankAccountTransaction(BankAccountTransaction transaction)
        {
            _transactions.Add(transaction);
            _balance += transaction.Amount;
        }

    }
}
