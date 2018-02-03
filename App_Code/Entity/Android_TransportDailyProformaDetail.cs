using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Android_TransportDailyProformaDetail
/// </summary>
public class Android_TransportDailyProformaDetail
{
    public Android_TransportDailyProformaDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]

    public int TProformaID { get; set; }

    public int? AcaID { get; set; }

    public string Teacher { get; set; }

    public string FirstSecurityGuard { get; set; }

    public string SecondSecurityGuard { get; set; }

   // public string AcaCampusVisitComplaint { get; set; }

    //public string AcaCampusVisitSolution { get; set; }

    public bool? Supervisor { get; set; }

    public int? PresentStaff { get; set; }

    public int? TotalStaff { get; set; }

    public int? PresentStuMorning { get; set; }

    public int? TotalStuMorning { get; set; }

    public int? TotalStuEvening { get; set; }

    public int? PresentStuEvening { get; set; }

  //  public DateTime? AcaCampusVisitTime { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public List<Android_AbsentConductorArray> Android_AbsentConductorArray { get; set; }

    public List<Android_WithoutUniformDriverAndConductorArray> Android_WithoutUniformDriverAndConductorArray { get; set; }

    public List<Android_TransportComplaintArray> Android_TransportComplaintArray { get; set; }

    public List<LateArrivingVehiclesMorningAndEvening> LateArrivingVehiclesMorningAndEvening { get; set; }

    public List<Android_AcademyVisitDetail> Android_AcademyVisitDetail { get; set; }
}