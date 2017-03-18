using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EstimateLog
/// </summary>
public class EstimateLog
{
    public EstimateLog()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]
    public int ID { get; set; }

    public int? EstimateNumber { get; set; }

    public int? ModifyBy { get; set; }

    public DateTime? ModifyOn { get; set; }
}