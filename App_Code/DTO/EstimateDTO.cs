using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EstimateDTO
/// </summary>
public class EstimateDTO
{
    public int EstId { get; set; }

    public int ZoneId { get; set; }

    public int AcaId { get; set; }

    public string SubEstimate { get; set; }

    public int TypeWorkId { get; set; }

    public string SanctionDate { get; set; }

    public int? Active { get; set; }

    public string CreatedBy { get; set; }

    public string CreatedOn { get; set; }

    public int ModifyBy { get; set; }

    public string ModifyOn { get; set; }

    public decimal EstmateCost { get; set; }

    public int WAId { get; set; }

    public string ShiftedStatus { get; set; }

    public string FileNme { get; set; }

    public string FilePath { get; set; }

    public bool IsApproved { get; set; }

    public bool? IsRejected { get; set; }

    public bool IsItemRejected { get; set; }

    public bool IsActive { get; set; }

    public int ModuleID { get; set; }

    public List<EstimateAndMaterialOthersRelationsDTO> EstimateAndMaterialOthersRelationsDTO { get; set; }
}

