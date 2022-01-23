CREATE PROCEDURE [dbo].[spInvitation_GetAllSentToUserId]
	@sentToUserId nvarchar(450)
AS
begin
	set nocount on;

	select i.*, e.*
	from dbo.Invitation i
	inner join dbo.[Event] e on e.Id = i.EventId
	where i.SentToUserId = @sentToUserId;
end
