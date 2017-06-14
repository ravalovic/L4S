USE [log4service]
GO

/****** Object:  Table [dbo].[Catalog_ServiceIdentifiers]    Script Date: 14.06.2017 22:07:12 ******/
DROP TABLE [dbo].[Catalog_ServiceIdentifiers]
GO

/****** Object:  Table [dbo].[Catalog_ServiceIdentifiers]    Script Date: 14.06.2017 22:07:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Catalog_ServiceIdentifiers](
	[PKServiceIdentifiersID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](100) NOT NULL,
	[ServiceCode] [varchar](50) NOT NULL,
	[ServiceIdentifier] [varchar](50) NOT NULL,
	[ServiceNote] [varchar](100) NULL,
	[FKCustomerDataID] [int] NULL,
	[TCInsertTime] [datetime] NULL,
	[TCLastUpdate] [datetime] NULL,
	[TCActive] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PKServiceIdentifiersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Catalog_ServiceIdentifiers] ADD  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[Catalog_ServiceIdentifiers] ADD  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[Catalog_ServiceIdentifiers] ADD  DEFAULT ((0)) FOR [TCActive]
GO

ALTER TABLE [dbo].[Catalog_ServiceIdentifiers]  WITH CHECK ADD FOREIGN KEY([FKCustomerDataID])
REFERENCES [dbo].[Catalog_CustomerData] ([PKCustomerDataID])
GO

