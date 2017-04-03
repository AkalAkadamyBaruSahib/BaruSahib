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

    public int? ModifyBy { get; set; }

    public bool? Active { get; set; }

    public int VendorState { get; set; }

    public int VendorCity { get; set; }

    public string VendorZip { get; set; }

    public string BankName { get; set; }

    public string IfscCode { get; set; }

    public string AccountNumber { get; set; }

    public string PanNumber { get; set; }

    public string TinNumber { get; set; }

    public string AltrenatePhoneNumber { get; set; }

    public List<VendorMaterialRelation> VendorMaterialRelations { get; set; }
}
