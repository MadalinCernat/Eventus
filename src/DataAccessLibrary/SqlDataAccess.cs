using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly string _connectionString;

        public SqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, bool isStoredProcedure = false)
        {
            var output = new List<T>();
            CommandType commandType = CommandType.Text;
            if (isStoredProcedure)
            {
                commandType = CommandType.StoredProcedure;
            }
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                output = (await conn.QueryAsync<T>(sql, parameters, commandType: commandType)).ToList();
            }
            return output;
        }

        public async Task SaveData<T>(string sql, T parameters, bool isStoredProcedure = false)
        {
            CommandType commandType = CommandType.Text;
            if (isStoredProcedure)
            {
                commandType = CommandType.StoredProcedure;
            }
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                await conn.ExecuteAsync(sql, parameters, commandType: commandType);
            }
        }
    }
}
