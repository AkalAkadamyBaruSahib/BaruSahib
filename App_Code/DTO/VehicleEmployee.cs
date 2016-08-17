using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VehicleEmployee
/// </summary>
public class VehicleEmployeeDTO
{
    public int ID { get; set; }
    public int? EmployeeType { get; set; }
    public string Name { get; set; }
    public string MobileNumber { get; set; }
    public int? VehicleID { get; set; }
    public int? DLType { get; set; }
    public string DLValidity { get; set; }
    public string CreatedOn { get; set; }
    public string Address { get; set; }
    public string FatherName { get; set; }
    public string DateOfBirth { get; set; }
    public string Qualification { get; set; }
    public string DLNumber { get; set; }
    public string DLScanCopy { get; set; }
    public string DateOfJoining { get; set; }
    public string ApplicationForm { get; set; }
    public string ContactNoInCaseOfEmergency { get; set; }
    public string PreviousCompanyName { get; set; }
    public string ExperienceInYear { get; set; }
    public string ExperienceInMonth { get; set; }
    public string ModifyOn { get; set; }
    public bool? IsActive { get; set; }
    public int? TransportTypeID { get; set; }

    public List<TransportEmployeeRelationDTO> TransportEmployeeRelationDTO { get; set; }
}

public class TransportEmployeeRelationDTO
{
    public int ID { get; set; }
    public int VehicleEmployeeID { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Relation { get; set; }
    public bool? Nominee { get; set; }
    public string PhoneNo { get; set; }
    public string Address { get; set; }
    public int RelationTypeID { get; set; }
    public string CreatedOn { get; set; }
    public string ModifyOn { get; set; }
}