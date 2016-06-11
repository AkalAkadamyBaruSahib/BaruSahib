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
        public DbSet<PurchaseSource> PurchaseSource { get; set; }
        public DbSet<VendorInfo> VendorInfo { get; set; }
        public DbSet<VendorMaterialRelation> VendorMaterialRelation { get; set; }
        public DbSet<StoreMaterialBill> StoreMaterialBill { get; set; }
        public DbSet<VechiclesRouteMap> VechiclesRouteMap { get; set; }
        public DbSet<MaterialRateApproved> MaterialRateApproved { get; set; }
        public DbSet<SecurityEmployeeInfo> SecurityEmployeeInfo { get; set; }
        public DbSet<MaterialNonApprovedRate> MaterialNonApprovedRate { get; set; }
        public DbSet<POBillingAddress> POBillingAddress { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<MaterialType> MaterialType { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<WorkAllot> WorkAllot { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention
            // If you keep this convention then the generated tables will have pluralized names.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Visitors>().HasMany(r => r.VisitorRoomNumbers).WithOptional().HasForeignKey(r => r.VisitorID);

            modelBuilder.Entity<VendorInfo>().HasMany(v => v.VendorMaterialRelations).WithOptional().HasForeignKey(r => r.VendorID);
            modelBuilder.Entity<Material>().HasMany(v => v.VendorMaterialRelation).WithOptional().HasForeignKey(r => r.MatID);
          

        }
    }
}