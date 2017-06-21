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
    [BatchID] [int] ,
	[RecordID] [int],
	[OriginalCheckSum] [varchar](100) ,
	[NodeIPAddress] [varchar](50) ,
	[UserID] [varchar](50) ,
	[DateOfRequest] [varchar](30) ,
	[RequestedURL] [varchar](max) ,
	[RequestStatus] [varchar](5) ,
	[BytesSent] [varchar](15) ,
	[RequestTime] [varchar](15) ,
	[HttpRefferer] [varchar](max) ,
	[UserAgent] [varchar](500) ,
	[UserIPAddress] [varchar](50) 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

