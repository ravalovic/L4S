USE [log4service]
GO

DROP TABLE [dbo].[Stage_LogImport]
GO

/****** Object:  Table [dbo].[Stage_LogImport]    Script Date: 14. 6. 2017 9:09:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stage_LogImport](
	[BatchID] [int] NOT NULL,
	[OriginalFileName] [varchar](200) NOT NULL,
	[OriginalCheckSum] [varchar](100) NOT NULL,
	[NodeIPAddress] [varchar](50) NULL,
	[UserID] [varchar](50) NULL,
	[DateOfRequest] [varchar](30) NULL,
	[RequestedURL] [varchar](max) NULL,
	[RequestStatus] [varchar](5) NULL,
	[BytesSent] [varchar](15) NULL,
	[RequestTime] [varchar](15) NULL,
	[HttpRefferer] [varchar](max) NULL,
	[UserAgent] [varchar](500) NULL,
	[UserIPAddress] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Stage_LogImport] ADD  CONSTRAINT [DF_Stage_LogImport_BatchID]  DEFAULT ((0)) FOR [BatchID]
GO

ALTER TABLE [dbo].[Stage_LogImport] ADD  CONSTRAINT [DF_Stage_LogImport_OriginalFileName]  DEFAULT ('n/a') FOR [OriginalFileName]
GO

ALTER TABLE [dbo].[Stage_LogImport] ADD  CONSTRAINT [DF_Stage_LogImport_OriginalCheckSum]  DEFAULT ('n/a') FOR [OriginalCheckSum]
GO
