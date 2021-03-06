﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebPortal.Models;

namespace WebPortal.DataContexts
{
    // To use automatic migrations and DB creation script enable with command in package manager console, run only one
    // > enable-migrations 
    // Add migration command in PM console, run everytime has DB classes changed
    // > add-migration 2017-01-01_Descrition
    // than update DB with command
    // > update-database

    public class L4SDb : IdentityDbContext<ApplicationUser>
    {
        public L4SDb()
            : base("DefaultConnection") //used connection string
        {

        }

        public static L4SDb Create()
        {
            return new L4SDb();
        }

        //Access to tables
        public DbSet<CATOwnerData> CATOwnerData { get; set; }
        public DbSet<CATCustomerData> CATCustomerData { get; set; }
        public DbSet<CATCustomerIdentifiers> CATCustomerIdentifiers { get; set; }
        public DbSet<CATCustomerServices> CATCustomerServices { get; set; }
        public DbSet<CATServiceParameters> CATServiceParameters { get; set; }
        public DbSet<CATServicePatterns> CATServicePatterns { get; set; }

        public DbSet<STLogImport> STLogImport { get; set; }
        public DbSet<CATLogsOfService > CATLogsOfService { get; set; }
        public DbSet<CATUnknownService> CATUnknownService { get; set; }
        
        public DbSet<CATCustomerDailyData> CATCustomerDailyData { get; set; }
        public DbSet<CATCustomerMonthlyData> CATCustomerMonthlyData { get; set; }

        public DbSet<STInputFileInfo> STInputFileInfo { get; set; }
        public DbSet<STInputFileDuplicity> STInputFileDuplicity { get; set; }

        public DbSet<CATProcessStatus> CATProcessStatus { get; set; }
        public DbSet<CATChangeDetect> CATChangeDetect { get; set; }

       public DbSet<CATCustomerServiceDetailInvoice> CATCustomerServiceDetailInvoice { get; set; }
        public DbSet<CATSummaryInvoice> CATSummaryInvoice { get; set; }
        public DbSet<CONFGeneralSettings> CONFGeneralSettings { get; set; }
        public DbSet<GAPAnalyze> GAPAnalyze { get; set; }
        public DbSet<CATInvoiceByDay> CATInvoiceByDay { get; set; }
        public DbSet<CATInvoiceByMonth> CATInvoiceByMonth { get; set; }


        //archive tables
        public DbSet<ARCHCustomerDailyData> ARCHCustomerDailyData { get; set; }
        public DbSet<ARCHCustomerMonthlyData> ARCHCustomerMonthlyData { get; set; }
        public DbSet<ARCHLogsOfService> ARCHLogsOfService { get; set; }
        public DbSet<ARCHProcessStatus> ARCHProcessStatus { get; set; }
        public DbSet<ARCHInputFileDuplicity> ARCHInputFileDuplicity { get; set; }
        public DbSet<ARCHInputFileInfo> ARCHInputFileInfo { get; set; }
        

        //views
        public virtual DbSet<view_DetailFromMonthly> view_DetailFromMonthly { get; set; }
        public virtual DbSet<view_InvoiceByDay> view_InvoiceByDay { get; set; }
        public virtual DbSet<view_InvoiceByMonth> view_InvoiceByMonth { get; set; }
        public virtual DbSet<view_DailyData> view_DailyData { get; set; }
        public virtual DbSet<view_MonthlyData> view_MonthlyData { get; set; }
        public virtual DbSet<view_CustomerMontlyTotalInvoice> view_CustomerMontlyTotalInvoice { get; set; }

    }
   
    
}