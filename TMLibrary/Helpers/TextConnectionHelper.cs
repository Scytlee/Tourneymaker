using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMLibrary.Models;

namespace TMLibrary.Helpers
{
    // TODO Just refactor this whole mess
    public static class TextConnectionHelper
    {
        private static List<EntryModel> _entries;
        private static bool _entriesOutdated = true;

        public static List<EntryModel> Entries
        {
            get
            {
                if (_entriesOutdated)
                {
                    _entries = GlobalConfig.EntriesFile.FullFilePath().LoadFile().ConvertToEntryModels();
                    _entriesOutdated = false;
                }

                return _entries;
            }
        }

        private static List<PersonModel> _people;
        private static bool _peopleOutdated = true;

        public static List<PersonModel> People
        {
            get
            {
                if (_peopleOutdated)
                {
                    _people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
                    _peopleOutdated = false;
                }

                return _people;
            }
        }

        private static List<TournamentModel> _tournaments;
        private static bool _tournamentsOutdated = true;

        public static List<TournamentModel> Tournaments
        {
            get
            {
                if (_tournamentsOutdated)
                {
                    _tournaments = GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertToTournamentModels();
                    _tournamentsOutdated = false;
                }

                return _tournaments;
            }
        }

        private static List<MatchupModel> _matchups;
        private static bool _matchupsOutdated = true;

        public static List<MatchupModel> Matchups
        {
            get
            {
                if (_matchupsOutdated)
                {
                    _matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchupModels();
                    _matchupsOutdated = false;
                }

                return _matchups;
            }
        }

        private static List<MatchupEntryModel> _matchupEntries;
        private static bool _matchupEntriesOutdated = true;

        public static List<MatchupEntryModel> MatchupEntries
        {
            get
            {
                if (_matchupEntriesOutdated)
                {
                    _matchupEntries = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();
                    _matchupEntriesOutdated = false;
                }

                return _matchupEntries;
            }
        }

        public static string FullFilePath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{ fileName }";
        }

