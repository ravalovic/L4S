﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPortal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class log4serviceEntities : DbContext
    {
        public log4serviceEntities()
            : base("name=log4serviceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Catalog_Service> Catalog_Service { get; set; }
        public virtual DbSet<Stage_InputFileInfo> Stage_InputFileInfo { get; set; }
        public virtual DbSet<Catalog_LogService> Catalog_LogService { get; set; }
        public virtual DbSet<Stage_InputFileDuplicity> Stage_InputFileDuplicity { get; set; }
        public virtual DbSet<Stage_LogImport> Stage_LogImport { get; set; }
    }
}