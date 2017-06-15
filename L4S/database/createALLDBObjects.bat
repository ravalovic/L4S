SET  SRV=localdb
SET  DB=LocalDBL4s
for %%A in (.\??_*.sql) do (
"c:\Program Files (x86)\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\"SQLCMD.EXE -S "(localdb)\LocalDBL4s" -i %%A
)
pause