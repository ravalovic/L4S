USE [log4service]
GO
IF (OBJECT_ID('FK_CATServicePatterns_FKServiceID', 'F') IS NOT NULL)
ALTER TABLE [dbo].[CATServicePatterns] DROP CONSTRAINT [FK_CATServicePatterns_FKServiceID]
GO

/****** Object:  Table [dbo].[CATServicePatterns]    Script Date: 15. 6. 2017 15:51:36 ******/
IF OBJECT_ID('dbo.CATServicePatterns', 'U') IS NOT NULL 
DROP TABLE [dbo].[CATServicePatterns]
GO

/****** Object:  Table [dbo].[CATServicePatterns]    Script Date: 15. 6. 2017 15:51:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATServicePatterns](
	[PKServicePatternID] [int] IDENTITY(1,1) NOT NULL,
	[PatternLike] [varchar](2000) NOT NULL,
	[PatternRegExp] [varchar](2000),
	[PatternDescription] [varchar](50) ,
	[FKServiceID] [int] NOT NULL,
	[Entity] varchar(150),
	[Explanation] varchar(150),
	[DatSelectMethod] varchar(150),
	[TCInsertTime] [datetime] ,
	[TCLastUpdate] [datetime] ,
	[TCActive] [int] ,
 CONSTRAINT [PK_CATServicePatterns] PRIMARY KEY CLUSTERED 
(
	[PKServicePatternID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CATServicePatterns]  WITH CHECK ADD  CONSTRAINT [FK_CATServicePatterns_FKServiceID] FOREIGN KEY([FKServiceID])
REFERENCES [dbo].[CATServiceParameters] ([PKServiceID])
GO

ALTER TABLE [dbo].[CATServicePatterns]  ADD  CONSTRAINT [DF_CATServicePatterns_TCInsertTime]  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[CATServicePatterns] ADD  CONSTRAINT [DF_CATServicePatterns_TCLastUpdate]  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[CATServicePatterns] ADD  CONSTRAINT [DF_CATServicePatterns_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO
