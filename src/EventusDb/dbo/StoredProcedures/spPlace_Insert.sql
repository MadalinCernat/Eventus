CREATE PROCEDURE [dbo].[spPlace_Insert]
	@Name nvarchar(100),
	@CityId int
as
begin
	set nocount on;
	insert into [dbo].[Place]
	([Name], [CityId])
	values (@Name, @CityId)
end