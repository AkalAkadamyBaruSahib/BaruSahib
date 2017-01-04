using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VendorBillInformation
/// </summary>
public class VendorBillInformation
{
	public VendorBillInformation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    [Key()]

    public int ID { get; set; }

    public DateTime CreatedOn { get; set; }

    public int VendorID { get; set; }

    public int VBillNumber { get; set; }

    public int BillNumber { get; set; }

    public int CreatedBy { get; set; }

}