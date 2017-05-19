using System;
using System.ComponentModel.DataAnnotations;
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

    public int PONumberID { get; set; }

    public int? EstID { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? VendorID { get; set; }

    public int? MatID { get; set; }

    public string UnitName { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Rate { get; set; }

    public decimal? Vat { get; set; }

    public decimal? Excise { get; set; }

    public string Description { get; set; }

    public decimal? FrieghtCharges { get; set; }

    public decimal? LoadingCharges { get; set; }

    public virtual Material Material { get; set; }

    public int? SnoID { get; set; }
}