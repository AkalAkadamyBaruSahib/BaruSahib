using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VehicleContractRate
/// </summary>
public class VehicleContractRate
{
    public VehicleContractRate()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]

    public int ID { get; set; }

    public int? SeatCapacity { get; set; }

    public int? CurrentYear { get; set; }

    public int? FiveYears { get; set; }

    public int? TenYears { get; set; }

    public int? Average { get; set; }
}