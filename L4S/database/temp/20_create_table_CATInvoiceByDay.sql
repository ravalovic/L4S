USE [log4service]
GO

/****** Object:  Table [dbo].[CATInvoiceByDay]    Script Date: 09.09.2017 0:25:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATInvoiceByDay](
	[ID] [int] NOT NULL,
	[DateOfRequest] [datetime] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[CustomerIdentification] [nvarchar](20) NULL,
	[CustomerName] [nvarchar](100) NULL,
	[ServiceID] [int] NOT NULL,
	[ServiceCode] [nvarchar](50) NULL,
	[ServiceDescription] [nvarchar](150) NULL,
	[NumberOfRequest] [bigint] NOT NULL,
	[ReceivedBytes] [decimal](18, 5) NOT NULL,
	[RequestedTime] [decimal](18, 5) NOT NULL,
	[CustomerServiceCode] [nvarchar](50) NULL,
	[CustomerServicename] [nvarchar](150) NULL,
	[UnitPrice] [decimal](18, 5) NOT NULL,
	[MeasureofUnits] [nvarchar](50) NULL,
	[BasicPriceWithoutVAT] [decimal](18, 5) NOT NULL,
	[VAT] [decimal](18, 5) NOT NULL,
	[BasicPriceWithVAT] [decimal](18, 5) NOT NULL,
	[TCInsertTime] [datetime] NULL,
	[TCLastUpdate] [datetime] NULL,
	[TCActive] [int] NULL,
	
 CONSTRAINT [PK_dbo.CATInvoiceByDay] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CATInvoiceByDay] ADD  CONSTRAINT [DF_dbo.CATInvoiceByDay_TCInsertTime]  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[CATInvoiceByDay] ADD  CONSTRAINT [DF_dbo.CATInvoiceByDay_TCLastUpdate]  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[CATInvoiceByDay] ADD  CONSTRAINT [DF_dbo.CATInvoiceByDay_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO


