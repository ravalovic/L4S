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
	[PatternCode] [varchar](2000) NOT NULL,
	[PatternDescription] [varchar](50) NULL,
	[FKServiceID] [int] NOT NULL,
 CONSTRAINT [PK_CATServicePatterns] PRIMARY KEY CLUSTERED 
(
	[PKServicePatternID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CATServicePatterns]  WITH CHECK ADD  CONSTRAINT [FK_CATServicePatterns_FKServiceID] FOREIGN KEY([PKServicePatternID])
REFERENCES [dbo].[CATServiceParameters] ([PKServiceID])
GO

