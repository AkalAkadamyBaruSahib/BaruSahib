using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for view_BillSubmitedDetails
/// </summary>
public class view_BillSubmitedDetails
{
    [Key]
    public int? EstimateNumber { get; set; }

    public string AcademyName { get; set; }

    public string WorkAllotName { get; set; }

    public int? BillNumber { get; set; }

    public string AgencyName { get; set; }

    public string MatName { get; set; }

    public decimal? EstQuantity { get; set; }

    public decimal? EstRate { get; set; }

    public decimal? EstAmount { get; set; }

    public decimal? PurchasedQTY { get; set; }

    public decimal? PurchasedRate { get; set; }

    public int? WAId { get; set; }

    public int? PSId { get; set; }

    public bool? IsApproved { get; set; }

    public int? AcaId { get; set; }
}