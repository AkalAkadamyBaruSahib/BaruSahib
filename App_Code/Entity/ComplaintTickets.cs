using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

/// <summary>
/// Summary description for ComplaintTickets
/// </summary>
public class ComplaintTickets
{
    public int ID { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int AssignedTo { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string ComplaintType { get; set; }
    public bool? IsApproved { get; set; }
    public int? ModifyBy { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string Comments { get; set; }
    public string Status { get; set; }
    public bool? IsApprovedRequired { get; set; }
    public DateTime? TentativeDate { get; set; }
    public string Severity { get; set; }
    public int? SeverityDays { get; set; }
    public string Feedback { get; set; }
    public int ZoneID { get; set; }
    public int AcaID { get; set; }
}