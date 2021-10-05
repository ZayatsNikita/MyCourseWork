CREATE TABLE [dbo].[Components]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [Title] NCHAR(100) NOT NULL, 
    [ProductionStandards] NCHAR(100) NOT NULL, 
    [Price] DECIMAL(25, 2) NOT NULL
)
