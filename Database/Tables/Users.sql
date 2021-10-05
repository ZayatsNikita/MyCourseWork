CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1, 1), 
    [WorkerId] INT NOT NULL, 
    [Login] NVARCHAR(30) NOT NULL, 
    [Password] NCHAR(30) NOT NULL
)
