﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRent.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseEntities : DbContext
    {
        public DatabaseEntities()
            : base("name=DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Avaiabilities> Avaiabilities { get; set; }
        public virtual DbSet<Bodies> Bodies { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<MileageHistory> MileageHistory { get; set; }
        public virtual DbSet<Models> Models { get; set; }
        public virtual DbSet<Rents> Rents { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Transmissions> Transmissions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
