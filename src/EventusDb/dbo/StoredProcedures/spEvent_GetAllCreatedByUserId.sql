﻿CREATE PROCEDURE [dbo].[spEvent_GetAllCreatedByUserId]
	@createdByUserId nvarchar(450)
AS

begin
	set nocount on;

	select [e].[Id] as 'EventId'
		, [e].[Title]
		, [e].[Description]
		, [e].[StartDateTime]
		, [e].[EndDateTime]
		, [e].[EntranceFee]
		, [e].[CreatedByUserId]
		, [e].[DateCreated]
		, [e].[IsActive]
		, [e].[Url]
		, [e].[AllowRequests]
		, [p].[Id] as 'PlaceId'
		, [p].[Name]
		, [c].[Id] as 'CityId'
		, [c].[Name]
	from dbo.[Event] e
	inner join [dbo].[Place] p on p.Id = e.PlaceId
	inner join [dbo].[City] c on c.Id = p.CityId
	where CreatedByUserId = @createdByUserId;
end
