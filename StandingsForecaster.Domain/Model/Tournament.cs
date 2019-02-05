using System.Collections.Generic;
using System.Linq;

namespace StandingsForecaster.Domain.Model
{
    public class Tournament
    {
        public IEnumerable<Game> Games { get; private set; }
        public IEnumerable<Team> Teams { get; private set; }

        public Tournament(IEnumerable<Team> teams)
        {
            Teams = teams;

            var games = new List<Game>();
            foreach (var homeTeam in teams)
            {
                foreach (var visitorTeam in teams)
                {
                    if (homeTeam != visitorTeam && 
                        !games.Any(g => 
                            (g.HomeTeam == homeTeam && g.VisitorTeam == visitorTeam) ||
                            (g.HomeTeam == visitorTeam && g.VisitorTeam == homeTeam)))
                    {
                        games.Add(new Game (homeTeam, visitorTeam));
                    }
                }
            }

            Games = games;
        }

        public IEnumerable<TeamStanding> Standings
        {
            get
            {
                var standings = new List<TeamStanding>();

                foreach (var team in Teams)
                {
                    standings.Add(new TeamStanding(team));
                }

                foreach (var game in Games)
                {
                    var homeStanding = standings.First(s => s.Team == game.HomeTeam);
                    homeStanding.Update(game);

                    var inversedGame = game.InverseSides();
                    var visitorStanding = standings.First(s => s.Team == inversedGame.HomeTeam);
                    visitorStanding.Update(inversedGame);
                }

                return standings;
            }
        }

        public int TotalGames => Games.Count();

        public void SubmitGameResult(string homeTeam, string visitorTeam, int homeScore, int visitorScore)
        {
            var game = Games.First(g => 
                g.HomeTeam.Name == homeTeam && 
                g.VisitorTeam.Name == visitorTeam);

            game.Finish(homeScore, visitorScore);
        }
    }
}
