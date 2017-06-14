USE [log4service]
GO

DROP TABLE [dbo].[Stage_InputFileInfo]
GO

/****** Object:  Table [dbo].[Stage_InputFileInfo]    Script Date: 06.06.2017 23:28:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stage_InputFileInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](200) NOT NULL,
	[Checksum] [varchar](50) NOT NULL,
	[LinesInFile] [int] NOT NULL,
	[InsertDateTime] [datetime] NOT NULL,
	[LoaderBatchID] [int] NOT NULL,
	[LoadedRecord] [int] NOT NULL,
	[OriFileName] [varchar](200) NULL,
	[OriginalFileChecksum] [varchar](50) NOT NULL,

 CONSTRAINT [PK_Stage_InputFileInfo] PRIMARY KEY CLUSTERED 
(
	[Checksum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_InsertDateTime]  DEFAULT (getdate()) FOR [InsertDateTime]
GO

ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_LoaderBatchID]  DEFAULT ((-1)) FOR [LoaderBatchID]
GO

ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_LinesInFile]  DEFAULT ((-1)) FOR [LinesInFile]
GO

ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_LoadedRecord]  DEFAULT ((-1)) FOR [LoadedRecord]
GO
