CREATE PROCEDURE [dbo].[spInvitation_GetAllSentByUserId]
	@sentByUserId nvarchar(450)
AS
begin
	set nocount on;

	select [i].[Id], [i].[SentByUserId], [i].[SentToUserId], [i].[EventId], [i].[Accepted], [i].[DateSent], [i].[IsActive],
		[e].[Id], [e].[Title], [e].[Description], [e].[PlaceId], [e].[StartDateTime], [e].[EndDateTime], [e].[EntranceFee],
		[e].[CreatedByUserId], [e].[DateCreated], [e].[IsActive], [e].[IsOver], [e].[Url], [e].[AllowRequests]
	from dbo.Invitation i
	inner join dbo.[Event] e on e.Id = i.EventId
	where i.SentByUserId = @sentByUserId;
end
