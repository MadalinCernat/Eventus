CREATE PROCEDURE [dbo].[spCity_GetAll]
AS
begin
	set nocount on;

	select *
	from dbo.City

end