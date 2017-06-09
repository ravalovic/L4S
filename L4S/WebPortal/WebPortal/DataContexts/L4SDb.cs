using L4S.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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
        public DbSet<Catalog_LogService> Catalog_LogServices { get; set; }
        public DbSet<Catalog_Service> Catalog_Services { get; set; }
        public DbSet<Stage_InputFileDuplicity> Stage_InputFileDuplicities { get; set; }
        public DbSet<Stage_InputFileInfo> Stage_InputFileInfos { get; set; }
        public DbSet<Stage_LogImport> Stage_LogImports { get; set; }

    }
   
    
}