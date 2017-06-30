USE [log4service]
GO

/****** Object:  Table [dbo].[STInputFileDuplicity]    Script Date: 14. 6. 2017 12:22:28 ******/
IF OBJECT_ID('dbo.STInputFileDuplicity', 'U') IS NOT NULL 
  DROP TABLE dbo.STInputFileDuplicity; 

GO

/****** Object:  Table [dbo].[STInputFileDuplicity]    Script Date: 14. 6. 2017 12:22:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[STInputFileDuplicity](
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

ALTER TABLE [dbo].[STInputFileDuplicity] ADD  CONSTRAINT [DF_STInputFileDuplicity_OriginalId]  DEFAULT ((-1)) FOR [OriginalId]
GO

ALTER TABLE [dbo].[STInputFileDuplicity] ADD  CONSTRAINT [DF_STInputFileDuplicity_LinesInFile]  DEFAULT ((-1)) FOR [LinesInFile]
GO

ALTER TABLE [dbo].[STInputFileDuplicity] ADD  CONSTRAINT [DF_STInputFileDuplicity_LoadDateTime]  DEFAULT (getdate()) FOR [LoadDateTime]
GO

ALTER TABLE [dbo].[STInputFileDuplicity] ADD  CONSTRAINT [DF_STInputFileDuplicity_InsertDateTime_1]  DEFAULT (getdate()) FOR [InsertDateTime]
GO

ALTER TABLE [dbo].[STInputFileDuplicity] ADD  CONSTRAINT [DF_STInputFileDuplicity_LoaderBatchID]  DEFAULT ((-1)) FOR [LoaderBatchID]
GO

