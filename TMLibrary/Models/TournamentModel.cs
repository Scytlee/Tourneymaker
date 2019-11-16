using System;
using System.Collections.Generic;

namespace TMLibrary.Models
{
    /// <summary>
    /// Represents one tournament, with all of the rounds, matchups and outcomes.
    /// </summary>
    public class TournamentModel
    {
        public event EventHandler OnRoundComplete;

        public event EventHandler OnTournamentComplete;

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
        /// Together with CurrentRound property determines status of the tournament
        /// Active = 0 && CurrentRound = 0 -> tournament not yet started
        /// Active = 0 && CurrentRound = Rounds.Count + 1 -> tournament finished
        /// Active = 1 && CurrentRound between 1 and Rounds.Count -> tournament in progress
        /// </summary>
        public int Active { get; set; }

        /// <summary>
        /// Together with Active property determines status of the tournament
        /// Active = 0 && CurrentRound = 0 -> tournament not yet started
        /// Active = 0 && CurrentRound = Rounds.Count + 1 -> tournament finished
        /// Active = 1 && CurrentRound between 1 and Rounds.Count -> tournament in progress
        /// </summary>
        public int CurrentRound { get; set; }

        /// <summary>
        /// Enum representing status of the tournament.
        /// </summary>
        public TournamentStatus Status { get; set; }

        public void CompleteRound()
        {
            OnRoundComplete?.Invoke(this, null);
        }

        public void CompleteTournament()
        {
            OnTournamentComplete?.Invoke(this, null);
        }
    }
}
