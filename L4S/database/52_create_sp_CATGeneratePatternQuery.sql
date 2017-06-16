declare 
@ServiceID int = 1,
@myLike varchar(2000),
@mySelect varchar(8000),
@myWhere varchar(100) = 'where l.RequestedURL like ''%',
@myOR varchar(100) = ' or l.RequestedURL like ''%',
@myENDLike varchar(4) = '%''',
@i int = 0
begin
SET @mySelect = 'SELECT l.DateOfRequest, l.RequestedURL, l.UserIPAddress from STLogImport l ';
declare like_cursor CURSOR FOR select p.PatternCode from CATServicePatterns p where p.FKServiceID = @ServiceID;
OPEN like_cursor
FETCH NEXT FROM like_cursor INTO @myLike


WHILE @@FETCH_STATUS = 0   
BEGIN   
       IF (@i=0)
	     SET @mySelect = @mySelect + @myWhere + @myLike+  @myENDLike;
	   ELSE 
	     SET @mySelect = @mySelect + @myOR + @myLike + @myENDLike;
	   --print @myLike;
	   --print @mySelect;
       FETCH NEXT FROM like_cursor INTO @myLike   
	   SET @i = @i+1;
END   
CLOSE like_cursor   
DEALLOCATE like_cursor

--print @mySelect;
exec(@mySelect);

end;