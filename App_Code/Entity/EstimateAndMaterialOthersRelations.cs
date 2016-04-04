using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EstimateAndMaterialOthersRelations
/// </summary>
public class EstimateAndMaterialOthersRelations
{

    [Key()]
    public int Sno { get; set; }
    public int EstId { get; set; }
    public int MatTypeId { get; set; }
    public int MatId { get; set; }
    public int PSId { get; set; }
    public decimal Qty { get; set; }
    public int UnitId { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public string Remark { get; set; }
    public int Active { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? ModifyOn { get; set; }
    public int ModifyBy { get; set; }
    public DateTime? TantiveDate { get; set; }
    public DateTime? DispatchDate { get; set; }
    public string remarkByPurchase { get; set; }
    public string DispatchBy { get; set; }
    public DateTime? DispatchOn { get; set; }
    public int DispatchStatus { get; set; }
    public int PurchaseEmpId { get; set; }
    public DateTime? EmployeeAssignDateTime { get; set; }
    public bool IsApproved { get; set; }
    public int VendorID { get; set; }
}


