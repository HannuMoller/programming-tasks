
namespace SkiJumping
{
    /// <summary>
    /// Skijumper data
    /// </summary>
    class Athlete
    {
        private int _number; // jumper's starting number (jump order)
        private string _name; // jumper's name
        private string _country; // country the jumper represents
        private float _jump; // length in meters
        private float _points; // total points

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="number"> starting number </param>
        /// <param name="name"> jumper's name </param>
        /// <param name="country"> jumper's country </param>
        public Athlete(int number, string name, string country)
        {
            Number = number;
            Name = name;
            Country = country;
            Jump = 0.0f;
            Points = 0.0f;
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public float Jump
        {
            get { return _jump; }
            set { _jump = value; }
        }

        public float Points
        {
            get { return _points; }
            set { _points = value; }
        }

        
        /// <summary>
        /// one-line presentation of jumper data
        /// </summary>
        /// <returns> jumper data </returns>
        public override string ToString()
        {
            return string.Format("{0}:{1} ({2}), {3:F1}m, {4:F1}", Number, Name, Country, Jump, Points);
        }

    }
}
