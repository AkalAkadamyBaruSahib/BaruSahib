using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialNonApprovedRate
/// </summary>
public class MaterialNonApprovedRate
{
    public MaterialNonApprovedRate()
    {

    }
    [Key()]
    public int ID { get; set; }

    public int? MatID { get; set; }

    public decimal? Rate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }
}