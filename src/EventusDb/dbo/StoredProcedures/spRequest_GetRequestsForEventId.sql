CREATE PROCEDURE [dbo].[spRequest_GetRequestsForEventId]
	@eventId int
AS
begin
	set nocount on;
	
	select [r].[Id], [r].[SentByUserId], [r].[ForEventId], [r].[RequestMessage], [r].[Date], [r].[Accepted],
		[e].[Id], [e].[Title], [e].[Description], [e].[PlaceId], [e].[StartDateTime], [e].[EndDateTime], [e].[EntranceFee],
		[e].[CreatedByUserId], [e].[DateCreated], [e].[IsActive], [e].[IsOver], [e].[Url], [e].AllowRequests
	from dbo.Request r
	inner join dbo.[Event] e on e.Id = r.ForEventId
	where r.ForEventId = @eventId;
end
