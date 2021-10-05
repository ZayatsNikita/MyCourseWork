CREATE TABLE [dbo].[Clients]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1, 1), 
    [Title] NCHAR(50) NOT NULL, 
    [ContactInformation] NCHAR(100) NOT NULL
)
