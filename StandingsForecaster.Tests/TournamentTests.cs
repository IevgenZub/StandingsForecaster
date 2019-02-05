using StandingsForecaster.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StandingsForecaster.Tests
{
    public class TournamentTests
    {
        private const string Team_EPAM = "EPAM";
        private const string Team_Ciklum = "Ciklum";
        private const string Team_Torsus = "Torsus";
        private const string Team_FreshNRebel = "FreshNRebel";

        private Tournament _sut;

        public TournamentTests()
        {
            _sut = new Tournament( 
                new List<Team>()
                {
                    new Team(Team_EPAM),
                    new Team(Team_Ciklum),
                    new Team(Team_Torsus),
                    new Team(Team_FreshNRebel)
                });
        }

        [Fact]
        public void Tournament_InitializesGames()
        {
            Assert.Equal(6, _sut.Games.Count());
        }

        [Fact]
        public void Tournament_InitializesStandings()
        {
            Assert.Equal(4, _sut.Standings.Count());
        }

        [Fact]
        public void Tournament_CalculateWins()
        {
            const int winScore = 102;
            const int lossScore = 41;

            _sut.SubmitGameResult(Team_EPAM, Team_Ciklum, winScore, lossScore);

            Assert.Equal(1, _sut.Standings.First(s => s.Team.Name == Team_EPAM).Wins);
        }

        [Fact]
        public void Tournament_CalculateLosses()
        {
            const int winScore = 102;
            const int lossScore = 41;

            _sut.SubmitGameResult(Team_EPAM, Team_Ciklum, winScore, lossScore);

            Assert.Equal(1, _sut.Standings.First(s => s.Team.Name == Team_Ciklum).Losses);
        }

        [Fact]
        public void Tournament_CalculateScoreBalance()
        {
            const int winScore = 102;
            const int lossScore = 41;

            _sut.SubmitGameResult(Team_EPAM, Team_Ciklum, winScore, lossScore);

            Assert.Equal(winScore - lossScore, _sut.Standings.First(s => s.Team.Name == Team_EPAM).ScoreBalance);
        }

    }
}
