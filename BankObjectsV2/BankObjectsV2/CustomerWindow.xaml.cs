using BankObjects;
using System.Windows;

namespace BankObjectsV2
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private Bank _bank;

        public CustomerWindow(Bank bank)
        {
            InitializeComponent();

            _bank = bank;
            textBoxBank.Text = bank.Name;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string lastName = textBoxLastName.Text;
            string firstName = textBoxFirstName.Text;
            var customer = new Customer(firstName, lastName);
            customer.BankAccountNumber = _bank.GetNewBankAccount();

            Window window = new ActivityWindow(_bank, customer);
            window.ShowDialog();

            _bank.AddCustomer(customer);

            this.DialogResult = true;
            Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
