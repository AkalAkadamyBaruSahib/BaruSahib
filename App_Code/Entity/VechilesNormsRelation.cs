using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VechilesNormsRelation
/// </summary>
public class VechilesNormsRelation
{
	public VechilesNormsRelation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    [Key()]
    public int ID { get; set; }

    public int? VehicleID { get; set; }

    public int? NormID { get; set; }
}