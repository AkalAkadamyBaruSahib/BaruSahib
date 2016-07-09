using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Summary description for Incharge
/// </summary>
/// 
public class Incharge
{
    [Key()]
    public int InchargeId { get; set; }
    public string InName { get; set; }
    public string InMobile { get; set; }
    public int DesigId { get; set; }
    public int DepId { get; set; }
    public int Active { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifyOn { get; set; }
    public string ModifyBy { get; set; }
    public string LoginId { get; set; }
    public string UserPwd { get; set; }
    public int UserTypeId { get; set; }
    public DateTime? ChangePwdOn { get; set; }
    public int? ModuleID { get; set; }
}
