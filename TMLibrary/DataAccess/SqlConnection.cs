using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using TMLibrary.Models;

namespace TMLibrary.DataAccess
{
    public class SqlConnection : IDataConnection
    {
        private const string databaseName = "TMData";

        public void CreatePerson(PersonModel newPerson)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(databaseName)))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nickname", newPerson.Nickname);
                parameters.Add("@FirstName", newPerson.FirstName);
                parameters.Add("@LastName", newPerson.LastName);
                parameters.Add("@DiscordTag", newPerson.DiscordTag);
                parameters.Add("@EmailAddress", newPerson.EmailAddress);
                parameters.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", parameters, commandType: CommandType.StoredProcedure);

                newPerson.Id = parameters.Get<int>("@Id");
            }
        }

        public void CreateEntry(EntryModel newEntry)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(databaseName)))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EntryName", newEntry.EntryName);
                parameters.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

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

        public List<PersonModel> LoadPersonModels()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(databaseName)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }

        public List<EntryModel> LoadEntryModels()
        {
            List<EntryModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(databaseName)))
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
    }
}
