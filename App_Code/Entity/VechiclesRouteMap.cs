using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VechiclesRouteMap
/// </summary>
public class VechiclesRouteMap
{
	public VechiclesRouteMap()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [Key()]
    public int ID { get; set; }

    public string RouteNo { get; set; }

    public int AcaID { get; set; }

    public int ZoneID { get; set; }

    public string VehicleNumber { get; set; }

}