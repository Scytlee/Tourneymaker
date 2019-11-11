using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMLibrary.Models;

namespace TMLibrary
{
    public static class TournamentLogic
    {
        public static void CreateRounds(TournamentModel tournament)
        {
            // Randomize order of the entries list
            List<EntryModel> randomizedEntries = RandomizeEntriesOrder(tournament.TournamentEntries);

            // Determine amount of rounds and byes
            int roundAmount = GetRoundAmount(randomizedEntries.Count);
            int byeAmount = GetByeAmount(randomizedEntries.Count, roundAmount);

            // Create first round of matchups
            tournament.Rounds.Add(CreateFirstRound(randomizedEntries, byeAmount));

            // Create all other rounds
            CreateOtherRounds(tournament, roundAmount);
        }

        private static void CreateOtherRounds(TournamentModel tournament, int roundAmount)
        {
            if (roundAmount == 1)
            {
                return;
            }

            int round = 2;
            List<MatchupModel> previousRound = tournament.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currentMatchup = new MatchupModel();

            while (round <= roundAmount)
            {
                foreach (MatchupModel matchup in previousRound)
                {
                     currentMatchup.MatchupEntries.Add(new MatchupEntryModel { ParentMatchup = matchup });

                     if (currentMatchup.MatchupEntries.Count > 1)
                     {
                         currentMatchup.MatchupRound = round;
                         currentRound.Add(currentMatchup);
                         currentMatchup = new MatchupModel();
                     }
                }

                tournament.Rounds.Add(currentRound);
                previousRound = currentRound;

                currentRound = new List<MatchupModel>();
                round++;
            }
        }

        private static List<MatchupModel> CreateFirstRound(List<EntryModel> entries, int byeAmount)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel currentMatchup = new MatchupModel();

            foreach (EntryModel entry in entries)
            {
                currentMatchup.MatchupEntries.Add(new MatchupEntryModel { EntryCompeting = entry });

                if (byeAmount > 0 || currentMatchup.MatchupEntries.Count > 1)
                {
                    currentMatchup.MatchupRound = 1;
                    output.Add(currentMatchup);
                    currentMatchup = new MatchupModel();

                    if (byeAmount > 0)
                    {
                        byeAmount--;
                    }
                }
            }

            return output;
        }

        private static int GetByeAmount(int entryAmount, int roundAmount)
        {
            int totalEntries = 1;

            for (int i = 0; i < roundAmount; i++)
            {
                totalEntries *= 2;
            }

            int output = totalEntries - entryAmount;

            return output;
        }

        private static int GetRoundAmount(int entryAmount)
        {
            double log = Math.Log(entryAmount, 2);
            int output = (int) Math.Ceiling(log);

            return output;
        }

        public static List<EntryModel> RandomizeEntriesOrder(List<EntryModel> entries)
        {
            return entries.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public static void UpdateTournament(TournamentModel tournament, MatchupModel matchup, string scoreOne, string scoreTwo)
        {
            UpdateMatchupScores(matchup, scoreOne, scoreTwo);

            DetermineWinner(matchup);

            AdvanceWinner(tournament, matchup);

            GlobalConfig.Connection.UpdateMatchup(matchup);
        }

        private static void AdvanceWinner(TournamentModel tournament, MatchupModel matchup)
        {
            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                foreach (MatchupModel roundMatchup in round)
                {
                    foreach (MatchupEntryModel matchupEntry in roundMatchup.MatchupEntries)
                    {
                        if (matchupEntry.ParentMatchup?.Id == matchup.Id)
                        {
                            matchupEntry.EntryCompeting = matchup.Winner;
                            GlobalConfig.Connection.UpdateMatchup(roundMatchup);
                        }
                    }
                } 
            }
        }

        private static void UpdateMatchupScores(MatchupModel matchup, string scoreOne, string scoreTwo)
        {
            matchup.MatchupEntries[0].Score = double.Parse(scoreOne);
            matchup.MatchupEntries[1].Score = double.Parse(scoreTwo);
        }

        private static void DetermineWinner(MatchupModel matchup)
        {
            string greaterWins = ConfigurationManager.AppSettings["greaterScoreWins"];

            if (matchup.MatchupEntries.Count == 1)
            {
                matchup.Winner = matchup.MatchupEntries[0].EntryCompeting;
            }
            else if (matchup.MatchupEntries.Count == 2)
            {
                if (greaterWins == "1")
                {
                    matchup.Winner = matchup.MatchupEntries[0].Score > matchup.MatchupEntries[1].Score ? matchup.MatchupEntries[0].EntryCompeting : matchup.MatchupEntries[1].EntryCompeting;
                }
                else if (greaterWins == "0")
                {
                    matchup.Winner = matchup.MatchupEntries[0].Score < matchup.MatchupEntries[1].Score ? matchup.MatchupEntries[0].EntryCompeting : matchup.MatchupEntries[1].EntryCompeting;
                }
                else
                {
                    throw new Exception("Invalid value of option \"greaterWins\".");
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public static void HandleByeMatchups(TournamentModel tournament)
        {
            foreach (MatchupModel matchup in tournament.Rounds[0])
            {
                if (matchup.MatchupEntries.Count == 1)
                {
                    DetermineWinner(matchup);

                    AdvanceWinner(tournament, matchup);

                    GlobalConfig.Connection.UpdateMatchup(matchup);
                }
            }
        }
    }
}
