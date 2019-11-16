using System;
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
            List<TournamentModel> tournaments = LoadTournamentModels();

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
            return TextConnectionHelper.People;
        }

        public List<EntryModel> LoadEntryModels()
        {
            return TextConnectionHelper.Entries;
        }

        public List<TournamentModel> LoadTournamentModels()
        {
            return TextConnectionHelper.Tournaments;
        }

        public void UpdateMatchup(MatchupModel matchup)
        {
            matchup.UpdateMatchupInFile();
        }

        public void UpdateTournament(TournamentModel tournament)
        {
            tournament.UpdateTournamentInFile();
        }

        public List<TournamentPreviewModel> LoadTournamentPreviews(TournamentStatus status)
        {
            // There shouldn't be any errors regarding invalid enum type passed, therefore
            // there is no check for that

            List<TournamentPreviewModel> output = new List<TournamentPreviewModel>();
            List<TournamentPreviewModel> allTournamentPreviews = GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertToTournamentPreviewModels();
            // TODO This should be refactored to not load all tournaments with every call

            foreach (TournamentPreviewModel tournamentPreview in allTournamentPreviews)
            {
                if (tournamentPreview.Status == status)
                {
                    output.Add(tournamentPreview);
                }
            }

            return output;
        }

        public TournamentModel LoadTournamentModel(int id)
        {
            TournamentModel output = null;

            List<string> allTournamentsSerialized = GlobalConfig.TournamentsFile.FullFilePath().LoadFile();

            foreach (string tournament in allTournamentsSerialized)
            {
                string[] tournamentData = tournament.Split(',');

                if (int.Parse(tournamentData[0]) == id)
                {
                    output = TextConnectionHelper.DeserializeTournament(tournament);
                }
            }

            return output;
        }
    }
}
