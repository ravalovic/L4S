USE [log4service]
GO

/****** Object:  Table [dbo].[ARCHCustomerMonthlyData]    Script Date: 15. 6. 2017 16:29:09 ******/
IF OBJECT_ID('dbo.ARCHCustomerMonthlyData', 'U') IS NOT NULL 
DROP TABLE [dbo].[ARCHCustomerMonthlyData]
GO

/****** Object:  Table [dbo].[ARCHCustomerMonthlyData]    Script Date: 15. 6. 2017 16:29:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ARCHCustomerMonthlyData](
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

ALTER TABLE [dbo].[ARCHCustomerMonthlyData]  ADD  CONSTRAINT [DF_ARCHCustomerMonthlyData_TCInsertTime]  DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].[ARCHCustomerMonthlyData] ADD  CONSTRAINT [DF_ARCHCustomerMonthlyData_TCLastUpdate]  DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].[ARCHCustomerMonthlyData] ADD  CONSTRAINT [DF_ARCHCustomerMonthlyData_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO