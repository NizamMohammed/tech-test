--Create the database
USE [master]
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'Aurora.CDRApi')
DROP DATABASE [Aurora.CDRApi]
GO
USE [master]
GO
CREATE DATABASE [Aurora.CDRApi] ON  PRIMARY 
( NAME = N'CDRApi', FILENAME = N'C:\SQL_Data\CDRApi.mdf' , SIZE = 16384KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CDRApi_log', FILENAME = N'C:\SQL_Data\CDRApi_log.ldf' , SIZE = 11200KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

USE [Aurora.CDRApi]
GO
-----Create CDR table
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CDR]') AND type in (N'U'))
DROP TABLE [dbo].[CDR]
GO

USE [Aurora.CDRApi]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CDR](
	[caller_id] [varchar](1000) NULL,
	[recipient] [varchar](1000) NULL,
	[call_date] [varchar](1000) NULL,
	[end_time] [varchar](1000) NULL,
	[duration] [varchar](1000) NULL,
	[cost] [varchar](1000) NULL,
	[reference] [varchar](1000) NULL,
	[currency] [varchar](1000) NULL,
	[id] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CDR] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

--Create Upoadfiles table
USE [Aurora.CDRApi]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UploadFiles]') AND type in (N'U'))
DROP TABLE [dbo].[UploadFiles]
GO
USE [Aurora.CDRApi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UploadFiles](
	[FileName] [varchar](1000) NULL,
	[FileSize] [float] NULL,
	[FileType] [varchar](100) NULL,
	[DateUploaded] [datetime] NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_UploadFiles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO


