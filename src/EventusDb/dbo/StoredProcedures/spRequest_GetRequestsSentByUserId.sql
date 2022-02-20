CREATE PROCEDURE [dbo].[spRequest_GetRequestsSentByUserId]
	@sentByUserId nvarchar(450)
AS
begin
	set nocount on;

	select [r].[Id]
		, [r].[SentByUserId]
		, [r].[ForEventId]
		, [r].[RequestMessage]
		, [r].[Date]
		, [r].[Accepted]
		, [e].[Id] as 'EventId'
		, [e].[Title]
		, [e].[Description]
		, [e].[StartDateTime]
		, [e].[EndDateTime]
		, [e].[EntranceFee]
		, [e].[CreatedByUserId]
		, [e].[DateCreated]
		, [e].[IsActive]
		, [e].[Url]
		, [e].[AllowRequests]
		, [p].[Id] as 'PlaceId'
		, [p].[Name]
		, [c].[Id] as 'CityId'
		, [c].[Name]
	from dbo.Request r
	inner join [dbo].[Event] e on e.Id = r.ForEventId
	inner join [dbo].[Place] p on p.Id = e.PlaceId
	inner join [dbo].[City] c on c.Id = p.CityId
	where SentByUserId = @sentByUserId;
end
