CREATE TABLE [dbo].[Invitation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SentByUserId] NVARCHAR(450) NOT NULL, 
    [SentToUserId] NVARCHAR(450) NOT NULL, 
    [EventId] INT NOT NULL, 
    [Accepted] BIT NOT NULL DEFAULT 0, 
    [DateSent] DATETIME2 NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [Message] NVARCHAR(200) NULL, 
    [Responded] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Invitation_Event] FOREIGN KEY (EventId) REFERENCES [dbo].[Event](Id)
)
