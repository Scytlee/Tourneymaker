namespace TMLibrary.Models
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// Represents one entry in the matchup.
        /// </summary>
        public EntryModel EntryCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particular entry.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the matchup that this entry came from as the winner.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
