using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorkshopStoreMaterial
/// </summary>
public class WorkshopStoreMaterial
{
    public WorkshopStoreMaterial()
    {

    }

    [Key()]
    public int ID { get; set; }

    public int? MatID { get; set; }

    public decimal? InStoreQty { get; set; }

    public int? ModifyBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public int? AcaID { get; set; }

    public DateTime? CreatedOn { get; set; }

    [ForeignKey("MatID")]
    public Material Material { get; set; }

    [ForeignKey("AcaID")]
    public Academy Academy { get; set; }
}