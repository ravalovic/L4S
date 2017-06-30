USE [log4service]
GO

IF OBJECT_ID('dbo.CATUnknownService', 'U') IS NOT NULL 
  DROP TABLE dbo.CATUnknownService; 

GO

/****** Object:  Table [dbo].[CATUnknownService]    Script Date: 14. 6. 2017 9:09:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATUnknownService](
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
	[TCInsertTime] [datetime] default (getdate()),
	[TCLastUpdate] [datetime] default (getdate()),
	[TCActive] [int] default((0))
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

