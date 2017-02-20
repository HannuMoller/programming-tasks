using System.IO;
using System.Collections;

namespace SkiJumping
{
    /// <summary>
    /// method for reading athletes
    /// </summary>
    class AthleteData
    {

        /// <summary>
        /// Read athlete data from file
        /// </summary>
        /// <param name="competition"> Hill name </param>
        /// <returns> SortedList containing athletes </returns>
        public static SortedList ReadAthletes(string competition)
        {
            var athletes = new SortedList();

            var f = File.OpenText("../../data/" + competition + ".Athletes.txt");
            while (!f.EndOfStream)
            {
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
