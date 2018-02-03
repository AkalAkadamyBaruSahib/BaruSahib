using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Android_AbsentConductorArray
/// </summary>
public class Android_AbsentConductorArray
{
    public Android_AbsentConductorArray()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int ID { get; set; }

    public int? TransportDailyProformaID { get; set; }

    public int? VehicleID { get; set; }

    public string Reason { get; set; }
}