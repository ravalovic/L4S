USE [log4service]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] DROP CONSTRAINT [DF_Stage_InputFileDuplicity_LoaderBatchID]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] DROP CONSTRAINT [DF_Stage_InputFileDuplicity_InsertDateTime_1]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] DROP CONSTRAINT [DF_Stage_InputFileDuplicity_LoadDateTime]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] DROP CONSTRAINT [DF_Stage_InputFileDuplicity_LinesInFile]
GO

/****** Object:  Table [dbo].[Stage_InputFileDuplicity]    Script Date: 14. 6. 2017 12:22:28 ******/
DROP TABLE [dbo].[Stage_InputFileDuplicity]
GO

/****** Object:  Table [dbo].[Stage_InputFileDuplicity]    Script Date: 14. 6. 2017 12:22:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stage_InputFileDuplicity](
	[OriginalId] [int] NOT NULL,
	[FileName] [varchar](200) NOT NULL,
	[LinesInFile] [int] NOT NULL,
	[Checksum] [varchar](50) NOT NULL,
	[LoadDateTime] [datetime] NOT NULL,
	[InsertDateTime] [datetime] NOT NULL,
	[OriFileName] [varchar](200) NULL,
	[OriginalFileChecksum] [varchar](50) NOT NULL,
	[LoaderBatchID] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_OriginalId]  DEFAULT ((-1)) FOR [OriginalId]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_LinesInFile]  DEFAULT ((-1)) FOR [LinesInFile]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_LoadDateTime]  DEFAULT (getdate()) FOR [LoadDateTime]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_InsertDateTime_1]  DEFAULT (getdate()) FOR [InsertDateTime]
GO

ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_LoaderBatchID]  DEFAULT ((-1)) FOR [LoaderBatchID]
GO

