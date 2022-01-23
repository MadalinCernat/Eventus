CREATE PROCEDURE [dbo].[spUserCity_Insert]
	@UserId nvarchar(450),
	@CityId int
as
begin
	set nocount on;
	insert into [dbo].[UserCity]
	([UserId], [CityId])
	values (@UserId, @CityId)
end
