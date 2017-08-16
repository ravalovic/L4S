USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_GAPAnalyze]  Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_GAPAnalyze]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_GAPAnalyze]
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_GAPAnalyze]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GAPAnalyze] 
	@mydebug int = 0
AS

DECLARE @StartDate DATE 
  , @EndDate DATE
  , @GapDays int = 10
BEGIN
       SELECT @GapDays = convert(int,ParamValue) FROM CONFGeneralSettings WHERE ParamName = 'GAPAnalyzeDays';

       SELECT @StartDate = cast(dateadd(day,-@GapDays,GETDATE()) as date); 
       SELECT @EndDate = cast(GETDATE() as date);

		DECLARE @DT TABLE
		(
		   GenDate DATE
		   , Number int
		   , fileName varchar(2000)
		 );
		
		DECLARE @CF TABLE
		(
		   GenDate DATE
		   , Number int
		   , fileName varchar(2000)
		   , InserTime datetime
		);
		
		INSERT INTO @DT (Number, GenDate) 
		SELECT  0, DATEADD(DAY, nbr - 1, @StartDate)
		FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY c.object_id ) AS Nbr
		          FROM      sys.columns c
		        ) nbrs
		WHERE   nbr - 1 <= DATEDIFF(DAY, @StartDate, @EndDate);
		
		INSERT INTO @CF (Number, GenDate, FileName, InserTime)  
		 SELECT count(*), CAST(f.InsertDateTime as date) RDate, f.OriFileName, f.InsertDateTime  FROM STInputFileInfo f
		   WHERE CAST(f.InsertDateTime as date) >=@StartDate and CAST(InsertDateTime as date) <=@EndDate
		 GROUP BY CAST(InsertDateTime as date), f.OriFileName, f.InsertDateTime;

		DBCC CHECKIDENT ('GAPAnalyze', RESEED, 0);
		DELETE FROM GAPAnalyze;
	
		INSERT INTO GAPAnalyze (Day, FileNumber, FileName, TCInsertTime)
		SELECT d.GenDate, c.Number, c.fileName, InserTime FROM  @DT d 
			left join @CF c on c.GenDate=d.GenDate order by InserTime;
END