USE [log4service]
GO

/****** Object:  Table [dbo].[Catalog_CustomerData]    Script Date: 14.06.2017 22:10:20 ******/
DROP TABLE [dbo].[Catalog_CustomerData]
GO

/****** Object:  Table [dbo].[Catalog_CustomerData]    Script Date: 14.06.2017 22:10:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Catalog_CustomerData](
	[PKCustomerDataID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerType] [int] NOT NULL,
	[CompanyName] [varchar](100) NULL,
	[CompanyType] [varchar](100) NOT NULL,
	[CompanyID] [varchar](20) NOT NULL,
	[IndividualTitle] [varchar](10) NULL,
	[IndividualFirstName] [varchar](50) NOT NULL,
	[IndividualLastName] [varchar](50) NOT NULL,
	[IndividualID] [varchar](20) NOT NULL,
	[AddressStreet] [varchar](50) NULL,
	[AddressBuildingNumber] [varchar](20) NOT NULL,
	[AddressCity] [varchar](50) NOT NULL,
	[AddressZipCode] [varchar](6) NOT NULL,
	[AddressCountry] [varchar](50) NULL,
	[ContactEmail] [varchar](50) NULL,
	[ContactMobile] [varchar](50) NULL,
	[ContactPhone] [varchar](50) NULL,
	[ContactWeb] [varchar](100) NULL,
	[TCInsertTime] [datetime] NULL,
	[TCLastUpdate] [datetime] NULL,
	[TCActive] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PKCustomerDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Catalog_CustomerData] ADD  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[Catalog_CustomerData] ADD  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[Catalog_CustomerData] ADD  DEFAULT ((0)) FOR [TCActive]
GO

ALTER TABLE [dbo].[Catalog_CustomerData]  WITH CHECK ADD CHECK  (([CustomerType]='POZ' OR [CustomerType]='FOP' OR [CustomerType]='FO' OR [CustomerType]='PO'))
GO

ALTER TABLE [dbo].[Catalog_CustomerData]  WITH CHECK ADD CHECK  (([CompanyType]=' v.o.s.' OR [CompanyType]='e.s.' OR [CompanyType]='k.s.' OR [CompanyType]='a.s.' OR [CompanyType]='s.r.o.'))
GO

