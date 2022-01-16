CREATE PROCEDURE [dbo].[spEvent_GetById]
	@Id int
AS
begin
	set nocount on;

	select [e].[Id], [e].[Title], [e].[Description], [e].[StartDate], [e].[EndDate], [e].[EntranceTax], [e].[CreatedByUserId],
		   [e].[DateCreated], [e].[IsActive], [e].[IsOver], [e].[Url], [p].Id, [p].[Name], [c].[Id], [c].[Name]
	from dbo.[Event] e
	inner join dbo.Place p on p.Id = e.PlaceId
	inner join dbo.City c on c.Id = p.CityId
	where e.Id = @Id;
end
