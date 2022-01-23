CREATE PROCEDURE [dbo].[spCity_Insert]
	@Name nvarchar(100)
AS
begin
	set nocount on;
	
	insert into [dbo].[City]
	([Name]) values (@Name)
end