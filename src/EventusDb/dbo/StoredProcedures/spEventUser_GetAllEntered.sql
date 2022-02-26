CREATE PROCEDURE [dbo].[spEventUser_GetAllEntered]
	@UserId nvarchar(450)

as
begin
	set nocount on;

	select [eu].[Id]
		, [eu].[UserId]
		, [eu].[EventId]
		, [e].[Id]
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
	from [dbo].[EventUser] eu
	inner join [dbo].[Event] e on e.Id = eu.EventId
	inner join [dbo].[Place] p on p.Id = e.PlaceId
	inner join [dbo].[City] c on c.Id = p.CityId
	where eu.UserId = @UserId and e.IsActive = 1

end
