USE [log4service]
GO


IF OBJECT_ID('dbo.CATLogsOfService', 'U') IS NOT NULL 
  DROP TABLE dbo.CATLogsOfService; 
  
/****** Object:  Table [dbo].[CATLogsOfService]    Script Date: 14.06.2017 22:39:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATLogsOfService](
	[BatchID] [int] NOT NULL,
	[RecordID] [int] NOT NULL,
	[CustomerID] int,
	[ServiceID] [int],
	[UserID] [varchar](50) NULL,
	[DateOfRequest] [datetime] NULL,
	[RequestedURL] [varchar](max) NULL,
	[RequestStatus] [varchar](5) NULL,
	[BytesSent] [varchar](15) NULL,
	[RequestTime] [varchar](15) NULL,
	[UserAgent] [varchar](500) NULL,
	[UserIPAddress] [varchar](50) NULL,
	[TCInsertTime] [datetime] default (getdate()),
	[TCLastUpdate] [datetime] default (getdate()),
	[TCActive] [int] default((0))
	CONSTRAINT PK_BatchIDRecordId PRIMARY KEY (BatchID,RecordID))
 ON [PRIMARY]
GO


