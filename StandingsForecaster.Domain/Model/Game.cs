using System;

namespace StandingsForecaster.Domain.Model
{
    public class Game
    {
        public Team HomeTeam { get; private set; }
        public Team VisitorTeam { get; private set; }
        public int HomeScore { get; private set; }
        public int VisitorScore { get; private set; }
        public bool IsFinished { get; private set; }

        public Game(Team homeTeam, Team visitorTeam)
        {
            HomeTeam = homeTeam;
            VisitorTeam = visitorTeam;
        }

        public void Finish(int homeScore, int visitorScore)
        {
            if (homeScore == visitorScore)
            {
                throw new ArgumentException("No draw game supported. Only basketball rules used as for now");
            }

            HomeScore = homeScore;
            VisitorScore = visitorScore;
            IsFinished = true;
        }

        public Game InverseSides()
        {
            var temp = VisitorTeam;
            var tempScore = VisitorScore;

            VisitorTeam = HomeTeam;
            VisitorScore = HomeScore;
            HomeTeam = temp;
            HomeScore = tempScore;

            return this;
        }
    }
}
