USE log4service
GO
DECLARE
 @roleName varchar(100) = 'loader' ,
 @myQuery varchar(100)
BEGIN
DECLARE myCursor CURSOR FOR SELECT 'GRANT EXEC ON [' + ROUTINE_SCHEMA + '].[' + ROUTINE_NAME + '] TO [' + @roleName + '];'
FROM INFORMATION_SCHEMA.Routines
WHERE ROUTINE_TYPE = 'Procedure'
AND ROUTINE_NAME NOT LIKE '%diagram%'
OPEN myCursor
FETCH NEXT FROM myCursor INTO @myQuery
		    WHILE @@FETCH_STATUS = 0   
		    BEGIN         
				   exec(@myquery);
				   FETCH NEXT FROM myCursor INTO @myQuery
		    END   
		    CLOSE myCursor   
		    DEALLOCATE myCursor
END

GO
grant select, insert on STLogImport to loader;
go