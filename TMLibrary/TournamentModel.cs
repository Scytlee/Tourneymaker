using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary
{
    public class TournamentModel
    {
        public string TournamentName { get; set; }
        public List<EntryModel> TournamentEntries { get; set; }
        public List<List<MatchupModel>> Rounds { get; set; }
        public bool Active { get; set; }
        public int CurrentRound { get; set; }
    }
}
