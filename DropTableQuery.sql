IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__EFMigrationsHistory]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategorieSpel]') AND type in (N'U'))
DROP TABLE [dbo].[CategorieSpel]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categorieen]') AND type in (N'U'))
DROP TABLE [dbo].[Categorieen]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MateriaalSpel]') AND type in (N'U'))
DROP TABLE [dbo].[MateriaalSpel]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Materiaal]') AND type in (N'U'))
DROP TABLE [dbo].[Materiaal]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Spellen]') AND type in (N'U'))
DROP TABLE [dbo].[Spellen]
GO
