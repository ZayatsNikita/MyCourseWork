CREATE TABLE [dbo].[Roles]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1, 1), 
    [Title] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(200) NOT NULL
)
