﻿namespace BankObjects
{
    /// <summary>
    /// Bank customer
    /// </summary>
    public class Customer
    {
        private string _firstName;
        private string _lastName;
        private string _bankAccountNumber;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName"> customer's first name </param>
        /// <param name="lastName"> customer's last name </param>
        public Customer(string firstName, string lastName)
        {
            this._firstName = firstName;
            this._lastName = lastName;
        }

        public string BankAccountNumber
        {
            get { return _bankAccountNumber; }
            set { _bankAccountNumber = value; }
        }

        /// <summary>
        /// return customer's basic data as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", _lastName, _firstName);
        }
    }
}
