USE [log4service]
GO
IF (OBJECT_ID('DF_CATCustomerData_TCInsertTime', 'D') IS NOT NULL)
ALTER TABLE [dbo].[CATCustomerData]  DROP  CONSTRAINT [DF_CATCustomerData_TCInsertTime] 
GO
IF (OBJECT_ID('DF_CATCustomerData_TCLastUpdate', 'D') IS NOT NULL)
ALTER TABLE [dbo].[CATCustomerData] DROP  CONSTRAINT [DF_CATCustomerData_TCLastUpdate] 
GO
IF (OBJECT_ID('DF_CATCustomerData_TCActive', 'D') IS NOT NULL)
ALTER TABLE [dbo].[CATCustomerData] DROP  CONSTRAINT [DF_CATCustomerData_TCActive]  
GO

IF (OBJECT_ID('DF_CATServiceIdentifiers_FKCustomerDataID', 'F') IS NOT NULL)
ALTER TABLE [dbo.CATServiceIdentifiers] DROP CONSTRAINT [DF_CATServiceIdentifiers_FKCustomerDataID]
GO


/****** Object:  Table [dbo].[CATCustomerData]    Script Date: 14.06.2017 22:10:20 ******/
IF OBJECT_ID('dbo.CATCustomerData', 'U') IS NOT NULL 
  DROP TABLE dbo.CATCustomerData; 

/****** Object:  Table [dbo].[CATCustomerData]    Script Date: 14.06.2017 22:10:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATCustomerData](
	[PKCustomerDataID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerType] [varchar](6),
	[CompanyName] [varchar](100) ,
	[CompanyType] [varchar](100) ,
	[CompanyID] [varchar](20)  ,
	[CompanyVATID] [varchar](20)  ,
	[CompanyTAXID] [varchar](20)  ,
	[IndividualTitle] [varchar](10) ,
	[IndividualFirstName] [varchar](50)  ,
	[IndividualLastName] [varchar](50)  ,
	[IndividualID] [varchar](20)  ,
	[AddressStreet] [varchar](50) ,
	[AddressBuildingNumber] [varchar](20) NOT NULL,
	[AddressCity] [varchar](50) NOT NULL,
	[AddressZipCode] [varchar](6) NOT NULL,
	[AddressCountry] [varchar](50) ,
	[ContactEmail] [varchar](50) ,
	[ContactMobile] [varchar](50) ,
	[ContactPhone] [varchar](50) ,
	[ContactWeb] [varchar](100) ,
	[TCInsertTime] [datetime] ,
	[TCLastUpdate] [datetime] ,
	[TCActive] [int] ,
PRIMARY KEY CLUSTERED 
(
	[PKCustomerDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CATCustomerData]  ADD  CONSTRAINT [DF_CATCustomerData_TCInsertTime] DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[CATCustomerData] ADD  CONSTRAINT [DF_CATCustomerData_TCLastUpdate] DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[CATCustomerData] ADD  CONSTRAINT [DF_CATCustomerData_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO

