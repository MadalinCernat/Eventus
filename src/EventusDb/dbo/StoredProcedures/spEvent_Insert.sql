CREATE PROCEDURE [dbo].[spEvent_Insert]
	@Title nvarchar(200),
	@Description nvarchar(1000),
	@PlaceId int,
	@StartDateTime datetime2(7),
	@EndDateTime datetime2(7),
	@EntranceFee money,
	@CreatedByUserId nvarchar(450),
	@DateCreated datetime2(7),
	@Url nvarchar(1000),
	@AllowRequests bit
AS
begin
	set nocount on;
	insert into [dbo].[Event]
	([Title], [Description], [PlaceId], [StartDateTime], [EndDateTime], [EntranceFee], [CreatedByUserId], [DateCreated], [Url], [AllowRequests])
	values (@Title, @Description, @PlaceId, @StartDateTime, @EndDateTime, @EntranceFee, @CreatedByUserId, @DateCreated, @Url, @AllowRequests)
end
