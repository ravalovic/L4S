USE [master]
GO
/****** Object:  Database [log4service]    Script Date: 13. 6. 2017 11:33:56 ******/
CREATE DATABASE [log4service]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'log4service', FILENAME = N'C:\Users\sl876ratvalo\log4service.mdf' , SIZE = 1515520KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'log4service_log', FILENAME = N'C:\Users\sl876ratvalo\log4service_log.ldf' , SIZE = 1318912KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [log4service] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [log4service].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [log4service] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [log4service] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [log4service] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [log4service] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [log4service] SET ARITHABORT OFF 
GO
ALTER DATABASE [log4service] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [log4service] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [log4service] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [log4service] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [log4service] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [log4service] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [log4service] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [log4service] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [log4service] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [log4service] SET  ENABLE_BROKER 
GO
ALTER DATABASE [log4service] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [log4service] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [log4service] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [log4service] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [log4service] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [log4service] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [log4service] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [log4service] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [log4service] SET  MULTI_USER 
GO
ALTER DATABASE [log4service] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [log4service] SET DB_CHAINING OFF 
GO
ALTER DATABASE [log4service] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [log4service] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [log4service] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [log4service] SET QUERY_STORE = OFF
GO
USE [log4service]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [log4service]
GO
/****** Object:  User [loader]    Script Date: 13. 6. 2017 11:33:56 ******/
CREATE USER [loader] FOR LOGIN [loader] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [l4sowner]    Script Date: 13. 6. 2017 11:33:57 ******/
CREATE USER [l4sowner] FOR LOGIN [l4sowner] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [l4sowner]
GO
/****** Object:  Table [dbo].[Catalog_LogService]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalog_LogService](
	[batchID] [bigint] NOT NULL,
	[fileID] [bigint] NOT NULL,
	[insertDateTime] [datetime] NOT NULL,
	[customerID] [nvarchar](50) NULL,
	[serviceID] [int] NULL,
	[col1] [nvarchar](50) NULL,
	[col2] [nvarchar](50) NULL,
	[col3] [nvarchar](50) NULL,
	[col4] [nvarchar](50) NULL,
	[col5] [nvarchar](50) NULL,
	[col6] [nvarchar](50) NULL,
	[col7] [nvarchar](50) NULL,
	[col8] [nvarchar](50) NULL,
	[col9] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Catalog_Service]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalog_Service](
	[serviceID] [int] IDENTITY(1,1) NOT NULL,
	[Identifier] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[userID] [int] NULL,
	[insertDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[serviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImportESKN$]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportESKN$](
	[Code] [nvarchar](255) NULL,
	[Service] [nvarchar](255) NULL,
	[Entity] [nvarchar](255) NULL,
	[Explanation] [nvarchar](255) NULL,
	[DatSelectMethod] [nvarchar](255) NULL,
	[RootURLProd] [nvarchar](255) NULL,
	[RootURLTest] [nvarchar](255) NULL,
	[TemplateFirewallPROD] [nvarchar](max) NULL,
	[TemplateFirewallPRODSample] [nvarchar](max) NULL,
	[KOSet] [nvarchar](255) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KN_C_CADASTRAL_UNITS$]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KN_C_CADASTRAL_UNITS$](
	[ID] [float] NULL,
	[CADASTRAL_UNIT_CODE] [float] NULL,
	[CADASTRAL_UNIT_NAME] [nvarchar](255) NULL,
	[MUNICIPALITY_ID] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KN_C_DISTRICTS$]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KN_C_DISTRICTS$](
	[ID] [float] NULL,
	[DISTRICT_CODE] [float] NULL,
	[DISTRICT_NAME] [nvarchar](255) NULL,
	[REGION_ID] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KN_C_MUNICIPALITIES$]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KN_C_MUNICIPALITIES$](
	[ID] [float] NULL,
	[MUNICIPALITY_CODE] [float] NULL,
	[MUNICIPALITY_NAME] [nvarchar](255) NULL,
	[DISTRICT_ID] [float] NULL,
	[CADASTRAL_ADM_UNIT_ID] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KN_C_REGIONS$]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KN_C_REGIONS$](
	[ID] [float] NULL,
	[REGION_CODE] [float] NULL,
	[REGION_NAME] [nvarchar](255) NULL,
	[F4] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stage_InputFileDuplicity]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stage_InputFileDuplicity](
	[id] [int] NOT NULL,
	[fileName] [varchar](100) NOT NULL,
	[checksum] [varchar](50) NOT NULL,
	[loadDateTime] [datetime] NOT NULL,
	[insertDateTime] [datetime] NOT NULL,
	[oriFileName] [varchar](100) NULL,
	[loaderBatchID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stage_InputFileInfo]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stage_InputFileInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [varchar](100) NOT NULL,
	[checksum] [varchar](50) NOT NULL,
	[insertDateTime] [datetime] NOT NULL,
	[loaderBatchID] [int] NOT NULL,
 CONSTRAINT [PK_Stage_InputFileInfo] PRIMARY KEY CLUSTERED 
(
	[checksum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stage_LogImport]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stage_LogImport](
	[batchID] [bigint] NOT NULL,
	[originalFileName] [varchar](100) NOT NULL,
	[originalCheckSum] [varchar](100) NOT NULL,
	[preProcessFileName] [varchar](100) NOT NULL,
	[Node_IP_Address] [varchar](50) NULL,
	[UserID] [varchar](50) NULL,
	[Date_Of_Request] [varchar](30) NULL,
	[Requested_URL] [varchar](max) NULL,
	[Request_Status] [varchar](5) NULL,
	[Bytes_Sent] [varchar](15) NULL,
	[Request_Time] [varchar](15) NULL,
	[Unknown] [varchar](max) NULL,
	[User_Agent] [varchar](500) NULL,
	[User_IP_Address] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Catalog_LogService] ADD  DEFAULT (getdate()) FOR [insertDateTime]
GO
ALTER TABLE [dbo].[Catalog_Service] ADD  DEFAULT (getdate()) FOR [insertDateTime]
GO
ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_insertDateTime]  DEFAULT (getdate()) FOR [loadDateTime]
GO
ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_insertDateTime_1]  DEFAULT (getdate()) FOR [insertDateTime]
GO
ALTER TABLE [dbo].[Stage_InputFileDuplicity] ADD  CONSTRAINT [DF_Stage_InputFileDuplicity_LoaderBatchID]  DEFAULT ((-1)) FOR [loaderBatchID]
GO
ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_checksum]  DEFAULT (getdate()) FOR [checksum]
GO
ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_insertDateTime]  DEFAULT (getdate()) FOR [insertDateTime]
GO
ALTER TABLE [dbo].[Stage_InputFileInfo] ADD  CONSTRAINT [DF_Stage_InputFileInfo_loaderBatchID]  DEFAULT ((-1)) FOR [loaderBatchID]
GO
/****** Object:  StoredProcedure [dbo].[sp_stage_fileInfo]    Script Date: 13. 6. 2017 11:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_stage_fileInfo]
	-- Add the parameters for the stored procedure here
	@FileName varchar(100),
	@FileCheckSum varchar(50),
	@BatchID int,
	@RetVal int output
AS
declare 
@oriID int,
@oriFN varchar(100),
@oriINDT datetime
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
INSERT INTO [dbo].[Stage_InputFileInfo] ([fileName],[checksum], [loaderBatchID])
     VALUES(@FileName, @FileCheckSum, @BatchID);
	 SET @RetVal = 0;
	  return @RetVal;
END TRY
BEGIN CATCH

SELECT @oriID = ID, @oriFN = fileName, @oriINDT = insertDateTime 
 from [Stage_InputFileInfo] t where t.checksum = @FileCheckSum;

INSERT INTO [dbo].[Stage_InputFileDuplicity] ([id], [fileName],[checksum], [loadDateTime], [oriFilename],[loaderBatchID])
     VALUES(@oriID, @FileName, @FileCheckSum, @oriINDT, @oriFN, @BatchID);
	 SET @RetVal = -1;
	 return @RetVal;
END CATCH;

GO
USE [master]
GO
ALTER DATABASE [log4service] SET  READ_WRITE 
GO
