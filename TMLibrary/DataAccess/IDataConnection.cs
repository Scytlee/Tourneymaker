using System.Collections.Generic;
using TMLibrary.Models;

namespace TMLibrary.DataAccess
{
    public interface IDataConnection
    {
        void CreatePerson(PersonModel newPerson);
        void CreateEntry(EntryModel newEntry);
        void CreateTournament(TournamentModel newTournament);
        List<PersonModel> LoadPersonModels();
        List<EntryModel> LoadEntryModels();
        List<TournamentModel> LoadTournamentModels();
        void UpdateMatchup(MatchupModel matchup);
        void UpdateTournament(TournamentModel tournament);
    }
}
