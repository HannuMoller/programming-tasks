using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiJumping
{
    /// <summary>
    /// Wind and Gate compensation methods
    /// </summary>
    class Compensation
    {
        public Hill hill; // reference to the hill
        public int baseGate; // gate number in the beginning of the competition
        public int currGate; // current gate number

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="hill"> reference to the hill object </param>
        public Compensation(Hill hill)
        {
            this.hill = hill;
            this.baseGate = 0;
            this.currGate = 0;
        }

        /// <summary>
        /// set base gate at the beginning of the competition
        /// </summary>
        /// <param name="baseGate"> gate number </param>
        public void setBaseGate(int baseGate)
        {
            this.baseGate = baseGate;
            this.currGate = baseGate;
        }

        /// <summary>
        /// change gate
        /// </summary>
        /// <param name="newGate"> new gate for the athletes </param>
        public void changeGate(int newGate)
        {
            currGate = newGate;
        }

        /// <summary>
        /// Calculate Wind Compensation
        /// negative wind gives positive compensation and vice versa
        /// </summary>
        /// <param name="wind"> wind in meters per second; negative = back wind </param>
        /// <returns> compensation in meters (rounded in accuracy of half a meter) </returns>
        public float WindCompensation(float wind)
        {
            float comp = -wind * (hill.Kpoint - 36) / 20;
            return RoundedHalfMeter(comp);
        }

        /// <summary>
        /// calculate Gate Compensation
        /// </summary>
        /// <returns> compensation in meters; rounded in accuracy of half a meter </returns>
        public float gateCompensation()
        {
            float comp = ((float)(baseGate - currGate));
            comp *= hill.gateStep * hill.gateComp;
            return RoundedHalfMeter(comp);
            
        }

        /// <summary>
        /// Rounds the meter count in accuracy of half a meter
        /// </summary>
        /// <param name="m"> the meter count </param>
        /// <returns> the rounded value </returns>
        float RoundedHalfMeter(float m)
        {
            decimal dcomp = (decimal)m;
            dcomp = Decimal.Round(2 * dcomp) / 2;
            return (float)dcomp;
        }

    }
}
