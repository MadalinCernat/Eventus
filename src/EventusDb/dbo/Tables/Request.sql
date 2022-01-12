CREATE TABLE [dbo].[Request]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SentByUserId] NVARCHAR(450) NOT NULL, 
    [ForEventId] INT NOT NULL, 
    [RequestMessage] NVARCHAR(200) NULL, 
    [Date] DATETIME2 NOT NULL, 
    [Accepted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Request_Event] FOREIGN KEY (ForEventId) REFERENCES [dbo].[Event](Id)
)
