USE [log4service]
GO

/****** Object:  Table [dbo].[Stage_LogImport]    Script Date: 30. 5. 2017 12:03:21 ******/
DROP TABLE [dbo].[Stage_LogImport]
GO

/****** Object:  Table [dbo].[Stage_LogImport]    Script Date: 30. 5. 2017 12:03:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stage_LogImport](
	[batchID] [bigint] NOT NULL,
	[originalFileName] [varchar](100) NOT NULL,
	[originalCheckSum] [varchar](100) NOT NULL,
	[preProcessFileName] [varchar](100) NOT NULL,
	[col1] [varchar](10) NULL,
	[col2] [varchar](50) NULL,
	[col3] [varchar](50) NULL,
	[col4] [varchar](50) NULL,
	[col5] [varchar](50) NULL,
	[col6] [varchar](50) NULL,
	[col7] [varchar](50) NULL,
	[col8] [varchar](50) NULL,
	[col9] [varchar](50) NULL,
) ON [PRIMARY]
GO

grant select, insert on Stage_LogImport to loader
go
grant execute on sp_stage_fileInfo to loader
go