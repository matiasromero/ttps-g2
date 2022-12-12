IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppliedVaccines]') AND type in (N'U'))
DROP TABLE [dbo].[AppliedVaccines]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND type in (N'U'))
DROP TABLE [dbo].[Patients]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LocalBatchVaccines]') AND type in (N'U'))
DROP TABLE [dbo].[LocalBatchVaccines]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BatchVaccines]') AND type in (N'U'))
DROP TABLE [dbo].[BatchVaccines]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PurchaseOrders]') AND type in (N'U'))
DROP TABLE [dbo].[PurchaseOrders]
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DevelopedVaccines]') AND type in (N'U'))
DROP TABLE [dbo].[DevelopedVaccines]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Departments]') AND type in (N'U'))
DROP TABLE [dbo].[Departments]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Laboratories]') AND type in (N'U'))
DROP TABLE [dbo].[Laboratories]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Province]') AND type in (N'U'))
DROP TABLE [dbo].[Province]
GO


delete __EFMigrationsHistory

