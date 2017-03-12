using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSkiJumping
{
    /// <summary>
    /// Ski jumping result for an athlete
    /// </summary>
    public class Result
    {
        private int _order;
        private Athlete _athlete;
        private float _jumpLength;
        private float _points;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="athlete"></param>
        /// <param name="jumpLength"></param>
        /// <param name="points"></param>
        public Result(Athlete athlete, float jumpLength, float points)
        {
            _athlete = athlete;
            _jumpLength = jumpLength;
            _points = points;
        }

        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public int Number
        {
            get { return _athlete.Number;  }
        }

        public string Name
        {
            get { return $"{_athlete.Name} ({_athlete.Country})"; }
        }

        public string JumpLength
        {
            get { return $"{_jumpLength:F1}"; }
        }

        public float Points
        {
            get { return _points; }
        }

        public string Pointss
        {
            get { return $"{_points:F1}"; }
        }

    }
}
