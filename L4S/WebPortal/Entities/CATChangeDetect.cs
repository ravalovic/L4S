﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    [Table("CATChangeDetect")]
    public partial class CATChangeDetect
    {
        /*****************  Chyba tu primarny kluc  - Napr. autogenerated **********************/
        //[Key]
        //[Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public Int64 ID { get; set; }

        [StringLength(20)]
        public string TableName { get; set; }

        public int Status { get; set; }

        [StringLength(100)]
        public string StatusName { get; set; }

        public DateTime TCInsertTime { get; set; } //DEFAULT (getdate())

        public DateTime TCUpdateTime { get; set; } //DEFAULT (getdate())
    }
}


/*
CREATE TABLE [dbo].CATChangeDetect(
    [TableName] varchar(20),
	[Status] int,
    [StatusName] varchar(100) ,
	[TCInsertTime] [datetime],
    [TCUpdateTime] [datetime]	
*/
