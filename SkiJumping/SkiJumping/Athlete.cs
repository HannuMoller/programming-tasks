
namespace SkiJumping
{
    /// <summary>
    /// Skijumper data
    /// </summary>
    class Athlete
    {
        public int number; // jumper's starting number (jump order)
        public string name; // jumper's name
        public string country; // country the jumper represents
        public float jump; // length in meters
        public float points; // total points

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="number"> starting number </param>
        /// <param name="name"> jumper's name </param>
        /// <param name="country"> jumper's country </param>
        public Athlete(int number, string name, string country)
        {
            this.number = number;
            this.name = name;
            this.country = country;
            this.jump = 0.0f;
            this.points = 0.0f;
        }

        /// <summary>
        /// set jumper's jump length
        /// </summary>
        /// <param name="jump"> jump length in meters </param>
        public void SetJump(float jump)
        {
            this.jump = jump;
        }

        /// <summary>
        /// set jumper's total points
        /// </summary>
        /// <param name="points"> points </param>
        public void SetPoints(float points)
        {
            this.points = points;
        }

        /// <summary>
        /// one-line presentation of jumper data
        /// </summary>
        /// <returns> jumper data </returns>
        public override string ToString()
        {
            return string.Format("{0}:{1} ({2}), {3:F1}m, {4:F1}", number, name, country, jump, points);
        }

    }
}
