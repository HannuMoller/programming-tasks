using System;
using System.Collections.Generic;

namespace BankObjects
{
    class Program
    {
        /// <summary>
        /// Setup bank, some customers, accounts and activities
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("The Bank v1.0");
            Console.Write("Enter bank name : ");
            string bankName = Console.ReadLine();
            Bank bank = new Bank(bankName);

            var customers = new List<Customer>();

            Console.WriteLine("Need 3 customers...");
            for (int i=0; i<3; i++)
            {
                Console.WriteLine("Enter data for customer #{0}", i + 1);
                Console.Write("- lastname : ");
                string lastName = Console.ReadLine();
                Console.Write("- first name : ");
                string firstname = Console.ReadLine();
                var customer = new Customer(firstname, lastName);
                customer.BankAccountNumber = bank.getNewBankAccount();
                customers.Add(customer);

                AddActivities(bank, customer);
            }

            foreach (var customer in customers)
            {
                Console.WriteLine("Customer:{0}, account:{1}, balance:{2:F2}", customer, customer.BankAccountNumber, bank.getBankAccount(customer.BankAccountNumber).getBalance());
            }
            Console.WriteLine("====================================");

            var firstDate = BankUtils.getDate("first");
            var lastDate = BankUtils.getDate("last");
            foreach (var customer in customers)
            {
                var account = bank.getBankAccount(customer.BankAccountNumber);
                var prevDate = BankUtils.PreviousDate(firstDate);
                float balance = account.getBalance(prevDate);
                Console.WriteLine("Customer:{0}, account:{1}, balance at {2}:{3:F2}", customer, customer.BankAccountNumber, prevDate, balance);

                var activities = account.getActivities(firstDate, lastDate);

                foreach (BankAccountActivity activity in activities)
                {
                    balance += activity.amount;
                    Console.WriteLine("Date:{0}, amount:{1,-12:F2}, balance:{2,-12:F2}", activity.date, activity.amount, balance);
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Add some activities to customer's bank account
        /// </summary>
        /// <param name="bank"> Bank object </param>
        /// <param name="customer"> Customer object </param>
        static void AddActivities(Bank bank, Customer customer)
        {
            Console.WriteLine("Enter withdrawal/deposit for customer {0} (0 ends)", customer);
            var account = bank.getBankAccount(customer.BankAccountNumber);
            var lastActivityDate = account.creationDate;
            while (true)
            {
                Console.Write("- amount : ");
                float amount;
                if (float.TryParse(Console.ReadLine(), out amount))
                {
                    if (amount == 0.0f)
                    {
                        return;
                    }
                    var date = "00000000";
                    while (date.CompareTo(lastActivityDate) < 0)
                    {
                        date = BankUtils.getDate("action");
                    }
                    account.AddBankAccountActivity(new BankAccountActivity(date, amount));
                    lastActivityDate = date;
                }
            }
        }


    }
}
