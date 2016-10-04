using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeTransfer
/// </summary>
public class EmployeeTransfer
{
	public EmployeeTransfer()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    [Key()]

    public int ID { get; set; }

    public int EmpID { get; set; }

    public int OldAcaID { get; set; }

    public int OldZoneID { get; set; }

    public int NewZoneID { get; set; }

    public string TransferLatter { get; set; }

    public int NewAcaID { get; set; }

    public DateTime? DateOfTransfer { get; set; }

    public int? CreatedBy { get; set; }
}