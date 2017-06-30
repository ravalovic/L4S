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
	[UserIPAddress] [varchar](1000) NULL,
	[TCInsertTime] [datetime] ,
	[TCLastUpdate] [datetime] ,
	[TCActive] [int] 
	)
 ON [PRIMARY]
GO

ALTER TABLE [dbo].[CATLogsOfService]  ADD  CONSTRAINT [DF_CATLogsOfService_TCInsertTime]  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[CATLogsOfService] ADD  CONSTRAINT [DF_CATLogsOfService_TCLastUpdate]  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[CATLogsOfService] ADD  CONSTRAINT [DF_CATLogsOfService_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO

CREATE NONCLUSTERED INDEX [BatchID] ON [dbo].[CATLogsOfService]
(
	[BatchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO
