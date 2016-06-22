using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Drawing
/// </summary>
public class DrawingDTO
{
    public int DwgId { get; set; }

    public int? ZoneId { get; set; }

    public int? AcaId { get; set; }

    public int? DwTypeId { get; set; }

    public string DwgNo { get; set; }

    public string RevisionNo { get; set; }

    public string DwgFileName { get; set; }

    public string DwgFilePath { get; set; }

    public string PdfFileName { get; set; }

    public string PdfFilePath { get; set; }

    public int? Active { get; set; }

    public string CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public string ModifyOn { get; set; }

    public int? ModifyBy { get; set; }

    public string DrawingName { get; set; }

    public int? ShiftedStatus { get; set; }

    public bool? IsApproved { get; set; }

    public int? SubDwgTypeID { get; set; }

    public string ZoneName { get; set; }

    public string AcaName { get; set; }
}