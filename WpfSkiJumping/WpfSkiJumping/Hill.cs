using System.Collections;

namespace WpfSkiJumping
{
    /// <summary>
    /// Hill data
    /// </summary>
    public class Hill
    {
        public const char FactorMeters = 'M';
        public const char FactorPoints = 'P';

        private string _name; // name of the hill
        private string _city; // name of the city where the hill is
        private int _Kpoint; // K-point, in meters
        private int _HSpoint; // HS-point, in meters
        private float _longestJump; // longest jump, in meters, accuracy half meter
        private int _lowestGate;
        private int _highestGate;
        private int _baseGate; // gate number in the beginning of the competition
        private int _currGate; // current gate number

        private float _gateStep; // meters (gap between successive gates)
        private float _gateFactor; // meters per meter or points per meter (depends on factorType)
        private float _windFactorHead; // points per meters/second (is factorType = 'P')
        private float _windFactorTail; // points per meters/second (is factorType = 'P')
        private char _factorType; // 'M' -> meters, 'P' -> points

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> hill name </param>
        /// <param name="city"></param>
        /// <param name="Kpoint"> K-point, in meters </param>
        /// <param name="HSpoint"></param>
        /// <param name="longestJump"></param>
        /// <param name="lowestGate"> lowest gate </param>
        /// <param name="highestGate"> highest gate </param>
        /// <param name="gateStep"> gap between successive gates </param>
        /// <param name="gateFactor"></param>
        /// <param name="windFactor"> compensation of gate change </param>
        /// <param name="factorType"></param>
        public Hill(string name, string city, int Kpoint, int HSpoint, float longestJump, int lowestGate, int highestGate, float gateStep, float gateFactor, float windFactorHead, float windFactorTail, char factorType)
        {
            Name = name;
            City = city;
            this.Kpoint = Kpoint;
            this.HSpoint = HSpoint;
            LongestJump = longestJump;
            LowestGate = lowestGate;
            HighestGate = highestGate;
            GateStep = gateStep;
            GateFactor = gateFactor;
            WindFactorHead = windFactorHead;
            WindFactorTail = windFactorTail;
            FactorType = factorType;

            BaseGate = lowestGate;
            CurrGate = lowestGate;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public int Kpoint
        {
            get { return _Kpoint; }
            set { _Kpoint = value; }
        }

        public int HSpoint
        {
            get { return _HSpoint; }
            set { _HSpoint = value; }
        }

        public float LongestJump
        {
            get { return _longestJump; }
            set { _longestJump = value; }
        }

        public int LowestGate
        {
            get { return _lowestGate; }
            set { _lowestGate = value; }
        }

        public int HighestGate
        {
            get { return _highestGate; }
            set { _highestGate = value; }
        }

        public int BaseGate
        {
            get { return _baseGate; }
            set { _baseGate = value; _currGate = value; }
        }

        public int CurrGate
        {
            get { return _currGate; }
            set { _currGate = value; }
        }

        public float GateStep
        {
            get { return _gateStep; }
            set { _gateStep = value; }
        }

        public float GateFactor
        {
            get { return _gateFactor; }
            set { _gateFactor = value; }
        }

        public float WindFactorHead
        {
            get { return _windFactorHead; }
            set { _windFactorHead = value; }
        }

        public float WindFactorTail
        {
            get { return _windFactorTail; }
            set { _windFactorTail = value; }
        }

        public char FactorType
        {
            get { return _factorType; }
            set { _factorType = value; }
        }

        /// <summary>
        /// Change gate
        /// </summary>
        /// <param name="newGate"> new gate for the athletes </param>
        public void ChangeGate(int newGate)
        {
            CurrGate = newGate;
        }



        /// <summary>
        /// Get total points (excluding points from judges)
        /// </summary>
        /// <param name="length"> jump length </param>
        /// <param name="windCompensation"> calculated wind compensation (in meters) </param>
        /// <param name="gateCompensation"> calculated gate compensation (in meters) </param>
        /// <returns> total points (without points from judges) </returns>
        public float GetPoints(float length, float windCompensation, float gateCompensation)
        {
            switch (FactorType)
            {
                case FactorMeters:
                    return GetKpointPoints() +
                           GetPointsPerMeter() * (length + windCompensation + gateCompensation - Kpoint);
                case FactorPoints:
                    return GetKpointPoints() + GetPointsPerMeter() * (length - Kpoint) + windCompensation +
                           gateCompensation;
            }

            return 0.0f;
        }

        /// <summary>
        /// Points per meter factor for jump length (based on FIS documents)
        /// - depends on hill's K-point
        /// </summary>
        /// <returns> factor (points per meter) </returns>
        public float GetPointsPerMeter()
        {
            if (Kpoint < 25)
                return 4.8f;
            else if (Kpoint < 30)
                return 4.4f;
            else if (Kpoint < 35)
                return 4.0f;
            else if (Kpoint < 40)
                return 3.6f;
            else if (Kpoint < 50)
                return 3.2f;
            else if (Kpoint < 60)
                return 2.8f;
            else if (Kpoint < 70)
                return 2.4f;
            else if (Kpoint < 80)
                return 2.2f;
            else if (Kpoint < 100)
                return 2.0f;
            else if (Kpoint < 170)
                return 1.8f;
            else
                return 1.2f; 
        }

        /// <summary>
        /// The base points for hill's K-point
        /// (unfortunately currently only large and flying hill information is found on FIS documents)
        /// </summary>
        /// <returns> base points </returns>
        public float GetKpointPoints()
        {
            if (Kpoint < 50)
                return 60.0f; // ??? small hill
            else if (Kpoint < 85)
                return 60.0f; // ??? medium hill
            else if (Kpoint < 110)
                return 60.0f; // ??? normal hill
            else if (Kpoint < 185)
                return 60.0f; // large hill
            else
                return 120.0f; // flying hill
        }

    }
}
