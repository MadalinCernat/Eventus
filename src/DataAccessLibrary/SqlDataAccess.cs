using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        public string ConnectionString { get; set; }

        public SqlDataAccess(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("Default");
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, bool isStoredProcedure = false)
        {
            var output = new List<T>();
            CommandType commandType = isStoredProcedure
                ? CommandType.StoredProcedure
                : CommandType.Text;
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            output = (await conn.QueryAsync<T>(sql, parameters, commandType: commandType)).ToList();
            return output;
        }

        public async Task SaveData<T>(string sql, T parameters, bool isStoredProcedure = false)
        {
            CommandType commandType = isStoredProcedure
                ? CommandType.StoredProcedure
                : CommandType.Text;
            using IDbConnection conn = new SqlConnection(ConnectionString);
            await conn.ExecuteAsync(sql, parameters, commandType: commandType);
        }
    }
}
