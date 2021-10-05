CREATE TABLE [dbo].[Services]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1, 1), 
    [Price] DECIMAL(25, 2) NOT NULL, 
    [Title] NCHAR(100) NOT NULL, 
    [Description] NCHAR(200) NOT NULL
)
