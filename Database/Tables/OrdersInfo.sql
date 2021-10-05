CREATE TABLE [dbo].[OrdersInfo]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [OrderNumber] int not null, 
    [ServiceId] INT NOT NULL, 
    [CountOfServicesRendered] INT NOT NULL,
)
