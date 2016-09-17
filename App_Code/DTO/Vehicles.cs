using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Vehicles
/// </summary>
public class VehiclesDTO
{
    public int? TypeID { get; set; }

    public string Number { get; set; }

    public int? Sitter { get; set; }

    public string OwnerName { get; set; }

    public string OwnerNumber { get; set; }

    public bool? IsApproved { get; set; }

    public int? AcademyID { get; set; }

    public int? ZoneID { get; set; }

    public int ID { get; set; }

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

    public int? ConductorID { get; set; }

    public int? DriverID { get; set; }

    public int? VehicleContractRate { get; set; }

    public decimal? VehicleAverage { get; set; }
}

public class VehicleIDAndNumber
{
    public int ID { get; set; }
    public string Number { get; set; }
}