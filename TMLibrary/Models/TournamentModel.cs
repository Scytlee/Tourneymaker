using System.Collections.Generic;

namespace TMLibrary.Models
{
    /// <summary>
    /// Represents one tournament, with all of the rounds, matchups and outcomes.
    /// </summary>
    public class TournamentModel
    {
        /// <summary>
        /// The unique identifier for the tournament.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name given to this tournament.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// The set of entries that have been entered.
        /// </summary>
        public List<EntryModel> TournamentEntries { get; set; } = new List<EntryModel>();

        /// <summary>
        /// The matchups per round.
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();

        /// <summary>
        /// The current round of the tournament.
        /// </summary>
        public int CurrentRound { get; set; }
    }
}
