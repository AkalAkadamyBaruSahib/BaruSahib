using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SittingTyreRelation
/// </summary>
public class SittingTyreRelation
{
    public SittingTyreRelation()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]

    public int ID { get; set; }

    public int? SittingCapacity { get; set; }

    public int? NumOfTyre { get; set; }
}