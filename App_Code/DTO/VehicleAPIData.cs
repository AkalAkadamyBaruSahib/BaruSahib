using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VehicleAPIData
/// </summary>
public class VehicleAPIData
{
	public VehicleAPIData()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string vname { get; set; }
    public string start_datetime { get; set; }
    public string to_datetime { get; set; }
    public List<vehicle_attendance_detail> vehicle_attendance_detail { get; set; }
}

public class vehicle_attendance_detail
{
    public string dated { get; set; }
    public string route_start_km { get; set; }
    public string school_km { get; set; }
    public string route_end_km { get; set; }
    public string attendance { get; set; }
}