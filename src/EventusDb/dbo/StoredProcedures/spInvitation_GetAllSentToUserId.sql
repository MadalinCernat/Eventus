CREATE PROCEDURE [dbo].[spInvitation_GetAllSentToUserId]
	@sentToUserId nvarchar(450)
AS
begin
	set nocount on;

	select [i].[Id]
		, [i].[SentByUserId]
		, [i].[SentToUserId]
		, [i].[Accepted]
		, [i].[Message]
		, [i].[DateSent]
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
	where i.SentToUserId = @sentToUserId;
end
