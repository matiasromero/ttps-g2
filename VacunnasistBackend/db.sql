IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE TABLE [DevelopedVaccines] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [IsActive] bit NOT NULL,
        [VaccineType] nvarchar(max) NOT NULL,
        [DaysToDelivery] int NOT NULL,
        [VaccineText] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_DevelopedVaccines] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE TABLE [Province] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(max) NOT NULL,
        [Name] nvarchar(80) NOT NULL,
        CONSTRAINT [PK_Province] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [PasswordHash] nvarchar(max) NULL,
        [UserName] nvarchar(20) NOT NULL,
        [FullName] nvarchar(100) NOT NULL,
        [Email] nvarchar(100) NOT NULL,
        [Address] nvarchar(200) NOT NULL,
        [Province] nvarchar(max) NOT NULL,
        [BirthDate] datetime2 NOT NULL,
        [Pregnant] bit NOT NULL,
        [HealthWorker] bit NOT NULL,
        [DNI] nvarchar(20) NOT NULL,
        [Gender] nvarchar(50) NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE TABLE [PurchaseOrders] (
        [Id] int NOT NULL IDENTITY,
        [PurchaseDate] datetime2 NOT NULL,
        [ETA] datetime2 NULL,
        [BatchNumber] nvarchar(20) NULL,
        [DeliveredTime] datetime2 NULL,
        [DevelopedVaccineId] int NOT NULL,
        [Quantity] int NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PurchaseOrders_DevelopedVaccines_DevelopedVaccineId] FOREIGN KEY ([DevelopedVaccineId]) REFERENCES [DevelopedVaccines] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE TABLE [Departments] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [ProvinceId] int NOT NULL,
        CONSTRAINT [PK_Departments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Departments_Province_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Province] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE TABLE [BatchVaccines] (
        [Id] int NOT NULL IDENTITY,
        [DueDate] datetime2 NOT NULL,
        [BatchNumber] nvarchar(20) NOT NULL,
        [DevelopedVaccineId] int NOT NULL,
        [PurchaseOrderId] int NOT NULL,
        [Quantity] int NOT NULL,
        [RemainingQuantity] int NOT NULL,
        [OverdueQuantity] int NOT NULL,
        [Status] int NOT NULL,
        [Synchronized] bit NOT NULL,
        CONSTRAINT [PK_BatchVaccines] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BatchVaccines_DevelopedVaccines_DevelopedVaccineId] FOREIGN KEY ([DevelopedVaccineId]) REFERENCES [DevelopedVaccines] ([Id]),
        CONSTRAINT [FK_BatchVaccines_PurchaseOrders_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrders] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE TABLE [Patients] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Surname] nvarchar(max) NOT NULL,
        [DNI] nvarchar(max) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [DepartmentId] int NOT NULL,
        [BirthDate] nvarchar(max) NOT NULL,
        [Pregnant] bit NOT NULL,
        [HealthWorker] bit NOT NULL,
        CONSTRAINT [PK_Patients] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Patients_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE TABLE [LocalBatchVaccines] (
        [Id] int NOT NULL IDENTITY,
        [BatchVaccineId] int NOT NULL,
        [Quantity] int NOT NULL,
        [RemainingQuantity] int NOT NULL,
        [OverdueQuantity] int NOT NULL,
        [Province] nvarchar(50) NOT NULL,
        [DistributionDate] datetime2 NOT NULL,
        [Synchronized] bit NOT NULL,
        CONSTRAINT [PK_LocalBatchVaccines] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LocalBatchVaccines_BatchVaccines_BatchVaccineId] FOREIGN KEY ([BatchVaccineId]) REFERENCES [BatchVaccines] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE TABLE [AppliedVaccines] (
        [Id] int NOT NULL IDENTITY,
        [AppliedDate] datetime2 NOT NULL,
        [PatientId] int NOT NULL,
        [UserId] int NOT NULL,
        [LocalBatchVaccineId] int NOT NULL,
        [AppliedDose] int NOT NULL,
        [Synchronized] bit NOT NULL,
        CONSTRAINT [PK_AppliedVaccines] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AppliedVaccines_LocalBatchVaccines_LocalBatchVaccineId] FOREIGN KEY ([LocalBatchVaccineId]) REFERENCES [LocalBatchVaccines] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AppliedVaccines_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AppliedVaccines_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DaysToDelivery', N'IsActive', N'Name', N'VaccineText', N'VaccineType') AND [object_id] = OBJECT_ID(N'[DevelopedVaccines]'))
        SET IDENTITY_INSERT [DevelopedVaccines] ON;
    EXEC(N'INSERT INTO [DevelopedVaccines] ([Id], [DaysToDelivery], [IsActive], [Name], [VaccineText], [VaccineType])
    VALUES (1, 30, CAST(1 AS bit), N''Pfizer'', N''{"Id":2000,"Name":"COVID-19","ShortName":"anticovid","Type":1,"Doses":[{"Id":2001,"Number":0,"IsReinforcement":false,"MinMonthsOfAge":0,"DaysAfterPreviousDose":null},{"Id":2002,"Number":1,"IsReinforcement":false,"MinMonthsOfAge":0,"DaysAfterPreviousDose":120}]}'', N''ARNM''),
    (2, 60, CAST(1 AS bit), N''ROCHE'', N''{"Id":1300,"Name":"Fiebre Amarilla","ShortName":"fiebreamarilla","Type":0,"Doses":[{"Id":1301,"Number":0,"IsReinforcement":false,"MinMonthsOfAge":18,"DaysAfterPreviousDose":null},{"Id":1302,"Number":1,"IsReinforcement":true,"MinMonthsOfAge":132,"DaysAfterPreviousDose":null}]}'', N''subunidades proteicas''),
    (3, 15, CAST(1 AS bit), N''Janssen'', N''{"Id":3000,"Name":"Antigripal","ShortName":"antigripal","Type":2,"Doses":[{"Id":3001,"Number":0,"IsReinforcement":false,"MinMonthsOfAge":0,"DaysAfterPreviousDose":365}]}'', N''Vector viral'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DaysToDelivery', N'IsActive', N'Name', N'VaccineText', N'VaccineType') AND [object_id] = OBJECT_ID(N'[DevelopedVaccines]'))
        SET IDENTITY_INSERT [DevelopedVaccines] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Province]'))
        SET IDENTITY_INSERT [Province] ON;
    EXEC(N'INSERT INTO [Province] ([Id], [Code], [Name])
    VALUES (1, N''02'', N''Ciudad Autónoma de Buenos Aires''),
    (2, N''06'', N''Buenos Aires''),
    (3, N''10'', N''Catamarca''),
    (4, N''14'', N''Córdoba''),
    (5, N''18'', N''Corrientes''),
    (6, N''22'', N''Chaco''),
    (7, N''26'', N''Chubut''),
    (8, N''30'', N''Entre Ríos''),
    (9, N''34'', N''Formosa''),
    (10, N''38'', N''Jujuy''),
    (11, N''42'', N''La Pampa''),
    (12, N''46'', N''La Rioja''),
    (13, N''50'', N''Mendoza''),
    (14, N''54'', N''Misiones''),
    (15, N''58'', N''Neuquén''),
    (16, N''62'', N''Río Negro''),
    (17, N''66'', N''Salta''),
    (18, N''70'', N''San Juan''),
    (19, N''74'', N''San Luis''),
    (20, N''78'', N''Santa Cruz''),
    (21, N''82'', N''Santa Fe''),
    (22, N''86'', N''Santiago del Estero''),
    (23, N''90'', N''Tucumán''),
    (24, N''94'', N''Tierra del Fuego, Antártida e Islas del Atlántico Sur'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name') AND [object_id] = OBJECT_ID(N'[Province]'))
        SET IDENTITY_INSERT [Province] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BirthDate', N'DNI', N'Email', N'FullName', N'Gender', N'HealthWorker', N'IsActive', N'PasswordHash', N'Pregnant', N'Province', N'Role', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [Address], [BirthDate], [DNI], [Email], [FullName], [Gender], [HealthWorker], [IsActive], [PasswordHash], [Pregnant], [Province], [Role], [UserName])
    VALUES (1, N''Calle Falsa 1234, La Plata'', ''2022-12-19T00:00:00.0000000-03:00'', N''11111111'', N''admin@vacunassist.com'', N''Administrador'', N''other'', CAST(0 AS bit), CAST(1 AS bit), N''1000:b/6nZon7alCkYVV2jwB+sPCJ/T6sr/r/:/p30BF6KvHKiHDp5E/uAswc+//LPi+Ov'', CAST(0 AS bit), N''Buenos Aires'', N''administrator'', N''Admin''),
    (2, N''Calle Falsa 2345, La Plata'', ''2022-12-19T00:00:00.0000000-03:00'', N''22345678'', N''operador1@vacunassist.com'', N''Luis Gutierrez'', N''male'', CAST(0 AS bit), CAST(1 AS bit), N''1000:EBp2B0LQeWXGIeRMjnT9sL2kt4d9/QaG:Ez8msxqy752668uMscKFcl20lGj4DVIH'', CAST(0 AS bit), N''Buenos Aires'', N''operator'', N''Operador1''),
    (3, N''Calle Falsa 9874, Salta'', ''2022-12-19T00:00:00.0000000-03:00'', N''89785451'', N''estefania@vacunassist.com'', N''Estefania Borzi'', N''female'', CAST(0 AS bit), CAST(1 AS bit), N''1000:px26EI5vnZ6+4qewz/DwxfqDtt0xiozK:rYgi0gUPQatRZII3D5+jLx+yIzfgz2Di'', CAST(0 AS bit), N''Salta'', N''operator'', N''Operador2''),
    (4, N''Calle Falsa 9874, Salta'', ''2022-12-19T00:00:00.0000000-03:00'', N''89785451'', N''jr@vacunassist.com'', N''Jose Luis Rodriguez'', N''male'', CAST(0 AS bit), CAST(1 AS bit), N''1000:da/CvEpD+3ZeCo1ZSh6WaQ/E/EVzqE2p:HxLA2csnSEme8zpzBqLv0NsclX01FMU9'', CAST(0 AS bit), N''Buenos Aires'', N''analyst'', N''Analista1''),
    (5, N''Calle Falsa 4567, La Plata'', ''2022-12-19T00:00:00.0000000-03:00'', N''11111111'', N''vacunador@email.com'', N''Vacunador'', N''other'', CAST(0 AS bit), CAST(1 AS bit), N''1000:xOI9MA0kRSRSTYQm5xe8fpivaqZ9PDW1:MygAgFW7y+6oIfHdQAVqP/7BQ+heVo/l'', CAST(0 AS bit), N''Buenos Aires'', N''vacunator'', N''Vacunador'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BirthDate', N'DNI', N'Email', N'FullName', N'Gender', N'HealthWorker', N'IsActive', N'PasswordHash', N'Pregnant', N'Province', N'Role', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchNumber', N'DeliveredTime', N'DevelopedVaccineId', N'ETA', N'PurchaseDate', N'Quantity', N'Status') AND [object_id] = OBJECT_ID(N'[PurchaseOrders]'))
        SET IDENTITY_INSERT [PurchaseOrders] ON;
    EXEC(N'INSERT INTO [PurchaseOrders] ([Id], [BatchNumber], [DeliveredTime], [DevelopedVaccineId], [ETA], [PurchaseDate], [Quantity], [Status])
    VALUES (1, NULL, NULL, 3, NULL, ''2022-12-19T18:26:09.5720733-03:00'', 1400, 0),
    (2, NULL, NULL, 3, NULL, ''2022-12-19T18:26:09.5720737-03:00'', 1200, 0),
    (3, N''PF1000001'', ''2022-12-19T00:00:00.0000000-03:00'', 1, ''2022-12-19T00:00:00.0000000-03:00'', ''2022-11-19T00:00:00.0000000-03:00'', 800, 2),
    (4, N''PF1000121'', ''2022-12-19T00:00:00.0000000-03:00'', 1, ''2022-12-19T00:00:00.0000000-03:00'', ''2022-11-19T00:00:00.0000000-03:00'', 400, 2),
    (5, N''R1000001'', ''2022-12-19T00:00:00.0000000-03:00'', 2, ''2022-12-19T00:00:00.0000000-03:00'', ''2022-10-20T00:00:00.0000000-03:00'', 560, 2),
    (6, N''FLU12214001'', ''2022-12-19T00:00:00.0000000-03:00'', 3, ''2022-12-19T00:00:00.0000000-03:00'', ''2022-12-04T00:00:00.0000000-03:00'', 1500, 2),
    (7, N''FLU12214003'', ''2022-12-19T00:00:00.0000000-03:00'', 3, ''2022-12-19T00:00:00.0000000-03:00'', ''2022-12-04T00:00:00.0000000-03:00'', 3600, 2),
    (8, N''FLU13214121'', ''2022-12-01T00:00:00.0000000-03:00'', 3, ''2022-12-01T00:00:00.0000000-03:00'', ''2022-11-16T00:00:00.0000000-03:00'', 3600, 2)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchNumber', N'DeliveredTime', N'DevelopedVaccineId', N'ETA', N'PurchaseDate', N'Quantity', N'Status') AND [object_id] = OBJECT_ID(N'[PurchaseOrders]'))
        SET IDENTITY_INSERT [PurchaseOrders] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchNumber', N'DevelopedVaccineId', N'DueDate', N'OverdueQuantity', N'PurchaseOrderId', N'Quantity', N'RemainingQuantity', N'Status', N'Synchronized') AND [object_id] = OBJECT_ID(N'[BatchVaccines]'))
        SET IDENTITY_INSERT [BatchVaccines] ON;
    EXEC(N'INSERT INTO [BatchVaccines] ([Id], [BatchNumber], [DevelopedVaccineId], [DueDate], [OverdueQuantity], [PurchaseOrderId], [Quantity], [RemainingQuantity], [Status], [Synchronized])
    VALUES (1, N''PF1000001'', 1, ''2023-01-21T00:00:00.0000000-03:00'', 0, 3, 800, 800, 0, CAST(0 AS bit)),
    (2, N''PF1000121'', 1, ''2023-01-01T00:00:00.0000000-03:00'', 0, 4, 400, 400, 0, CAST(0 AS bit)),
    (3, N''R1000001'', 2, ''2023-02-10T00:00:00.0000000-03:00'', 0, 5, 560, 560, 0, CAST(0 AS bit)),
    (4, N''FLU12214001'', 3, ''2022-12-27T00:00:00.0000000-03:00'', 0, 6, 1500, 1500, 0, CAST(0 AS bit)),
    (5, N''FLU12214003'', 3, ''2023-03-27T00:00:00.0000000-03:00'', 0, 7, 3600, 3000, 0, CAST(0 AS bit)),
    (6, N''FLU13214121'', 3, ''2022-12-01T00:00:00.0000000-03:00'', 0, 8, 3600, 3600, 0, CAST(0 AS bit))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchNumber', N'DevelopedVaccineId', N'DueDate', N'OverdueQuantity', N'PurchaseOrderId', N'Quantity', N'RemainingQuantity', N'Status', N'Synchronized') AND [object_id] = OBJECT_ID(N'[BatchVaccines]'))
        SET IDENTITY_INSERT [BatchVaccines] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchVaccineId', N'DistributionDate', N'OverdueQuantity', N'Province', N'Quantity', N'RemainingQuantity', N'Synchronized') AND [object_id] = OBJECT_ID(N'[LocalBatchVaccines]'))
        SET IDENTITY_INSERT [LocalBatchVaccines] ON;
    EXEC(N'INSERT INTO [LocalBatchVaccines] ([Id], [BatchVaccineId], [DistributionDate], [OverdueQuantity], [Province], [Quantity], [RemainingQuantity], [Synchronized])
    VALUES (1, 5, ''2022-12-19T18:26:09.5720834-03:00'', 0, N''Buenos Aires'', 600, 600, CAST(0 AS bit))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchVaccineId', N'DistributionDate', N'OverdueQuantity', N'Province', N'Quantity', N'RemainingQuantity', N'Synchronized') AND [object_id] = OBJECT_ID(N'[LocalBatchVaccines]'))
        SET IDENTITY_INSERT [LocalBatchVaccines] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE INDEX [IX_AppliedVaccines_LocalBatchVaccineId] ON [AppliedVaccines] ([LocalBatchVaccineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE INDEX [IX_AppliedVaccines_PatientId] ON [AppliedVaccines] ([PatientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE INDEX [IX_AppliedVaccines_UserId] ON [AppliedVaccines] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE UNIQUE INDEX [IX_BatchVaccines_BatchNumber] ON [BatchVaccines] ([BatchNumber]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE INDEX [IX_BatchVaccines_DevelopedVaccineId] ON [BatchVaccines] ([DevelopedVaccineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE INDEX [IX_BatchVaccines_PurchaseOrderId] ON [BatchVaccines] ([PurchaseOrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE INDEX [IX_Departments_ProvinceId] ON [Departments] ([ProvinceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE INDEX [IX_LocalBatchVaccines_BatchVaccineId] ON [LocalBatchVaccines] ([BatchVaccineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE INDEX [IX_Patients_DepartmentId] ON [Patients] ([DepartmentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    CREATE INDEX [IX_PurchaseOrders_DevelopedVaccineId] ON [PurchaseOrders] ([DevelopedVaccineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221219212609_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221219212609_InitialMigration', N'6.0.4');
END;
GO

COMMIT;
GO

