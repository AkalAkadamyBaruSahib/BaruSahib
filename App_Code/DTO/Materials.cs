using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Materials
/// </summary>
public class MaterialsDTO
{
    public int MatID { get; set; }
    public String MatName { get; set; }
    public int? MatTypeID { get; set; }
    public Unit Unit { get; set; }
    public decimal? MatCost { get; set; }
    public MaterialType MaterialType { get; set; }
    public decimal? LocalRate { get; set; }
}