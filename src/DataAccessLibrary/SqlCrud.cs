using Dapper;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlCrud : ISqlCrud
    {
        private readonly ISqlDataAccess _db;

        public SqlCrud(ISqlDataAccess db)
        {
            _db = db;
        }
        public Task<List<EventModel>> GetAllEvents()
        {
            return _db.LoadData<EventModel, dynamic>("dbo.spEvent_GetAll", new { }, true);
        }

        public async Task<List<PlaceModel>> GetAllPlaces()
        {

            string sql = "dbo.spPlace_GetAll";
            using (IDbConnection conn = new SqlConnection(_db.ConnectionString))
            {
                return (await conn.QueryAsync<PlaceModel, CityModel, PlaceModel>(sql, (place, city) => { place.City = city; return place; }, splitOn: "CityId", commandType: CommandType.StoredProcedure)).ToList();
            }

        }
    }
}
