using BankObjects;

using System.Windows;


namespace BankObjectsV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateBank_Click(object sender, RoutedEventArgs e)
        {
            var bankName = textBoxBank.Text;
            var bank = new Bank(bankName);

            while ((bool) (new CustomerWindow(bank).ShowDialog()));

            new ActivitiesWindow(bank).ShowDialog();

            Close();
        }

    }
}
