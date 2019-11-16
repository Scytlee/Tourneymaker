using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary.Models
{
    public class TournamentPreviewModel
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
        /// Enum representing status of the tournament.
        /// </summary>
        public TournamentStatus Status { get; set; }
    }
}
