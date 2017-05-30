USE [master]
GO
/****** Object:  Database [log4service]    Script Date: 30. 5. 2017 9:28:52 ******/
CREATE DATABASE [log4service]
 GO
ALTER DATABASE [log4service] SET COMPATIBILITY_LEVEL = 130
GO

USE [log4service]
GO
/****** Object:  Table [dbo].[Catalog_LogService]    Script Date: 30. 5. 2017 9:28:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalog_LogService](
	[batchID] [bigint] NOT NULL,
	[fileID] [bigint] NOT NULL,
	[insertDateTime] [datetime] NOT NULL,
	[customerID] [nvarchar](50) NULL,
	[serviceID] [int] NULL,
	[col1] [nvarchar](50) NULL,
	[col2] [nvarchar](50) NULL,
	[col3] [nvarchar](50) NULL,
	[col4] [nvarchar](50) NULL,
	[col5] [nvarchar](50) NULL,
	[col6] [nvarchar](50) NULL,
	[col7] [nvarchar](50) NULL,
	[col8] [nvarchar](50) NULL,
	[col9] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Catalog_Service]    Script Date: 30. 5. 2017 9:28:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalog_Service](
	[serviceID] [int] IDENTITY(1,1) NOT NULL,
	[Identifier] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[userID] [int] NULL,
	[insertDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[serviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stage_InputFileDuplicity]    Script Date: 30. 5. 2017 9:28:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stage_InputFileDuplicity](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [nvarchar](50) NOT NULL,
	[checksum] [nvarchar](50) NOT NULL,
	[insertDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stage_InputFileInfo]    Script Date: 30. 5. 2017 9:28:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stage_InputFileInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [nvarchar](50) NOT NULL,
	[checksum] [nvarchar](50) NOT NULL,
	[insertDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Stage_InputFileInfo] PRIMARY KEY CLUSTERED 
(
	[checksum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stage_LogImport]    Script Date: 30. 5. 2017 9:28:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stage_LogImport](
	[batchID] [bigint] NOT NULL,
	[fileName] [nvarchar](50) NOT NULL,
	[col1] [nvarchar](50) NULL,
	[col2] [nvarchar](50) NULL,
	[col3] [nvarchar](50) NULL,
	[col4] [nvarchar](50) NULL,
	[col5] [nvarchar](50) NULL,
	[col6] [nvarchar](50) NULL,
	[col7] [nvarchar](50) NULL,
	[col8] [nvarchar](50) NULL,
	[col9] [nvarchar](50) NULL,
	[insertDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Catalog_LogService] ADD  DEFAULT (getdate()) FOR [insertDateTime]
GO
ALTER TABLE [dbo].[Catalog_Service] ADD  DEFAULT (getdate()) FOR [insertDateTime]
GO
ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_insertDateTime]  DEFAULT (getdate()) FOR [insertDateTime]
GO
ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_checksum]  DEFAULT (getdate()) FOR [checksum]
GO
ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_insertDateTime]  DEFAULT (getdate()) FOR [insertDateTime]
GO
ALTER TABLE [dbo].[Stage_LogImport] ADD  DEFAULT (getdate()) FOR [insertDateTime]
GO
/****** Object:  StoredProcedure [dbo].[sp_stage_fileInfo]    Script Date: 30. 5. 2017 9:28:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_stage_fileInfo]
	-- Add the parameters for the stored procedure here
	@FileName nvarchar(50),
	@FileCheckSum nvarchar(50)
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
INSERT INTO [dbo].[Stage_InputFileInfo] ([fileName],[checksum])
     VALUES(@FileName, @FileCheckSum);
END TRY
BEGIN CATCH
INSERT INTO [dbo].[Stage_InputFileDuplicity] ([fileName],[checksum])
     VALUES(@FileName, @FileCheckSum);
END CATCH;
GO
USE [master]
GO
ALTER DATABASE [log4service] SET  READ_WRITE 
GO
