CREATE PROCEDURE [dbo].[spInvitation_GetById]
	@Id int
AS
begin
	set nocount on;

	select [i].[Id]
		, [i].[SentByUserId]
		, [i].[SentToUserId]
		, [i].[Accepted]
		, [i].[DateSent]
		, [i].[Message]
		, [i].[IsActive]
		, [e].[Id] as 'EventId'
		, [e].[Title]
		, [e].[Description]
		, [e].[PlaceId]
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
	from [dbo].[Invitation] i
	inner join [dbo].[Event] e on e.Id = i.EventId
	inner join [dbo].[Place] p on p.Id = e.PlaceId
	inner join [dbo].[City] c on c.Id = p.CityId
	where i.Id = @Id
end