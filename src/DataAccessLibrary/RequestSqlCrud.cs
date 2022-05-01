using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccessLibrary
{
    public  partial class SqlCrud
    {
        private const string eventRequestsCacheKey = "eventRequests";
        private const string userRequestsCacheKey = "userRequests";
        private async Task<List<RequestModel>> ExecuteRequestCrudSql(string sql, object param = null)
        {
            using IDbConnection conn = new SqlConnection(_db.ConnectionString);
            return (await conn.QueryAsync<RequestModel, EventModel, PlaceModel, CityModel, RequestModel>(sql,
                (req, ev, pl, c) => { pl.City = c; ev.Place = pl; req.Event = ev; return req; },
                param ?? null,
                splitOn: "EventId,PlaceId,CityId",
                commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<List<RequestModel>> GetAllRequestsForEventId(int eventId)
        {
            var output = _cache.Get<List<RequestModel>>($"{eventRequestsCacheKey}_{eventId}");

            if (output is null)
            {
                output = await ExecuteRequestCrudSql("dbo.spRequest_GetRequestsForEventId", new { EventId = eventId });
                _cache.Set($"{eventRequestsCacheKey}_{eventId}", output, TimeSpan.FromSeconds(30));
            }

            return output;
        }

        public async Task<List<RequestModel>> GetAllRequestsSentByUserId(string userId)
        {
            var output = _cache.Get<List<RequestModel>>($"{userRequestsCacheKey}_{userId}");

            if (output is null)
            {
                output = await ExecuteRequestCrudSql("dbo.spRequest_GetRequestsSentByUserId", new { SentByUserId = userId });
                _cache.Set($"{userRequestsCacheKey}_{userId}", output, TimeSpan.FromSeconds(30));
            }

            return output;
        }

        public async Task InsertRequest(RequestModel model)
        {
            var p = new
            {
                SentByUserId = model.SentByUserId,
                ForEventId = model.Event.EventId,
                RequestMessage = model.RequestMessage,
                Date = model.Date
            };
            await _db.SaveData("dbo.spRequest_Insert", p, true);
        }
    }
}