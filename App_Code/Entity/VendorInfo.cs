using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VendorInfo
/// </summary>
public class VendorInfo
{
    public VendorInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]
    public int ID { get; set; }

    public string VendorName { get; set; }

    public string VendorAddress { get; set; }

    public string VendorContactNo { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string ModifyBy { get; set; }

    public bool Active { get; set; }

    public List<VendorMaterialRelation> VendorMaterialRelation { get; set; }
}