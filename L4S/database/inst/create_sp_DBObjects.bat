SET  SRV=localdb
SET  DB=LocalDBL4s
set  SQLRUN="c:\Program Files (x86)\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\SQLCMD.EXE"

for %%A in (.\??_create_view*.sql) do (%SQLRUN% -S "vugkvosk" -i %%A)

for %%A in (.\??_create_sp*.sql) do (%SQLRUN% -S "vugkvosk" -i %%A)

for %%A in (.\??_create_Trigger*.sql) do (%SQLRUN% -S "vugkvosk" -i %%A)

for %%A in (.\??_insert*.sql) do (%SQLRUN% -S "vugkvosk" -i %%A -f 65001)

for %%A in (.\??_grant*.sql) do (%SQLRUN% -S "vugkvosk" -i %%A)

for %%A in (.\??_create_index*.sql) do (%SQLRUN% -S "vugkvosk" -i %%A)
pause