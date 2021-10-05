Insert into Clients(Title, ContactInformation)
values
('Zayats Mikita', '70-957'),
('Kiril Sergeev','34-123');

Insert into Components(Title, ProductionStandards,Price)
values
('GTX 1080', 'GDDR 5 18 Gb', 10),
('I7 7700HQ', '14 nm', 7);

Insert into Services (Price, Title, Description)
values
(10, 'Instaling', 'Adding a component to the system'),
(15.5, 'Removing', 'Removing a component from the system');

Insert into ServiceComponents (ServiceId, ComponetId)
values
(1, 1),
(1, 2),
(2, 1),
(2, 2);

insert into Workers(PassportNumber, PersonalData)
values
(2201207, 'Viktor Nikolaevich'),
(4321123, 'Nikolay Vladimirovich'),
(1427247, 'Dmtriy Puchkov');

insert into Users (WorkerId, Login, Password)
values 
((select PassportNumber from Workers where PersonalData = 'Viktor Nikolaevich'), 'Login', 'Password'),
((select PassportNumber from Workers where PersonalData = 'Nikolay Vladimirovich'), 'Login1', 'Password1'),
((select PassportNumber from Workers where PersonalData = 'Dmtriy Puchkov'), 'Login2', 'Password2');

insert into Roles(Title, Description)
values
('Admin', 'Main user'),
('Master', 'Performs repair work'),
('Manager', 'Performs orders managment'),
('Director', 'Look to the statistic');

insert into UserRoles (UserId, RoleId)
values
((select id from Users where Login = 'Login'), (select id from Roles where Title = 'Admin')),
((select id from Users where Login = 'Login'), (select id from Roles where Title = 'Master')),
((select id from Users where Login = 'Login'), (select id from Roles where Title = 'Manager')),
((select id from Users where Login = 'Login'), (select id from Roles where Title = 'Director'));

insert into Orders(ClientId, MasterId, ManagerId, StartDate)
values
(1, 1, 1, (select GETUTCDATE()));

insert into OrdersInfo(OrderNumber, ServiceId, CountOfServicesRendered)
values
(1, 1, 3),
(1, 2, 4),
(1, 2, 5);