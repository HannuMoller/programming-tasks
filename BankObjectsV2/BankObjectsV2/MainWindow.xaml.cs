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

        private void buttonCreateBank_Click(object sender, RoutedEventArgs e)
        {
            var bankName = textBoxBank.Text;
            var bank = new Bank(bankName);
            
            Window window = null;

            while (true)
            {
                window = new CustomerWindow(bank);
                bool ok = (bool) window.ShowDialog();
                if (!ok) break;
            }

            window = new ActivitiesWindow(bank);
            window.ShowDialog();

            Close();
        }

    }
}
