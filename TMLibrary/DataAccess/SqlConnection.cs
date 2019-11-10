using System.Data;
using Dapper;
using TMLibrary.Models;

namespace TMLibrary.DataAccess
{
    public class SqlConnection : IDataConnection
    {
        public void CreatePerson(PersonModel personModel)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionString("TMData")))
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
    }
}
