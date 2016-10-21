using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Visitors
/// </summary>
public class Visitors
{
    public Visitors()
    {

    }

    [Key()]
    public int ID { get; set; }
    public string Name { get; set; }
    public int? TotalNoOfMen { get; set; }
    public int? TotalNoOfWomen { get; set; }
    public int? TotalNoOfChildren { get; set; }
    public string PurposeOfVisit { get; set; }
    public string VehicleNo { get; set; }
    public string Identification { get; set; }
    public string IdentificationPath { get; set; }
    public int? NoOfDaysToStay { get; set; }
    public string ContactNumber { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string VisitorAddress { get; set; }
    public int? BuildingID { get; set; }
    public int? VisitorTypeID { get; set; }
    public int? CreatedBy { get; set; }
    public int? ModifyBy { get; set; }
    public DateTime? ModifyOn { get; set; }
    public DateTime? TimePeriodTo { get; set; }
    public DateTime? TimePeriodFrom { get; set; }
    public string VisitorsPhoto { get; set; }
    public string VisitorsAuthorityLetter { get; set; }
    public string RoomRent { get; set; }
    public string ElectricityBill { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public bool IsActive { get; set; }
    public string VisitorReference { get; set; }
    public int? RoomRentType { get; set; }
    public string AdmissionNumber { get; set; }

    public List<VisitorRoomNumbers> VisitorRoomNumbers { get; set; }

    public VisitorType VisitorType { get; set; }
}

public class VisitorType
{
    public int ID { get; set; }
    public string Type { get; set; }
}



