using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubmitBillByUser
/// </summary>
public class SubmitBillByUser
{
    public SubmitBillByUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int SubBillId { get; set; }

    public DateTime? BillDate { get; set; }

    public string GateEntryNo { get; set; }

    public string StockEntryNo { get; set; }

    public int? VendorID { get; set; }

    public decimal? TotalAmount { get; set; }

    public string Remark { get; set; }

    public int? Active { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public int? ModifyBy { get; set; }

    public int? EstId { get; set; }

    public int? AcaId { get; set; }

    public int? ZoneId { get; set; }

    public string NatureOfBill { get; set; }

    public int? FirstVarify { get; set; }

    public DateTime? FirstVarifyOn { get; set; }

    public string FirstVarifyRemark { get; set; }

    public int? SeccondVarify { get; set; }

    public DateTime? SeccondVarifyOn { get; set; }

    public string SecondVarifyRemark { get; set; }

    public int? BillType { get; set; }

    public int? ChargetoBillTyId { get; set; }

    public int? FirstVarifyStatus { get; set; }

    public int? SecondVarifyStatus { get; set; }

    public int? PaymentStatus { get; set; }

    public string ReciptNoByEmp { get; set; }

    public DateTime? DateOfReceving { get; set; }

    public string RecevingRemark { get; set; }

    public int? RecevingStatus { get; set; }

    public int? RecevingBy { get; set; }

    public int? ThirdVarifyBy { get; set; }

    public string ThirdVarifyRemark { get; set; }

    public DateTime? ThirdVarifyOn { get; set; }

    public int? WAId { get; set; }

    public string VendorBillPath { get; set; }

    public int? VendorBillNumber { get; set; }

    public string AgencyName { get; set; }

    public List<SubmitBillByUserAndMaterialOthersRelation> SubmitBillByUserAndMaterialOthersRelation { get; set; }

}