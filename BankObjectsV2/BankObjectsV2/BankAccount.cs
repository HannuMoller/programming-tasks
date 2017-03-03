using System.Collections;
using System.Linq;

namespace BankObjects
{
    public class BankAccount
    {
        private string _accountNumber;
        private ArrayList _transactions;
        private float _balance; // current balance

        public BankAccount(string accountNumber)
        {
            this._accountNumber = accountNumber;
            _transactions = new ArrayList();
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
            var balance2 = 0.0f;

            var transactions2 = from BankAccountTransaction transaction in _transactions
                              where (transaction.Date.CompareTo(date) <= 0)
                              select transaction;
            foreach (BankAccountTransaction transaction in transactions2)
            {
                balance2 += transaction.Amount;
            }
            return balance2;
        }

        /// <summary>
        /// get all bank account activities
        /// </summary>
        /// <returns></returns>
        public ArrayList GetTransactions()
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
            var transactions2 = from BankAccountTransaction transaction in _transactions
                where (transaction.Date.CompareTo(firstDate) >= 0 && transaction.Date.CompareTo(lastDate) <= 0)
                select transaction;

            return transactions2.ToArray();
        }

        /// <summary>
        /// Add new bank account activity
        /// </summary>
        /// <param name="activity"></param>
        public void AddBankAccountActivity(BankAccountTransaction transaction)
        {
            _transactions.Add(transaction);
            _balance += transaction.Amount;
        }

    }
}
