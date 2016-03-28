using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VendorMaterialRelation
/// </summary>
public class VendorMaterialRelation
{
    public VendorMaterialRelation()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]
    public int ID { get; set; }

    public int VendorID { get; set; }

    public int MatID { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifyOn { get; set; }

    public int MatType { get; set; }
}