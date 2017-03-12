using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSkiJumping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TournamentData _data;
        private ComboBox[] _judges;
        private Compensation _compensation;
        private bool _closePending;

        public MainWindow()
        {
            InitializeComponent();

            _data = new TournamentData();
            _data.Athletes = TournamentUtilities.GetAthletes();
            if (!_data.MoreAthletes())
            {
                MessageBox.Show("No athletes!");
                return;
            }

            _closePending = false;

            var dlg = new SelectTournamentWindow(_data);
            dlg.ShowDialog();

            var hill = _data.Hill;
            _compensation = new Compensation(hill);

            // gate
            textBoxTournament.Text = $"{hill.Name} ({hill.City})";
            textBoxGate.Text = $"{hill.CurrGate}";

            // wind
            sliderWind.Minimum = -5.0;
            sliderWind.Maximum = +5.0;

            // jump length
            sliderJumpLength.Minimum = 0.0;
            sliderJumpLength.Maximum = hill.LongestJump + 10.0;

            // judges
            _judges = new ComboBox[] {comboBoxJudge1, comboBoxJudge2, comboBoxJudge3, comboBoxJudge4, comboBoxJudge5};
            for (var f = 20.0; f > 0.0; f -= 0.5)
            {
                var s = $"{f:F1}";
                foreach (var judge in _judges)
                {
                    judge.Items.Add(s);
                }
            }

            NextAthlete();
        }

        private void NextAthlete()
        {
            var athlete = _data.GetNextAthlete();
            textBoxJumper.Text = athlete.ToString();

            var hill = _data.Hill;

            sliderWind.Value = 0.0;
            textBoxWind.Text = "0.0";

            sliderJumpLength.Value = hill.Kpoint;
            textBoxJumpLength.Text = $"{hill.Kpoint:F1}";

            foreach (var judge in _judges)
            {
                judge.SelectedIndex = 0;
            }
        }

        private void sliderWind_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBoxWind.Text = $"{sliderWind.Value:+0.0;-0.0}";
        }

        private void sliderJumpLength_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBoxJumpLength.Text = $"{sliderJumpLength.Value:F1}";
        }

        private void buttonReady_Click(object sender, RoutedEventArgs e)
        {
            if (_closePending)
            {
                Close();
                return;
            }
            // calculate points
            // 1. get jump length
            var jumpLength = float.Parse(textBoxJumpLength.Text);
            // 1a. wind compensation
            var wind = float.Parse(textBoxWind.Text);
            var windCompensation = _compensation.WindCompensation(wind);
            // 1b. gate compensation
            var gateCompensation = _compensation.GateCompensation();
            
            // 2. Judge points
            var judgePoints = new float[_judges.Length];
            var i = 0;
            foreach (var judge in _judges)
            {
                judgePoints[i] = float.Parse(judge.Text);
                i++;
            }
            var judgeTotal = judgePoints.Sum() - judgePoints.Max() - judgePoints.Min();
            // 3. total points
            var hill = _data.Hill;
            var totalPoints = hill.GetPoints(jumpLength, windCompensation, gateCompensation) + judgeTotal;
            if (totalPoints < 0.0)
            {
                totalPoints = (float) 0.0;
            }
            var athlete = _data.GetCurrAthlete();

            var result = new Result(athlete, jumpLength, totalPoints);

            _data.AddResult(result);

            listViewResults.ItemsSource = _data.GetResults();
            listViewResults.Items.Refresh();

 
            if (_data.MoreAthletes())
            {
                NextAthlete();
            }
            else
            {
                _closePending = true;
                buttonReady.Content = "Lopeta";

                buttonChangeGate.IsEnabled = false;
                sliderJumpLength.IsEnabled = false;
                sliderWind.IsEnabled = false;
                foreach (var judge in _judges)
                {
                    judge.IsEnabled = false;
                }
                textBoxJumper.Text = "";
            }
        }

        private void buttonChangeGate_Click(object sender, RoutedEventArgs e)
        {
            var hill = _data.Hill;
            var dlg = new ChangeGateWindow(hill);
            dlg.ShowDialog();
            textBoxGate.Text = $"{hill.CurrGate}";
        }

        private void listViewResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
