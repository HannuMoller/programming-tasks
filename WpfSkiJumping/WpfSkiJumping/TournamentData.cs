using System.Collections.Generic;

namespace WpfSkiJumping
{
    public class TournamentData
    {
        private List<Athlete> _athletes;
        private Hill _hill;
        private int _currAthlete;
        private List<Result> _results;

        public TournamentData()
        {
            _currAthlete = -1;
            _results = new List<Result>();
        }

       
        public bool MoreAthletes()
        {
            return (_currAthlete < Athletes.Count-1);
        }

        public Athlete GetNextAthlete()
        {
            _currAthlete++;
            return GetCurrAthlete();
        }

        public Athlete GetCurrAthlete()
        {
            return _athletes[_currAthlete];
        }

        public List<Athlete> Athletes
        {
            get { return _athletes; }
            set { _athletes = value; }
        }

        public Hill Hill
        {
            get { return _hill; }
            set { _hill = value; }
        }

        /// <summary>
        /// Add result to List
        /// - manually keep the order
        /// </summary>
        /// <param name="result"></param>
        public void AddResult(Result result)
        {
            var i = 0;
            while (i < _results.Count && _results[i].Points >= result.Points) i++;
            _results.Insert(i, result);
          
            var prevOrder = 0;
            var currOrder = 0;
            var prevPoints = 999.9;

            _results.ForEach(result2 =>
            {
                currOrder++;
                if (result2.Points < prevPoints)
                {
                    result2.Order = currOrder;
                    prevOrder = currOrder;
                    prevPoints = result2.Points;
                }
                else
                {
                    result2.Order = prevOrder;
                }
            });
        }

        public List<Result> GetResults()
        {
            return _results;
        }
    }
}
