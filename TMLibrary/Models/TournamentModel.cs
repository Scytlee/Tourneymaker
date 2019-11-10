using System.Collections.Generic;

namespace TMLibrary.Models
{
    public class TournamentModel
    {
        public string TournamentName { get; set; }
        public List<EntryModel> TournamentEntries { get; set; } = new List<EntryModel>();
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();
        public bool Active { get; set; }
        public int CurrentRound { get; set; }
    }
}
