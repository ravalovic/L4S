USE [log4service]
GO

/****** Object:  Table [dbo].[Catalog_LogService]    Script Date: 14.06.2017 22:39:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Catalog_LogsOfService](
	[BatchID] [int] NOT NULL,
	[CustomerID] int,
	[ServiceName] [varchar](50),
	[UserID] [varchar](50) NULL,
	[DateOfRequest] [varchar](30) NULL,
	[RequestedURL] [varchar](max) NULL,
	[RequestStatus] [varchar](5) NULL,
	[BytesSent] [varchar](15) NULL,
	[RequestTime] [varchar](15) NULL,
	[UserAgent] [varchar](500) NULL,
	[UserIPAddress] [varchar](50) NULL,
	[TCInsertTime] [datetime] default (getdate()),
	[TCLastUpdate] [datetime] default (getdate()),
	[TCActive] [int] default((0))
) ON [PRIMARY]
GO


