using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VendorInfoDTO
/// </summary>
public class VendorInfoDTO
{
    public int ID { get; set; }

    public string VendorName { get; set; }

    public string VendorAddress { get; set; }

    public string VendorContactNo { get; set; }

    public string CreatedOn { get; set; }

    public string ModifyOn { get; set; }

    public string ModifyBy { get; set; }

    public bool? Active { get; set; }

    public string VendorState { get; set; }

    public string VendorCity { get; set; }

    public string VendorZip { get; set; }

    public string BankName { get; set; }

    public string IfscCode { get; set; }

    public string AccountNumber { get; set; }

    public string PanNumber { get; set; }

    public string TinNumber { get; set; }

    public string AltrenatePhoneNumber { get; set; }

    public List<VendorMaterialRelationDTO> VendorMaterialRelationDTO { get; set; }
}



public class VendorMaterialRelationDTO
{
    public int ID { get; set; }

    public int VendorID { get; set; }

    public int MatID { get; set; }

    public string CreatedOn { get; set; }

    public string ModifyOn { get; set; }

    public int MatType { get; set; }

    public string MatName { get; set; }
}