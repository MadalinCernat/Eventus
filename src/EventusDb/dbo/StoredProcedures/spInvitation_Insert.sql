CREATE PROCEDURE [dbo].[spInvitation_Insert]
	@SentByUserId nvarchar(450),
	@SentToUserId nvarchar(450),
	@EventId int,
	@DateSent datetime2(7),
	@Message nvarchar(200)
AS
begin
	set nocount on;
	insert into [dbo].Invitation
	([SentByUserId], [SentToUserId], [EventId], [DateSent], [Message])
	values (@SentByUserId, @SentToUserId, @EventId, @DateSent, @Message)
end
