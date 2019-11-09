using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary
{
    public class MatchupModel
    {
        public List<MatchupEntryModel> MatchupEntries { get; set; }
        public EntryModel Winner { get; set; }
        public int MatchupRound { get; set; }
    }
}
