using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GetBillDetailsByVendorID
/// </summary>
public class GetBillDetailsByVendorID
{
    public GetBillDetailsByVendorID()
    {
    }

    public string VendorName { get; set; }
    public int SubBillId { get; set; }
    public string WorkAllotName { get; set; }
    public decimal TotalAmount { get; set; }
    public string MatName { get; set; }
    public string InName { get; set; }
    public DateTime CreatedOn { get; set; }

}