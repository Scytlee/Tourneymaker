using System.Collections.Generic;
using System.Linq;
using TMLibrary.Helpers;
using TMLibrary.Models;

namespace TMLibrary.DataAccess
{
    public class TextConnection : IDataConnection
    {
        public void CreatePerson(PersonModel newPerson)
        {
            // Load the PersonModels text file and convert the text to List<PersonModel>
            List<PersonModel> people = LoadPersonModels();

            // Find the max ID
            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            // Set ID for the person
            newPerson.Id = currentId;

            // Add the person to the list
            people.Add(newPerson);

            // Convert PersonModels to List<string>
            // Save the List<string> to the PersonModels text file
            people.SaveAllToPersonModelsFile();
        }

        public void CreateEntry(EntryModel newEntry)
        {
            // Load the EntryModels text file and convert the text to List<EntryModel>
            List<EntryModel> entries = LoadEntryModels();

            // Find the max ID
            int currentId = 1;

            if (entries.Count > 0)
            {
                currentId = entries.OrderByDescending(x => x.Id).First().Id + 1;
            }

            // Set ID for the entry
            newEntry.Id = currentId;

            // Add the entry to the list
            entries.Add(newEntry);

            // Convert EntryModels to List<string>
            // Save the List<string> to the EntryModels text file
            entries.SaveAllToEntryModelsFile();
        }

        public void CreateTournament(TournamentModel newTournament)
        {
            // Load the TournamentModels text file and convert the text to List<TournamentModel>
            List<TournamentModel> tournaments =
                GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertToTournamentModels();

            // Find the max ID
            int currentId = 1;

            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }

            // Set ID for the entry
            newTournament.Id = currentId;

            newTournament.SaveRounds();

            // Add the entry to the list
            tournaments.Add(newTournament);

            // Convert TournamentModels to List<string>
            // Save the List<string> to the TournamentModels text file
            tournaments.SaveAllToTournamentModelsFile();
        }

        public List<PersonModel> LoadPersonModels()
        {
            return GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<EntryModel> LoadEntryModels()
        {
            return GlobalConfig.EntriesFile.FullFilePath().LoadFile().ConvertToEntryModels();
        }

        public List<TournamentModel> LoadTournamentModels()
        {
            return GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertToTournamentModels();
        }

        public void UpdateMatchup(MatchupModel matchup)
        {
            matchup.UpdateMatchupInFile();
        }

        public void UpdateTournament(TournamentModel tournament)
        {
            tournament.UpdateTournamentInFile();
        }
    }
}
