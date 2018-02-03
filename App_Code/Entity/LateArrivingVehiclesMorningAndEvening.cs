using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LateArrivingVehiclesMorningAndEvening
/// </summary>
public class LateArrivingVehiclesMorningAndEvening
{
    public LateArrivingVehiclesMorningAndEvening()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int ID { get; set; }

    public int? TransportDailyProformaID { get; set; }

    public int? VehicleID { get; set; }

    public DateTime? TimeOfArrival { get; set; }

    public int? ReasonID { get; set; }

    public string DayType { get; set; }

    public string OtherReason { get; set; }
}