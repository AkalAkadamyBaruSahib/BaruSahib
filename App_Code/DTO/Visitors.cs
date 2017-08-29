using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Visitors
/// </summary>
public class VisitorsDTO
{
    public VisitorsDTO()
    {
        //
        // TODO: Add constructor logic here
        //
    }

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

    public string CreatedOn { get; set; }

    public string VisitorAddress { get; set; }

    public int? BuildingID { get; set; }

    public int? VisitorTypeID { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifyBy { get; set; }

    public string ModifyOn { get; set; }

    public string VisitorsPhoto { get; set; }

    public string VisitorsAuthorityLetter { get; set; }

    public string TimePeriodTo { get; set; }

    public string TimePeriodFrom { get; set; }

    public int? RoomRent { get; set; }

    public int? ElectricityBill { get; set; }

    public int? State { get; set; }

    public int? Country { get; set; }

    public int? City { get; set; }

    public bool IsActive { get; set; }

    public string VisitorReference { get; set; }

    public int? RoomRentType { get; set; }

    public string AdmissionNumber { get; set; }

    public string BuildingName { get; set; }

    public string RoomNumbers { get; set; }

    public string PurposeOfVisitRemarks { get; set; }
}