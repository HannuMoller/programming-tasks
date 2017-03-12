using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfSkiJumping
{
    /// <summary>
    /// Interaction logic for SelectTournamentWindow.xaml
    /// </summary>
    public partial class SelectTournamentWindow : Window
    {
        private TournamentData _data;
        private Dictionary<string, Hill> _hills;
        
        public SelectTournamentWindow(TournamentData data)
        {
            InitializeComponent();
            _data = data;

            _hills = new Dictionary<string, Hill>();
            TournamentUtilities.GetHills().ForEach(hill =>
            {
                var hillName = $"{hill.Name} ({hill.City})";
                _hills.Add(hillName, hill);
                listBoxTournament.Items.Add(hillName);
            });

            SetGateRange(0, 0);
        }

        /// <summary>
        /// Tournament selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelect_Click(object sender, RoutedEventArgs e)
        {
            var tournament = listBoxTournament.SelectedItems[0];

            _data.Hill = _hills[(string) tournament];
            _data.Hill.CurrGate = int.Parse(textBoxGate.Text);
            _data.Hill.BaseGate = _data.Hill.CurrGate;

            this.DialogResult = true;
            Close();
        }

        private void listBoxTournament_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tournament = listBoxTournament.SelectedItems[0];
            var hill = _hills[(string) tournament];
            SetGateRange(hill.LowestGate, hill.HighestGate);
            buttonSelect.IsEnabled = true;
        }

        private void SetGateRange(int lowGate, int highGate)
        {
            sliderGate.Minimum = lowGate;
            sliderGate.Maximum = highGate;
            var sliderPos = (lowGate + highGate) / 2;
            sliderGate.Value = sliderPos;
            textBoxGate.Text = $"{sliderPos}";
        }

        private void sliderGate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBoxGate.Text = $"{sliderGate.Value}";
        }
    }
}
