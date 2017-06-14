use log4service

grant select, insert on Stage_LogImport to loader;
grant execute on sp_stage_InputFileInfo to loader;
grant execute on sp_stage_CheckFileDuplicity to loader;

