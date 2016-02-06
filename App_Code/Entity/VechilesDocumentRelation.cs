using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VechilesDocumentRelation
/// </summary>
public class VechilesDocumentRelation
{
    [Key]
    public int ID { get; set; }

    public int? VehicleID { get; set; }

    public int? TransportDocumentID { get; set; }

    public string Path { get; set; }

    public DateTime? DocumentEndDate { get; set; }

    public DateTime? CreatedOn { get; set; }

    [ForeignKey("VehicleID")]
    public Vehicles Vehicles { get; set; }
}