        public static List<string> LoadFile(this string file)
        {
            return !File.Exists(file) ? new List<string>() : File.ReadAllLines(file).ToList();
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> peopleSerialized)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string person in peopleSerialized)
            {
                output.Add(DeserializePerson(person));
            }

            return output;
        }

        public static PersonModel DeserializePerson(string personSerialized)
        {
            // [Id],[Nickname],[FirstName],[LastName],[DiscordTag],[EmailAddress]

            PersonModel output;

            string[] personData = personSerialized.Split(',');

            output = new PersonModel
            {
                Id = int.Parse(personData[0]),
                Nickname = personData[1],
                FirstName = personData[2],
                LastName = personData[3],
                DiscordTag = personData[4],
                EmailAddress = personData[5]
            };

            return output;
        }

        public static List<EntryModel> ConvertToEntryModels(this List<string> entriesSerialized)
        {
            List<EntryModel> output = new List<EntryModel>();

            List<PersonModel> allPeople = People;

            foreach (string entry in entriesSerialized)
            {
                output.Add(DeserializeEntry(entry, allPeople));
            }

            return output;
        }

        public static EntryModel DeserializeEntry(string entrySerialized)
        {
            return DeserializeEntry(entrySerialized, People);
        }

        public static EntryModel DeserializeEntry(string entrySerialized, List<PersonModel> allPeople)
        {
            // [Id],[EntryName],(EntryMembers)[Id|Id|Id]

            EntryModel output;

            string[] entryData = entrySerialized.Split(',');

            output = new EntryModel
            {
                Id = int.Parse(entryData[0]),
                EntryName = entryData[1]
            };

            string[] personIds = entryData[2].Split('|');

            foreach (string id in personIds)
            {
                output.EntryMembers.Add(allPeople.First(x => x.Id == int.Parse(id)));
            }

            return output;
        }

        public static List<TournamentModel> ConvertToTournamentModels(this List<string> tournamentsSerialized)
        {
            List<TournamentModel> output = new List<TournamentModel>();

            List<EntryModel> allEntries = Entries;
            List<MatchupModel> allMatchups = Matchups;

            foreach (string tournament in tournamentsSerialized)
            {
                output.Add(DeserializeTournament(tournament, allEntries, allMatchups));
            }

            return output;
        }

        public static TournamentModel DeserializeTournament(string tournamentSerialized)
        {
            return DeserializeTournament(tournamentSerialized, Entries, Matchups);
        }

        public static TournamentModel DeserializeTournament(string tournamentSerialized, List<EntryModel> allEntries, List<MatchupModel> allMatchups)
        {
            // [Id],[TournamentName],(TournamentEntries)[Id|Id|Id],(Rounds)[Id^Id^Id|Id^Id^Id|Id^Id^Id],[Active],[CurrentRound],[Status]

            TournamentModel output;

            string[] tournamentData = tournamentSerialized.Split(',');

            output = new TournamentModel
            {
                Id = int.Parse(tournamentData[0]),
                TournamentName = tournamentData[1],
                Active = int.Parse(tournamentData[4]),
                CurrentRound = int.Parse(tournamentData[5]),
                Status = (TournamentStatus)Enum.Parse(typeof(TournamentStatus), tournamentData[6])
            };

            string[] entryIds = tournamentData[2].Split('|');

            foreach (string id in entryIds)
            {
                output.TournamentEntries.Add(allEntries.First(x => x.Id == int.Parse(id)));
            }

            // Capture Rounds information
            string[] rounds = tournamentData[3].Split('|');

            foreach (string round in rounds)
            {
                string[] matchupIds = round.Split('^');
                List<MatchupModel> ms = new List<MatchupModel>();

                foreach (string matchupId in matchupIds)
                {
                    ms.Add(allMatchups.First(x => x.Id == int.Parse(matchupId)));
                }

                output.Rounds.Add(ms);
            }

            return output;
        }

        public static List<TournamentPreviewModel> ConvertToTournamentPreviewModels(this List<string> lines)
        {
            // [Id],[TournamentName],(TournamentEntries)[Id|Id|Id],(Rounds)[Id^Id^Id|Id^Id^Id|Id^Id^Id],[Active],[CurrentRound],[Status]

            List<TournamentPreviewModel> output = new List<TournamentPreviewModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TournamentPreviewModel tournamentPreview = new TournamentPreviewModel
                {
                    Id = int.Parse(cols[0]),
                    TournamentName = cols[1],
                    Status = (TournamentStatus)Enum.Parse(typeof(TournamentStatus), cols[6])
                };

                output.Add(tournamentPreview);
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
            _peopleOutdated = true;
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
            _entriesOutdated = true;
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

        public static List<MatchupEntryModel> ConvertToMatchupEntryModels(this List<string> matchupEntriesSerialized)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            foreach (string matchupEntry in matchupEntriesSerialized)
            {
                output.Add(DeserializeMatchupEntry(matchupEntry));
            }

            return output;
        }

        public static MatchupEntryModel DeserializeMatchupEntry(string matchupEntrySerialized)
        {
            // [Id],(EntryCompeting)[Id],[Score],(ParentMatchup)[Id]

            MatchupEntryModel output;

            string[] matchupEntryData = matchupEntrySerialized.Split(',');

            output = new MatchupEntryModel
            {
                Id = int.Parse(matchupEntryData[0]),
                EntryCompeting = LookupEntryById(matchupEntryData[1]),
                Score = double.Parse(matchupEntryData[2]),
                ParentMatchup = LookupMatchupById(matchupEntryData[3])
            };

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
                        return DeserializeEntry(entry);
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
                        return DeserializeMatchup(matchup);
                    }
                }

                return null;
            }
        }

        public static List<MatchupModel> ConvertToMatchupModels(this List<string> matchupsSerialized)
        {
            List<MatchupModel> output = new List<MatchupModel>();

            foreach (string matchup in matchupsSerialized)
            {
                output.Add(DeserializeMatchup(matchup));
            }

            return output;
        }
        public static MatchupModel DeserializeMatchup(string matchupSerialized)
        {
            // [Id],(MatchupEntries)[Id|Id|Id],(Winner)[Id],[MatchupRound]

            MatchupModel output;

            string[] matchupData = matchupSerialized.Split(',');

            output = new MatchupModel
            {
                Id = int.Parse(matchupData[0]),
                MatchupEntries = ConvertStringToMatchupEntryModels(matchupData[1]),
                Winner = LookupEntryById(matchupData[2]),
                MatchupRound = int.Parse(matchupData[3])
            };

            return output;
        }

        public static void SaveOneToMatchupModelsFile(this MatchupModel matchup)
        {
            // [Id],(MatchupEntries)[Id|Id|Id],(Winner)[Id],[MatchupRound]

            List<MatchupModel> matchups = Matchups;

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
            _matchupsOutdated = true;
        }

        public static void UpdateMatchupInFile(this MatchupModel updatedMatchup)
        {
            // [Id],(MatchupEntries)[Id|Id|Id],(Winner)[Id],[MatchupRound]

            List<MatchupModel> matchups = Matchups;

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
            _matchupsOutdated = true;
        }

        public static void UpdateTournamentInFile(this TournamentModel updatedTournament)
        {
            // [Id],[TournamentName],(TournamentEntries)[Id|Id|Id],(Rounds)[Id^Id^Id|Id^Id^Id|Id^Id^Id],[Active],[CurrentRound],[Status]

            List<TournamentModel> tournaments = Tournaments;

            TournamentModel outdatedTournament = tournaments.First(x => x.Id == updatedTournament.Id);

            tournaments.Remove(outdatedTournament);
            tournaments.Add(updatedTournament);

            // save to file
            List<string> lines = new List<string>();

            foreach (TournamentModel tournament in tournaments)
            {
                lines.Add($"{ tournament.Id },{ tournament.TournamentName },{ ConvertEntriesListToString(tournament.TournamentEntries) }," +
                          $"{ ConvertRoundsToString(tournament.Rounds) },{ tournament.Active },{ tournament.CurrentRound },{ tournament.Status.ToString() }");
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
            _tournamentsOutdated = true;
        }

        public static void UpdateEntryInFile(this MatchupEntryModel updatedMatchupEntry)
        {
            // [Id],(EntryCompeting)[Id],[Score],(ParentMatchup)[Id]

            List<MatchupEntryModel> matchupEntries = MatchupEntries;

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
            _matchupEntriesOutdated = true;
        }

        public static void SaveOneToMatchupEntryModelsFile(this MatchupEntryModel matchupEntry)
        {
            // [Id],(EntryCompeting)[Id],[Score],(ParentMatchup)[Id]

            List<MatchupEntryModel> matchupEntries = MatchupEntries;

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
            _matchupEntriesOutdated = true;
        }

        public static void SaveAllToTournamentModelsFile(this List<TournamentModel> tournaments)
        {
            // [Id],[TournamentName],(TournamentEntries)[Id|Id|Id],(Rounds)[Id^Id^Id|Id^Id^Id|Id^Id^Id],[Active],[CurrentRound],[Status]

            List<string> lines = new List<string>();

            foreach (TournamentModel tournament in tournaments)
            {
                lines.Add($"{ tournament.Id },{ tournament.TournamentName },{ ConvertEntriesListToString(tournament.TournamentEntries) }," +
                          $"{ ConvertRoundsToString(tournament.Rounds) },{ tournament.Active },{ tournament.CurrentRound },{ tournament.Status.ToString() }");
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
            _tournamentsOutdated = true;
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
