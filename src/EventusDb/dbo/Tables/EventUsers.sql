CREATE TABLE [dbo].[EventUser]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(450) NOT NULL, 
    [EventId] INT NOT NULL, 
    CONSTRAINT [FK_EventUsers_Event] FOREIGN KEY (EventId) REFERENCES [dbo].[Event](Id)
)
