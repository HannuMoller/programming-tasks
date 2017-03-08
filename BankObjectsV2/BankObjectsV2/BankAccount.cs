using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BankObjects
{
    public class BankAccount
    {
        private List<BankAccountTransaction> _transactions;

        public BankAccount(string accountNumber)
        {
            this.AccountNumber = accountNumber;
            _transactions = new List<BankAccountTransaction>();
            Balance = 0.0f;
        }

        public string AccountNumber { get; set; }

        public float Balance { get; private set; }


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
        public List<BankAccountTransaction> GetTransactions(string firstDate, string lastDate)
        {
            var transactions = from BankAccountTransaction transaction in _transactions
                where (transaction.Date.CompareTo(firstDate) >= 0 && transaction.Date.CompareTo(lastDate) <= 0)
                select transaction;

            return transactions.ToList();
        }

        /// <summary>
        /// Add new bank account activity
        /// </summary>
        /// <param name="transaction"></param>
        public void AddBankAccountTransaction(BankAccountTransaction transaction)
        {
            _transactions.Add(transaction);
            Balance += transaction.Amount;
        }

    }
}
