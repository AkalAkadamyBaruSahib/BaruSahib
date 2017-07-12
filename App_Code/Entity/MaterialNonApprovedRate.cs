using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialNonApprovedRate
/// </summary>
public class MaterialNonApprovedRate
{
    public MaterialNonApprovedRate()
    {

    }
    [Key()]
    public int ID { get; set; }

    public int? MatID { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public decimal? MRP { get; set; }

    public decimal? Discount { get; set; }

    public decimal? AdditionalDiscount { get; set; }

    public decimal? Vat { get; set; }

    public decimal? NetRate { get; set; }

    public int? VendorID { get; set; }

    public decimal? GST { get; set; }
}