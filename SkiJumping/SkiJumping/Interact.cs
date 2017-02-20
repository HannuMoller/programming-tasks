using System;

namespace SkiJumping
{
    /// <summary>
    /// Helper class
    /// </summary>
    class Interact
    {


        /// <summary>
        /// read number
        /// </summary>
        /// <param name="prompt"> question/prompt to the user </param>
        /// <returns> number read </returns>
        public static float ReadFloat(string prompt)
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
        /// Ask user if there is a need for gate change
        /// </summary>
        /// <returns> true, if gate need to be changed </returns>
        public static bool ChangeGate()
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
        public static int SelectGate(Hill hill, string type)
        {
            while (true)
            {
                Console.Write("Select {0} gate ({1}..{2}, current {3}) : ", type, hill.lowestGate, hill.highestGate, hill.currGate);
                try
                {
                    int gate = int.Parse(Console.ReadLine());
                    if (gate >= hill.lowestGate && gate <= hill.highestGate)
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

    }
}
