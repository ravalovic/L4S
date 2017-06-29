USE [log4service]
GO
IF OBJECT_ID('dbo.DF_CATCustomerDailyData_RequestedTime', 'D') IS NOT NULL 
ALTER TABLE [dbo].[CATCustomerDailyData] DROP CONSTRAINT [DF_CATCustomerDailyData_RequestedTime]
GO
IF OBJECT_ID('dbo.DF_CATCustomerDailyData_ReceivedBytes', 'D') IS NOT NULL 
ALTER TABLE [dbo].[CATCustomerDailyData] DROP CONSTRAINT [DF_CATCustomerDailyData_ReceivedBytes]
GO
IF OBJECT_ID('dbo.DF_CATCustomerDailyData_NumberOfRequest', 'D') IS NOT NULL 
ALTER TABLE [dbo].[CATCustomerDailyData] DROP CONSTRAINT [DF_CATCustomerDailyData_NumberOfRequest]
GO

/****** Object:  Table [dbo].[CATCustomerDailyData]    Script Date: 15. 6. 2017 16:29:09 ******/
IF OBJECT_ID('dbo.CATCustomerDailyData', 'U') IS NOT NULL 
DROP TABLE [dbo].[CATCustomerDailyData]
GO

/****** Object:  Table [dbo].[CATCustomerDailyData]    Script Date: 15. 6. 2017 16:29:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATCustomerDailyData](
	[RequestDate] [datetime] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[ServiceID] [int] NOT NULL,
	[NumberOfRequest] [bigint] NULL,
	[ReceivedBytes] [bigint] NULL,
	[RequestedTime] [bigint] NULL,
	[TCInsertTime] [datetime] ,
	[TCLastUpdate] [datetime] NULL,
	[TCActive] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CATCustomerDailyData] ADD  CONSTRAINT [DF_CATCustomerDailyData_NumberOfRequest]  DEFAULT ((0)) FOR [NumberOfRequest]
GO

ALTER TABLE [dbo].[CATCustomerDailyData] ADD  CONSTRAINT [DF_CATCustomerDailyData_ReceivedBytes]  DEFAULT ((0)) FOR [ReceivedBytes]
GO

ALTER TABLE [dbo].[CATCustomerDailyData] ADD  CONSTRAINT [DF_CATCustomerDailyData_RequestedTime]  DEFAULT ((0)) FOR [RequestedTime]
GO

ALTER TABLE [dbo].[CATCustomerDailyData]  ADD  CONSTRAINT [DF_CATCustomerDailyData_TCInsertTime]  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[CATCustomerDailyData] ADD  CONSTRAINT [DF_CATCustomerDailyData_TCLastUpdate]  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[CATCustomerDailyData] ADD  CONSTRAINT [DF_CATCustomerDailyData_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO