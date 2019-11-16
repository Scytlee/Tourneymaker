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
                    _matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().LoadMatchups();
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

            // Remove header
            if (peopleSerialized.Count != 0)
            {
                peopleSerialized.RemoveAt(0);
            }

            foreach (string person in peopleSerialized)
            {
                output.Add(DeserializePerson(person));
            }

            return output;
        }

        public static PersonModel DeserializePerson(string personSerialized)
        {
            // [Id],[Nickname],[FirstName],[LastName],[DiscordTag],[EmailAddress]

            string[] personData = personSerialized.Split(',');

            PersonModel output = new PersonModel
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

            // Remove header
            if (entriesSerialized.Count != 0)
            {
                entriesSerialized.RemoveAt(0);
            }

            foreach (string entry in entriesSerialized)
            {
                output.Add(DeserializeEntry(entry));
            }

            return output;
        }

        public static EntryModel DeserializeEntry(string entrySerialized)
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
                output.EntryMembers.Add(LoadPerson(id));
            }

            return output;
        }

        public static PersonModel LoadPerson(string id)
        {
            if (id == "")
            {
                return null;
            }

            List<string> peopleSerialized = GlobalConfig.PeopleFile.FullFilePath().LoadFile();

            // Remove header
            if (peopleSerialized.Count != 0)
            {
                peopleSerialized.RemoveAt(0);
            }

            foreach (string person in peopleSerialized)
            {
                string[] personData = person.Split(',');

                if (personData[0] == id)
                {
                    return DeserializePerson(person);
                }
            }

            return null;
        }

        public static List<TournamentModel> ConvertToTournamentModels(this List<string> tournamentsSerialized)
        {
            List<TournamentModel> output = new List<TournamentModel>();

            // Remove header
            if (tournamentsSerialized.Count != 0)
            {
                tournamentsSerialized.RemoveAt(0);
            }

            foreach (string tournament in tournamentsSerialized)
            {
                output.Add(DeserializeTournament(tournament));
            }

            return output;
        }

        public static TournamentModel DeserializeTournament(string tournamentSerialized)
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
                output.TournamentEntries.Add(LoadEntry(id));
            }

            // Capture Rounds information
            string[] rounds = tournamentData[3].Split('|');

            foreach (string round in rounds)
            {
                string[] matchupIds = round.Split('^');
                List<MatchupModel> ms = new List<MatchupModel>();

                foreach (string matchupId in matchupIds)
                {
                    ms.Add(LoadMatchup(matchupId));
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
                    matchup.SaveMatchup();
                }
            }
        }

        public static List<MatchupEntryModel> ConvertToMatchupEntryModels(this List<string> matchupEntriesSerialized)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            // Remove header
            matchupEntriesSerialized.RemoveAt(0);

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
                EntryCompeting = LoadEntry(matchupEntryData[1]),
                Score = double.Parse(matchupEntryData[2]),
                ParentMatchup = LoadMatchup(matchupEntryData[3])
            };

            return output;
        }

        private static List<MatchupEntryModel> DeserializeMatchupEntryList(string input)
        {
            string[] ids = input.Split('|');

            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            List<string> matchupEntriesSerialized = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile();

            // Remove header
            if (matchupEntriesSerialized.Count != 0)
            {
                matchupEntriesSerialized.RemoveAt(0); 
            }

            foreach (string id in ids)
            {
                foreach (string matchupEntry in matchupEntriesSerialized)
                {
                    string[] cols = matchupEntry.Split(',');

                    if (cols[0] == id)
                    {
                        output.Add(DeserializeMatchupEntry(matchupEntry));
                    }
                }
            }

            return output;
        }

        public static EntryModel LoadEntry(string id)
        {
            if (id == "")
            {
                return null;
            }

            List<string> entriesSerialized = GlobalConfig.EntriesFile.FullFilePath().LoadFile();

            // Remove header
            if (entriesSerialized.Count != 0)
            {
                entriesSerialized.RemoveAt(0);
            }

            foreach (string entry in entriesSerialized)
            {
                string[] entryData = entry.Split(',');

                if (entryData[0] == id)
                {
                    return DeserializeEntry(entry);
                }
            }

            return null;
        }

        public static MatchupModel LoadMatchup(string id)
        {
            if (id == "")
            {
                return null;
            }

            List<string> matchupsSerialized = GlobalConfig.MatchupsFile.FullFilePath().LoadFile();

            // Remove header
            if (matchupsSerialized.Count != 0)
            {
                matchupsSerialized.RemoveAt(0);
            }

            foreach (string matchup in matchupsSerialized)
            {
                string[] matchupData = matchup.Split(',');

                if (matchupData[0] == id)
                {
                    return DeserializeMatchup(matchup);
                }
            }

            return null;
        }

        public static List<MatchupModel> LoadMatchups(this List<string> matchupsSerialized)
        {
            List<MatchupModel> output = new List<MatchupModel>();

            // Remove header
            if (matchupsSerialized.Count != 0)
            {
                matchupsSerialized.RemoveAt(0);
            }

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
                MatchupEntries = DeserializeMatchupEntryList(matchupData[1]),
                Winner = LoadEntry(matchupData[2]),
                MatchupRound = int.Parse(matchupData[3])
            };

            return output;
        }

        public static void SaveMatchup(this MatchupModel matchup)
        {
            List<string> matchupsSerialized = GlobalConfig.MatchupsFile.FullFilePath().LoadFile();

            int matchupId;

            if (matchupsSerialized.Count == 0)
            {
                matchupsSerialized.Add("1");
                matchupId = 1;
            }
            else
            {
                matchupId = int.Parse(matchupsSerialized[0]);
            }

            matchup.Id = matchupId++;
            matchupsSerialized[0] = matchupId.ToString();
            matchupsSerialized.Add(SerializeMatchup(matchup));

            foreach (MatchupEntryModel matchupEntry in matchup.MatchupEntries)
            {
                matchupEntry.SaveMatchupEntry();
            }

            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), matchupsSerialized);
            _matchupsOutdated = true;
        }

        private static string SerializeMatchup(MatchupModel matchup)
        {
            // [Id],(MatchupEntries)[Id|Id|Id],(Winner)[Id],[MatchupRound]

            string output = $"{ matchup.Id },{ SerializeMatchupEntryList(matchup.MatchupEntries) },{ matchup.Winner?.Id.ToString() },{ matchup.MatchupRound }";

            return output;
        }

        public static void UpdateMatchup(this MatchupModel updatedMatchup)
        {
            List<string> matchupsSerialized = GlobalConfig.MatchupsFile.FullFilePath().LoadFile();

            // Remove header
            string header = matchupsSerialized[0];
            matchupsSerialized.RemoveAt(0);

            foreach (string matchup in matchupsSerialized)
            {
                string[] matchupEntryData = matchup.Split(',');

                if (matchupEntryData[0] == updatedMatchup.Id.ToString())
                {
                    matchupsSerialized.Remove(matchup);
                    matchupsSerialized.Add(SerializeMatchup(updatedMatchup));
                    break;
                }
            }

            matchupsSerialized.Insert(0, header);

            foreach (MatchupEntryModel matchupEntry in updatedMatchup.MatchupEntries)
            {
                matchupEntry.UpdateMatchupEntry();
            }

            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), matchupsSerialized);
            _matchupsOutdated = true;
        }

        public static void UpdateTournament(this TournamentModel updatedTournament)
        {
            List<string> tournamentsSerialized = GlobalConfig.TournamentsFile.FullFilePath().LoadFile();

            // Remove header
            string header = tournamentsSerialized[0];
            tournamentsSerialized.RemoveAt(0);

            foreach (string tournament in tournamentsSerialized)
            {
                string[] tournamentData = tournament.Split(',');

                if (tournamentData[0] == updatedTournament.Id.ToString())
                {
                    tournamentsSerialized.Remove(tournament);
                    tournamentsSerialized.Add(SerializeTournament(updatedTournament));
                    break;
                }
            }

            tournamentsSerialized.Insert(0, header);

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), tournamentsSerialized);
            _tournamentsOutdated = true;
        }

        public static void UpdateMatchupEntry(this MatchupEntryModel updatedMatchupEntry)
        {
            List<string> matchupEntriesSerialized = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile();

            // Remove header
            string header = matchupEntriesSerialized[0];
            matchupEntriesSerialized.RemoveAt(0);

            foreach (string matchupEntry in matchupEntriesSerialized)
            {
                string[] matchupEntryData = matchupEntry.Split(',');

                if (matchupEntryData[0] == updatedMatchupEntry.Id.ToString())
                {
                    matchupEntriesSerialized.Remove(matchupEntry);
                    matchupEntriesSerialized.Add(SerializeMatchupEntry(updatedMatchupEntry));
                    break;
                }
            }

            matchupEntriesSerialized.Insert(0, header);

            File.WriteAllLines(GlobalConfig.MatchupEntriesFile.FullFilePath(), matchupEntriesSerialized);
            _matchupEntriesOutdated = true;
        }

        public static MatchupEntryModel LoadMatchupEntry(string id)
        {
            if (id == "")
            {
                return null;
            }

            List<string> matchupEntriesSerialized = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile();

            // Remove header
            if (matchupEntriesSerialized.Count != 0)
            {
                matchupEntriesSerialized.RemoveAt(0);
            }

            foreach (string matchupEntry in matchupEntriesSerialized)
            {
                string[] matchupEntryData = matchupEntry.Split(',');

                if (matchupEntryData[0] == id)
                {
                    return DeserializeMatchupEntry(matchupEntry);
                }
            }

            return null;
        }

        public static void SaveMatchupEntry(this MatchupEntryModel matchupEntry)
        {
            List<string> matchupEntriesSerialized = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile();

            int matchupEntryId;

            if (matchupEntriesSerialized.Count == 0)
            {
                matchupEntriesSerialized.Add("1");
                matchupEntryId = 1;
            }
            else
            {
                matchupEntryId = int.Parse(matchupEntriesSerialized[0]);
            }

            matchupEntry.Id = matchupEntryId++;
            matchupEntriesSerialized[0] = matchupEntryId.ToString();
            matchupEntriesSerialized.Add(SerializeMatchupEntry(matchupEntry));

            File.WriteAllLines(GlobalConfig.MatchupEntriesFile.FullFilePath(), matchupEntriesSerialized);
            _matchupEntriesOutdated = true;
        }

        public static void SaveEntry(this EntryModel entry)
        {
            List<string> entriesSerialized = GlobalConfig.EntriesFile.FullFilePath().LoadFile();

            int entryId;

            if (entriesSerialized.Count == 0)
            {
                entriesSerialized.Add("1");
                entryId = 1;
            }
            else
            {
                entryId = int.Parse(entriesSerialized[0]);
            }

            entry.Id = entryId++;
            entriesSerialized[0] = entryId.ToString();
            entriesSerialized.Add(SerializeEntry(entry));

            File.WriteAllLines(GlobalConfig.EntriesFile.FullFilePath(), entriesSerialized);
            _entriesOutdated = true;
        }

        public static void SavePerson(this PersonModel person)
        {
            List<string> peopleSerialized = GlobalConfig.PeopleFile.FullFilePath().LoadFile();

            int personId;

            if (peopleSerialized.Count == 0)
            {
                peopleSerialized.Add("1");
                personId = 1;
            }
            else
            {
                personId = int.Parse(peopleSerialized[0]);
            }

            person.Id = personId++;
            peopleSerialized[0] = personId.ToString();
            peopleSerialized.Add(SerializePerson(person));

            File.WriteAllLines(GlobalConfig.PeopleFile.FullFilePath(), peopleSerialized);
            _peopleOutdated = true;
        }

        public static void SaveTournament(this TournamentModel tournament)
        {
            List<string> tournamentsSerialized = GlobalConfig.TournamentsFile.FullFilePath().LoadFile();

            int tournamentId;

            if (tournamentsSerialized.Count == 0)
            {
                tournamentsSerialized.Add("1");
                tournamentId = 1;
            }
            else
            {
                tournamentId = int.Parse(tournamentsSerialized[0]);
            }

            tournament.Id = tournamentId++;
            tournamentsSerialized[0] = tournamentId.ToString();
            tournamentsSerialized.Add(SerializeTournament(tournament));

            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    matchup.SaveMatchup();
                }
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), tournamentsSerialized);
            _tournamentsOutdated = true;
        }

        private static string SerializeTournament(TournamentModel tournament)
        {
            // [Id],[TournamentName],(TournamentEntries)[Id|Id|Id],(Rounds)[Id^Id^Id|Id^Id^Id|Id^Id^Id],[Active],[CurrentRound],[Status]

            string output = $"{ tournament.Id },{ tournament.TournamentName },{ SerializeEntryList(tournament.TournamentEntries) }," +
                            $"{ SerializeRounds(tournament.Rounds) },{ tournament.Active },{ tournament.CurrentRound },{ tournament.Status.ToString() }";

            return output;
        }

        private static string SerializePerson(PersonModel person)
        {
            // [Id],[Nickname],[FirstName],[LastName],[DiscordTag],[EmailAddress]

            string output = $"{ person.Id },{ person.Nickname },{ person.FirstName },{ person.LastName },{ person.DiscordTag },{ person.EmailAddress }";

            return output;
        }

        private static string SerializeEntry(EntryModel entry)
        {
            // [Id],[EntryName],(EntryMembers)[Id|Id|Id]

            string output = $"{ entry.Id },{ entry.EntryName },{ ConvertPeopleListToString(entry.EntryMembers) }";

            return output;
        }

        private static string SerializeMatchupEntry(MatchupEntryModel matchupEntry)
        {
            // [Id],(EntryCompeting)[Id],[Score],(ParentMatchup)[Id]

            string output = $"{ matchupEntry.Id },{ matchupEntry.EntryCompeting?.Id.ToString() },{ matchupEntry.Score },{ matchupEntry.ParentMatchup?.Id.ToString() }";

            return output;
        }

        public static void SaveTournaments(this List<TournamentModel> tournaments)
        {
            // [Id],[TournamentName],(TournamentEntries)[Id|Id|Id],(Rounds)[Id^Id^Id|Id^Id^Id|Id^Id^Id],[Active],[CurrentRound],[Status]

            List<string> lines = new List<string>();

            foreach (TournamentModel tournament in tournaments)
            {
                lines.Add($"{ tournament.Id },{ tournament.TournamentName },{ SerializeEntryList(tournament.TournamentEntries) }," +
                          $"{ SerializeRounds(tournament.Rounds) },{ tournament.Active },{ tournament.CurrentRound },{ tournament.Status.ToString() }");
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
            _tournamentsOutdated = true;
        }

        private static string SerializeRounds(List<List<MatchupModel>> rounds)
        {
            string output = "";

            foreach (List<MatchupModel> round in rounds)
            {
                output += $"{ SerializeMatchupList(round) }|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string SerializeMatchupList(List<MatchupModel> matchups)
        {
            string output = "";

            foreach (MatchupModel matchup in matchups)
            {
                output += $"{ matchup.Id }^";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string SerializeMatchupEntryList(List<MatchupEntryModel> matchupEntries)
        {
            string output = "";

            foreach (MatchupEntryModel matchupEntry in matchupEntries)
            {
                output += $"{ matchupEntry.Id }|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string SerializeEntryList(List<EntryModel> entries)
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
