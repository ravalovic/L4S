use log4service;
delete  FROM STInputFileInfo;  
delete from  STInputFileDuplicity;
truncate table STLogImport;
delete from STLogImport;
truncate table CATUnknownService;
delete from CATUnknownService;
truncate table CATLogsOfService;
delete from CATLogsOfService;
delete from CATProcessStatus;
delete from CATCustomerMonthlyData;
delete from CATCustomerDailyData;
delete from CATChangeDetect;
delete from CATBillingInfo;
delete from CATInvoiceByDay;
delete from CATInvoiceByMonth
delete from CATCustomerServiceDetailInvoice;
delete from CATSummaryInvoice;
delete from GAPAnalyze;

