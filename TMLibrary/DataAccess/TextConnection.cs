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
            newPerson.SavePerson();
            //// Load the PersonModels text file and convert the text to List<PersonModel>
            //List<PersonModel> people = LoadPersonModels();

            //// Find the max ID
            //int currentId = 1;

            //if (people.Count > 0)
            //{
            //    currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            //}

            //// Set ID for the person
            //newPerson.Id = currentId;

            //// Add the person to the list
            //people.Add(newPerson);

            //// Convert PersonModels to List<string>
            //// Save the List<string> to the PersonModels text file
            //people.SaveAllToPersonModelsFile();
        }

        public void CreateEntry(EntryModel newEntry)
        {
            newEntry.SaveEntry();
        }

        public void CreateTournament(TournamentModel newTournament)
        {
            newTournament.SaveTournament();
        }

        public List<PersonModel> LoadPersonModels()
        {
            return TextConnectionHelper.People;
        }

        public List<EntryModel> LoadEntryModels()
        {
            return TextConnectionHelper.Entries;
        }

        [Obsolete]
        public List<TournamentModel> LoadTournamentModels()
        {
            return TextConnectionHelper.Tournaments;
        }

        public void UpdateMatchup(MatchupModel matchup)
        {
            matchup.UpdateMatchup();
        }

        public void UpdateTournament(TournamentModel tournament)
        {
            tournament.UpdateTournament();
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
