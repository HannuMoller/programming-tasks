using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiJumping
{
    /// <summary>
    /// Skijumper data
    /// </summary>
    class Athlete // : IComparable
    {
        public int number; // jumper's starting number (jump order)
        public string name; // jumper's name
        public string country; // country the jumper represents
        public float jump; // length in meters
        public float points; // total points

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="number"> starting number </param>
        /// <param name="name"> jumper's name </param>
        /// <param name="country"> jumper's country </param>
        public Athlete(int number, string name, string country)
        {
            this.number = number;
            this.name = name;
            this.country = country;
            this.jump = 0.0f;
            this.points = 0.0f;
        }

        /// <summary>
        /// set jumper's jump length
        /// </summary>
        /// <param name="jump"> jump length in meters </param>
        public void setJump(float jump)
        {
            this.jump = jump;
        }

        /// <summary>
        /// set jumper's total points
        /// </summary>
        /// <param name="points"> points </param>
        public void setPoints(float points)
        {
            this.points = points;
        }

        /// <summary>
        /// one-line presentation of jumper data
        /// </summary>
        /// <returns> jumper data </returns>
        public string toString()
        {
            return string.Format("{0}:{1} ({2}), {3:F1}m, {4:F1}", number, name, country, jump, points);
        }

/*        int IComparable.CompareTo(object a)
        {
            Athlete a2 = (Athlete) a;
            return (this.number - a2.number);
        }

*/

    }
}
