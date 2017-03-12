using System;

namespace WpfSkiJumping
{
    /// <summary>
    /// Wind and Gate compensation methods
    /// </summary>
    class Compensation
    {
        private Hill _hill; // reference to the hill

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hill"> reference to the hill object </param>
        public Compensation(Hill hill)
        {
            _hill = hill;
        }
        
        /// <summary>
        /// Calculate Wind Compensation
        /// negative wind gives positive compensation and vice versa
        /// </summary>
        /// <param name="wind"> wind in meters per second; positive = tail wind </param>
        /// <returns> compensation in meters (rounded in accuracy of half a meter) or in points </returns>
        public float WindCompensation(float wind)
        {
            float compensation;
            switch (_hill.FactorType)
            {
                case Hill.FactorMeters:
                    compensation = wind * (_hill.Kpoint - 36) / 20;
                    compensation = RoundedHalfMeter(compensation);
                    break;
                case Hill.FactorPoints:
                    if (wind < 0)
                    {
                        compensation = wind * _hill.WindFactorTail;
                    }
                    else
                    {
                        compensation = wind * _hill.WindFactorHead;
                    }
                    break;
                default:
                    // should never come here...
                    compensation = 0.0f;
                    break;
            }

            return compensation;
        }

        /// <summary>
        /// Calculate Gate Compensation
        /// </summary>
        /// <returns> compensation in meters (rounded in accuracy of half a meter) or in points </returns>
        public float GateCompensation()
        {
            float compensation = ((float)(_hill.BaseGate - _hill.CurrGate));
            compensation *=_hill.GateStep * _hill.GateFactor;
            if (_hill.FactorType == Hill.FactorMeters)
            {
                compensation = RoundedHalfMeter(compensation);
            }
            return compensation;
            
        }

        /// <summary>
        /// Rounds the meter count in accuracy of half a meter
        /// </summary>
        /// <param name="m"> the meter count </param>
        /// <returns> the rounded value </returns>
        private float RoundedHalfMeter(float m)
        {
            var dcomp = (decimal)m;
            dcomp = Decimal.Round(2 * dcomp) / 2;
            return (float)dcomp;
        }

    }
}
