using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiJumping
{
    /// <summary>
    /// Comparer class to help maintain the results in descending order (based on jumper's points)
    /// </summary>
    class FloatComparer : IComparer
    {

        /// <summary>
        /// Compare the points of two jumper
        /// </summary>
        /// <param name="f1"> points of jumper A </param>
        /// <param name="f2"> points of jumper B </param>
        /// <returns> 1, if B has better points compared to A; else -1</returns>
        int IComparer.Compare(object f1, object f2)
        {
            int i = (int) ( 10.0f * ( (float)f2 - (float)f1 ) );
            if (i == 0) i = -1; // must not return 0, as SortedList won't add such entry

            return i;
        }
    }
}
