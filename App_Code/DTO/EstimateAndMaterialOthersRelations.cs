using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EstimateAndMaterialOthersRelations
/// </summary>
public class EstimateAndMaterialOthersRelationsDTO
{
    public int Sno { get; set; }
    public int EstId { get; set; }
    public int MatId { get; set; }
    public decimal Qty { get; set; }
    public int UnitId { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public string Remark { get; set; }
    public int Active { get; set; }
    public bool IsApproved { get; set; }
    public int VendorID { get; set; }
}