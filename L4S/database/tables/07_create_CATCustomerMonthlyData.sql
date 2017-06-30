USE [log4service]
GO
IF OBJECT_ID('dbo.DF_CATCustomerMonthlyData_RequestedTime', 'D') IS NOT NULL 
ALTER TABLE [dbo].[CATCustomerMonthlyData] DROP CONSTRAINT [DF_CATCustomerMonthlyData_RequestedTime]
GO
IF OBJECT_ID('dbo.DF_CATCustomerMonthlyData_ReceivedBytes', 'D') IS NOT NULL 
ALTER TABLE [dbo].[CATCustomerMonthlyData] DROP CONSTRAINT [DF_CATCustomerMonthlyData_ReceivedBytes]
GO
IF OBJECT_ID('dbo.DF_CATCustomerMonthlyData_NumberOfRequest', 'D') IS NOT NULL 
ALTER TABLE [dbo].[CATCustomerMonthlyData] DROP CONSTRAINT [DF_CATCustomerMonthlyData_NumberOfRequest]
GO

/****** Object:  Table [dbo].[CATCustomerMonthlyData]    Script Date: 15. 6. 2017 16:29:09 ******/
IF OBJECT_ID('dbo.CATCustomerMonthlyData', 'U') IS NOT NULL 
DROP TABLE [dbo].[CATCustomerMonthlyData]
GO

/****** Object:  Table [dbo].[CATCustomerMonthlyData]    Script Date: 15. 6. 2017 16:29:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATCustomerMonthlyData](
	[RequestDate] [datetime] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[ServiceID] [int] NOT NULL,
	[NumberOfRequest] [bigint] NULL,
	[ReceivedBytes] [bigint] NULL,
	[RequestedTime] [decimal](18, 3) NULL,
	[TCInsertTime] [datetime] ,
	[TCLastUpdate] [datetime] NULL,
	[TCActive] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CATCustomerMonthlyData] ADD  CONSTRAINT [DF_CATCustomerMonthlyData_NumberOfRequest]  DEFAULT ((0)) FOR [NumberOfRequest]
GO

ALTER TABLE [dbo].[CATCustomerMonthlyData] ADD  CONSTRAINT [DF_CATCustomerMonthlyData_ReceivedBytes]  DEFAULT ((0)) FOR [ReceivedBytes]
GO

ALTER TABLE [dbo].[CATCustomerMonthlyData] ADD  CONSTRAINT [DF_CATCustomerMonthlyData_RequestedTime]  DEFAULT ((0)) FOR [RequestedTime]
GO

ALTER TABLE [dbo].[CATCustomerMonthlyData]  ADD  CONSTRAINT [DF_CATCustomerMonthlyData_TCInsertTime]  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[CATCustomerMonthlyData] ADD  CONSTRAINT [DF_CATCustomerMonthlyData_TCLastUpdate]  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[CATCustomerMonthlyData] ADD  CONSTRAINT [DF_CATCustomerMonthlyData_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO