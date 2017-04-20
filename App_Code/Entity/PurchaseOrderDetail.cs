using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseOrder
/// </summary>
public class PurchaseOrderDetail
{
    public PurchaseOrderDetail()
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

    public int? VendorID { get; set; }

    public int? MatID { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Rate { get; set; }

    public decimal? Vat { get; set; }

    public decimal? Excise { get; set; }

    public string Description { get; set; }
}