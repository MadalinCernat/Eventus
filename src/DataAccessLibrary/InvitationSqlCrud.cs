using Dapper;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLibrary
{
    public partial class SqlCrud
    {
        private const string invitationsSentByUserCacheKey = "invitationsSentByUser";
        private async Task<List<InvitationModel>> ExecuteInvitationCrudSql(string sql, object param = null)
        {
            using IDbConnection conn = new SqlConnection(_db.ConnectionString);
            return (await conn.QueryAsync<InvitationModel, EventModel, PlaceModel, CityModel, InvitationModel>(
                sql,
                (inv, ev, place, city) => { place.City = city; ev.Place = place; inv.Event = ev; return inv; },
                param: param ?? null,
                splitOn: "EventId,PlaceId,CityId",
                commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<List<InvitationModel>> GetAllInvitationsSentByUser(string userId)
        {
            var output = _cache.Get<List<InvitationModel>>(invitationsSentByUserCacheKey);

            if(output is null)
            {
                output = await ExecuteInvitationCrudSql("dbo.spInvitation_GetAllSentByUserId", new { SentByUserId = userId });
                _cache.Set(invitationsSentByUserCacheKey, output, TimeSpan.FromSeconds(30));
            }

            return output;
        }

        public async Task<List<InvitationModel>> GetAllInvitationsSentToUser(string userId)
        {
            return await ExecuteInvitationCrudSql("dbo.spInvitation_GetAllSentToUserId", new { SentToUserId = userId });
        }

        public async Task<List<InvitationModel>> GetInvitationById(int id)
        {
            return (await ExecuteInvitationCrudSql("dbo.spInvitation_GetById", new { Id = id }))?.ToList();
        }

        public async Task InsertInvitation(InvitationModel model)
        {
            var p = new
            {
                model.SentByUserId,
                model.SentToUserId,
                EventId = model.Event.EventId,
                model.DateSent,
                model.Message
            };
            await _db.SaveData("dbo.spInvitation_Insert", p, true);
        }

        public async Task RespondToInvitation(int id, bool accept = true)
        {
            string spName = "";
            if(accept)
            {
                spName = "dbo.spInvitation_Accept";
            }
            else
            {
                spName = "dbo.spInvitation_Reject";
            }

            using IDbConnection conn = new SqlConnection(_db.ConnectionString);

            conn.Open();

            using var trans = conn.BeginTransaction();

            try
            {
                var invitation = (await GetInvitationById(id)).First();

                await conn.ExecuteAsync(
                    spName,
                    new { Id = id },
                    transaction: trans,
                    commandType: CommandType.StoredProcedure);

                await conn.ExecuteAsync(
                    "dbo.spEventUser_Insert",
                    new { UserId = invitation.SentToUserId, EventId = invitation.Event.EventId },
                    transaction: trans,
                    commandType: CommandType.StoredProcedure);

                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
        }

    }
}