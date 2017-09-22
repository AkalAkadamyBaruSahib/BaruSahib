using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProformaDetail
/// </summary>
public class ProformaDetail
{
	public ProformaDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [Key()]
    public int ID { get; set; }

    public int? AcaID { get; set; }

    public int? VehicleID { get; set; }

    public int? ProformaType { get; set; }

    public decimal? TyreTotalRunningKm { get; set; }

    public string FrontLeftSerialNum { get; set; }

    public string FrontRightSerialNum { get; set; }

    public int? FrontLeftKm { get; set; }

    public int? FrontRightKm { get; set; }

    public int? RearLeftKm { get; set; }

    public int? RearRightKm { get; set; }

    public string RearLeftSerialNum { get; set; }

    public string RearRightSerialNum { get; set; }

    public string FrontLeftOldTyreCondition { get; set; }

    public string FrontRightOldTyreCondition { get; set; }

    public string RearLeftOldTyreCondition { get; set; }

    public string RearRightOldTyreCondition { get; set; }

    public string StafneyOldTyreCondition { get; set; }

    public int? FrontLeftNewTyreRequired { get; set; }

    public int? FrontRightNewTyreRequired { get; set; }

    public int? RearLeftNewTyreRequired { get; set; }

    public int? RearRightNewTyreRequired { get; set; }

    public int? StafneyNewTyreRequired { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? StafneyKm { get; set; }

    public string StafneySerialNum { get; set; }

    public DateTime? LastDateTyreChanged { get; set; }

    public string TyreSize { get; set; }

    public int? NoOfTyreRequired { get; set; }

    public int? CurrentMeterReading { get; set; }

    public string NewTyreAmount { get; set; }

    public string LastMeterReadingOfTyreChanged { get; set; }

    public string OldTyreSaleAmount { get; set; }

    public string ApprovalAmount { get; set; }

    public string TyreChangOnlastMeterReading { get; set; }

    public decimal? MrfRates { get; set; }

    public decimal? MrfQty { get; set; }

    public decimal? MrfAmount { get; set; }

    public decimal? ApoloRates { get; set; }

    public decimal? ApoloQty { get; set; }

    public decimal? ApoloAmount { get; set; }

    public decimal? CeatRates { get; set; }

    public decimal? CeatQty { get; set; }

    public decimal? CeatAmount { get; set; }

    public decimal? JkRates { get; set; }

    public decimal? JkQty { get; set; }

    public decimal? JkAmount { get; set; }

    public int? BatteryBillNo { get; set; }

    public string BatteryType { get; set; }

    public string InvertarCompany { get; set; }

    public int? NoOfRequiredNewBattery { get; set; }

    public string NewMakeOfBattery { get; set; }

    public string NewBatteryCapacity { get; set; }

    public string NewBatterySrNo { get; set; }

    public string NewBatteryLifeInYears { get; set; }

    public string MakeOfBatteryAndCapacityOldBattery { get; set; }

    public string OldBatterySrNo { get; set; }

    public DateTime? OldBatteryPurchaseDate { get; set; }

    public string OldBatterySalePrice { get; set; }

    public string MicrotekSizeOfBattery { get; set; }

    public int? MicrotekNoOfRequired { get; set; }

    public decimal? MicrotekPriceOfBattery { get; set; }

    public string TataSizeOfBattery { get; set; }

    public int? TataNoOfRequired { get; set; }

    public decimal? TataPriceOfBattery { get; set; }

    public string ExideSizeOfBattery { get; set; }

    public int? ExideNoOfRequired { get; set; }

    public decimal? ExidePriceOfBattery { get; set; }

    public string OkayaSizeOfBattery { get; set; }

    public int? OkayaNoOfRequired { get; set; }

    public decimal? OkayaPriceOfBattery { get; set; }

    public string GensetCompany { get; set; }

    public string GensetSrNo { get; set; }

    public string GensetPowerInKVA { get; set; }

    public DateTime? GensetLastRepairDate { get; set; }

    public string GensetLastQuotationAmount { get; set; }

    public string GensetCurrentQuotationAmount { get; set; }

    public string GensetTotalRunning { get; set; }

    public string AverageRunning { get; set; }

    public string ServicePlaceAgency { get; set; }

    public string ServiceLastMeterReading { get; set; }

    public string ServiceCurrentMeterReading { get; set; }

    public string ServiceQuotationAmount { get; set; }

    public string ServiceApprovalAmount { get; set; }

    public string AverageOfVehicle { get; set; }

    public DateTime? GensetDate { get; set; }
}