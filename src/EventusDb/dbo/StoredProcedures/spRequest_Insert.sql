CREATE PROCEDURE [dbo].[spRequest_Insert]
	@SentByUserId nvarchar(450),
	@ForEventId int,
	@RequestMessage nvarchar(200),
	@Date datetime2(7)
AS
begin
	set nocount on;

	insert into [dbo].[Request]
	([SentByUserId], [ForEventId], [RequestMessage], [Date])
	values (@SentByUserId, @ForEventId, @RequestMessage, @Date)
end
