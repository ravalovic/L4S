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
	[Node_IP_Address] [varchar](50) NULL,
	[UserID] [varchar](50) NULL,
	[Date_Of_Request] [varchar](30) NULL,
	[Requested_URL] [varchar](MAX) NULL,
	[Request_Status] [varchar](5) NULL,
	[Bytes_Sent] [varchar](15) NULL,
	[Request_Time] [varchar](15) NULL,
	[http_referer] [varchar](Max) NULL,
	[User_Agent] [varchar](500) NULL,
	[User_IP_Address] [varchar](50) NULL,
) ON [PRIMARY]
GO

grant select, insert on Stage_LogImport to loader
go
grant execute on sp_stage_fileInfo to loader
go