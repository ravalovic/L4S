USE [log4service]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] DROP CONSTRAINT [DF_Stage_InputFileDuplicity_insertDateTime_1]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] DROP CONSTRAINT [DF_Stage_InputFileDuplicity_insertDateTime]
GO

/****** Object:  Table [dbo].[Stage_InputFileDuplicity]    Script Date: 30. 5. 2017 10:32:10 ******/
DROP TABLE [dbo].[Stage_InputFileDuplicity]
GO

/****** Object:  Table [dbo].[Stage_InputFileDuplicity]    Script Date: 30. 5. 2017 10:32:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stage_InputFileDuplicity](
	[id] [int] NOT NULL,
	[fileName] [varchar](100) NOT NULL,
	[checksum] [varchar](50) NOT NULL,
	[loadDateTime] [datetime] NOT NULL,
	[insertDateTime] [datetime] NOT NULL,
	[oriFileName] [varchar](100) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_insertDateTime]  DEFAULT (getdate()) FOR [loadDateTime]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_insertDateTime_1]  DEFAULT (getdate()) FOR [insertDateTime]
GO

