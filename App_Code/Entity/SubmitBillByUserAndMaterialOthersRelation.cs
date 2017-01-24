using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubmitBillByUserAndMaterialOthersRelation
/// </summary>
public class SubmitBillByUserAndMaterialOthersRelation
{
    public SubmitBillByUserAndMaterialOthersRelation()
    {
        //
        // TODO: Add constructor logic here
        //
    }
     [Key()]
    public int Sno { get; set; }

    public int? SubBillId { get; set; }

    public int? MatTypeId { get; set; }

    public int? MatId { get; set; }

    public string ItemName { get; set; }

    public decimal? Qty { get; set; }

    public int? UnitId { get; set; }

    public decimal? Rate { get; set; }

    public decimal? Amount { get; set; }

    public int? Active { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public int? ModifyBy { get; set; }

    public string Remark { get; set; }

    public string StockEntryNo { get; set; }

    public string UnitName { get; set; }

    public decimal? Vat { get; set; }

 
}