using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialRateApproved
/// </summary>
public class MaterialRateApproved
{
    public MaterialRateApproved()
    {
    }
    [Key()]

    public int ID { get; set; }

    public int MatID { get; set; }

    public decimal ApprovedRate { get; set; }

    public int ApprovedBy { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public int? RequestedBy { get; set; }
}