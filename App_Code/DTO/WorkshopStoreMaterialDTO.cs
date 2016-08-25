using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorkshopStoreMaterialDTO
/// </summary>
public class WorkshopStoreMaterialDTO
{
    public WorkshopStoreMaterialDTO()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int ID { get; set; }
    public int? MatID { get; set; }
    public decimal? InStoreQty { get; set; }
    public int? ModifyBy { get; set; }
    public DateTime? ModifyOn { get; set; }
    public DateTime? CreatedOn { get; set; }
    public decimal? Rate { get; set; }
    public int? AcaID { get; set; }
}