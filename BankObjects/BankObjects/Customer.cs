namespace BankObjects
{
    /// <summary>
    /// Bank customer
    /// </summary>
    public class Customer
    {
        public string firstName;
        public string lastName;
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName"> customer's first name </param>
        /// <param name="lastName"> customer's last name </param>
        public Customer(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        /// <summary>
        /// return customer's basic data as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", lastName, firstName);
        }
    }
}
