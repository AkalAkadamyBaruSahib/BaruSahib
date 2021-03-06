﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Vehicles
/// </summary>
/// 
[Table("Vehicles")]
public class Vehicles
{

    public int TypeID { get; set; }
    public string Number { get; set; }
    public int? Sitter { get; set; }
    public string OwnerName { get; set; }
    public string OwnerNumber { get; set; }
    public bool IsApproved { get; set; }
    public int AcademyID { get; set; }
    public int ZoneID { get; set; }
    [Key()]
    public int ID { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? ModifyOn { get; set; }
    public bool? IsTemporary { get; set; }
    public string FileNumber { get; set; }
    public string EngineNumber { get; set; }
    public string ChassisNumber { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public bool? WrittenContract { get; set; }
    public int? PeriodOfContract { get; set; }
    public decimal? ContractDieselRate { get; set; }
    public string OilSlab { get; set; }
    public string FrontRightTyreCondition { get; set; }
    public string FrontLeftTyreCondition { get; set; }
    public string RearRightTyreCondition { get; set; }
    public string RearLeftTyreCondition { get; set; }
    public int? KMPerDay { get; set; }
    public string RearRightTyre2Condition { get; set; }
    public string RearLeftTyre2Condition { get; set; }
    public int? NumberOfTypres { get; set; }
    public int? VehicleContractRate { get; set; }
    public decimal? VehicleAverage { get; set; }
    public bool? IsCompanyOwned { get; set; }


    [ForeignKey("ZoneID")]
    public Zone Zone { get; set; }

    [ForeignKey("AcademyID")]
    public Academy Academy { get; set; }

    [ForeignKey("TypeID")]
    public TransportTypes TransportTypes { get; set; }

    public List<VehicleEmployee> VehicleEmployee { get; set; }
  
}