use log4service
go
grant select, insert on STLogImport to loader;
go
grant execute on sp_STInputFileInfo to loader;
go
grant execute on sp_STCheckFileDuplicity to loader;
go
grant execute on sp_CATRunDataProcessing to loader;
go
grant execute on sp_CATGetBatchID to loader;
go
grant execute on sp_CATGetServiceQuery to loader;
go
grant execute on sp_CATPreProcess to loader;
go
grant execute on sp_STUpdateCustomerID to loader;
go