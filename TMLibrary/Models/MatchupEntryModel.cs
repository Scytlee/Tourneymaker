namespace TMLibrary.Models
{
    /// <summary>
    /// Represents one entry in a matchup.
    /// </summary>
    public class MatchupEntryModel
    {
        /// <summary>
        /// The unique identifier for the matchup entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique identifier for the entry.
        /// </summary>
        public int EntryCompetingId { get; set; }

        /// <summary>
        /// Represents one entry in the matchup.
        /// </summary>
        public EntryModel EntryCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particular entry.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// The unique identifier for the parent matchup (entry).
        /// </summary>
        public int ParentMatchupId { get; set; }

        /// <summary>
        /// Represents the matchup that this entry came from as the winner.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
