user l4sowner
user loader

loader - L0aDer,12
l4sowner - 0wNer,123




/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [l4sowner]    Script Date: 15. 6. 2017 9:28:56 ******/
CREATE LOGIN [l4sowner] WITH PASSWORD=N'joEKFI+ujXmKTlVHY3aYxJgqg7DCqwSrhuRMyNV6xOE=', DEFAULT_DATABASE=[log4service], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=ON, CHECK_POLICY=ON
GO

ALTER LOGIN [l4sowner] DISABLE
GO



USE [log4service]
GO

/****** Object:  User [loader]    Script Date: 15. 6. 2017 9:27:58 ******/
DROP USER [loader]
GO

/****** Object:  User [loader]    Script Date: 15. 6. 2017 9:27:58 ******/
CREATE USER [loader] FOR LOGIN [loader] WITH DEFAULT_SCHEMA=[dbo]
GO

USE [log4service]
GO

/****** Object:  User [l4sowner]    Script Date: 15. 6. 2017 9:28:33 ******/
DROP USER [l4sowner]
GO

/****** Object:  User [l4sowner]    Script Date: 15. 6. 2017 9:28:33 ******/
CREATE USER [l4sowner] FOR LOGIN [l4sowner] WITH DEFAULT_SCHEMA=[dbo]
GO

USE [master]
GO

/****** Object:  Login [l4sowner]    Script Date: 15. 6. 2017 9:28:56 ******/
DROP LOGIN [l4sowner]
GO


