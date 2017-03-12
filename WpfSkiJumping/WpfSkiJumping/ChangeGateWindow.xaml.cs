using System.Windows;

namespace WpfSkiJumping
{
    /// <summary>
    /// Interaction logic for ChangeGateWindow.xaml
    /// </summary>
    public partial class ChangeGateWindow : Window
    {
        private Hill _hill;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hill"></param>
        public ChangeGateWindow(Hill hill)
        {
            InitializeComponent();
            _hill = hill;

            sliderGate.Minimum = hill.LowestGate;
            sliderGate.Maximum = hill.HighestGate;
            sliderGate.Value = hill.CurrGate;
            textBoxGate.Text = $"{hill.CurrGate}";
        }

        /// <summary>
        /// Accept changed value for gate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelect_Click(object sender, RoutedEventArgs e)
        {
            _hill.CurrGate = int.Parse(textBoxGate.Text);

            this.DialogResult = true;
            Close();
        }

        /// <summary>
        /// show changed value as number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sliderGate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBoxGate.Text = $"{sliderGate.Value}";
        }
    }
}
