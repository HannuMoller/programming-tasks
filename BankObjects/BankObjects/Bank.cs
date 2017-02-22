using System;
using System.Collections.Generic;

namespace BankObjects
{
    /// <summary>
    /// Bank
    /// </summary>
    class Bank
    {
        private string name;
        private List<BankAccount> bankAccounts;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> bank's name </param>
        public Bank(string name)
        {
            this.name = name;
            bankAccounts = new List<BankAccount>();
        }

        /// <summary>
        /// Create new bank account
        /// </summary>
        /// <returns> account number for the new bank account </returns>
        public string getNewBankAccount()
        {
            
            var bankAccountNumber = BankUtils.generateIBAN();
            Console.WriteLine("New bank account: {0}", bankAccountNumber);
            var bankAccountCreationDate = BankUtils.getDate("bank account creation");

            bankAccounts.Add(new BankAccount(bankAccountNumber, bankAccountCreationDate));

            return bankAccountNumber;
        }

        /// <summary>
        /// get bank account having specified account number
        /// </summary>
        /// <param name="accountNumber"> account number </param>
        /// <returns> bank account </returns>
        public BankAccount getBankAccount(string accountNumber)
        {
            return bankAccounts.Find(x => x.accountNumber.Equals(accountNumber));
        }

    }
}
