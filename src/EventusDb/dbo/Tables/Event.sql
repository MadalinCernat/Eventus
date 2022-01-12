CREATE TABLE [dbo].[Event]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(200) NOT NULL, 
    [Description] NVARCHAR(1000) NULL, 
    [PlaceId] INT NULL, 
    [StartDate] DATETIME2 NOT NULL, 
    [EndDate] DATETIME2 NOT NULL, 
    [EntranceTax] MONEY NULL, 
    [CreatedByUserId] NVARCHAR(450) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_Event_Place] FOREIGN KEY (PlaceId) REFERENCES [dbo].[Place](Id)
)
