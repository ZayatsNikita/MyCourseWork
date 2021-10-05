CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [ClientId] INT NOT NULL, 
    [MasterId] INT NOT NULL, 
    [ManagerId] INT NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [CompletionDate] DATETIME NULL,
)
