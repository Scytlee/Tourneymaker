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
        public const string PeopleFile = "PersonModels.csv";
        public const string EntriesFile = "EntryModels.csv";
        public const string TournamentsFile = "TournamentModels.csv";
        public const string MatchupsFile = "MatchupModels.csv";
        public const string MatchupEntriesFile = "MatchupEntryModels.csv";

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
