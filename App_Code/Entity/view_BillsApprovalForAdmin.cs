using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for view_BillsApprovalForAdmin
/// </summary>
public class view_BillsApprovalForAdmin
{
    public view_BillsApprovalForAdmin()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key]
    public int SubBillId { get; set; }

    public string BillDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string AgencyName { get; set; }

    public int AcaID { get; set; }

    public string AcaName { get; set; }

    public string ZoneName { get; set; }

    public int? FirstVarifyStatus { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? SecondVarifyStatus { get; set; }

    public int? PaymentStatus { get; set; }

    public int? RecevingStatus { get; set; }


}