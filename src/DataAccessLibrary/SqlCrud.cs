using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccessLibrary.Models;

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
        private async Task<List<EventModel>> ExecuteEventCrudSql(string sql, object param = null)
        {
            using IDbConnection conn = new SqlConnection(_db.ConnectionString);
            return (await conn.QueryAsync<EventModel, PlaceModel, CityModel, EventModel>(
                sql,
                (ev, place, city) => { place.City = city; ev.Place = place; return ev; },
                param: param ?? "",
                splitOn: "PlaceId,CityId",
                commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<EventModel>> GetAllEvents()
        {
            return await ExecuteEventCrudSql("dbo.spEvent_GetAll");
        }
        public async Task<EventModel> GetEventById(int id)
        {
            return (await ExecuteEventCrudSql("dbo.spEvent_GetById", new { Id = id }))?.FirstOrDefault();
        }

        public async Task<List<EventModel>> GetEventsByUserId(string userId)
        {
            return await ExecuteEventCrudSql("dbo.spEvent_GetAllCreatedByUserId", new { CreatedByUserId = userId });
        }

        public async Task InsertEvent(EventModel model)
        {
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
            await _db.SaveData("dbo.spEvent_Insert", p, true);
        }
        #endregion

        #region Place CRUD
        private async Task<List<PlaceModel>> ExecutePlaceCrudSql(string sql, object param = null)
        {
            using IDbConnection conn = new SqlConnection(_db.ConnectionString);
            return (await conn.QueryAsync<PlaceModel, CityModel, PlaceModel>(
                sql,
                (place, city) => { place.City = city; return place; },
                splitOn: "CityId",
                commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<PlaceModel>> GetAllPlaces()
        {
            return await ExecutePlaceCrudSql("dbo.spPlace_GetAll");
        }

        public async Task<PlaceModel> GetPlaceById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id);
            return (await ExecutePlaceCrudSql("dbo.spPlace_GetById", parameters))?.FirstOrDefault();
        }

        public async Task InsertPlace(PlaceModel model)
        {
            var p = new
            {
                Name = model.Name,
                CityId = model.City.Id
            };
            await _db.SaveData("dbo.spPlace_Insert", p, true);
        }

        #endregion

        #region City CRUD
        public async Task<List<CityModel>> GetAllCities()
        {
            return await _db.LoadData<CityModel, dynamic>("dbo.spCity_GetAll", new { }, true);
        }

        public async Task<CityModel> GetCityById(int id)
        {
            return (await _db.LoadData<CityModel, dynamic>("dbo.spCity_GetById", new { Id = id }, true)).SingleOrDefault();
        }

        public async Task InsertCity(CityModel model)
        {
            var p = new
            {
                Name = model.Name
            };
            await _db.SaveData("dbo.spCity_Insert", p, true);
        }
        #endregion

        #region Invitation CRUD
        public async Task<List<InvitationModel>> GetAllInvitationsSentByUser(string userId)
        {
            return (await _db.LoadData<InvitationModel, dynamic>("dbo.spInvitation_GetAllSentByUserId", new { SentByUserId = userId }, true))?.ToList();
        }

        public async Task<List<InvitationModel>> GetAllInvitationsSentToUser(string userId)
        {
            return (await _db.LoadData<InvitationModel, dynamic>("dbo.spInvitation_GetAllSentToUserId", new { SentToUserId = userId }, true))?.ToList();
        }

        public async Task InsertInvitation(InvitationModel model)
        {
            var p = new
            {
                model.SentByUserId,
                model.SentToUserId,
                EventId = model.Event.Id,
                model.DateSent,
                model.Message
            };
            await _db.SaveData("dbo.spInvitation_Insert", p, true);
        }
        #endregion

        #region Request CRUD
        private async Task<List<RequestModel>> ExecuteRequestCrudSql(string sql, object param = null)
        {
            using IDbConnection conn = new SqlConnection(_db.ConnectionString);
            return (await conn.QueryAsync<RequestModel, EventModel, PlaceModel, CityModel, RequestModel>(sql,
                (req, ev, pl, c) => { pl.City = c; ev.Place = pl; req.Event = ev; return req; },
                param ?? "",
                splitOn: "EventId,PlaceId,CityId",
                commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<RequestModel>> GetAllRequestsForEventId(int eventId)
        {
            return await ExecuteRequestCrudSql("dbo.spRequest_GetRequestsForEventId", new { EventId = eventId });
        }

        public async Task<List<RequestModel>> GetAllRequestsSentByUserId(string userId)
        {
            return await ExecuteRequestCrudSql("dbo.spRequest_GetRequestsSentByUserId", new { SentByUserId = userId });
        }

        public async Task InsertRequest(RequestModel model)
        {
            var p = new
            {
                SentByUserId = model.SentByUserId,
                ForEventId = model.Event.Id,
                RequestMessage = model.RequestMessage,
                Date = model.Date
            };
            await _db.SaveData("dbo.spRequest_Insert", p, true);
        }

        #endregion
    }
}
