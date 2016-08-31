using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TransportNewDriver
/// </summary>
public class VehicleEmployee
{
    public VehicleEmployee()
    {

    }
    [Key()]

    public int ID { get; set; }

    public int? EmployeeType { get; set; }

    public string Name { get; set; }

    public string MobileNumber { get; set; }

    public int? VehicleID { get; set; }

    public int? DLType { get; set; }

    public string DLValidity { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string Address { get; set; }

    public string FatherName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string Qualification { get; set; }

    public string DLNumber { get; set; }

    public string DLScanCopy { get; set; }

    public DateTime? DateOfJoining { get; set; }

    public string ApplicationForm { get; set; }

    public string ContactNoInCaseOfEmergency { get; set; }

    public string PreviousCompanyName { get; set; }

    public string ExperienceInYear { get; set; }

    public string ExperienceInMonth { get; set; }

    public DateTime? ModifyOn { get; set; }

    public bool? IsActive { get; set; }

    public int? TransportTypeID { get; set; }

    public List<TransportEmployeeRelation> TransportEmployeeRelation { get; set; }

    [ForeignKey("VehicleID")]
    public Vehicles Vehicle { get; set; }
}