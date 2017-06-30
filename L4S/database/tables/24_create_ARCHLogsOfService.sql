USE [log4service]
GO


IF OBJECT_ID('dbo.ARCHLogsOfService', 'U') IS NOT NULL 
  DROP TABLE dbo.ARCHLogsOfService; 
  
/****** Object:  Table [dbo].[ARCHLogsOfService]    Script Date: 14.06.2017 22:39:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ARCHLogsOfService](
	[BatchID] [int] ,
	[RecordID] [int] ,
	[CustomerID] int,
	[ServiceID] [int],
	[UserID] [varchar](50) NULL,
	[DateOfRequest] [datetime] NULL,
	[RequestedURL] [varchar](max) NULL,
	[RequestStatus] [varchar](5) NULL,
	[BytesSent] [varchar](15) NULL,
	[RequestTime] [varchar](15) NULL,
	[UserAgent] [varchar](500) NULL,
	[UserIPAddress] [varchar](1000) NULL,
	[TCInsertTime] [datetime],
	[TCLastUpdate] [datetime],
	[TCActive] [int]
	)
 ON [PRIMARY]
GO
ALTER TABLE [dbo].[ARCHLogsOfService]  ADD  CONSTRAINT [DF_ARCHLogsOfService_TCInsertTime]  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[ARCHLogsOfService] ADD  CONSTRAINT [DF_ARCHLogsOfService_TCLastUpdate]  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[ARCHLogsOfService] ADD  CONSTRAINT [DF_ARCHLogsOfService_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO