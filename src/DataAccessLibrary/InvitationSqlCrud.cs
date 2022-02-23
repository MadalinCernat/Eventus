using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public partial class SqlCrud
    {
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
    }
}