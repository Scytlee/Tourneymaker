using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using TMLibrary.Models;

namespace TMLibrary.DataAccess
{
    public class SqlConnection : IDataConnection
    {
        private const string _databaseName = "TMData";

        public void CreatePerson(PersonModel newPerson)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(_databaseName)))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nickname", newPerson.Nickname);
                parameters.Add("@FirstName", newPerson.FirstName);
                parameters.Add("@LastName", newPerson.LastName);
                parameters.Add("@DiscordTag", newPerson.DiscordTag);
                parameters.Add("@EmailAddress", newPerson.EmailAddress);
                parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", parameters, commandType: CommandType.StoredProcedure);

                newPerson.Id = parameters.Get<int>("@Id");
            }
        }

        public void CreateEntry(EntryModel newEntry)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(_databaseName)))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EntryName", newEntry.EntryName);
                parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

                connection.Execute("dbo.spEntries_Insert", parameters, commandType: CommandType.StoredProcedure);

                newEntry.Id = parameters.Get<int>("@Id");

                foreach (PersonModel person in newEntry.EntryMembers)
                {
                    parameters = new DynamicParameters();
                    parameters.Add("@EntryId", newEntry.Id);
                    parameters.Add("@PersonId", person.Id);

                    connection.Execute("dbo.spEntryMembers_Insert", parameters, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public void CreateTournament(TournamentModel newTournament)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(_databaseName)))
            {
                SaveTournament(newTournament, connection);

                SaveTournamentEntries(newTournament, connection);

                SaveTournamentRounds(newTournament, connection);
            }
        }

        private void SaveTournament(TournamentModel newTournament, IDbConnection connection)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TournamentName", newTournament.TournamentName);
            parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

            connection.Execute("dbo.spTournaments_Insert", parameters, commandType: CommandType.StoredProcedure);

            newTournament.Id = parameters.Get<int>("@Id");
        }

        private void SaveTournamentEntries(TournamentModel newTournament, IDbConnection connection)
        {
            foreach (EntryModel entry in newTournament.TournamentEntries)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TournamentId", newTournament.Id);
                parameters.Add("@EntryId", entry.Id);
                parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

                connection.Execute("dbo.spTournamentEntries_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        private void SaveTournamentRounds(TournamentModel newTournament, IDbConnection connection)
        {
            // Loop through the rounds
            foreach (List<MatchupModel> round in newTournament.Rounds)
            {
                // Loop through the matchups
                foreach (MatchupModel matchup in round)
                {
                    // Save the matchup
                    var parameters = new DynamicParameters();
                    parameters.Add("@TournamentId", newTournament.Id);
                    parameters.Add("@MatchupRound", matchup.MatchupRound);
                    parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

                    connection.Execute("dbo.spMatchups_Insert", parameters, commandType: CommandType.StoredProcedure);

                    matchup.Id = parameters.Get<int>("@Id");

                    // Loop through the matchup entries
                    foreach (MatchupEntryModel matchupEntry in matchup.MatchupEntries)
                    {
                        // Save the matchup entry
                        parameters = new DynamicParameters();
                        parameters.Add("@MatchupId", matchup.Id);
                        parameters.Add("@ParentMatchupId", matchupEntry.ParentMatchup?.Id);
                        parameters.Add("@EntryCompetingId", matchupEntry.EntryCompeting?.Id);
                        parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

                        connection.Execute("dbo.spMatchupEntries_Insert", parameters, commandType: CommandType.StoredProcedure);

                        matchupEntry.Id = parameters.Get<int>("@Id");
                    }
                }
            }
        }

        public List<PersonModel> LoadPersonModels()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(_databaseName)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }

        public List<EntryModel> LoadEntryModels()
        {
            List<EntryModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(_databaseName)))
            {
                output = connection.Query<EntryModel>("dbo.spEntries_GetAll").ToList();

                foreach (EntryModel entry in output)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@EntryId", entry.Id);

                    entry.EntryMembers = connection.Query<PersonModel>("dbo.spEntryMembers_GetByEntry", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }

            return output;
        }

        public List<TournamentModel> LoadTournamentModels()
        {
            List<TournamentModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(_databaseName)))
            {
                output = connection.Query<TournamentModel>("dbo.spTournaments_GetAll").ToList();

                foreach (TournamentModel tournament in output)
                {
                    DynamicParameters parameters;

                    // Populate TournamentEntries
                    parameters = new DynamicParameters();
                    parameters.Add("@TournamentId", tournament.Id);

                    tournament.TournamentEntries = connection.Query<EntryModel>("dbo.spEntries_GetByTournament", parameters, commandType: CommandType.StoredProcedure).ToList();

                    foreach (EntryModel entry in tournament.TournamentEntries)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@EntryId", entry.Id);

                        entry.EntryMembers = connection.Query<PersonModel>("dbo.spEntryMembers_GetByEntry", parameters, commandType: CommandType.StoredProcedure).ToList();
                    }

                    // Populate Rounds
                    parameters = new DynamicParameters();
                    parameters.Add("@TournamentId", tournament.Id);

                    List<MatchupModel> matchups = connection.Query<MatchupModel>("dbo.spMatchups_GetByTournament", parameters, commandType: CommandType.StoredProcedure).ToList();

                    foreach (MatchupModel matchup in matchups)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@MatchupId", matchup.Id);

                        matchup.MatchupEntries = connection.Query<MatchupEntryModel>("dbo.spMatchupEntries_GetByMatchup", parameters, commandType: CommandType.StoredProcedure).ToList();

                        List<EntryModel> allEntries = LoadEntryModels();

                        if (matchup.WinnerId > 0)
                        {
                            matchup.Winner = allEntries.First(x => x.Id == matchup.WinnerId);
                        }

                        foreach (MatchupEntryModel matchupEntry in matchup.MatchupEntries)
                        {
                            if (matchupEntry.EntryCompetingId > 0)
                            {
                                matchupEntry.EntryCompeting = allEntries.First(x => x.Id == matchupEntry.EntryCompetingId);
                            }

                            if (matchupEntry.ParentMatchupId > 0)
                            {
                                matchupEntry.ParentMatchup = matchups.First(x => x.Id == matchupEntry.ParentMatchupId);
                            }
                        }
                    }

                    List<MatchupModel> currentRound = new List<MatchupModel>();
                    int currentRoundNumber = 1;

                    foreach (MatchupModel matchup in matchups)
                    {
                        if (matchup.MatchupRound > currentRoundNumber)
                        {
                            tournament.Rounds.Add(currentRound);
                            currentRound = new List<MatchupModel>();
                            currentRoundNumber++;
                        }

                        currentRound.Add(matchup);
                    }

                    tournament.Rounds.Add(currentRound);
                }
            }

            return output;
        }

        public void UpdateMatchup(MatchupModel matchup)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(_databaseName)))
            {
                var parameters = new DynamicParameters();
                if (matchup.Winner != null)
                {
                    parameters.Add("@Id", matchup.Id);
                    parameters.Add("@WinnerId", matchup.Winner.Id);

                    connection.Execute("dbo.spMatchups_Update", parameters, commandType: CommandType.StoredProcedure); 
                }

                foreach (MatchupEntryModel matchupEntry in matchup.MatchupEntries)
                {
                    if (matchupEntry.EntryCompeting != null)
                    {
                        parameters = new DynamicParameters();
                        parameters.Add("@Id", matchupEntry.Id);
                        parameters.Add("@EntryCompetingId", matchupEntry.EntryCompeting.Id);
                        parameters.Add("@Score", matchupEntry.Score);

                        connection.Execute("dbo.spMatchupEntries_Update", parameters, commandType: CommandType.StoredProcedure); 
                    }
                }
            }

        }

        public void UpdateTournament(TournamentModel tournament)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(_databaseName)))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", tournament.Id);
                parameters.Add("@Active", tournament.Active);
                parameters.Add("@CurrentRound", tournament.CurrentRound);

                connection.Execute("dbo.spTournaments_Update", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
