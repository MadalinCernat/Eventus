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
        #region Event CRUD
        public async Task<List<EventModel>> GetAllEvents()
        {
            string sql = "dbo.spEvent_GetAll";
            using (IDbConnection conn = new SqlConnection(_db.ConnectionString))
            {
                return (await conn.QueryAsync<EventModel, PlaceModel, CityModel, EventModel>(
                    sql,
                    (ev, place, city) => { place.City = city; ev.Place = place; return ev; },
                    splitOn: "PlaceId,CityId",
                    commandType: CommandType.StoredProcedure)).ToList();
            }
        }
        public async Task<EventModel> GetEventById(int id)
        {
            string sql = "dbo.spEvent_GetById";
            using (IDbConnection conn = new SqlConnection(_db.ConnectionString))
            {
                return (await conn.QueryAsync<EventModel, PlaceModel, CityModel, EventModel>(
                    sql,
                    (ev, place, city) => { place.City = city; ev.Place = place; return ev; },
                    new { Id = id },
                    splitOn: "PlaceId,CityId",
                    commandType: CommandType.StoredProcedure)).SingleOrDefault();
            }
        }

        public async Task<List<EventModel>> GetEventsByUserId(string userId)
        {
            var sql = "dbo.spEvent_GetAllCreatedByUserId";
            using (IDbConnection conn = new SqlConnection(_db.ConnectionString))
            {
                var output =  (await conn.QueryAsync<EventModel, PlaceModel, CityModel, EventModel>(
                    sql,
                    (ev, place, city) => { place.City = city; ev.Place = place; return ev; },
                    param: new { CreatedByUserId = userId },
                    splitOn: "PlaceId,CityId",
                    commandType: CommandType.StoredProcedure)).ToList();

                return output;
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
        #endregion

        #region Place CRUD
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

        public async Task InsertPlace(PlaceModel model)
        {
            string sql = "dbo.spPlace_Insert";
            var p = new
            {
                Name = model.Name,
                CityId = model.City.Id
            };

            await _db.SaveData(sql, p, true);
        }

        #endregion

        #region City CRUD
        public async Task<List<CityModel>> GetAllCities()
        {
            var sql = "dbo.spCity_GetAll";

            return await _db.LoadData<CityModel, dynamic>(sql, new { }, true);
        }

        public async Task<CityModel> GetCityById(int id)
        {
            var sql = "dbo.spCity_GetById";

            return (await _db.LoadData<CityModel, dynamic>(sql, new { Id = id }, true)).SingleOrDefault() ;
        }

        public async Task InsertCity(CityModel model)
        {
            var sql = "dbo.spCity_Insert";

            var p = new
            {
                Name = model.Name
            };

            await _db.SaveData(sql, p, true);
        }
        #endregion

        #region
        public async Task<List<InvitationModel>> GetAllInvitationsSentByUser(string userId)
        {
            var sql = "dbo.spInvitation_GetAllSentByUserId";

            var output = await _db.LoadData<InvitationModel, dynamic>(sql, new { SentByUserId = userId }, true);

            return output.ToList();
        }

        public async Task<List<InvitationModel>> GetAllInvitationsSentToUser(string userId)
        {
            var sql = "dbo.spInvitation_GetAllSentToUserId";

            var output = await _db.LoadData<InvitationModel, dynamic>(sql, new { SentToUserId = userId }, true);

            return output.ToList();
        }
        public async Task InsertInvitation(InvitationModel model)
        {
            var sql = "dbo.spInvitation_Insert";

            var p = new
            {
                model.SentByUserId, 
                model.SentToUserId,
                EventId = model.Event.Id,
                model.DateSent,
                model.Message
            };

            await _db.SaveData(sql, p, true);
        }


        #endregion
    }
}
