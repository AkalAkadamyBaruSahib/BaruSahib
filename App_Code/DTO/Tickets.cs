using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Tickets
/// </summary>
/// 
namespace DTO
{
    public class Ticket
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ComplaintType { get; set; }
        public bool? IsApproved { get; set; }
        public string ModifyBy { get; set; }
        public string CompletionDate { get; set; }
        public string Comments { get; set; }
        public bool? IsApprovedRequired { get; set; }
        public string TentativeDate { get; set; }
        public string Severity { get; set; }
        public string SeverityDays { get; set; }
        public string Zone { get; set; }
        public string Academy { get; set; }
        public string Feedback { get; set; }
        public string StatusID { get; set; }
    }
}