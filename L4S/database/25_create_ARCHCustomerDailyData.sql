USE [log4service]
GO

/****** Object:  Table [dbo].[ARCHCustomerDailyData]    Script Date: 15. 6. 2017 16:29:09 ******/
IF OBJECT_ID('dbo.ARCHCustomerDailyData', 'U') IS NOT NULL 
DROP TABLE [dbo].[ARCHCustomerDailyData]
GO

/****** Object:  Table [dbo].[ARCHCustomerDailyData]    Script Date: 15. 6. 2017 16:29:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ARCHCustomerDailyData](
	[RequestDate] [datetime],
	[CustomerID] [int] ,
	[ServiceID] [int],
	[NumberOfRequest] [bigint] ,
	[ReceivedBytes] [bigint] ,
	[RequestedTime] [bigint] ,
	[TCInsertTime] [datetime] ,
	[TCLastUpdate] [datetime] ,
	[TCActive] [int] 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ARCHCustomerDailyData]  ADD  CONSTRAINT [DF_ARCHCustomerDailyData_TCInsertTime]  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[ARCHCustomerDailyData] ADD  CONSTRAINT [DF_ARCHCustomerDailyData_TCLastUpdate]  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[ARCHCustomerDailyData] ADD  CONSTRAINT [DF_ARCHCustomerDailyData_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO