CREATE TABLE [dbo].[Event]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(200) NOT NULL, 
    [Description] NVARCHAR(1000) NULL, 
    [PlaceId] INT NULL, 
    [StartDateTime] DATETIME2 NOT NULL, 
    [EndDateTime] DATETIME2 NOT NULL, 
    [EntranceFee] MONEY NULL, 
    [CreatedByUserId] NVARCHAR(450) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsOver] BIT NOT NULL DEFAULT 0, 
    [Url] NVARCHAR(1000) NULL, 
    [AllowRequests] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Event_Place] FOREIGN KEY (PlaceId) REFERENCES [dbo].[Place](Id)
)
