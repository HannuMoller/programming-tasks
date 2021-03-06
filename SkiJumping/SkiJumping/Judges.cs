﻿using System;

namespace SkiJumping
{
    /// <summary>
    /// Logic simulating skijumping judges
    /// </summary>
    class Judges
    {
        const int JUDGES = 5; // five judges

        /// <summary>
        /// get the technical points from judges
        /// </summary>
        /// <returns></returns>
        public static float GetPoints()
        {
            var points = new float[JUDGES];

            var i = 0;
            while (i < JUDGES)
            {
                try
                {
                    Console.Write("Points of judge#{0} (1.0 .. 20.0) : ", i+1);
                    var f = float.Parse(Console.ReadLine());
                    if (f >= 1.0f && f <= 20.0f)
                    {
                        points[i] = f;
                        i++;
                    }
                }
                catch (Exception ex) {
                    // illegal input -> read again
                }
            }

            // sort the points
            Array.Sort(points);

            // sum the points given by the judges, ignoring highest value and lowest value (= first and last values in the array)
            var total = 0f;
            i = 1;
            while (i < JUDGES-1)
            {
                total += points[i];
                i++;
            }

            Console.WriteLine("Judge points : {0:F1}", total);

            return total;
        }

    }
}
