using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorkshopDispatchMaterial
/// </summary>
public class WorkshopDispatchMaterial
{
    public WorkshopDispatchMaterial()
    {

    }
    [Key()]

    public int ID { get; set; }

    public int? EstID { get; set; }

    public int? EMRID { get; set; }

    public int? DispatchQty { get; set; }

    public int? DispatchBy { get; set; }

    public DateTime? DispatchOn { get; set; }

    public int? MatID { get; set; }

    public decimal? DispatchRate { get; set; }

    public virtual Material Material { get; set; }
}