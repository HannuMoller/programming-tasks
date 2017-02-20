using System;

namespace SkiJumping
{
    /// <summary>
    /// Wind and Gate compensation methods
    /// </summary>
    class Compensation
    {
        public Hill hill; // reference to the hill

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="hill"> reference to the hill object </param>
        public Compensation(Hill hill)
        {
            this.hill = hill;
        }

        /// <summary>
        /// Calculate Wind Compensation
        /// negative wind gives positive compensation and vice versa
        /// </summary>
        /// <param name="wind"> wind in meters per second; negative = back wind </param>
        /// <returns> compensation in meters (rounded in accuracy of half a meter) </returns>
        public float WindCompensation(float wind)
        {
            float compensation = -wind * (hill.Kpoint - 36) / 20;
            return RoundedHalfMeter(compensation);
        }

        /// <summary>
        /// calculate Gate Compensation
        /// </summary>
        /// <returns> compensation in meters; rounded in accuracy of half a meter </returns>
        public float GateCompensation()
        {
            float comp = ((float)(hill.baseGate - hill.currGate));
            comp *= hill.gateStep * hill.gateCompensationPerMeter;
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
