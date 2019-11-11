using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TMLibrary.Models
{
    /// <summary>
    /// Represents one match in the tournament.
    /// </summary>
    public class MatchupModel
    {
        /// <summary>
        /// The unique identifier for the matchup.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The set of entries that were involved in this match.
        /// </summary>
        public List<MatchupEntryModel> MatchupEntries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// The ID from the database that will be used to identify the winner.
        /// </summary>
        public int WinnerId { get; set; }

        /// <summary>
        /// The winner of the match.
        /// </summary>
        public EntryModel Winner { get; set; }

        /// <summary>
        /// Which round this match is a part of.
        /// </summary>
        public int MatchupRound { get; set; }

        public string DisplayName
        {
            get
            {
                string output = "";

                foreach (MatchupEntryModel matchupEntry in MatchupEntries)
                {
                    if (output.Length == 0)
                    {
                        if (matchupEntry.EntryCompeting == null)
                        {
                            output = "TBD";
                        }
                        else
                        {
                            output = matchupEntry.EntryCompeting.DisplayName;
                        }
                    }
                    else
                    {
                        if (matchupEntry.EntryCompeting == null)
                        {
                            output += " -vs- TBD";
                        }
                        else
                        {
                            output += $" -vs- { matchupEntry.EntryCompeting.DisplayName }";
                        }
                    }
                }

                return output;
            }
        }
    }
}
