﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StageSchoolProjectAuto2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TEST_StagiairEntities : DbContext
    {
        public TEST_StagiairEntities()
            : base("name=TEST_StagiairEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Carfavs> Carfavs { get; set; }
        public virtual DbSet<Kentekens> Kentekens { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
