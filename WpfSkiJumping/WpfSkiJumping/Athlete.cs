namespace WpfSkiJumping
{
    /// <summary>
    /// Skijumper data
    /// </summary>
    public class Athlete
    {
        private int _number; // jumper's starting number (jump order)
        private string _name; // jumper's name
        private string _country; // country the jumper represents

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

        
        /// <summary>
        /// one-line presentation of jumper data
        /// </summary>
        /// <returns> jumper data </returns>
        public override string ToString()
        {
            return $"{Number}: {Name} ({Country})";
        }

    }
}
