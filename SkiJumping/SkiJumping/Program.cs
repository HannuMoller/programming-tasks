using System;
using System.Collections;

namespace SkiJumping
{
    /// <summary>
    /// Ski jumping result calculation system v1.0
    /// (currently only one round competition supported!)
    /// 
    /// The initial data needed:
    /// - hill:
    ///   - K-point
    ///   - gap between successive gates
    ///   - compensation for gate change
    /// - athletes:
    ///   - athlete number
    ///   - name
    ///   - country
    ///   
    /// Input data during competition:
    /// - gate change
    /// - wind
    /// - jump length
    /// - technical points given by judges
    /// 
    /// Output data during competition:
    /// - current standings after each jump
    /// 
    /// Output data after competition:
    /// - Thank's folks! ;)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ski Jumping Competition Result Calculation System v1.0");
            Console.Write("Enter competition name : ");
            var competition = Console.ReadLine();

            var hill = HillData.ReadHill(competition);
            var athletes = AthleteData.ReadAthletes(competition);
            Console.WriteLine("{0} athletes.", athletes.Count);

            var compensation = new Compensation(hill);
            hill.BaseGate = Interact.SelectGate(hill, "startup");

            var results = new SortedList(new FloatComparer());

            Console.WriteLine("Hill {0}, K-point = {1}m, starting at gate {2}", competition, hill.Kpoint, hill.BaseGate);

            Console.WriteLine("Competition starts:");
            var i = 0;
            while (i < athletes.Count)
            {
                var a1 = (Athlete)athletes.GetByIndex(i);
                i++;

                Console.WriteLine("Athlete #{0}: {1} {2} ({3})", i, a1.Number, a1.Name, a1.Country);

                if (Interact.ChangeGate())
                {
                    hill.ChangeGate(Interact.SelectGate(hill, "new"));
                }

                var length = Interact.ReadFloat("jump length");
                var wind = Interact.ReadFloat("wind");

                var points = hill.GetPoints(length, compensation.WindCompensation(wind), compensation.GateCompensation());
                points += Judges.GetPoints();
                if (points < 0.0f)
                {
                    // can not give negative total points
                    points = 0.0f;
                }
                Console.WriteLine("- points: {0:F1}", points);

                a1.Jump = length;
                a1.Points = points;

                results.Add(points, a1);

                ShowStandings(results);
            }

            Console.WriteLine("Competition over, final results can be seen above. \n Thank you!");
            Console.ReadKey();
        }

        /// <summary>
        /// Show current standings
        /// </summary>
        /// <param name="results"> athletes who have jumped, ordered by total points </param>
        public static void ShowStandings(SortedList standings)
        {
            Console.WriteLine("Current standings:\n==================");
            var i = 0;
            var prev_points = -1.0f;
            var pos = -1;
            while (i < standings.Count)
            {
                var a = (Athlete) standings.GetByIndex(i);
                i++;
                if (a.Points != prev_points)
                {
                    prev_points = a.Points;
                    pos = i;
                }
                Console.WriteLine("{0}: [{1}] {2} ({3}) {4:F1}m {5:F1}p", pos, a.Number, a.Name, a.Country, a.Jump, a.Points);
            }
            Console.WriteLine("");
        }


    }
}
