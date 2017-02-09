using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skills;

namespace SkillsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var homeTeamPlayers = new List<Player>();
            homeTeamPlayers.Add(new Player { IsPresent = false, Name = "Hendricks, Lesly", PlayerNumber = "13913", SkillLevel = 4, TodaySkillLevel = 4.5m });
            homeTeamPlayers.Add(new Player { IsPresent = true, Name = "Maldonado, Michael", PlayerNumber = "13547", SkillLevel = 3, TodaySkillLevel = 3m });
            homeTeamPlayers.Add(new Player { IsPresent = true, Name = "Mason, Matt", PlayerNumber = "14841", SkillLevel = 4, TodaySkillLevel = 4m });
            homeTeamPlayers.Add(new Player { IsPresent = true, Name = "Janecek, Gary", PlayerNumber = "14862", SkillLevel = 5, TodaySkillLevel = 5.5m });
            homeTeamPlayers.Add(new Player { IsPresent = false, Name = "Hughes, Kyle", PlayerNumber = "13446", SkillLevel = 5, TodaySkillLevel = 6m });
            homeTeamPlayers.Add(new Player { IsPresent = true, Name = "Washington, Derek", PlayerNumber = "13551", SkillLevel = 7, TodaySkillLevel = 6.5m });
            homeTeamPlayers.Add(new Player { IsPresent = true, Name = "Parnell, John", PlayerNumber = "12511", SkillLevel = 4, TodaySkillLevel = 4m });
            homeTeamPlayers.Add(new Player { IsPresent = false, Name = "Hasan, Conner", PlayerNumber = "14342", SkillLevel = 8, TodaySkillLevel = 7m });

            var visitingTeamPlayers = new List<Player>();
            visitingTeamPlayers.Add(new Player { IsPresent = true, Name = "Salazar, George", PlayerNumber = "08689", SkillLevel = 5, TodaySkillLevel = 5m });
            visitingTeamPlayers.Add(new Player { IsPresent = true, Name = "Yates, Michelle", PlayerNumber = "11845", SkillLevel = 3, TodaySkillLevel = 3m });
            visitingTeamPlayers.Add(new Player { IsPresent = true, Name = "Martinez, Marc", PlayerNumber = "10950", SkillLevel = 4, TodaySkillLevel = 4m });
            visitingTeamPlayers.Add(new Player { IsPresent = true, Name = "Salazar, George", PlayerNumber = "08638", SkillLevel = 5, TodaySkillLevel = 5m });
            visitingTeamPlayers.Add(new Player { IsPresent = true, Name = "Abusaada, Firas", PlayerNumber = "10647", SkillLevel = 9, TodaySkillLevel = 9m });
            visitingTeamPlayers.Add(new Player { IsPresent = true, Name = "Saleh, Tarik", PlayerNumber = "15067", SkillLevel = 5, TodaySkillLevel = 5m });
            visitingTeamPlayers.Add(new Player { IsPresent = true, Name = "Slonaker, Scott", PlayerNumber = "14724", SkillLevel = 2, TodaySkillLevel = 2m });
            visitingTeamPlayers.Add(new Player { IsPresent = true, Name = "Welch, Lonnie", PlayerNumber = "13507", SkillLevel = 2, TodaySkillLevel = 2m });
            //var analyzeVisitingTeam = new Analyze(visitingTeamPlayers);
            var potentialVisitingTeamLineups = Analyze.PotentialLineups(23, 5, visitingTeamPlayers);
            //PrintPotentialLineups("<---- Visiting Team Potential Lineups  ---->", potentialVisitingTeamLineups);

            //var analyzeHomeTeam = new Analyze(homeTeamPlayers);
            var potentialHomeTeamLineups = Analyze.PotentialLineups(23, 5, homeTeamPlayers);
            //PrintPotentialLineups("<---- Visiting Team Potential Lineups  ---->", potentialVisitingTeamLineups);

            var bestHomeTeamLineups = Analyze.BestLineupAgainst(potentialHomeTeamLineups, potentialVisitingTeamLineups);

            foreach (var lineup in bestHomeTeamLineups.OrderByDescending(x => x.IsEqual))
            {
                lineup.PlayersVsPlayers.ForEach(x => Console.WriteLine(x.ToString()));
                Console.WriteLine(lineup.ToString());
                Console.WriteLine();
            }





            //var playedPlayer = homeTeamPlayers.FirstOrDefault(x => x.Name == "Washington, Derek");
            //playedPlayer.HasPlayed = true;
            //var availableLineups = potentialHomeTeamLineups.Where(lineup => lineup.Any(player => player == playedPlayer)).ToList();
            //PrintPotentialLineups("<---- After First Round  ---->", availableLineups);

            //playedPlayer = homeTeamPlayers.FirstOrDefault(x => x.Name == "Hughes, Kyle");
            //playedPlayer.HasPlayed = true;
            //availableLineups = availableLineups.Where(lineup => lineup.Any(player => player == playedPlayer)).ToList();
            //PrintPotentialLineups("<---- After Second Round  ---->", availableLineups);

            //playedPlayer = homeTeamPlayers.FirstOrDefault(x => x.Name == "Hendricks, Lesly");
            //playedPlayer.HasPlayed = true;
            //availableLineups = availableLineups.Where(lineup => lineup.Any(player => player == playedPlayer)).ToList();
            //PrintPotentialLineups("<---- After Third Round  ---->", availableLineups);

            //playedPlayer = homeTeamPlayers.FirstOrDefault(x => x.Name == "Parnell, John");
            //playedPlayer.HasPlayed = true;
            //availableLineups = availableLineups.Where(lineup => lineup.Any(player => player == playedPlayer)).ToList();
            //PrintPotentialLineups("<---- After Fourth Round  ---->", availableLineups);
        }

        private static void PrintPotentialLineups(string message, List<List<Player>> potentialLineups)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine("Total: {0}", potentialLineups.Count);
            var count = 0;
            foreach (var potentialLineup in potentialLineups)
            {
                Console.WriteLine("Lineup #{0}", ++count);
                foreach (var player in potentialLineup)
                {
                    Console.WriteLine(player.ToString());
                }
                Console.WriteLine(" {0, -2} {1:00.0} Total Skill Level", 
                    potentialLineup.Sum(x => x.SkillLevel),
                    potentialLineup.Sum(x => x.TodaySkillLevel)
                    );
                Console.WriteLine();
            }
        }
    }
}
