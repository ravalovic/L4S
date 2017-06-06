USE [log4service]
GO

/****** Object:  Table [dbo].[Stage_InputFileInfo]    Script Date: 06.06.2017 23:28:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stage_InputFileInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [varchar](100) NOT NULL,
	[checksum] [varchar](50) NOT NULL,
	[insertDateTime] [datetime] NOT NULL,
	[loaderBatchID] [int] NOT NULL,
 CONSTRAINT [PK_Stage_InputFileInfo] PRIMARY KEY CLUSTERED 
(
	[checksum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_checksum]  DEFAULT (getdate()) FOR [checksum]
GO

ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_insertDateTime]  DEFAULT (getdate()) FOR [insertDateTime]
GO

ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_loaderBatchID]  DEFAULT ((-1)) FOR [loaderBatchID]
GO


