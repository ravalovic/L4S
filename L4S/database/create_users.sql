USE [master]
GO

/****** Object:  Login [l4sowner]    Script Date: 30. 5. 2017 10:04:41 ******/
DROP LOGIN [l4sowner]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [l4sowner]    Script Date: 30. 5. 2017 10:04:41 ******/
CREATE LOGIN [l4sowner] WITH PASSWORD=N'6RROxTxVavF1t6eWKERtpciR/v1nyJfsN/WQAUE6ZDk=', DEFAULT_DATABASE=[log4service], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=ON, CHECK_POLICY=ON
GO

ALTER LOGIN [l4sowner] DISABLE
GO


USE [master]
GO

/****** Object:  Login [loader]    Script Date: 30. 5. 2017 10:05:20 ******/
DROP LOGIN [loader]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [loader]    Script Date: 30. 5. 2017 10:05:20 ******/
CREATE LOGIN [loader] WITH PASSWORD=N'GkOlTTiU6rf9B0redplGtrfVGW7n4kNytzyfK0vraHU=', DEFAULT_DATABASE=[log4service], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=ON, CHECK_POLICY=ON
GO

ALTER LOGIN [loader] DISABLE
GO

