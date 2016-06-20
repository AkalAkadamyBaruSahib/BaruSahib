using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DrawingType
/// </summary>
public class DrawingType
{
    public DrawingType()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]
    public int DwTypeId { get; set; }

    public string DwTypeName { get; set; }

    public int? Active { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string ModifyBy { get; set; }
}