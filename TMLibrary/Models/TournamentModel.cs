using System;
using System.Collections.Generic;

namespace TMLibrary.Models
{
    /// <summary>
    /// Represents one tournament, with all of the rounds, matchups and outcomes.
    /// </summary>
    public class TournamentModel : IModel
    {
        public event EventHandler OnRoundComplete;

        public event EventHandler OnTournamentComplete;

        /// <summary>
        /// The unique identifier for the tournament.
        /// </summary>
        public int id { get; set; }

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
        /// Represents status of the tournament - ReadyToStart, InProgress or Finished.
        /// </summary>
        public TournamentStatus Status
        {
            get
            {
                TournamentStatus output;

                if (CurrentRound == 0)
                {
                    output = TournamentStatus.ReadyToStart;
                }
                else if (CurrentRound == -1)
                {
                    output = TournamentStatus.Finished;
                }
                else
                {
                    output = TournamentStatus.InProgress;
                }

                return output;
            }
        }

        /// <summary>
        /// Current round of the tournament, or 0 if ReadyToStart, or -1 if Finished.
        /// </summary>
        public int CurrentRound { get; set; }

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
