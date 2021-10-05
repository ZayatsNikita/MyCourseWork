CREATE TABLE [dbo].[ServiceComponents]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1, 1), 
    [ComponetId] INT NOT NULL, 
    [ServiceId] INT NOT NULL
)
