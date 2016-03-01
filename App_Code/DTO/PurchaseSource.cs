using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseSource
/// </summary>
public class PurchaseSourceDTO
{
    public int PSId { get; set; }

    public string PSName { get; set; }

    public int? Active { get; set; }

    public string CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public string ModifyOn { get; set; }

    public string ModifyBy { get; set; }

    public int? AssignToEmp { get; set; }
}