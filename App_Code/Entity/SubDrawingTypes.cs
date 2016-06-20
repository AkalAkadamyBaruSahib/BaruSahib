using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubDrawingTypes
/// </summary>
public class SubDrawingTypes
{
    public SubDrawingTypes()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]

    public int ID { get; set; }

    public int? DwgTypeId { get; set; }

    public string Description { get; set; }

}