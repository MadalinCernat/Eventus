CREATE PROCEDURE [dbo].[spInvitation_Reject]
	@Id int
AS
begin
	set nocount on;

	update dbo.Invitation
	set Responded = 1, IsActive = 0
	where Id = @Id and Responded = 0

end
