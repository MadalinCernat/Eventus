CREATE PROCEDURE [dbo].[spPlace_GetById]
	@Id int
AS
begin
	set nocount on;

	select [p].[Id], [p].[Name], [c].[Id] as 'CityId', [c].[Name]
	from dbo.Place p
	inner join dbo.City c on c.Id = p.CityId
	where p.Id = @Id
end