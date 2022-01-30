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
        public async Task<PlaceModel> GetPlaceById(int id)
        {
            string sql = $"dbo.spPlace_GetById";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (IDbConnection conn = new SqlConnection(_db.ConnectionString))
            {
                return (await conn.QueryAsync<PlaceModel, CityModel, PlaceModel>(
                    sql,
                    (place, city) => { place.City = city; return place; },
                    parameters,
                    splitOn: "CityId",
                    commandType: CommandType.StoredProcedure
                    )).FirstOrDefault();
            }
        }
        public async Task<List<PlaceModel>> GetAllPlaces()
        {

            string sql = "dbo.spPlace_GetAll";
            using (IDbConnection conn = new SqlConnection(_db.ConnectionString))
            {
                return (await conn.QueryAsync<PlaceModel, CityModel, PlaceModel>(
                    sql,
                    (place, city) => { place.City = city; return place; },
                    splitOn: "CityId",
                    commandType: CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task InsertEvent(EventModel model)
        {
            string sql = "dbo.spEvent_Insert";

            var p = new
            {
                Title = model.Title,
                Description = model.Description,
                PlaceId = model.Place.Id,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
                EntranceFee = model.EntranceFee,
                CreatedByUserId = model.CreatedByUserId,
                DateCreated = model.DateCreated,
                Url = model.Url,
                AllowRequests = 1
            };

            await _db.SaveData(sql, p, true);
        }
    }
}
