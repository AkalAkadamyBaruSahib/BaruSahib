using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Estimate
/// </summary>
public class Estimate
{
    public Estimate()
    {

    }

    [Key()]

    public int EstId { get; set; }

    public int ZoneId { get; set; }

    public int AcaId { get; set; }

    public string SubEstimate { get; set; }

    public int TypeWorkId { get; set; }

    public DateTime? SanctionDate { get; set; }

    public int? Active { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int ModifyBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public decimal EstmateCost { get; set; }

    public int WAId { get; set; }

    public int? ShiftedStatus { get; set; }

    public string FileNme { get; set; }

    public string FilePath { get; set; }

    public bool IsApproved { get; set; }

    public bool? IsRejected { get; set; }

    public bool IsItemRejected { get; set; }

    public bool IsActive { get; set; }

    public int? ModuleID { get; set; }

    public List<EstimateAndMaterialOthersRelations> EstimateAndMaterialOthersRelations { get; set; }

}