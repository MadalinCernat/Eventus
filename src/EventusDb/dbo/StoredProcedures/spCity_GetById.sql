CREATE PROCEDURE [dbo].[spCity_GetById]
	@Id int
as
begin
	set nocount on;

	select [Id], [Name]
	from dbo.City
	where Id = @Id
end
