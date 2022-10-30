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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE TABLE [DevelopedVaccines] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [IsActive] bit NOT NULL,
        [DaysToDelivery] int NOT NULL,
        [VaccineText] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_DevelopedVaccines] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE TABLE [Patients] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Surname] nvarchar(max) NOT NULL,
        [DNI] nvarchar(max) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [Province] nvarchar(max) NOT NULL,
        [BirthDate] nvarchar(max) NOT NULL,
        [Pregnant] bit NOT NULL,
        [HealthWorker] bit NOT NULL,
        CONSTRAINT [PK_Patients] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE TABLE [PurchaseOrders] (
        [Id] int NOT NULL IDENTITY,
        [PurchaseDate] datetime2 NOT NULL,
        [ETA] datetime2 NULL,
        [DeliveredTime] datetime2 NULL,
        [BatchNumber] nvarchar(20) NOT NULL,
        [DevelopedVaccineId] int NOT NULL,
        [Quantity] int NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PurchaseOrders_DevelopedVaccines_DevelopedVaccineId] FOREIGN KEY ([DevelopedVaccineId]) REFERENCES [DevelopedVaccines] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
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
        CONSTRAINT [PK_BatchVaccines] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BatchVaccines_DevelopedVaccines_DevelopedVaccineId] FOREIGN KEY ([DevelopedVaccineId]) REFERENCES [DevelopedVaccines] ([Id]),
        CONSTRAINT [FK_BatchVaccines_PurchaseOrders_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrders] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE TABLE [LocalBatchVaccines] (
        [Id] int NOT NULL IDENTITY,
        [BatchVaccineId] int NOT NULL,
        [Quantity] int NOT NULL,
        [RemainingQuantity] int NOT NULL,
        [OverdueQuantity] int NOT NULL,
        [Province] nvarchar(50) NOT NULL,
        [DistributionDate] datetime2 NOT NULL,
        CONSTRAINT [PK_LocalBatchVaccines] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LocalBatchVaccines_BatchVaccines_BatchVaccineId] FOREIGN KEY ([BatchVaccineId]) REFERENCES [BatchVaccines] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE TABLE [AppliedVaccines] (
        [Id] int NOT NULL IDENTITY,
        [AppliedDate] datetime2 NOT NULL,
        [PatientId] int NOT NULL,
        [UserId] int NOT NULL,
        [LocalBatchVaccineId] int NOT NULL,
        [AppliedDose] int NOT NULL,
        CONSTRAINT [PK_AppliedVaccines] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AppliedVaccines_LocalBatchVaccines_LocalBatchVaccineId] FOREIGN KEY ([LocalBatchVaccineId]) REFERENCES [LocalBatchVaccines] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AppliedVaccines_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AppliedVaccines_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DaysToDelivery', N'IsActive', N'Name', N'VaccineText') AND [object_id] = OBJECT_ID(N'[DevelopedVaccines]'))
        SET IDENTITY_INSERT [DevelopedVaccines] ON;
    EXEC(N'INSERT INTO [DevelopedVaccines] ([Id], [DaysToDelivery], [IsActive], [Name], [VaccineText])
    VALUES (1, 30, CAST(1 AS bit), N''Pfizer COVID-19'', N''{"Id":2000,"Name":"COVID-19","Type":1,"Doses":[{"Id":2001,"Number":0,"IsReinforcement":false,"MinMonthsOfAge":0,"DaysAfterPreviousDose":null},{"Id":2002,"Number":1,"IsReinforcement":false,"MinMonthsOfAge":0,"DaysAfterPreviousDose":120}]}''),
    (2, 60, CAST(1 AS bit), N''ROCHE Fiebre amarilla'', N''{"Id":1300,"Name":"Fiebre Amarilla","Type":0,"Doses":[{"Id":1301,"Number":0,"IsReinforcement":false,"MinMonthsOfAge":18,"DaysAfterPreviousDose":null},{"Id":1302,"Number":1,"IsReinforcement":true,"MinMonthsOfAge":132,"DaysAfterPreviousDose":null}]}''),
    (3, 15, CAST(1 AS bit), N''Fluarix Antigripal'', N''{"Id":3000,"Name":"Antigripal","Type":2,"Doses":[{"Id":3001,"Number":0,"IsReinforcement":false,"MinMonthsOfAge":0,"DaysAfterPreviousDose":365}]}'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DaysToDelivery', N'IsActive', N'Name', N'VaccineText') AND [object_id] = OBJECT_ID(N'[DevelopedVaccines]'))
        SET IDENTITY_INSERT [DevelopedVaccines] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BirthDate', N'DNI', N'Gender', N'HealthWorker', N'Name', N'Pregnant', N'Province', N'Surname') AND [object_id] = OBJECT_ID(N'[Patients]'))
        SET IDENTITY_INSERT [Patients] ON;
    EXEC(N'INSERT INTO [Patients] ([Id], [BirthDate], [DNI], [Gender], [HealthWorker], [Name], [Pregnant], [Province], [Surname])
    VALUES (1, N''10/30/2022 12:00:00 AM'', N''29999998'', N''male'', CAST(0 AS bit), N''Paciente'', CAST(0 AS bit), N''Buenos Aires'', N''Prueba''),
    (2, N''10/30/2022 12:00:00 AM'', N''29999999'', N''female'', CAST(0 AS bit), N''Paciente2'', CAST(0 AS bit), N''Buenos Aires'', N''Prueba2'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BirthDate', N'DNI', N'Gender', N'HealthWorker', N'Name', N'Pregnant', N'Province', N'Surname') AND [object_id] = OBJECT_ID(N'[Patients]'))
        SET IDENTITY_INSERT [Patients] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BirthDate', N'DNI', N'Email', N'FullName', N'Gender', N'HealthWorker', N'IsActive', N'PasswordHash', N'Pregnant', N'Province', N'Role', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [Address], [BirthDate], [DNI], [Email], [FullName], [Gender], [HealthWorker], [IsActive], [PasswordHash], [Pregnant], [Province], [Role], [UserName])
    VALUES (1, N''Calle Falsa 1234, La Plata'', ''2022-10-30T00:00:00.0000000-03:00'', N''11111111'', N''admin@vacunassist.com'', N''Administrador'', N''other'', CAST(0 AS bit), CAST(1 AS bit), N''1000:9FdQSeERrnUCcvmO6MRVClc+WPcJVQ5+:FqczwUftwm6yNAH9ljlYme5Qa4sDgwoa'', CAST(0 AS bit), N''Buenos Aires'', N''administrator'', N''Admin''),
    (2, N''Calle Falsa 2345, La Plata'', ''2022-10-30T00:00:00.0000000-03:00'', N''22345678'', N''operador1@vacunassist.com'', N''Luis Gutierrez'', N''male'', CAST(0 AS bit), CAST(1 AS bit), N''1000:NoeZ1VvProipTOj9rzqRECwlr08UMogY:Bi2XsE4OJRrg2g5ErkyEW9rN8Wf22FOg'', CAST(0 AS bit), N''Buenos Aires'', N''operator'', N''Operador1''),
    (3, N''Calle Falsa 9874, Salta'', ''2022-10-30T00:00:00.0000000-03:00'', N''89785451'', N''estefania@vacunassist.com'', N''Estefania Borzi'', N''female'', CAST(0 AS bit), CAST(1 AS bit), N''1000:jpYzihKGnDljxGtfXw2E3GGN2JhQ2ILD:qKPIjzAYNhfLg3G5QAv1XOVXKUeN6Hz4'', CAST(0 AS bit), N''Salta'', N''operator'', N''Operador2''),
    (4, N''Calle Falsa 9874, Salta'', ''2022-10-30T00:00:00.0000000-03:00'', N''89785451'', N''jr@vacunassist.com'', N''Jose Luis Rodriguez'', N''male'', CAST(0 AS bit), CAST(1 AS bit), N''1000:Z2DuNXHt8DxHDpkmXqON8fPXpS3rGZ9W:iNws01+0VVDdnxUlCsU33MQ9gqbQAuo/'', CAST(0 AS bit), N''Buenos Aires'', N''analyst'', N''Analista1''),
    (5, N''Calle Falsa 4567, La Plata'', ''2022-10-30T00:00:00.0000000-03:00'', N''11111111'', N''vacunador@email.com'', N''Vacunador'', N''other'', CAST(0 AS bit), CAST(1 AS bit), N''1000:dAO3WCKhhRNimwK7l0FZbOHLjniD8fAK:4AD1eZCFjFjNRKc/pJCj6Demik76mH+s'', CAST(0 AS bit), N''Buenos Aires'', N''vacunator'', N''Vacunador'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BirthDate', N'DNI', N'Email', N'FullName', N'Gender', N'HealthWorker', N'IsActive', N'PasswordHash', N'Pregnant', N'Province', N'Role', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchNumber', N'DeliveredTime', N'DevelopedVaccineId', N'ETA', N'PurchaseDate', N'Quantity', N'Status') AND [object_id] = OBJECT_ID(N'[PurchaseOrders]'))
        SET IDENTITY_INSERT [PurchaseOrders] ON;
    EXEC(N'INSERT INTO [PurchaseOrders] ([Id], [BatchNumber], [DeliveredTime], [DevelopedVaccineId], [ETA], [PurchaseDate], [Quantity], [Status])
    VALUES (1, N''FLU140012580'', NULL, 3, NULL, ''2022-10-30T15:50:32.0523710-03:00'', 1400, 0),
    (2, N''FLU140012581'', NULL, 3, NULL, ''2022-10-30T15:50:32.0523718-03:00'', 1200, 0),
    (3, N''PF1000001'', ''2022-10-30T00:00:00.0000000-03:00'', 1, ''2022-10-30T00:00:00.0000000-03:00'', ''2022-09-30T00:00:00.0000000-03:00'', 800, 2),
    (4, N''PF1000121'', ''2022-10-30T00:00:00.0000000-03:00'', 1, ''2022-10-30T00:00:00.0000000-03:00'', ''2022-09-30T00:00:00.0000000-03:00'', 400, 2),
    (5, N''R1000001'', ''2022-10-30T00:00:00.0000000-03:00'', 2, ''2022-10-30T00:00:00.0000000-03:00'', ''2022-08-31T00:00:00.0000000-03:00'', 560, 2),
    (6, N''FLU12214001'', ''2022-10-30T00:00:00.0000000-03:00'', 3, ''2022-10-30T00:00:00.0000000-03:00'', ''2022-10-15T00:00:00.0000000-03:00'', 1500, 2),
    (7, N''FLU12214003'', ''2022-10-30T00:00:00.0000000-03:00'', 3, ''2022-10-30T00:00:00.0000000-03:00'', ''2022-10-15T00:00:00.0000000-03:00'', 3600, 2),
    (8, N''FLU13214121'', ''2022-10-12T00:00:00.0000000-03:00'', 3, ''2022-10-12T00:00:00.0000000-03:00'', ''2022-09-27T00:00:00.0000000-03:00'', 3600, 2)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchNumber', N'DeliveredTime', N'DevelopedVaccineId', N'ETA', N'PurchaseDate', N'Quantity', N'Status') AND [object_id] = OBJECT_ID(N'[PurchaseOrders]'))
        SET IDENTITY_INSERT [PurchaseOrders] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchNumber', N'DevelopedVaccineId', N'DueDate', N'OverdueQuantity', N'PurchaseOrderId', N'Quantity', N'RemainingQuantity', N'Status') AND [object_id] = OBJECT_ID(N'[BatchVaccines]'))
        SET IDENTITY_INSERT [BatchVaccines] ON;
    EXEC(N'INSERT INTO [BatchVaccines] ([Id], [BatchNumber], [DevelopedVaccineId], [DueDate], [OverdueQuantity], [PurchaseOrderId], [Quantity], [RemainingQuantity], [Status])
    VALUES (1, N''PF1000001'', 1, ''2022-12-02T00:00:00.0000000-03:00'', 0, 3, 800, 800, 0),
    (2, N''PF1000121'', 1, ''2022-11-12T00:00:00.0000000-03:00'', 0, 4, 400, 400, 0),
    (3, N''R1000001'', 2, ''2022-12-22T00:00:00.0000000-03:00'', 0, 5, 560, 560, 0),
    (4, N''FLU12214001'', 3, ''2022-11-07T00:00:00.0000000-03:00'', 0, 6, 1500, 1500, 0),
    (5, N''FLU12214003'', 3, ''2023-02-05T00:00:00.0000000-03:00'', 0, 7, 3600, 3200, 0),
    (6, N''FLU13214121'', 3, ''2022-10-12T00:00:00.0000000-03:00'', 0, 8, 3600, 3600, 0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchNumber', N'DevelopedVaccineId', N'DueDate', N'OverdueQuantity', N'PurchaseOrderId', N'Quantity', N'RemainingQuantity', N'Status') AND [object_id] = OBJECT_ID(N'[BatchVaccines]'))
        SET IDENTITY_INSERT [BatchVaccines] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchVaccineId', N'DistributionDate', N'OverdueQuantity', N'Province', N'Quantity', N'RemainingQuantity') AND [object_id] = OBJECT_ID(N'[LocalBatchVaccines]'))
        SET IDENTITY_INSERT [LocalBatchVaccines] ON;
    EXEC(N'INSERT INTO [LocalBatchVaccines] ([Id], [BatchVaccineId], [DistributionDate], [OverdueQuantity], [Province], [Quantity], [RemainingQuantity])
    VALUES (1, 5, ''2022-10-30T15:50:32.0523795-03:00'', 0, N''Buenos Aires'', 600, 600)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BatchVaccineId', N'DistributionDate', N'OverdueQuantity', N'Province', N'Quantity', N'RemainingQuantity') AND [object_id] = OBJECT_ID(N'[LocalBatchVaccines]'))
        SET IDENTITY_INSERT [LocalBatchVaccines] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppliedDate', N'AppliedDose', N'LocalBatchVaccineId', N'PatientId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppliedVaccines]'))
        SET IDENTITY_INSERT [AppliedVaccines] ON;
    EXEC(N'INSERT INTO [AppliedVaccines] ([Id], [AppliedDate], [AppliedDose], [LocalBatchVaccineId], [PatientId], [UserId])
    VALUES (1, ''2022-10-30T00:00:00.0000000-03:00'', 3001, 1, 1, 5)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppliedDate', N'AppliedDose', N'LocalBatchVaccineId', N'PatientId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppliedVaccines]'))
        SET IDENTITY_INSERT [AppliedVaccines] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppliedDate', N'AppliedDose', N'LocalBatchVaccineId', N'PatientId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppliedVaccines]'))
        SET IDENTITY_INSERT [AppliedVaccines] ON;
    EXEC(N'INSERT INTO [AppliedVaccines] ([Id], [AppliedDate], [AppliedDose], [LocalBatchVaccineId], [PatientId], [UserId])
    VALUES (2, ''2022-10-30T00:00:00.0000000-03:00'', 3001, 1, 2, 5)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppliedDate', N'AppliedDose', N'LocalBatchVaccineId', N'PatientId', N'UserId') AND [object_id] = OBJECT_ID(N'[AppliedVaccines]'))
        SET IDENTITY_INSERT [AppliedVaccines] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE INDEX [IX_AppliedVaccines_LocalBatchVaccineId] ON [AppliedVaccines] ([LocalBatchVaccineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE INDEX [IX_AppliedVaccines_PatientId] ON [AppliedVaccines] ([PatientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE INDEX [IX_AppliedVaccines_UserId] ON [AppliedVaccines] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE UNIQUE INDEX [IX_BatchVaccines_BatchNumber] ON [BatchVaccines] ([BatchNumber]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE INDEX [IX_BatchVaccines_DevelopedVaccineId] ON [BatchVaccines] ([DevelopedVaccineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE INDEX [IX_BatchVaccines_PurchaseOrderId] ON [BatchVaccines] ([PurchaseOrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE INDEX [IX_LocalBatchVaccines_BatchVaccineId] ON [LocalBatchVaccines] ([BatchVaccineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE UNIQUE INDEX [IX_PurchaseOrders_BatchNumber] ON [PurchaseOrders] ([BatchNumber]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    CREATE INDEX [IX_PurchaseOrders_DevelopedVaccineId] ON [PurchaseOrders] ([DevelopedVaccineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221030185032_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221030185032_InitialMigration', N'6.0.4');
END;
GO

COMMIT;
GO

