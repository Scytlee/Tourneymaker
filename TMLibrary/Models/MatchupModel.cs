using System.Collections.Generic;

namespace TMLibrary.Models
{
    public class MatchupModel
    {
        public List<MatchupEntryModel> MatchupEntries { get; set; } = new List<MatchupEntryModel>();
        public EntryModel Winner { get; set; }
        public int MatchupRound { get; set; }
    }
}
