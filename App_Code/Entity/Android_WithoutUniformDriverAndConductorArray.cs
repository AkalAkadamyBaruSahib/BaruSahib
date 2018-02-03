using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Android_WithoutUniformDriverAndConductorArray
/// </summary>
public class Android_WithoutUniformDriverAndConductorArray
{
	public Android_WithoutUniformDriverAndConductorArray()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    [Key()]
    public int ID { get; set; }

    public int? TransportDailyProformaID { get; set; }

    public int? VehicleID { get; set; }

    public string Name { get; set; }

    public int? EmployeeTypeId { get; set; }
}