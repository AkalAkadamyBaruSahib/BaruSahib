using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StockRegister
/// </summary>
public class StockRegister
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
}