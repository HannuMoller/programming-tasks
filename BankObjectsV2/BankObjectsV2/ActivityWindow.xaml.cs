using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using BankObjects;

namespace BankObjectsV2
{

 
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private Bank _bank;
        private Customer _customer;

        public ActivityWindow(Bank bank, Customer customer)
        {
            InitializeComponent();
            _bank = bank;
            _customer = customer;
            textBoxBank.Text = bank.Name;
            textBoxCustomer.Text = customer.FirstName + " " + customer.LastName;
            textBoxAccount.Text = customer.BankAccountNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddActivity_Click(object sender, RoutedEventArgs e)
        {
            var date = textBoxDate.Text;
            if (!BankUtils.ValidateDate(date))
            {
                MessageBox.Show("Virheellinen päivämäärä!");
                return;
            }
            float amount;
            if (!float.TryParse(textBoxAmount.Text, out amount))
            {
                MessageBox.Show("Virheellinen summa!");
                return;
            }

            var transaction = new BankAccountTransaction(date, amount);
            var account = _bank.GetBankAccount(_customer.BankAccountNumber);
            account.AddBankAccountTransaction(transaction);

            List<BankAccountTransaction> items = (List<BankAccountTransaction>) listViewActivities.ItemsSource;
            if (items == null)
            {
                items = new List<BankAccountTransaction>();
            }
            items.Add(transaction);

            
            listViewActivities.ItemsSource = items;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewActivities.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));

            textBoxAmount.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReady_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBoxDate_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
