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

        public void CreatePerson(PersonModel personModel)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(databaseName)))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nickname", personModel.Nickname);
                parameters.Add("@FirstName", personModel.FirstName);
                parameters.Add("@LastName", personModel.LastName);
                parameters.Add("@DiscordTag", personModel.DiscordTag);
                parameters.Add("@EmailAddress", personModel.EmailAddress);
                parameters.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", parameters, commandType: CommandType.StoredProcedure);

                personModel.Id = parameters.Get<int>("@Id");
            }
        }

        public List<PersonModel> LoadPersonModels()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString(databaseName)))
            {
                output = connection.Query<PersonModel>("spPeople_GetAll").ToList();
            }

            return output;
        }
    }
}
