using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public  partial class SqlCrud
    {
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
    }
}