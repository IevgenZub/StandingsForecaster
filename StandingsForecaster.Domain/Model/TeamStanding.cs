namespace StandingsForecaster.Domain.Model
{
    public class TeamStanding
    {
        public const int WinPoints = 2;
        public const int LossPoints = 1;

        public Team Team { get; private set; }
        public int ScoreBalance { get; private set; }
        public int Games { get; private set; }
        public int Points { get; private set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; } 

        public TeamStanding(Team team)
        {
            Team = team;
        }

        public void Update(Game game)
        {
            var isGameFinished = game.IsFinished;

            if (isGameFinished)
            {
                var isHomeWin = game.HomeScore > game.VisitorScore;

                Wins = isHomeWin ? ++Wins : Wins;
                Losses = isHomeWin ? Losses : ++Losses;
                Points = isHomeWin ? Points + WinPoints : Points + LossPoints;
                ScoreBalance = ScoreBalance + game.HomeScore - game.VisitorScore;
            }
        }
    }
}