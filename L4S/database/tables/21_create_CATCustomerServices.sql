USE [log4service]
GO
IF (OBJECT_ID('DF_CATCustomerServices_TCInsertTime', 'D') IS NOT NULL)
ALTER TABLE [dbo].[CATCustomerServices]  DROP  CONSTRAINT DF_CATCustomerServices_TCInsertTime 
GO
IF (OBJECT_ID('DF_CATCustomerServices_TCLastUpdate', 'D') IS NOT NULL)
ALTER TABLE [dbo].[CATCustomerServices] DROP  CONSTRAINT DF_CATCustomerServices_TCLastUpdate 
GO
IF (OBJECT_ID('DF_CATCustomerServices_TCActive', 'D') IS NOT NULL)
ALTER TABLE [dbo].[CATCustomerServices] DROP  CONSTRAINT [DF_CATCustomerServices_TCActive]  
GO


/****** Object:  Table [dbo].[CATCustomerServices]    Script Date: 14.06.2017 22:07:12 ******/
IF OBJECT_ID('dbo.CATCustomerServices', 'U') IS NOT NULL 
  DROP TABLE dbo.CATCustomerServices; 
GO

/****** Object:  Table [dbo].[CATCustomerServices]    Script Date: 14.06.2017 22:07:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATCustomerServices](
	[PKServiceCustomerIdentifiersID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NOT NULL,
	[ServiceName] [varchar](100) NOT NULL,
	[ServiceCode] [varchar](50) NOT NULL,
	[ServiceCustomerName] [varchar](100),
	[ServiceNote] [varchar](100) NULL,
	[FKCustomerDataID] [int] NULL,
	[TCInsertTime] [datetime] NULL,
	[TCLastUpdate] [datetime] NULL,
	[TCActive] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PKServiceCustomerIdentifiersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CATCustomerServices]  ADD  CONSTRAINT [DF_CATCustomerServices_TCInsertTime]  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[CATCustomerServices] ADD  CONSTRAINT [DF_CATCustomerServices_TCLastUpdate]  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[CATCustomerServices] ADD  CONSTRAINT [DF_CATCustomerServices_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO

ALTER TABLE [dbo].[CATCustomerServices]  WITH CHECK ADD  CONSTRAINT [DF_CATCustomerServices_FKCustomerDataID] FOREIGN KEY([FKCustomerDataID])
REFERENCES [dbo].[CATCustomerData] ([PKCustomerDataID])
GO

ALTER TABLE [dbo].[CATCustomerServices]  WITH CHECK ADD  CONSTRAINT [DF_CATCustomerServices_FKServiceID] FOREIGN KEY([ServiceID])
REFERENCES [dbo].[CATServiceParameters] ([PKServiceID])
GO
