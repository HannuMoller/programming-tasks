using System;
using System.Collections.Generic;

namespace BankObjects
{
    /// <summary>
    /// Bank
    /// </summary>
    class Bank
    {
        private string _name;
        private List<BankAccount> _bankAccounts;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> bank's name </param>
        public Bank(string name)
        {
            this._name = name;
            this._bankAccounts = new List<BankAccount>();
        }

        /// <summary>
        /// Create new bank account
        /// </summary>
        /// <returns> account number for the new bank account </returns>
        public string GetNewBankAccount()
        {
            
            var bankAccountNumber = BankUtils.GenerateIban();
            Console.WriteLine("New bank account: {0}", bankAccountNumber);
            var bankAccountCreationDate = BankUtils.GetDate("bank account creation");

            _bankAccounts.Add(new BankAccount(bankAccountNumber, bankAccountCreationDate));

            return bankAccountNumber;
        }

        /// <summary>
        /// get bank account having specified account number
        /// </summary>
        /// <param name="accountNumber"> account number </param>
        /// <returns> bank account </returns>
        public BankAccount GetBankAccount(string accountNumber)
        {
            return _bankAccounts.Find(x => x.AccountNumber.Equals(accountNumber));
        }

    }
}
