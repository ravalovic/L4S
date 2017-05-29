USE [log4service]
GO

/****** Object:  Table [dbo].[Stage_InputFileInfo]    Script Date: 29. 5. 2017 16:40:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stage_InputFileDuplicity](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [nvarchar](50) NOT NULL,
	[checksum] [nvarchar](50) NOT NULL,
	[insertDateTime] [datetime] NOT NULL,
) ON [PRIMARY]
GO


