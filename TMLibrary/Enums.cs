using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary
{
    public enum ConnectionType
    {
        SqlDatabase,
        TextFile
    }

    public enum TournamentStatus
    {
        ReadyToStart,
        InProgress,
        Finished
    }
}
