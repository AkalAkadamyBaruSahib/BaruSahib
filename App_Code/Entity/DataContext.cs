﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


/// <summary>
/// Summary description for DataContext
/// </summary>
/// 
namespace AkalAcademy
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=ConnectionString")
        {
        }

        public DbSet<Incharge> Incharge { get; set; }
        public DbSet<AcademyAssignToEmployee> AcademyAssignToEmployee { get; set; }
        public DbSet<ComplaintTickets> ComplaintTickets { get; set; }
        public DbSet<Zone> Zone { get; set; }
        public DbSet<Academy> Academy { get; set; }
        public DbSet<TransportZoneAcademyRelation> TransportZoneAcademyRelation { get; set; }
        public DbSet<Estimate> Estimate { get; set; }
        public DbSet<EstimateAndMaterialOthersRelations> EstimateAndMaterialOthersRelations { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<VechilesDocumentRelation> VechilesDocumentRelation { get; set; }
        public DbSet<Visitors> Visitors { get; set; }
        public DbSet<BuildingName> BuildingName { get; set; }
        public DbSet<RoomNumbers> RoomNumbers { get; set; }
        public DbSet<VisitorRoomNumbers> VisitorRoomNumbers { get; set; }
        public DbSet<VisitorType> VisitorType { get; set; }
        public DbSet<TransportEmployeeRelation> TransportEmployeeRelation { get; set; }
        public DbSet<VehicleEmployee> VehicleEmployee { get; set; }
        public DbSet<Material> Material { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention
            // If you keep this convention then the generated tables will have pluralized names.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Visitors>().HasMany(r => r.VisitorRoomNumbers).WithOptional().HasForeignKey(r => r.VisitorID);

        }
    }
}