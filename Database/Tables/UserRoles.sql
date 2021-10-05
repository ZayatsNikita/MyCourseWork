CREATE TABLE [dbo].[UserRoles]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1, 1), 
    [RoleId] INT NOT NULL, 
    [UserId] INT NOT NULL
)
