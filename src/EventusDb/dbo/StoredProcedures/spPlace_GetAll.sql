CREATE PROCEDURE [dbo].[spPlace_GetAll]
AS
begin
	set nocount on;

	select [p].[Id], [p].[Name], [c].[Id] as 'CityId', [c].[Name]
	from dbo.Place p
	inner join dbo.City c on c.Id = p.CityId
end