CREATE PROCEDURE [dbo].[spEvent_GetAllCreatedByUserId]
	@createdByUserId nvarchar(450)
AS

begin
	set nocount on;

	select [Id], [Title], [Description], [PlaceId], [StartDateTime], [EndDateTime], [EntranceFee], [CreatedByUserId],
		[DateCreated], [IsActive], [IsOver], [Url], [AllowRequests]
	from dbo.[Event]
	where CreatedByUserId = @createdByUserId;
end
