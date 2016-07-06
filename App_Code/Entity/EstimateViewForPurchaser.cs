using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EstimateViewForPurchaser
/// </summary>
public class EstimateViewForPurchaser
{
    public int EstId { get; set; }
    public int PSId { get; set; }
    public int AcaID { get; set; }
    public string SubEstimate { get; set; }
    public string ZoneName { get; set; }
    public string AcaName { get; set; }
    public DateTime SanctionDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifyOn { get; set; }
    public bool IsApproved { get; set; }
    public string LoginId { get; set; }


    public List<EstimateAndMaterialOthersRelations> EstimateAndMaterialOthersRelationsPurchaser { get; set; }
    public Zone Zone { get; set; }
    public Academy Academy { get; set; }
}