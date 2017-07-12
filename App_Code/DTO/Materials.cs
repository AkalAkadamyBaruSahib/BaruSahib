using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Materials
/// </summary>
public class MaterialsDTO
{
    public int MatID { get; set; }
    public String MatName { get; set; }
    public int? MatTypeID { get; set; }
    public string MatTypeName { get; set; }
    public Unit Unit { get; set; }
    public decimal? MatCost { get; set; }
    public  MaterialType MaterialType { get; set; }
    public decimal? LocalRate { get; set; }
    public int? AcaID { get; set; }
    public decimal? AkalWorkshopRate { get; set; }
    public decimal? MRP { get; set; }
    public decimal? Discount { get; set; }
    public decimal? Vat { get; set; }
    public decimal? GST { get; set; }
    public decimal? AdditionalDiscount { get; set; }

}