﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineOlympDesctop
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnlineOlymp2016Entities : DbContext
    {
        public OnlineOlymp2016Entities()
            : base("name=OnlineOlymp2016Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<FileType> FileType { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonFile> PersonFile { get; set; }
        public virtual DbSet<PlaceList> PlaceList { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<SchoolClass> SchoolClass { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Sex> Sex { get; set; }
        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<Columns> Columns { get; set; }
        public virtual DbSet<PrintList> PrintList { get; set; }
        public virtual DbSet<PrintListColumns> PrintListColumns { get; set; }
        public virtual DbSet<PrintListOrder> PrintListOrder { get; set; }
    }
}
