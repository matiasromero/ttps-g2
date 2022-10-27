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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221027002345_InitialMigration')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221027002345_InitialMigration')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221027002345_InitialMigration')
BEGIN
    CREATE TABLE [AppliedVaccines] (
        [Id] int NOT NULL IDENTITY,
        [AppliedDate] datetime2 NULL,
        [AppliedBy] nvarchar(max) NULL,
        [Remark] nvarchar(max) NULL,
        [PersonId] int NOT NULL,
        [UserId] int NOT NULL,
        [VaccineId] int NOT NULL,
        CONSTRAINT [PK_AppliedVaccines] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AppliedVaccines_DevelopedVaccines_VaccineId] FOREIGN KEY ([VaccineId]) REFERENCES [DevelopedVaccines] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AppliedVaccines_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221027002345_InitialMigration')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221027002345_InitialMigration')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BirthDate', N'DNI', N'Email', N'FullName', N'Gender', N'HealthWorker', N'IsActive', N'PasswordHash', N'Pregnant', N'Province', N'Role', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [Address], [BirthDate], [DNI], [Email], [FullName], [Gender], [HealthWorker], [IsActive], [PasswordHash], [Pregnant], [Province], [Role], [UserName])
    VALUES (1, N''Calle Falsa 1234, La Plata'', ''2022-10-26T00:00:00.0000000-03:00'', N''11111111'', N''admin@vacunassist.com'', N''Administrador'', N''other'', CAST(0 AS bit), CAST(1 AS bit), N''1000:AH8kbImBb/pxOQkaZgQb2u5tKLv5v80h:qH2OM4aBB+pqNQaWyzZewsC6LHGmcPss'', CAST(0 AS bit), N''Buenos Aires'', N''administrator'', N''Admin''),
    (2, N''Calle Falsa 2345, La Plata'', ''2022-10-26T00:00:00.0000000-03:00'', N''22345678'', N''operador1@vacunassist.com'', N''Luis Gutierrez'', N''male'', CAST(0 AS bit), CAST(1 AS bit), N''1000:/Mcy0GamTI832cnk6wjGAJKbDYEBPMnX:XaWG9zaWcqhUCcHZYiHZoePeyas1P9v3'', CAST(0 AS bit), N''Buenos Aires'', N''operator'', N''Operador1''),
    (3, N''Calle Falsa 9874, Salta'', ''2022-10-26T00:00:00.0000000-03:00'', N''89785451'', N''estefania@vacunassist.com'', N''Estefania Borzi'', N''female'', CAST(0 AS bit), CAST(1 AS bit), N''1000:ey6xcCsi14qUuT2Sd7hZqX/G3mjWggh5:0q4QVJFpt1OiKqmYqHwjoulrppOPfVW1'', CAST(0 AS bit), N''Salta'', N''operator'', N''Operador2''),
    (4, N''Calle Falsa 9874, Salta'', ''2022-10-26T00:00:00.0000000-03:00'', N''89785451'', N''jr@vacunassist.com'', N''Jose Luis Rodriguez'', N''male'', CAST(0 AS bit), CAST(1 AS bit), N''1000:7YzQIgKRQ99GvYyvPDWACxlGL/h2pD43:n4cJewVaNsQYhR/XFICPV/lgTWr1PiXW'', CAST(0 AS bit), N''Buenos Aires'', N''analyst'', N''Analista1''),
    (5, N''Calle Falsa 4567, La Plata'', ''2022-10-26T00:00:00.0000000-03:00'', N''11111111'', N''vacunador@email.com'', N''Vacunador'', N''other'', CAST(0 AS bit), CAST(1 AS bit), N''1000:XsSVAtK31XwsaW22UlRr3LgaA+3lo+nb:OgPbTqOdK3YGKHUxnWXJ4kTIc+Gjd4tm'', CAST(0 AS bit), N''Buenos Aires'', N''vacunator'', N''Vacunador'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'BirthDate', N'DNI', N'Email', N'FullName', N'Gender', N'HealthWorker', N'IsActive', N'PasswordHash', N'Pregnant', N'Province', N'Role', N'UserName') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221027002345_InitialMigration')
BEGIN
    CREATE INDEX [IX_AppliedVaccines_UserId] ON [AppliedVaccines] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221027002345_InitialMigration')
BEGIN
    CREATE INDEX [IX_AppliedVaccines_VaccineId] ON [AppliedVaccines] ([VaccineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221027002345_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221027002345_InitialMigration', N'6.0.4');
END;
GO

COMMIT;
GO

