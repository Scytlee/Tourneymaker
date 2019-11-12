using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMLibrary.Models;

namespace TMLibrary.Helpers
{
    // TODO Just refactor this whole mess
    public static class TextConnectionHelper
    {
        public static string FullFilePath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{ fileName }";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            // [Id],[Nickname],[FirstName],[LastName],[DiscordTag],[EmailAddress]

            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel person = new PersonModel
                {
                    Id = int.Parse(cols[0]),
                    Nickname = cols[1],
                    FirstName = cols[2],
                    LastName = cols[3],
                    DiscordTag = cols[4],
                    EmailAddress = cols[5]
                };

                output.Add(person);
            }

            return output;
        }

        public static List<EntryModel> ConvertToEntryModels(this List<string> lines)
        {
            // [Id],[EntryName],(EntryMembers)[Id|Id|Id]

            List<EntryModel> output = new List<EntryModel>();

            List<PersonModel> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                EntryModel entry = new EntryModel
                {
                    Id = int.Parse(cols[0]),
                    EntryName = cols[1]
                };

                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    entry.EntryMembers.Add(people.First(x => x.Id == int.Parse(id)));
                }

                output.Add(entry);
            }

            return output;
        }

        public static List<TournamentModel> ConvertToTournamentModels(this List<string> lines)
        {
            // [Id],[TournamentName],(TournamentEntries)[Id|Id|Id],(Rounds)[Id^Id^Id|Id^Id^Id|Id^Id^Id],[Active],[CurrentRound]

            List<TournamentModel> output = new List<TournamentModel>();

            List<EntryModel> entries = GlobalConfig.EntriesFile.FullFilePath().LoadFile().ConvertToEntryModels();
            List<MatchupModel> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchupModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TournamentModel tournament = new TournamentModel
                {
                    Id = int.Parse(cols[0]),
                    TournamentName = cols[1],
                    Active = int.Parse(cols[4]),
                    CurrentRound = int.Parse(cols[5])
                };

                string[] entryIds = cols[2].Split('|');

                foreach (string id in entryIds)
                {
                    tournament.TournamentEntries.Add(entries.First(x => x.Id == int.Parse(id)));
                }

                // Capture Rounds information
                string[] rounds = cols[3].Split('|');

                foreach (string round in rounds)
                {
                    string[] matchupIds = round.Split('^');
                    List<MatchupModel> ms = new List<MatchupModel>();

                    foreach (string matchupId in matchupIds)
                    {
                        ms.Add(matchups.First(x => x.Id == int.Parse(matchupId)));
                    }

                    tournament.Rounds.Add(ms);
                }

                output.Add(tournament);
            }

            return output;
        }

        public static void SaveAllToPersonModelsFile(this List<PersonModel> people)
        {
            // [Id],[Nickname],[FirstName],[LastName],[DiscordTag],[EmailAddress]

            List<string> lines = new List<string>();

            foreach (PersonModel person in people)
            {
                lines.Add($"{ person.Id },{ person.Nickname },{ person.FirstName },{ person.LastName },{ person.DiscordTag },{ person.EmailAddress }");
            }

            File.WriteAllLines(GlobalConfig.PeopleFile.FullFilePath(), lines);
        }

        public static void SaveAllToEntryModelsFile(this List<EntryModel> entries)
        {
            // [Id],[EntryName],(EntryMembers)[Id|Id|Id]

            List<string> lines = new List<string>();

            foreach (EntryModel entry in entries)
            {
                lines.Add($"{ entry.Id },{ entry.EntryName },{ ConvertPeopleListToString(entry.EntryMembers) }");
            }

            File.WriteAllLines(GlobalConfig.EntriesFile.FullFilePath(), lines);
        }

        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";

            foreach (PersonModel person in people)
            {
                output += $"{ person.Id }|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        public static void SaveRounds(this TournamentModel tournament)
        {
            // Loop through the rounds
            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                // Loop through the matchups
                foreach (MatchupModel matchup in round)
                {
                    matchup.SaveOneToMatchupModelsFile();
                }
            }
        }

        public static List<MatchupEntryModel> ConvertToMatchupEntryModels(this List<string> lines)
        {
            // [Id],(EntryCompeting)[Id],[Score],(ParentMatchup)[Id]

            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupEntryModel matchupEntry = new MatchupEntryModel
                {
                    Id = int.Parse(cols[0]),
                    EntryCompeting = LookupEntryById(cols[1]),
                    Score = double.Parse(cols[2]),
                    ParentMatchup = LookupMatchupById(cols[3])
                };

                output.Add(matchupEntry);
            }

            return output;
        }

        private static List<MatchupEntryModel> ConvertStringToMatchupEntryModels(string input)
        {
            string[] ids = input.Split('|');

            List<MatchupEntryModel> output = new List<MatchupEntryModel>();
            List<string> matchupEntries = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile();
            List<string> matchingMatchupEntries = new List<string>();

            foreach (string id in ids)
            {
                foreach (string matchupEntry in matchupEntries)
                {
                    string[] cols = matchupEntry.Split(',');

                    if (cols[0] == id)
                    {
                        matchingMatchupEntries.Add(matchupEntry);
                    }
                }
            }

            output = matchingMatchupEntries.ConvertToMatchupEntryModels();

            return output;
        }

        private static EntryModel LookupEntryById(string stringId)
        {
            if (stringId == "")
            {
                return null;
            }
            else
            {
                List<string> entries = GlobalConfig.EntriesFile.FullFilePath().LoadFile();

                foreach (string entry in entries)
                {
                    string[] cols = entry.Split(',');

                    if (cols[0] == stringId)
                    {
                        List<string> matchingEntries = new List<string>();
                        matchingEntries.Add(entry);
                        return matchingEntries.ConvertToEntryModels().First();
                    }
                }

                return null;
            }
        }

        private static MatchupModel LookupMatchupById(string stringId)
        {
            if (stringId == "")
            {
                return null;
            }
            else
            {
                List<string> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile();

                foreach (string matchup in matchups)
                {
                    string[] cols = matchup.Split(',');

                    if (cols[0] == stringId)
                    {
                        List<string> matchingMatchups = new List<string>();
                        matchingMatchups.Add(matchup);
                        return matchingMatchups.ConvertToMatchupModels().First();
                    }
                }

                return null;
            }
        }

        public static List<MatchupModel> ConvertToMatchupModels(this List<string> lines)
        {
            // [Id],(MatchupEntries)[Id|Id|Id],(Winner)[Id],[MatchupRound]

            List<MatchupModel> output = new List<MatchupModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                MatchupModel matchup = new MatchupModel
                {
                    Id = int.Parse(cols[0]),
                    MatchupEntries = ConvertStringToMatchupEntryModels(cols[1]),
                    Winner = LookupEntryById(cols[2]),
                    MatchupRound = int.Parse(cols[3])
                };

                output.Add(matchup);
            }

            return output;
        }

        public static void SaveOneToMatchupModelsFile(this MatchupModel matchup)
        {
            // [Id],(MatchupEntries)[Id|Id|Id],(Winner)[Id],[MatchupRound]

            List<MatchupModel> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchupModels();

            int currentId = 1;

            if (matchups.Count > 0)
            {
                currentId = matchups.OrderByDescending(x => x.Id).First().Id + 1;
            }

            matchup.Id = currentId;

            matchups.Add(matchup);

            foreach (MatchupEntryModel matchupEntry in matchup.MatchupEntries)
            {
                matchupEntry.SaveOneToMatchupEntryModelsFile();
            }

            // save to file
            List<string> lines = new List<string>();

            foreach (MatchupModel m in matchups)
            {
                string winner = "";
                if (m.Winner != null)
                {
                    winner = m.Winner.Id.ToString();
                }

                lines.Add($"{ m.Id },{ ConvertMatchupEntriesListToString(m.MatchupEntries) },{ winner },{ m.MatchupRound }");
            }

            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), lines);
        }

        public static void UpdateMatchupInFile(this MatchupModel updatedMatchup)
        {
            // [Id],(MatchupEntries)[Id|Id|Id],(Winner)[Id],[MatchupRound]

            List<MatchupModel> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchupModels();

            MatchupModel outdatedMatchup = matchups.First(x => x.Id == updatedMatchup.Id);

            matchups.Remove(outdatedMatchup);
            matchups.Add(updatedMatchup);

            foreach (MatchupEntryModel matchupEntry in updatedMatchup.MatchupEntries)
            {
                matchupEntry.UpdateEntryInFile();
            }

            // save to file
            List<string> lines = new List<string>();

            foreach (MatchupModel m in matchups)
            {
                string winner = "";
                if (m.Winner != null)
                {
                    winner = m.Winner.Id.ToString();
                }

                lines.Add($"{ m.Id },{ ConvertMatchupEntriesListToString(m.MatchupEntries) },{ winner },{ m.MatchupRound }");
            }

            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), lines);
        }

        public static void UpdateTournamentInFile(this TournamentModel updatedTournament)
        {
            // [Id],[TournamentName],(TournamentEntries)[Id|Id|Id],(Rounds)[Id^Id^Id|Id^Id^Id|Id^Id^Id],[Active],[CurrentRound]

            List<TournamentModel> tournaments = GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertToTournamentModels();

            TournamentModel outdatedTournament = tournaments.First(x => x.Id == updatedTournament.Id);

            tournaments.Remove(outdatedTournament);
            tournaments.Add(updatedTournament);

            // save to file
            List<string> lines = new List<string>();

            foreach (TournamentModel tournament in tournaments)
            {
                lines.Add($"{ tournament.Id },{ tournament.TournamentName },{ ConvertEntriesListToString(tournament.TournamentEntries) }," +
                          $"{ ConvertRoundsToString(tournament.Rounds) },{ tournament.Active },{ tournament.CurrentRound }");
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
        }

        public static void UpdateEntryInFile(this MatchupEntryModel updatedMatchupEntry)
        {
            // [Id],(EntryCompeting)[Id],[Score],(ParentMatchup)[Id]

            List<MatchupEntryModel> matchupEntries =
                GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();

            MatchupEntryModel outdatedMatchupEntry = matchupEntries.First(x => x.Id == updatedMatchupEntry.Id);

            matchupEntries.Remove(outdatedMatchupEntry);
            matchupEntries.Add(updatedMatchupEntry);

            List<string> lines = new List<string>();

            foreach (MatchupEntryModel me in matchupEntries)
            {
                string entryCompeting = "";
                if (me.EntryCompeting != null)
                {
                    entryCompeting = me.EntryCompeting.Id.ToString();
                }

                string parentMatchup = "";
                if (me.ParentMatchup != null)
                {
                    parentMatchup = me.ParentMatchup.Id.ToString();
                }

                lines.Add($"{ me.Id },{ entryCompeting },{ me.Score },{ parentMatchup }");
            }

            File.WriteAllLines(GlobalConfig.MatchupEntriesFile.FullFilePath(), lines);
        }

        public static void SaveOneToMatchupEntryModelsFile(this MatchupEntryModel matchupEntry)
        {
            // [Id],(EntryCompeting)[Id],[Score],(ParentMatchup)[Id]

            List<MatchupEntryModel> matchupEntries =
                GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();

            int currentId = 1;

            if (matchupEntries.Count > 0)
            {
                currentId = matchupEntries.OrderByDescending(x => x.Id).First().Id + 1;
            }

            matchupEntry.Id = currentId;

            matchupEntries.Add(matchupEntry);

            List<string> lines = new List<string>();

            foreach (MatchupEntryModel me in matchupEntries)
            {
                string entryCompeting = "";
                if (me.EntryCompeting != null)
                {
                    entryCompeting = me.EntryCompeting.Id.ToString();
                }

                string parentMatchup = "";
                if (me.ParentMatchup != null)
                {
                    parentMatchup = me.ParentMatchup.Id.ToString();
                }

                lines.Add($"{ me.Id },{ entryCompeting },{ me.Score },{ parentMatchup }");
            }

            File.WriteAllLines(GlobalConfig.MatchupEntriesFile.FullFilePath(), lines);
        }

        public static void SaveAllToTournamentModelsFile(this List<TournamentModel> tournaments)
        {
            // [Id],[TournamentName],(TournamentEntries)[Id|Id|Id],(Rounds)[Id^Id^Id|Id^Id^Id|Id^Id^Id],[Active],[CurrentRound]

            List<string> lines = new List<string>();

            foreach (TournamentModel tournament in tournaments)
            {
                lines.Add($"{ tournament.Id },{ tournament.TournamentName },{ ConvertEntriesListToString(tournament.TournamentEntries) }," +
                          $"{ ConvertRoundsToString(tournament.Rounds) },{ tournament.Active },{ tournament.CurrentRound }");
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
        }

        private static string ConvertRoundsToString(List<List<MatchupModel>> rounds)
        {
            string output = "";

            foreach (List<MatchupModel> round in rounds)
            {
                output += $"{ ConvertMatchupsListToString(round) }|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertMatchupsListToString(List<MatchupModel> matchups)
        {
            string output = "";

            foreach (MatchupModel matchup in matchups)
            {
                output += $"{ matchup.Id }^";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertMatchupEntriesListToString(List<MatchupEntryModel> matchupEntries)
        {
            string output = "";

            foreach (MatchupEntryModel matchupEntry in matchupEntries)
            {
                output += $"{ matchupEntry.Id }|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertEntriesListToString(List<EntryModel> entries)
        {
            string output = "";

            foreach (EntryModel entry in entries)
            {
                output += $"{ entry.Id }|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }
    }
}
