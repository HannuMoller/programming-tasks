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
            var date1 = textBoxDate1.Text;
            if (!BankUtils.ValidateDate(date1))
            {
                MessageBox.Show("Alkupäivämäärä virheellinen");
                return;
            }
            var date2 = textBoxDate2.Text;
            if (!BankUtils.ValidateDate(date2))
            {
                MessageBox.Show("Loppupäivämäärä virheellinen!");
                return;
            }

            listBoxActivities.Items.Clear();

            _bank.Customers.ForEach(
                customer =>
                {
                    var account = _bank.GetBankAccount(customer.BankAccountNumber);
                    var prevDate = BankUtils.PreviousDate(date1);
                    var balance = account.GetBalance(prevDate);
                    listBoxActivities.Items.Add(
                        $"Asiakas: {customer}, tili: {customer.BankAccountNumber}, saldo {prevDate}: {balance:F2}");

                    account.GetTransactions(date1, date2).ForEach(
                        transaction =>
                        {
                            balance += transaction.Amount;
                            listBoxActivities.Items.Add(
                                $"Päivämäärä: {transaction.Date}, summa: {transaction.Amount,12:F2}, saldo: {balance,12:F2}");
                        }
                    );
                    listBoxActivities.Items.Add("");
                }
            );

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
