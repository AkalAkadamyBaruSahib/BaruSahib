using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for lateRowModel
/// </summary>
public class lateRowModel
{
	public lateRowModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int? VehicleID { get; set; }

    public DateTime? TimeOfArrival { get; set; }

    public int? ReasonID { get; set; }

    public string OtherReason { get; set; }
}