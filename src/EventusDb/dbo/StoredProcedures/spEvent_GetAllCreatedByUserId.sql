CREATE PROCEDURE [dbo].[spEvent_GetAllCreatedByUserId]
	@createdByUserId nvarchar(450)
AS

begin
	set nocount on;

	select [Id], [Title], [Description], [PlaceId], [StartDate], [EndDate], [EntranceTax], [CreatedByUserId],
		[DateCreated], [IsActive], [IsOver], [Url]
	from dbo.[Event]
	where CreatedByUserId = @createdByUserId;
end
