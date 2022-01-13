CREATE PROCEDURE [dbo].[spEvent_GetAll]
AS
begin
	set nocount on;
	select *
	from [dbo].[Event]
end
