using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMLibrary.DataAccess;

namespace TMLibrary
{
    public static class GlobalConfig
    {
        public const string PeopleFile = "People.csv";
        public const string EntriesFile = "Entries.csv";
        public const string TournamentsFile = "Tournaments.csv";
        public const string MatchupsFile = "Matchups.csv";
        public const string MatchupEntriesFile = "MatchupEntries.csv";

        public const string TournamentsReadyToStartFile = "TournamentsReadyToStart.csv";
        public const string TournamentsInProgressFile = "TournamentsInProgress.csv";
        public const string TournamentsFinishedFile = "TournamentsFinished.csv";

        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnection(ConnectionType connectionType)
        {
            if (connectionType == ConnectionType.SqlDatabase)
            {
                SqlConnection sqlConnection = new SqlConnection();
                Connection = sqlConnection;
            }
            else if (connectionType == ConnectionType.TextFile)
            {
                TextConnection textConnection = new TextConnection();
                Connection = textConnection;
            }
        }

        public static string ConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
