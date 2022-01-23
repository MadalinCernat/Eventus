CREATE PROCEDURE [dbo].[spInvitation_GetAllSentByUserId]
	@sentByUserId nvarchar(450)
AS
begin
	set nocount on;

	select i.*, e.*
	from dbo.Invitation i
	inner join dbo.[Event] e on e.Id = i.EventId
	where i.SentByUserId = @sentByUserId;
end
