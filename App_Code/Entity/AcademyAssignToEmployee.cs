using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Summary description for AcademyAssignToEmployee
/// </summary>
/// 
[Table("AcademyAssignToEmployee")]
public class AcademyAssignToEmployee
{
    [Key()]
    public int AAEId { get; set; }

    public int? AcaId { get; set; }

    public int? EmpId { get; set; }

    public bool? Active { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string ModifyBy { get; set; }

    public int? ZoneId { get; set; }

    public int? LastEmp { get; set; }

    public int? AcaShiftedStatus { get; set; }

    public int? ChangeLocationStatus { get; set; }

    public DateTime? ChangeLocationOn { get; set; }

    public int? LastLocation { get; set; }

    public int? ModuleID { get; set; }

    [ForeignKey("EmpId")]
    public Incharge Incharge { get; set; }

}