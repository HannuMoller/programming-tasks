using System;
using System.Collections.Generic;

namespace BankObjects
{
    /// <summary>
    /// Bank
    /// </summary>
    public class Bank
    {
        private string _name;
        private List<BankAccount> _bankAccounts;
        private List<Customer> _customers;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> bank's name </param>
        public Bank(string name)
        {
            this._name = name;
            this._bankAccounts = new List<BankAccount>();
            this._customers = new List<Customer>();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public List<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; }
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
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
