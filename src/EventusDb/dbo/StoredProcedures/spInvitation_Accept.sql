CREATE PROCEDURE [dbo].[spInvitation_Accept]
	@Id int
AS
begin
	set nocount on;

	update dbo.Invitation
	set Accepted = 1, Responded = 1, IsActive = 0
	where Id = @Id and Responded = 0
end
