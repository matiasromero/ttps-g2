IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppliedVaccines]') AND type in (N'U'))
DROP TABLE [dbo].[AppliedVaccines]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DevelopedVaccines]') AND type in (N'U'))
DROP TABLE [dbo].[DevelopedVaccines]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO


delete __EFMigrationsHistory

