using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorkshopBills
/// </summary>
public class WorkshopBills
{
    public WorkshopBills()
    {

    }
    [Key()]
    public int ID { get; set; }

    public string BillNumber { get; set; }

    public string BillPath { get; set; }

    public int? WSId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int EstID { get; set; }

}