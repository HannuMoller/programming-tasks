using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
            string competition = Console.ReadLine();

            var hill = readHill(competition);
            var athletes = readAthletes(competition);
            Console.WriteLine("{0} athletes.", athletes.Count);

            var compensation = new Compensation(hill);
            compensation.setBaseGate(selectGate("startup"));

            var results = new SortedList(new FloatComparer());

            Console.WriteLine("Hill {0}, K-point = {1}m, starting at gate {2}", competition, hill.Kpoint, compensation.baseGate);

            Console.WriteLine("Competition starts:");
            var i = 0;
            while (i < athletes.Count)
            {
                var a1 = (Athlete)athletes.GetByIndex(i);
                i++;

                Console.WriteLine("Athlete #{0}: {1} {2} ({3})", i, a1.number, a1.name, a1.country);

                if (changeGate())
                {
                    compensation.changeGate(selectGate("new"));
                }

                float length = readFloat("jump length");
                float wind = readFloat("wind");

                float points = hill.getPoints(length, compensation.WindCompensation(wind), compensation.gateCompensation());
                points += Judges.getPoints();
                if (points < 0.0f)
                {
                    // can not give negative total points
                    points = 0.0f;
                }
                Console.WriteLine("- points: {0:F1}", points);

                a1.setJump(length);
                a1.setPoints(points);

                results.Add(points, a1);

                showStandings(results);
            }

            Console.WriteLine("Competition over, final results can be seen above. \n Thank you!");
            Console.ReadKey();
        }

        /// <summary>
        /// Show current standings
        /// </summary>
        /// <param name="results"> athletes who have jumped, ordered by total points </param>
        public static void showStandings(SortedList standings)
        {
            Console.WriteLine("Current standings:\n==================");
            int i = 0;
            float prev_points = -1.0f;
            int pos = -1;
            while (i < standings.Count)
            {
                Athlete a = (Athlete) standings.GetByIndex(i);
                i++;
                if (a.points != prev_points)
                {
                    prev_points = a.points;
                    pos = i;
                }
                Console.WriteLine("{0}: [{1}] {2} ({3}) {4:F1}m {5:F1}p", pos, a.number, a.name, a.country, a.jump, a.points);
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// read number
        /// </summary>
        /// <param name="prompt"> question/prompt to the user </param>
        /// <returns> number read </returns>
        static float readFloat(string prompt)
        {
            float f = float.NaN;
            while (float.IsNaN(f))
            {
                try
                {
                    Console.Write("- {0} : ", prompt);
                    f = float.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    // illegal input
                    f = float.NaN;
                }
            }

            return f;
        }

        /// <summary>
        /// Read hill data from file
        /// </summary>
        /// <param name="competition"> Hill name </param>
        /// <returns> Hill object </returns>
        static Hill readHill(string competition)
        {
            var f = File.OpenText("../../data/" + competition + ".Hill.txt");
            String s = f.ReadLine();
            string[] data = s.Split(':');
            f.Close();

            int Kpoint = int.Parse(data[0]);
            float gateStep = float.Parse(data[1]);
            float gateComp = float.Parse(data[2]);
         
            return new Hill(competition, Kpoint, gateStep, gateComp);
        }

        /// <summary>
        /// Ask user if there is a need for gate change
        /// </summary>
        /// <returns> true, if gate need to be changed </returns>
        static bool changeGate()
        {
            while (true)
            {
                Console.Write("Change gate (Y/N) : ");
                char c = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                if (c == 'Y')
                {
                    return true;
                }
                if (c == 'N')
                {
                    return false;
                }
                Console.WriteLine("Please answer Y or N!");
            }

        }

        /// <summary>
        /// Ask user for which gate to be used
        /// </summary>
        /// <param name="type"> clarification (is this the startup gate or gate change) </param>
        /// <returns> gate number </returns>
        static int selectGate(string type)
        {
            while (true) {
                Console.Write("Select {0} gate (1..18) : ", type);
                try
                {
                    int gate = int.Parse(Console.ReadLine());
                    if (gate > 0 && gate < 19)
                    {
                        return gate;
                    }
                }
                catch (Exception ex)
                {
                    // 
                }
            }
        }

        /// <summary>
        /// Read athlete data from file
        /// </summary>
        /// <param name="competition"> Hill name </param>
        /// <returns> SortedList containing athletes </returns>
        static SortedList readAthletes(string competition)
        {
            var athletes = new SortedList();
//            athletes.Add(0, 0); // placeholder for zero index

            var f = File.OpenText("../../data/" + competition + ".Athletes.txt");
            while (!f.EndOfStream) {
                string[] data = f.ReadLine().Split(':');
             
                int number = int.Parse(data[0]);
                string name = data[1];
                string country = data[2];
                athletes.Add(number, new Athlete(number, name, country));
            }
            f.Close();

            return athletes;
        }

    }
}
