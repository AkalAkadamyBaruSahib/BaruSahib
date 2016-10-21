using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VisitorReceipt
/// </summary>
public class VisitorReceipt
{
    public VisitorReceipt()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]

    public int ID { get; set; }

    public decimal? ReceivedAmount { get; set; }

    public string ReceivedFrom { get; set; }

    public int? PaymentMode { get; set; }

    public string ChequeDDNumber { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? Createdby { get; set; }

    public string ReceiptPath { get; set; }

}