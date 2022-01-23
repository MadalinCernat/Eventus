CREATE PROCEDURE [dbo].[spEventUser_Insert]
	@UserId nvarchar(450),
	@EventId int
as
begin
	set nocount on;
	insert into [dbo].EventUser
	([UserId], [EventId])
	values (@UserId, @EventId)
end
