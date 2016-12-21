using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for viewMaterials
/// </summary>
public class viewMaterials
{
    [Key]
    public int MatID { get; set; }

    public string MatName { get; set; }

    public decimal? MatCost { get; set; }

    public decimal? AkalWorkshopRate { get; set; }

    public decimal? LocalRate { get; set; }

    public int UnitId { get; set; }

    public string UnitName { get; set; }

    public int? MatTypeId { get; set; }

    public string MatTypeName { get; set; }

    public int? Active { get; set; }

}
