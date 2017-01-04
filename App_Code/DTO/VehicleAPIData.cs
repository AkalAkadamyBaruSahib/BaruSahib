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
    public DateTime start_datetime { get; set; }
    public DateTime to_datetime { get; set; }
    public List<vehicle_attendance_detail> vehicle_attendance_detail { get; set; }
}

public class vehicle_attendance_detail
{
    public DateTime dated { get; set; }
    public decimal route_start_km { get; set; }
    public decimal school_km { get; set; }
    public decimal route_end_km { get; set; }
    public int attendance { get; set; }
}