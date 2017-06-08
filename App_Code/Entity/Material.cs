using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Material
/// </summary>
public class Material
{
    public Material()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int MatId { get; set; }

    public int? MatTypeId { get; set; }

    public string MatName { get; set; }

    public decimal? MatCost { get; set; }

    public int? Active { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string ModifyBy { get; set; }

    public int? UnitId { get; set; }

    public string LastCreateBy { get; set; }

    public int? ChangeMatTypeStatus { get; set; }

    public DateTime? ChangeMatTypeOn { get; set; }

    public string ImageUrl { get; set; }

    public bool? IsRateApproved { get; set; }

    public List<VendorMaterialRelation> VendorMaterialRelation { get; set; }

    public Unit Unit { get; set; }

    public decimal? LocalRate { get; set; }

    public decimal? AkalWorkshopRate { get; set; }

    public decimal? MRP { get; set; }

    public decimal? Vat { get; set; }

    public decimal? Discount { get; set; }

    [ForeignKey("MatTypeId")]
    public  MaterialType MaterialType { get; set; }
}