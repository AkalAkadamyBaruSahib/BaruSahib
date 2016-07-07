using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EstimateViewForPurchaser
/// </summary>
public class EstimateViewForPurchaser
{
    public int EstId { get; set; }
    public int PSId { get; set; }
    public int AcaID { get; set; }
    public string SubEstimate { get; set; }
    public string ZoneName { get; set; }
    public string AcaName { get; set; }
    public DateTime SanctionDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifyOn { get; set; }
    public bool IsApproved { get; set; }
    public string LoginId { get; set; }
    public string UnitName { get; set; }
    public string MatName { get; set; }
    public int Sno { get; set; }
    public decimal Qty { get; set; }
    public DateTime? TantiveDate { get; set; }
    public DateTime? DispatchDate { get; set; }
    public string remarkByPurchase { get; set; }
    public int DispatchStatus { get; set; }
    public DateTime? EmployeeAssignDateTime { get; set; }
    public string PSName { get; set; }
    public string InName { get; set; }


    public List<EstimateAndMaterialOthersRelations> EstimateAndMaterialOthersRelationsPurchaser { get; set; }
 
}