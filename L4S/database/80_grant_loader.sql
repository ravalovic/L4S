use log4service
go
grant select, insert on STLogImport to loader;
go
grant execute on sp_STInputFileInfo to loader;
go
grant execute on sp_STCheckFileDuplicity to loader;
go
