using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Android_TransportComplaintArray
/// </summary>
public class Android_TransportComplaintArray
{
	public Android_TransportComplaintArray()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    [Key()]
    public int ID { get; set; }

    public int? TransportDailyProformaID { get; set; }

    public int? VehicleID { get; set; }

    public int? ComplaintID { get; set; }

    public string Solution { get; set; }

    public string OtherComplaint { get; set; }
}