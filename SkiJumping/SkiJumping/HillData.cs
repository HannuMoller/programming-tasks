using System;
using System.IO;

namespace SkiJumping
{
    /// <summary>
    /// method for retrieving hill data
    /// </summary>
    class HillData
    {

        /// <summary>
        /// Read hill data from file
        /// </summary>
        /// <param name="competition"> Hill name </param>
        /// <returns> Hill object </returns>
        public static Hill ReadHill(string competition)
        {
            var f = File.OpenText("../../data/" + competition + ".Hill.txt");
            var s = f.ReadLine();
            var data = s.Split(':');
            f.Close();

            var Kpoint = int.Parse(data[0]);
            var lowestGate = int.Parse(data[1]);
            var highestGate = int.Parse(data[2]);
            var gateStep = float.Parse(data[3]);
            var gateCompensationPerMeter = float.Parse(data[4]);

            return new Hill(competition, lowestGate, highestGate, Kpoint, gateStep, gateCompensationPerMeter);
        }

    }
}
