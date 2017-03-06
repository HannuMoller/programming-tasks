using System.Windows;
using System.Windows.Controls;
using BankObjects;

namespace BankObjectsV2
{
    /// <summary>
    /// Interaction logic for ActivitiesWindow.xaml
    /// </summary>
    public partial class ActivitiesWindow : Window
    {
        private Bank _bank;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bank"></param>
        public ActivitiesWindow(Bank bank)
        {
            InitializeComponent();
            textBoxBank.Text = bank.Name;
            _bank = bank;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            string date1 = textBoxDate1.Text;
            if (!BankUtils.ValidateDate(date1))
            {
                MessageBox.Show("Alkupäivämäärä virheellinen");
                return;
            }
            string date2 = textBoxDate2.Text;
            if (!BankUtils.ValidateDate(date2))
            {
                MessageBox.Show("Loppupäivämäärä virheellinen!");
                return;
            }

            //

            listBoxActivities.Items.Clear();
            foreach (var customer in _bank.Customers)
            {
                var account = _bank.GetBankAccount(customer.BankAccountNumber);
                var prevDate = BankUtils.PreviousDate(date1);
                float balance = account.GetBalance(prevDate);
                listBoxActivities.Items.Add(string.Format("Asiakas: {0}, tili: {1}, saldo {2}: {3:F2}", customer, customer.BankAccountNumber, prevDate, balance));

                var transactions = account.GetTransactions(date1, date2);

                foreach (BankAccountTransaction transaction in transactions)
                {
                    balance += transaction.Amount;
                    listBoxActivities.Items.Add(string.Format("Päivämäärä: {0}, summa: {1,12:F2}, saldo: {2,12:F2}", transaction.Date, transaction.Amount, balance));
                }
                listBoxActivities.Items.Add("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
