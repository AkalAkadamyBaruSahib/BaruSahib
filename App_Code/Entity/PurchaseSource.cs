using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for PurchaseSource
/// </summary>
public class PurchaseSource
{
    public PurchaseSource()
    {

    }
    [Key()]
    public int PSId { get; set; }

    public string PSName { get; set; }

    public int Active { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string ModifyBy { get; set; }

    public int AssignToEmp { get; set; }

}