using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VehicleServiceRecord
/// </summary>
public class VehicleServiceRecord
{
    public VehicleServiceRecord()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]

    public int ID { get; set; }

    public int? AcaID { get; set; }

    public int? VehicleID { get; set; }

    public decimal? CurrentKm { get; set; }

    public string FrontLeftSerialNum { get; set; }

    public string FrontRightSerialNum { get; set; }

    public int? FrontLeftKm { get; set; }

    public int? FrontRightKm { get; set; }

    public int? RearLeftOneKm { get; set; }

    public int? RearLeftSecondKm { get; set; }

    public int? RearRightOneKm { get; set; }

    public int? RearRightSecondKm { get; set; }

    public string RearLeftOneSerialNum { get; set; }

    public string RearLeftSecondSerialNum { get; set; }

    public string RearRightOneSerialNum { get; set; }

    public string RearRightSecondSerialNum { get; set; }

    public int? LastServiceKm { get; set; }

    public string MeterReadingFilePath { get; set; }

    public DateTime? BatteryInstalationDate { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? StafneyKm { get; set; }

    public string StafneySerialNum { get; set; }

    public string MakeofBattery { get; set; }

    public string BatteryCapacity { get; set; }

    public string BatterySerialNum { get; set; }

    public int? BatteryLifeInYears { get; set; }

    public DateTime? LastServiceDate { get; set; }

    [ForeignKey("VehicleID")]
    public Vehicles Vehicles { get; set; }

    [ForeignKey("AcaID")]
    public Academy Academy { get; set; }
}