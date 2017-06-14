USE [log4service]
GO

/****** Object:  Table [dbo].[Catalog_CustomerData]    Script Date: 14. 6. 2017 16:09:45 ******/
DROP TABLE [dbo].[Catalog_CustomerData]
GO

/****** Object:  Table [dbo].[Catalog_CustomerData]    Script Date: 14. 6. 2017 16:09:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Catalog_CustomerData](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerType] [int] NOT NULL,
	[CompanyName] [varchar](100) NULL,
	[CompanyType] [varchar](100) NOT NULL,
	[CompanyID] [varchar](20) NULL,
	[BuildingNumber] [varchar](20) NULL,
	[ZipCode] [nchar](10) NULL
) ON [PRIMARY]
GO

