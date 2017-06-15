USE [log4service]
GO

/****** Object:  Table [dbo].[CATServiceParameters]    Script Date: 15. 6. 2017 15:56:13 ******/
IF OBJECT_ID('dbo.CATServiceParameters', 'U') IS NOT NULL 
DROP TABLE [dbo].[CATServiceParameters]
GO

/****** Object:  Table [dbo].[CATServiceParameters]    Script Date: 15. 6. 2017 15:56:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATServiceParameters](
	[PKServiceID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceCode] [varchar](50) NOT NULL,
	[ServiceDescription] [varchar](50) NOT NULL,
	[ServiceBasicPrice] [decimal](8, 4) NOT NULL,
	[TCInsertTime] [datetime] NULL,
	[TCLastUpdate] [datetime] NULL,
	[TCActive] [int] NULL,
 CONSTRAINT [PK_CATServiceParameters] PRIMARY KEY CLUSTERED 
(
	[PKServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

