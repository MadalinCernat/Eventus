CREATE PROCEDURE [dbo].[spComment_Insert]
	@Content nvarchar(200),
	@PostedByUserId nvarchar(450),
	@DatePosted datetime2(7),
	@EventId int
as
begin
	set nocount on;
	insert into [dbo].Comment
	([Content], [PostedByUserId], [DatePosted], [EventId])
	values (@Content, @PostedByUserId, @DatePosted, @EventId)
end