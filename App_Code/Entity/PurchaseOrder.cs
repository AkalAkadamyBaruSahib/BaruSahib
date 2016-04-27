using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseOrder
/// </summary>
public class PurchaseOrder
{
    public PurchaseOrder()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]

    public int ID { get; set; }

    public string PONumber { get; set; }

    public int? EstID { get; set; }

    public DateTime? CreatedOn { get; set; }
}