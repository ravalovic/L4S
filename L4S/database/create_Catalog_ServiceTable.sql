USE [log4service]
GO

/****** Object:  Table [dbo].[Stage_TestTable]    Script Date: 27.05.2017 20:06:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
if OBJECT_ID('log4service..Catalog_Service') is not null
begin 
 drop table log4service..Catalog_Service
end;

CREATE TABLE [dbo].[Catalog_Service](
	[serviceID] int not null  IDENTITY(1,1) PRIMARY KEY,
	[Identifier] nvarchar(50) not null ,
	[Description] nvarchar(50) not null,
	[userID] int,
	[insertDateTime] datetime not null default GETDATE(),
) ON [PRIMARY]
GO


