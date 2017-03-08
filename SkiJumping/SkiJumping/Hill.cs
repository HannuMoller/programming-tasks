
namespace SkiJumping
{
    /// <summary>
    /// Hill data
    /// </summary>
    class Hill
    {
        private string _name; // name of the hill
        private int _Kpoint; // K-point, in meters
        private int _lowestGate;
        private int _highestGate;
        private int _baseGate; // gate number in the beginning of the competition
        private int _currGate; // current gate number

        private float _gateStep; // meters (gap between successive gates)
        private float _gateCompensationPerMeter; // meters per meter
       
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name"> hill name </param>
        /// <param name="Kpoint"> K-point, in meters </param>
        /// <param name="lowestGate"> lowest gate </param>
        /// <param name="highestGate"> highest gate </param>
        /// <param name="gateStep"> gap between successive gates </param>
        /// <param name="gateCompensation"> compensation of gate change </param>
        public Hill(string name, int lowestGate, int highestGate, int Kpoint, float gateStep, float gateCompensationPerMeter)
        {
            Name = name;
            this.Kpoint = Kpoint;
            LowestGate = lowestGate;
            HighestGate = highestGate;
            GateStep = gateStep;
            GateCompensationPerMeter = gateCompensationPerMeter;

            BaseGate = lowestGate;
            CurrGate = lowestGate;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Kpoint
        {
            get { return _Kpoint; }
            set { _Kpoint = value; }
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

        public float GateCompensationPerMeter
        {
            get { return _gateCompensationPerMeter; }
            set { _gateCompensationPerMeter = value; }
        }

        
        /// <summary>
        /// change gate
        /// </summary>
        /// <param name="newGate"> new gate for the athletes </param>
        public void ChangeGate(int newGate)
        {
            CurrGate = newGate;
        }



        /// <summary>
        /// get total points (excluding points from judges)
        /// </summary>
        /// <param name="length"> jump length </param>
        /// <param name="windCompensation"> calculated wind compensation </param>
        /// <param name="gateCompensation"> calculated gate compensation </param>
        /// <returns> total points (without points from judges) </returns>
        public float GetPoints(float length, float windCompensation, float gateCompensation)
        {
            return GetKpointPoints() + GetPointsPerMeter() * (length + windCompensation + gateCompensation - Kpoint);
        }

        /// <summary>
        /// points per meter factor for jump length (based on FIS documents)
        /// depends on hill's K-point
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
        /// the base points for hill's K-point
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
