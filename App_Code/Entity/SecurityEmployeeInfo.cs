using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SecurityEmployeeInfo
/// </summary>
public class SecurityEmployeeInfo
{
    public SecurityEmployeeInfo()
    {

    }

    [Key()]

    public int ID { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string MobileNo { get; set; }

    public string Salary { get; set; }

    public string Cutting { get; set; }

    public int? AcaID { get; set; }

    public int? ZoneID { get; set; }

    public string Education { get; set; }

    public int? DesigID { get; set; }

    public int? DeptID { get; set; }

    public string AppointmentLetter { get; set; }

    public string ExperienceLetter { get; set; }

    public string FamilyRationCard { get; set; }

    public string PCC { get; set; }

    public string QualificationLetter { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifyOn { get; set; }

    public bool? IsApproved { get; set; }

    public string Photo { get; set; }
}