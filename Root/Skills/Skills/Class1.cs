using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Skills
{
    public static class Analyze
    {
        public static List<List<Player>> PotentialLineups(int maxSkillLevel, int playerCount, List<Player> players)
        {

            var possibleLineups = new List<List<Player>>();
            var potentialLineups = new List<List<Player>>();
            var playersPresent = players.Where(x => x.IsPresent).ToList();
            PowerSet(playersPresent, possibleLineups);
            foreach (var potentialLineup in possibleLineups)
            {
                if (potentialLineup.Sum(x => x.SkillLevel) <= maxSkillLevel && potentialLineup.Count() == playerCount)
                {
                    potentialLineups.Add(potentialLineup);
                }
            }
            return potentialLineups;
        }



        private static void PowerSet(List<Player> players, List<List<Player>> output)
        {
            // ToDo: validate args
            output.Add(new List<Player>());
            ExpandSet(players, 0, ref output);
        }

        private static void ExpandSet(List<Player> players, int pos, ref List<List<Player>> output)
        {
            if (pos == players.Count)
            {
                return;
            }

            List<Player> tmp;
            var item = players[pos];

            for (int i = 0, n = output.Count; i < n; i++)
            {
                tmp = new List<Player>();
                tmp.AddRange(output[i]);
                tmp.Add(item);
                output.Add(tmp);
            }

            ExpandSet(players, pos + 1, ref output);
        }

        public static Matches BestLineupAgainst(List<List<Player>> potentialHomeTeamLineups, List<List<Player>> potentialVisitingTeamLineups)
        {
            var potentialMatches = new Matches();
            foreach (var homeLineup in potentialHomeTeamLineups)
            {
                potentialMatches.AddRange(potentialVisitingTeamLineups.Select(visitingLineup => new Match(homeLineup, visitingLineup)));
            }

            foreach (var potentialMatch in potentialMatches.OrderByDescending(x => x.IsEqual))
            {
                var orderedHomeTeamLineup = potentialMatch.HomeTeamLineup.OrderBy(x => x.SkillLevel).ToList();
                var orderedVisitingTeamLineup = potentialMatch.VisitingTeamLineup.OrderBy(x => x.SkillLevel).ToList();

                for (var i = 0; i < orderedHomeTeamLineup.Count; i++)
                {
                    potentialMatch.PlayersVsPlayers.Add(new PlayerVsPlayer
                    {
                        HomeTeamPlayer = orderedHomeTeamLineup[i],
                        VisitingTeamPlayer = orderedVisitingTeamLineup[i],
                    });
                }
            }

            return potentialMatches;
        }
    }

    public class PlayerVsPlayer
    {
        public Player HomeTeamPlayer { get; set; }
        public Player VisitingTeamPlayer { get; set; }

        public decimal HomeTeamAdvantage
        {
            get { return HomeTeamPlayer.TodaySkillLevel - HomeTeamPlayer.SkillLevel; }
        }

        public string HomeTeamProjection
        {
            get
            {
                if (HomeTeamAdvantage > 0) return "Projected Win!";
                if (HomeTeamAdvantage < 0) return "Projected Loss!";
                return "Equal match";
            }
        }
        
        public override string ToString()
        {
            return string.Format("{0, -35}{1, -35}{2, 4} - {3,-2} {4}", 
                        HomeTeamPlayer.ToString(),
                        VisitingTeamPlayer.ToString(),
                        HomeTeamPlayer.NineBallPoints, 
                        VisitingTeamPlayer.NineBallPoints,
                        HomeTeamProjection
                        );

        }
    }

    public class Matches : List<Match>
    {
    }

    public class Match
    {
        public List<Player> HomeTeamLineup { get; set; }
        public List<Player> VisitingTeamLineup { get; set;}

        private List<PlayerVsPlayer> _playersVsPlayers = new List<PlayerVsPlayer>();

        public List<PlayerVsPlayer> PlayersVsPlayers 
        {
            get { return _playersVsPlayers; }
            set { _playersVsPlayers = value; }
        }
        public bool IsEqual
        {
            get { return HomeTeamLineup.Sum(x => x.SkillLevel) == VisitingTeamLineup.Sum(x => x.SkillLevel); }
        }

        public decimal HomeTeamAdvantage
        {
            get { return HomeTeamLineup.Sum(x => x.TodaySkillLevel) - HomeTeamLineup.Sum(y => y.SkillLevel); }
        }
        public decimal VisitingTeamAdvantage
        {
            get { return VisitingTeamLineup.Sum(x => x.TodaySkillLevel) - VisitingTeamLineup.Sum(y => y.SkillLevel); }
        }

        public Match(List<Player> homeTeamLineup, List<Player> visitingTeamLineup)
        {
            HomeTeamLineup = homeTeamLineup;
            VisitingTeamLineup = visitingTeamLineup;
        }
        public override string ToString()
        {
            return string.Format(" {0,2} {1:00.0}{2,30} {3:00.0}",
                    HomeTeamLineup.Sum(x => x.SkillLevel),
                    HomeTeamLineup.Sum(x => x.TodaySkillLevel),
                    VisitingTeamLineup.Sum(x => x.SkillLevel),
                    VisitingTeamLineup.Sum(x => x.TodaySkillLevel)
                    );

        }

    }
    public class Player
    {
        public bool HasPlayed { get; set; }
        public bool IsPresent { get; set; }
        public string Name { get; set; }
        public string PlayerNumber { get; set; }
        public int SkillLevel { get; set; }
        public int NineBallPoints { get{ return Get9BallPoints();} }
        public decimal TodaySkillLevel { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1, -2} {2:0.0} {3,5} {4}", this.HasPlayed ? "*" : " ", this.SkillLevel, this.TodaySkillLevel, this.PlayerNumber, this.Name);
        }

        private int Get9BallPoints()
        {
            if (SkillLevel == 1) return 14;
            else if (SkillLevel == 2) return 19;
            else if (SkillLevel == 3) return 25;
            else if (SkillLevel == 4) return 31;
            else if (SkillLevel == 5) return 38;
            else if (SkillLevel == 6) return 46;
            else if (SkillLevel == 7) return 55;
            else if (SkillLevel == 8) return 65;
            else if (SkillLevel == 9) return 75;
            return 0;
        }

    }
}
