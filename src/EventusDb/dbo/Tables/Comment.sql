CREATE TABLE [dbo].[Comment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Content] NVARCHAR(200) NOT NULL, 
    [PostedByUserId] NVARCHAR(450) NOT NULL, 
    [DatePosted] DATETIME2 NOT NULL, 
    [EventId] INT NOT NULL, 
    CONSTRAINT [FK_Comment_Event] FOREIGN KEY (EventId) REFERENCES [dbo].[Event](Id)
)
