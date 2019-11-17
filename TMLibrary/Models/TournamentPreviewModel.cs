using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary.Models
{
    public class TournamentPreviewModel : IModel
    {
        /// <summary>
        /// The unique identifier for the tournament.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The name given to this tournament.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Represents status of the tournament - ReadyToStart, InProgress or Finished.
        /// </summary>
        public TournamentStatus Status { get; set; }

        /// <summary>
        /// Current round of the tournament, or 0 if ReadyToStart, or Rounds.Count + 1 if Finished.
        /// </summary>
        public int CurrentRound { get; set; }
    }
}
