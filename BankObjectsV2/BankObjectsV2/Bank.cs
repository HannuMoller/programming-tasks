using System;
using System.Collections.Generic;

namespace BankObjects
{
    /// <summary>
    /// Bank
    /// </summary>
    public class Bank
    {
        private List<BankAccount> _bankAccounts;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> bank's name </param>
        public Bank(string name)
        {
            Name = name;
            this._bankAccounts = new List<BankAccount>();
            Customers = new List<Customer>();
        }

        public string Name { get; set; }

        public List<Customer> Customers { get; set; }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        /// <summary>
        /// Create new bank account
        /// </summary>
        /// <returns> account number for the new bank account </returns>
        public string GetNewBankAccount()
        {
            
            var bankAccountNumber = BankUtils.GenerateIban();

            _bankAccounts.Add(new BankAccount(bankAccountNumber));

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
