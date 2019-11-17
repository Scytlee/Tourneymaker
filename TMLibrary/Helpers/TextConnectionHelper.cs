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

        public static string FullFilePath(this string fileName)
        {
            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{ fileName }";
        }

        public static List<string> LoadFile(this string file)
        {
            return !File.Exists(file) ? new List<string>() : File.ReadAllLines(file).ToList();
        }

        private static List<PersonModel> ConvertToPersonModels(this List<string> peopleSerialized)
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

        private static PersonModel DeserializePerson(string personSerialized)
        {
            // [id],[Nickname],[FirstName],[LastName],[DiscordTag],[EmailAddress]

            string[] personData = personSerialized.Split(',');

            PersonModel output = new PersonModel
            {
                id = int.Parse(personData[0]),
                Nickname = personData[1],
                FirstName = personData[2],
                LastName = personData[3],
                DiscordTag = personData[4],
                EmailAddress = personData[5]
            };

            return output;
        }

        private static List<EntryModel> ConvertToEntryModels(this List<string> entriesSerialized)
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

        private static EntryModel DeserializeEntry(string entrySerialized)
        {
            // [id],[EntryName],(EntryMembers)[id|id|id]

            EntryModel output;

            string[] entryData = entrySerialized.Split(',');

            output = new EntryModel
            {
                id = int.Parse(entryData[0]),
                EntryName = entryData[1]
            };

            string[] personIds = entryData[2].Split('|');

            foreach (string id in personIds)
            {
                output.EntryMembers.Add(LoadPerson(id));
            }

            return output;
        }

        private static PersonModel LoadPerson(string id)
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

        public static TournamentModel DeserializeTournament(string tournamentSerialized)
        {
            // [id],[TournamentName],(TournamentEntries)[id|id|id],(Rounds)[id^id^id|id^id^id|id^id^id],[CurrentRound]

            TournamentModel output;

            string[] tournamentData = tournamentSerialized.Split(',');

            output = new TournamentModel
            {
                id = int.Parse(tournamentData[0]),
                TournamentName = tournamentData[1],
                CurrentRound = int.Parse(tournamentData[4])
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

        public static List<TournamentPreviewModel> ConvertToTournamentPreviewModels(this List<string> tournamentsSerialized)
        {
            // [id],[TournamentName],(TournamentEntries)[id|id|id],(Rounds)[id^id^id|id^id^id|id^id^id],[CurrentRound]

            List<TournamentPreviewModel> output = new List<TournamentPreviewModel>();

            // Remove header
            if (tournamentsSerialized.Count != 0)
            {
                tournamentsSerialized.RemoveAt(0);
            }

            foreach (string tournament in tournamentsSerialized)
            {
                string[] tournamentData = tournament.Split(',');

                TournamentPreviewModel tournamentPreview = new TournamentPreviewModel
                {
                    id = int.Parse(tournamentData[0]),
                    TournamentName = tournamentData[1]
                };

                if (tournamentData[4] == "-1")
                {
                    tournamentPreview.Status = TournamentStatus.Finished;
                }
                else if (tournamentData[4] == "0")
                {
                    tournamentPreview.Status = TournamentStatus.ReadyToStart;
                }
                else
                {
                    tournamentPreview.Status = TournamentStatus.InProgress;
                }

                output.Add(tournamentPreview);
            }

            return output;
        }

        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";

            foreach (PersonModel person in people)
            {
                output += $"{ person.id }|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static MatchupEntryModel DeserializeMatchupEntry(string matchupEntrySerialized)
        {
            // [id],(EntryCompeting)[id],[Score],(ParentMatchup)[id]

            MatchupEntryModel output;

            string[] matchupEntryData = matchupEntrySerialized.Split(',');

            output = new MatchupEntryModel
            {
                id = int.Parse(matchupEntryData[0]),
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

        private static EntryModel LoadEntry(string id)
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

        private static MatchupModel LoadMatchup(string id)
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

        private static MatchupModel DeserializeMatchup(string matchupSerialized)
        {
            // [id],(MatchupEntries)[id|id|id],(Winner)[id],[MatchupRound]

            MatchupModel output;

            string[] matchupData = matchupSerialized.Split(',');

            output = new MatchupModel
            {
                id = int.Parse(matchupData[0]),
                MatchupEntries = DeserializeMatchupEntryList(matchupData[1]),
                Winner = LoadEntry(matchupData[2]),
                MatchupRound = int.Parse(matchupData[3])
            };

            return output;
        }

        private static void SaveMatchup(this MatchupModel matchup)
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

            foreach (MatchupEntryModel matchupEntry in matchup.MatchupEntries)
            {
                matchupEntry.SaveMatchupEntry();
            }

            matchup.id = matchupId++;
            matchupsSerialized[0] = matchupId.ToString();
            matchupsSerialized.Add(SerializeMatchup(matchup));

            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), matchupsSerialized);
        }

        private static string SerializeMatchup(MatchupModel matchup)
        {
            // [id],(MatchupEntries)[id|id|id],(Winner)[id],[MatchupRound]

            string output = $"{ matchup.id },{ SerializeList(matchup.MatchupEntries, '|') },{ matchup.Winner?.id.ToString() },{ matchup.MatchupRound }";

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

                if (matchupEntryData[0] == updatedMatchup.id.ToString())
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

                if (tournamentData[0] == updatedTournament.id.ToString())
                {
                    tournamentsSerialized.Remove(tournament);
                    tournamentsSerialized.Add(SerializeTournament(updatedTournament));
                    break;
                }
            }

            tournamentsSerialized.Insert(0, header);

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), tournamentsSerialized);
        }

        private static void UpdateMatchupEntry(this MatchupEntryModel updatedMatchupEntry)
        {
            List<string> matchupEntriesSerialized = GlobalConfig.MatchupEntriesFile.FullFilePath().LoadFile();

            // Remove header
            string header = matchupEntriesSerialized[0];
            matchupEntriesSerialized.RemoveAt(0);

            foreach (string matchupEntry in matchupEntriesSerialized)
            {
                string[] matchupEntryData = matchupEntry.Split(',');

                if (matchupEntryData[0] == updatedMatchupEntry.id.ToString())
                {
                    matchupEntriesSerialized.Remove(matchupEntry);
                    matchupEntriesSerialized.Add(SerializeMatchupEntry(updatedMatchupEntry));
                    break;
                }
            }

            matchupEntriesSerialized.Insert(0, header);

            File.WriteAllLines(GlobalConfig.MatchupEntriesFile.FullFilePath(), matchupEntriesSerialized);
        }

        private static void SaveMatchupEntry(this MatchupEntryModel matchupEntry)
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

            matchupEntry.id = matchupEntryId++;
            matchupEntriesSerialized[0] = matchupEntryId.ToString();
            matchupEntriesSerialized.Add(SerializeMatchupEntry(matchupEntry));

            File.WriteAllLines(GlobalConfig.MatchupEntriesFile.FullFilePath(), matchupEntriesSerialized);
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

            entry.id = entryId++;
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

            person.id = personId++;
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

            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    matchup.SaveMatchup();
                }
            }

            tournament.id = tournamentId++;
            tournamentsSerialized[0] = tournamentId.ToString();
            tournamentsSerialized.Add(SerializeTournament(tournament));

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), tournamentsSerialized);
        }

        private static string SerializeTournament(TournamentModel tournament)
        {
            // [id],[TournamentName],(TournamentEntries)[id|id|id],(Rounds)[id^id^id|id^id^id|id^id^id],[CurrentRound]

            string output = $"{ tournament.id },{ tournament.TournamentName },{ SerializeList(tournament.TournamentEntries, '|') }," +
                            $"{ SerializeRounds(tournament.Rounds) },{ tournament.CurrentRound }";

            return output;
        }

        private static string SerializePerson(PersonModel person)
        {
            // [id],[Nickname],[FirstName],[LastName],[DiscordTag],[EmailAddress]

            string output = $"{ person.id },{ person.Nickname },{ person.FirstName },{ person.LastName },{ person.DiscordTag },{ person.EmailAddress }";

            return output;
        }

        private static string SerializeEntry(EntryModel entry)
        {
            // [id],[EntryName],(EntryMembers)[id|id|id]

            string output = $"{ entry.id },{ entry.EntryName },{ ConvertPeopleListToString(entry.EntryMembers) }";

            return output;
        }

        private static string SerializeMatchupEntry(MatchupEntryModel matchupEntry)
        {
            // [id],(EntryCompeting)[id],[Score],(ParentMatchup)[id]

            string output = $"{ matchupEntry.id },{ matchupEntry.EntryCompeting?.id.ToString() },{ matchupEntry.Score },{ matchupEntry.ParentMatchup?.id.ToString() }";

            return output;
        }

        private static string SerializeRounds(List<List<MatchupModel>> rounds)
        {
            string output = "";

            foreach (List<MatchupModel> round in rounds)
            {
                output += $"{ SerializeList(round, '^') }|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string SerializeList<T>(List<T> elements, char separator) where T : IModel
        {
            string output = "";

            foreach (T element in elements)
            {
                output += $"{ element.id }{ separator }";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }
    }
}
