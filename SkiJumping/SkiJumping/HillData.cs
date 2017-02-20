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
            String s = f.ReadLine();
            string[] data = s.Split(':');
            f.Close();

            int Kpoint = int.Parse(data[0]);
            int lowestGate = int.Parse(data[1]);
            int highestGate = int.Parse(data[2]);
            float gateStep = float.Parse(data[3]);
            float gateCompensationPerMeter = float.Parse(data[4]);

            return new Hill(competition, lowestGate, highestGate, Kpoint, gateStep, gateCompensationPerMeter);
        }

    }
}
