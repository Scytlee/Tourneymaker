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

        public void UpdateMatchup(MatchupModel matchup)
        {
            matchup.UpdateMatchup();
        }

        public void UpdateTournament(TournamentModel tournament)
        {
            tournament.UpdateTournament();
        }

        public List<TournamentPreviewModel> LoadTournamentPreviews()
        {
            List<TournamentPreviewModel> output = GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertToTournamentPreviewModels();

            return output;
        }

        public TournamentModel LoadTournamentModel(int id)
        {
            TournamentModel output;

            List<string> allTournamentsSerialized = GlobalConfig.TournamentsFile.FullFilePath().LoadFile();

            string tournamentSerialized = allTournamentsSerialized.First(x => int.Parse(x.Split(',')[0]) == id);

            output = TextConnectionHelper.DeserializeTournament(tournamentSerialized);

            return output;
        }
    }
}
