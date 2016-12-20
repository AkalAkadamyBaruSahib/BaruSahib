﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Transport_Proforma : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataRow dr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            hdnInchargeID.Value = Session["InchargeID"].ToString();
            hdnUserId.Value = Session["InName"].ToString();
        }


    }

    public class CookieAwareWebClient : WebClient
    {
        public CookieAwareWebClient()
        {
            CookieContainer = new CookieContainer();
        }
        public CookieContainer CookieContainer { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        getHTML();
    }

    public void getHTML()
    {
        string htmlCode = string.Empty;
        Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        string appPath = HttpContext.Current.Request.ApplicationPath;

        string pathQuery = myuri.PathAndQuery;
        string FolderPath = myuri.ToString().Replace(pathQuery, "");
        string hostName = FolderPath + appPath;

        using (var client = new CookieAwareWebClient())
        {
            if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.GENSETREAPIRANDSERVICE).ToString())
            {
                htmlCode = client.DownloadString(hostName + "/GensetRepairAndService.html");
                htmlCode = htmlCode.Replace("[AcademyName]", hdnGensetAcaName.Value);
                htmlCode = htmlCode.Replace("[GensetDate]", Request.Form["txtDate"]);
                htmlCode = htmlCode.Replace("[GensetCompany]", Request.Form["txtGansetCompany"]);
                htmlCode = htmlCode.Replace("[GensetSerialNumber]", Request.Form["txtGansetSrNumber"]);
                htmlCode = htmlCode.Replace("[GensetPowerInKVA]", Request.Form["txtGensetPower"]);
                htmlCode = htmlCode.Replace("[LastTimeRepairDate]", Request.Form["txtLastRepairDate"]);
                htmlCode = htmlCode.Replace("[LastTimeRepairAmount]", Request.Form["txtLastRepairAmount"]);
                htmlCode = htmlCode.Replace("[QuotationAmount]", Request.Form["txtQuotationAmount"]);
                htmlCode = htmlCode.Replace("[TotalRunningOfGenset]", Request.Form["txtGensetTotalRunning"]);
                htmlCode = htmlCode.Replace("[AverageRunning]", Request.Form["txtGensetAverageRunning"]);
                htmlCode = htmlCode.Replace("[ServicePlaceAgency]", Request.Form["txtService"]);
                htmlCode = htmlCode.Replace("[MaterialName0]", Request.Form["txtMaterialName0"]);
                htmlCode = htmlCode.Replace("[txtQty0]", Request.Form["txtQty0"]);
                htmlCode = htmlCode.Replace("[txtRate0]", Request.Form["txtRate0"]);
                htmlCode = htmlCode.Replace("[MaterialName1]", Request.Form["txtMaterialName1"]);
                htmlCode = htmlCode.Replace("[txtQty1]", Request.Form["txtQty1"]);
                htmlCode = htmlCode.Replace("[txtRate1]", Request.Form["txtRate1"]);
                htmlCode = htmlCode.Replace("[MaterialName2]", Request.Form["txtMaterialName2"]);
                htmlCode = htmlCode.Replace("[txtQty2]", Request.Form["txtQty2"]);
                htmlCode = htmlCode.Replace("[txtRate2]", Request.Form["txtRate2"]);
                htmlCode = htmlCode.Replace("[MaterialName3]", Request.Form["txtMaterialName3"]);
                htmlCode = htmlCode.Replace("[txtQty3]", Request.Form["txtQty3"]);
                htmlCode = htmlCode.Replace("[txtRate3]", Request.Form["txtRate3"]);
                htmlCode = htmlCode.Replace("[MaterialName4]", Request.Form["txtMaterialName4"]);
                htmlCode = htmlCode.Replace("[txtQty4]", Request.Form["txtQty4"]);
                htmlCode = htmlCode.Replace("[txtRate4]", Request.Form["txtRate4"]);
                htmlCode = htmlCode.Replace("[MaterialName5]", Request.Form["txtMaterialName5"]);
                htmlCode = htmlCode.Replace("[txtQty5]", Request.Form["txtQty5"]);
                htmlCode = htmlCode.Replace("[txtRate5]", Request.Form["txtRate5"]);
                htmlCode = htmlCode.Replace("[MaterialName6]", Request.Form["txtMaterialName6"]);
                htmlCode = htmlCode.Replace("[txtQty6]", Request.Form["txtQty6"]);
                htmlCode = htmlCode.Replace("[txtRate6]", Request.Form["txtRate6"]);
                htmlCode = htmlCode.Replace("[MaterialName7]", Request.Form["txtMaterialName7"]);
                htmlCode = htmlCode.Replace("[txtQty7]", Request.Form["txtQty7"]);
                htmlCode = htmlCode.Replace("[txtRate7]", Request.Form["txtRate7"]);
                htmlCode = htmlCode.Replace("[MaterialName8]", Request.Form["txtMaterialName8"]);
                htmlCode = htmlCode.Replace("[txtQty8]", Request.Form["txtQty8"]);
                htmlCode = htmlCode.Replace("[txtRate8]", Request.Form["txtRate8"]);
                htmlCode = htmlCode.Replace("[MaterialName9]", Request.Form["txtMaterialName9"]);
                htmlCode = htmlCode.Replace("[txtQty9]", Request.Form["txtQty9"]);
                htmlCode = htmlCode.Replace("[txtRate9]", Request.Form["txtRate9"]);
                htmlCode = htmlCode.Replace("[MaterialName10]", Request.Form["txtMaterialName10"]);
                htmlCode = htmlCode.Replace("[txtQty10]", Request.Form["txtQty10"]);
                htmlCode = htmlCode.Replace("[txtRate10]", Request.Form["txtRate10"]);
                htmlCode = htmlCode.Replace("[MaterialName11]", Request.Form["txtMaterialName11"]);
                htmlCode = htmlCode.Replace("[txtQty11]", Request.Form["txtQty11"]);
                htmlCode = htmlCode.Replace("[txtRate11]", Request.Form["txtRate11"]);
                htmlCode = htmlCode.Replace("[MaterialName12]", Request.Form["txtMaterialName12"]);
                htmlCode = htmlCode.Replace("[txtQty12]", Request.Form["txtQty12"]);
                htmlCode = htmlCode.Replace("[txtRate12]", Request.Form["txtRate12"]);
                htmlCode = htmlCode.Replace("[MaterialName13]", Request.Form["txtMaterialName13"]);
                htmlCode = htmlCode.Replace("[txtQty13]", Request.Form["txtQty13"]);
                htmlCode = htmlCode.Replace("[txtRate13]", Request.Form["txtRate13"]);
                htmlCode = htmlCode.Replace("[MaterialName14]", Request.Form["txtMaterialName14"]);
                htmlCode = htmlCode.Replace("[txtQty14]", Request.Form["txtQty14"]);
                htmlCode = htmlCode.Replace("[txtRate14]", Request.Form["txtRate14"]);
                htmlCode = htmlCode.Replace("[MaterialName15]", Request.Form["txtMaterialName15"]);
                htmlCode = htmlCode.Replace("[txtQty15]", Request.Form["txtQty15"]);
                htmlCode = htmlCode.Replace("[txtRate15]", Request.Form["txtRate15"]);
                htmlCode = htmlCode.Replace("[MaterialName16]", Request.Form["txtMaterialName16"]);
                htmlCode = htmlCode.Replace("[txtQty16]", Request.Form["txtQty16"]);
                htmlCode = htmlCode.Replace("[txtRate16]", Request.Form["txtRate16"]);
                htmlCode = htmlCode.Replace("[MaterialName17]", Request.Form["txtMaterialName17"]);
                htmlCode = htmlCode.Replace("[txtQty17]", Request.Form["txtQty17"]);
                htmlCode = htmlCode.Replace("[txtRate17]", Request.Form["txtRate17"]);
                htmlCode = htmlCode.Replace("[MaterialName18]", Request.Form["txtMaterialName18"]);
                htmlCode = htmlCode.Replace("[txtQty18]", Request.Form["txtQty18"]);
                htmlCode = htmlCode.Replace("[txtRate18]", Request.Form["txtRate18"]);
                htmlCode = htmlCode.Replace("[MaterialName19]", Request.Form["txtMaterialName19"]);
                htmlCode = htmlCode.Replace("[txtQty19]", Request.Form["txtQty19"]);
                htmlCode = htmlCode.Replace("[txtRate19]", Request.Form["txtRate19"]);
                htmlCode = htmlCode.Replace("[GensetRemarks]", Request.Form["txtGensetRemarks"]);
                htmlCode = htmlCode.Replace("[TotalGensetAmount]", hdnGensetTotal.Value);
                pnlGenset.InnerHtml = htmlCode;
            }
            else if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.BATTERYQUOTATION).ToString())
            {
                htmlCode = client.DownloadString(hostName + "/BatteryQuotation.html");
                htmlCode = htmlCode.Replace("[BatteryAcaName]", hdnBatteryAcaName.Value);
                htmlCode = htmlCode.Replace("[BatteryTye]", Request.Form["ddlBatteryTye"]);
                htmlCode = htmlCode.Replace("[BatteryCurrentMeterReading]", Request.Form["txtCurrentMeterReading"]);
                htmlCode = htmlCode.Replace("[NoofRequired]", Request.Form["txtNoRequird"]);
                htmlCode = htmlCode.Replace("[MakeOfBatteryCapacity]", Request.Form["txtBatteryCapacity"]);
                htmlCode = htmlCode.Replace("[NewMakeOfBattery]", Request.Form["txtNewMakeBattery"]);
                htmlCode = htmlCode.Replace("[OldBatterySerialNumber]", Request.Form["txtOldBatterySrNum"]);
                htmlCode = htmlCode.Replace("[NewBatteryCapacity]", Request.Form["txtNewBatteryCapacity"]);
                htmlCode = htmlCode.Replace("[OldPurchaseDate]", Request.Form["txtPurchaseDate"]);
                htmlCode = htmlCode.Replace("[NewBatterySerialNumber]", Request.Form["txtNewBatterySrNum"]);
                htmlCode = htmlCode.Replace("[OldBatterySalePrice]", Request.Form["txtOldBatterySale"]);
                htmlCode = htmlCode.Replace("[NewbatteryLifeInYears]", Request.Form["txtNewBatteryLife"]);
                htmlCode = htmlCode.Replace("[ApprovalAmount]", Request.Form["txtBatteryApprovalAmount"]);
                htmlCode = htmlCode.Replace("[MocrotaxSize]", Request.Form["txtMocrotaxSize"]);
                htmlCode = htmlCode.Replace("[Mocrotax]", Request.Form["txtMocrotax"]);
                htmlCode = htmlCode.Replace("[MocrotaxPrice]", Request.Form["txtMocrotaxPrice"]);
                htmlCode = htmlCode.Replace("[AmaronSize]", Request.Form["txtAmaronSize"]);
                htmlCode = htmlCode.Replace("[Amaron]", Request.Form["txtAmaron"]);
                htmlCode = htmlCode.Replace("[AmaronPrice]", Request.Form["txtAmaronPrice"]);
                htmlCode = htmlCode.Replace("[ExideSize]", Request.Form["txtExideSize"]);
                htmlCode = htmlCode.Replace("[Exide]", Request.Form["txtExide"]);
                htmlCode = htmlCode.Replace("[ExidePrice]", Request.Form["txtExidePrice"]);
                htmlCode = htmlCode.Replace("[MicroTechSize]", Request.Form["txtMicroTechSize"]);
                htmlCode = htmlCode.Replace("[MicroTech]", Request.Form["txtMicroTech"]);
                htmlCode = htmlCode.Replace("[MicroTechPrice]", Request.Form["txtMicroTechPrice"]);
                htmlCode = htmlCode.Replace("[BatteryDate]", DateTime.Now.ToShortDateString());
                htmlCode = htmlCode.Replace("[BillNo]", Request.Form["txtBillNo"]);
                htmlCode = htmlCode.Replace("[BatteryRemarks]", Request.Form["txtBatteryRemarks"]);
                if (Request.Form["ddlBatteryTye"] == "Vehicle Battery")
                {
                    htmlCode = htmlCode.Replace("[BatteryVehicleNumber]", hdnBatteryVehicleNo.Value);
                    htmlCode = htmlCode.Replace("[BatteryVehicleType]", Request.Form["txtVehicelType"]);
                    htmlCode = htmlCode.Replace("[BatteryDriverName]", Request.Form["txtBatteryDriverandNumber"]);
                    htmlCode = htmlCode.Replace("[BatterySeated]", Request.Form["txtBatterySeated"]);
                    htmlCode = htmlCode.Replace("[BatteryModel]", Request.Form["txtBatteryModel"]);
                    htmlCode = htmlCode.Replace("[BatteryGensetNo]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryGensetPower]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryGensetCompany]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryInvertarCompany]", "NA");
                }
                else if (Request.Form["ddlBatteryTye"] == "Genset Battery")
                {
                    htmlCode = htmlCode.Replace("[BatteryGensetNo]", Request.Form["txtBatteryGensetNo"]);
                    htmlCode = htmlCode.Replace("[BatteryGensetPower]", Request.Form["txtBatteryGensetPower"]);
                    htmlCode = htmlCode.Replace("[BatteryGensetCompany]", Request.Form["txtBatteryGensetCompany"]);
                    htmlCode = htmlCode.Replace("[BatteryVehicleNumber]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryVehicleType]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryDriverName]", "NA");
                    htmlCode = htmlCode.Replace("[BatterySeated]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryModel]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryInvertarCompany]", "NA");
                }
                else
                {
                    htmlCode = htmlCode.Replace("[BatteryInvertarCompany]", Request.Form["txtBatteryInvertarCompany"]);
                    htmlCode = htmlCode.Replace("[BatteryGensetNo]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryGensetPower]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryGensetCompany]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryVehicleNumber]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryVehicleType]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryDriverName]", "NA");
                    htmlCode = htmlCode.Replace("[BatterySeated]", "NA");
                    htmlCode = htmlCode.Replace("[BatteryModel]", "NA");
                }
                pnlBatteryQuotation.InnerHtml = htmlCode;
            }
            else if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.TYREREQUIREMENTORQUOTATION).ToString())
            {
                htmlCode = client.DownloadString(hostName + "/TyreRequirementOrQuotation.html");
                htmlCode = htmlCode.Replace("[TyreAcademyName]", hdnTyreAcaName.Value);
                htmlCode = htmlCode.Replace("[TyreDriverName]", Request.Form["txtNameofDriver"]);
                htmlCode = htmlCode.Replace("[TyreVehicleNumber]", hdnTyreVehicleNo.Value);
                htmlCode = htmlCode.Replace("[TyreVehicleType]", Request.Form["txtTyreVehicleType"]);
                htmlCode = htmlCode.Replace("[TyreSize]", Request.Form["txtTyreSize"]);
                htmlCode = htmlCode.Replace("[TyreDate]", Request.Form["txtTyreDate"]);
                htmlCode = htmlCode.Replace("[NoOfTyreRequired]", Request.Form["txtNoofRequird"]);
                htmlCode = htmlCode.Replace("[CurrentMeterReading]", Request.Form["txtCurrentMeter"]);
                htmlCode = htmlCode.Replace("[NewTyreAmount]", Request.Form["txtNewTyreAmount"]);
                htmlCode = htmlCode.Replace("[LastMeterReadingOfTyresChange]", Request.Form["txtTyreMeterReading"]);
                htmlCode = htmlCode.Replace("[OldTyreSaleAmount]", Request.Form["txtOldTyreSaleAmount"]);
                htmlCode = htmlCode.Replace("[LastDateOfTyreChange]", Request.Form["txtTyreLastChangeDate"]);
                htmlCode = htmlCode.Replace("[TyreApprovalAmount]", Request.Form["txtTyreApproval"]);
                htmlCode = htmlCode.Replace("[TyreChangeOnLastReading]", Request.Form["txtTyreLastReading"]);
                htmlCode = htmlCode.Replace("[TyreTotalRunningKM]", Request.Form["txtTotalRuningKm"]);
                htmlCode = htmlCode.Replace("[MrfRates]", Request.Form["txtMrfRates"]);
                htmlCode = htmlCode.Replace("[MrfQty]", Request.Form["txtMrfQty"]);
                htmlCode = htmlCode.Replace("[MrfAmount]", Request.Form["txtMrfAmount"]);
                htmlCode = htmlCode.Replace("[ApoloRates]", Request.Form["txtApoloRates"]);
                htmlCode = htmlCode.Replace("[ApoloQty]", Request.Form["txtApoloQty"]);
                htmlCode = htmlCode.Replace("[ApoloAmount]", Request.Form["txtApoloAmount"]);
                htmlCode = htmlCode.Replace("[CeatRates]", Request.Form["txtCeatRates"]);
                htmlCode = htmlCode.Replace("[CeatQty]", Request.Form["txtCeatQty"]);
                htmlCode = htmlCode.Replace("[CeatAmount]", Request.Form["txtCeatAmount"]);
                htmlCode = htmlCode.Replace("[JkRates]", Request.Form["txtJkRates"]);
                htmlCode = htmlCode.Replace("[JkQty]", Request.Form["txtJkQty"]);
                htmlCode = htmlCode.Replace("[JkAmount]", Request.Form["txtJkAmount"]);
                htmlCode = htmlCode.Replace("[FrontRightRequired]", Request.Form["txtFrontRightRequired"]);
                htmlCode = htmlCode.Replace("[FrontRightCondition]", Request.Form["txtFrontRightCondition"]);
                htmlCode = htmlCode.Replace("[FrontRightRunning]", Request.Form["txtFrontRightRunning"]);
                htmlCode = htmlCode.Replace("[FrontRightOldTyreNo]", Request.Form["txtFrontRightOldTyreNo"]);
                htmlCode = htmlCode.Replace("[FrontLeftRequired]", Request.Form["txtFrontLeftRequired"]);
                htmlCode = htmlCode.Replace("[FrontLeftCondition]", Request.Form["txtFrontLeftCondition"]);
                htmlCode = htmlCode.Replace("[FrontLeftRunning]", Request.Form["txtFrontLeftRunning"]);
                htmlCode = htmlCode.Replace("[FrontLeftOldTyreNo]", Request.Form["txtFrontLeftOldTyreNo"]);
                htmlCode = htmlCode.Replace("[RearRightRequired]", Request.Form["txtRearRightRequired"]);
                htmlCode = htmlCode.Replace("[RearRightCondition]", Request.Form["txtRearRightCondition"]);
                htmlCode = htmlCode.Replace("[RearRightRunning]", Request.Form["txtRearRightRunning"]);
                htmlCode = htmlCode.Replace("[RearRightOldTyreNo]", Request.Form["txtRearRightOldTyreNo"]);
                htmlCode = htmlCode.Replace("[RearLeftOldTyreNo]", Request.Form["txtRearRightOldTyreNo"]);
                htmlCode = htmlCode.Replace("[RearLeftRequired]", Request.Form["txtRearLeftRequired"]);
                htmlCode = htmlCode.Replace("[RearLeftCondition]", Request.Form["txtRearLeftCondition"]);
                htmlCode = htmlCode.Replace("[RearLeftRunning]", Request.Form["txtRearLeftRunning"]);
                htmlCode = htmlCode.Replace("[RearLeftOldTyreNo]", Request.Form["txtRearLeftOldTyreNo"]);
                htmlCode = htmlCode.Replace("[StafneyRequired]", Request.Form["txtStafneyRequired"]);
                htmlCode = htmlCode.Replace("[StafneyCondition]", Request.Form["txtStafneyCondition"]);
                htmlCode = htmlCode.Replace("[StafneyRunning]", Request.Form["txtStafneyRunning"]);
                htmlCode = htmlCode.Replace("[StafneyldTyreNo]", Request.Form["txtStafneyldTyreNo"]);
                htmlCode = htmlCode.Replace("[TyreRemarks]", Request.Form["txtTyreRemarks"]);
                htmlCode = htmlCode.Replace("[TyreSeated]", Request.Form["txtTyreSeated"]);
                htmlCode = htmlCode.Replace("[TyreModel]", Request.Form["txtTyreModel"]);
                pnlTyreRequirement.InnerHtml = htmlCode;
            }
            else if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.SERVICEOTHERREAPIRSOFVEHICLE).ToString())
            {
                htmlCode = client.DownloadString(hostName + "/ServiceOtherRepairsOfVehicle.html");
                htmlCode = htmlCode.Replace("[ServiceAcademyName]", hdnServiceAcaName.Value);
                htmlCode = htmlCode.Replace("[ServiceVehicleNo]", hdnServiceVehicleNo.Value);
                htmlCode = htmlCode.Replace("[Seated]", Request.Form["txtSeated"]);
                htmlCode = htmlCode.Replace("[Model]", Request.Form["txtModel"]);
                htmlCode = htmlCode.Replace("[CurrentMeterReading]", Request.Form["txtSrvicCurntMetrReading"]);
                htmlCode = htmlCode.Replace("[LastMeterReading]", Request.Form["txtSrvicLastMetrReading"]);
                htmlCode = htmlCode.Replace("[ServiceQuotationAmount]", Request.Form["txtServiceQuotationAmount"]);
                htmlCode = htmlCode.Replace("[ServiceApprovalAmount]", Request.Form["txtServiceApprovalAmount"]);
                htmlCode = htmlCode.Replace("[AvergeVehicle]", Request.Form["txtAvergeVehicle"]);
                htmlCode = htmlCode.Replace("[ServicePlace]", Request.Form["txtServicePlace"]);
                htmlCode = htmlCode.Replace("[Material0]", Request.Form["txtMaterial0"]);
                htmlCode = htmlCode.Replace("[Quantity0]", Request.Form["txtQuantity0"]);
                htmlCode = htmlCode.Replace("[Price0]", Request.Form["txtPrice0"]);
                htmlCode = htmlCode.Replace("[Material1]", Request.Form["txtMaterial1"]);
                htmlCode = htmlCode.Replace("[Quantity1]", Request.Form["txtQuantity1"]);
                htmlCode = htmlCode.Replace("[Price1]", Request.Form["txtPrice1"]);
                htmlCode = htmlCode.Replace("[Material2]", Request.Form["txtMaterial2"]);
                htmlCode = htmlCode.Replace("[Quantity2]", Request.Form["txtQuantity2"]);
                htmlCode = htmlCode.Replace("[Price2]", Request.Form["txtPrice2"]);
                htmlCode = htmlCode.Replace("[Material3]", Request.Form["txtMaterial3"]);
                htmlCode = htmlCode.Replace("[Quantity3]", Request.Form["txtQuantity3"]);
                htmlCode = htmlCode.Replace("[Price3]", Request.Form["txtPrice3"]);
                htmlCode = htmlCode.Replace("[Material4]", Request.Form["txtMaterial4"]);
                htmlCode = htmlCode.Replace("[Quantity4]", Request.Form["txtQuantity4"]);
                htmlCode = htmlCode.Replace("[Price4]", Request.Form["txtPrice4"]);
                htmlCode = htmlCode.Replace("[Material5]", Request.Form["txtMaterial5"]);
                htmlCode = htmlCode.Replace("[Quantity5]", Request.Form["txtQuantity5"]);
                htmlCode = htmlCode.Replace("[Price5]", Request.Form["txtPrice5"]);
                htmlCode = htmlCode.Replace("[Material6]", Request.Form["txtMaterial6"]);
                htmlCode = htmlCode.Replace("[Quantity6]", Request.Form["txtQuantity6"]);
                htmlCode = htmlCode.Replace("[Price6]", Request.Form["txtPrice6"]);
                htmlCode = htmlCode.Replace("[Material7]", Request.Form["txtMaterial7"]);
                htmlCode = htmlCode.Replace("[Quantity7]", Request.Form["txtQuantity7"]);
                htmlCode = htmlCode.Replace("[Price7]", Request.Form["txtPrice7"]);
                htmlCode = htmlCode.Replace("[Material8]", Request.Form["txtMaterial8"]);
                htmlCode = htmlCode.Replace("[Quantity8]", Request.Form["txtQuantity8"]);
                htmlCode = htmlCode.Replace("[Price8]", Request.Form["txtPrice8"]);
                htmlCode = htmlCode.Replace("[Material9]", Request.Form["txtMaterial9"]);
                htmlCode = htmlCode.Replace("[Quantity9]", Request.Form["txtQuantity9"]);
                htmlCode = htmlCode.Replace("[Price9]", Request.Form["txtPrice9"]);
                htmlCode = htmlCode.Replace("[Material10]", Request.Form["txtMaterial10"]);
                htmlCode = htmlCode.Replace("[Quantity10]", Request.Form["txtQuantity10"]);
                htmlCode = htmlCode.Replace("[Price10]", Request.Form["txtPrice10"]);
                htmlCode = htmlCode.Replace("[Material11]", Request.Form["txtMaterial11"]);
                htmlCode = htmlCode.Replace("[Quantity11]", Request.Form["txtQuantity11"]);
                htmlCode = htmlCode.Replace("[Price11]", Request.Form["txtPrice11"]);
                htmlCode = htmlCode.Replace("[Material12]", Request.Form["txtMaterial12"]);
                htmlCode = htmlCode.Replace("[Quantity12]", Request.Form["txtQuantity12"]);
                htmlCode = htmlCode.Replace("[Price12]", Request.Form["txtPrice12"]);
                htmlCode = htmlCode.Replace("[Material13]", Request.Form["txtMaterial13"]);
                htmlCode = htmlCode.Replace("[Quantity13]", Request.Form["txtQuantity13"]);
                htmlCode = htmlCode.Replace("[Price13]", Request.Form["txtPrice13"]);
                htmlCode = htmlCode.Replace("[Material14]", Request.Form["txtMaterial14"]);
                htmlCode = htmlCode.Replace("[Quantity14]", Request.Form["txtQuantity14"]);
                htmlCode = htmlCode.Replace("[Price14]", Request.Form["txtPrice14"]);
                htmlCode = htmlCode.Replace("[Material15]", Request.Form["txtMaterial15"]);
                htmlCode = htmlCode.Replace("[Quantity15]", Request.Form["txtQuantity15"]);
                htmlCode = htmlCode.Replace("[Price15]", Request.Form["txtPrice15"]);
                htmlCode = htmlCode.Replace("[Material16]", Request.Form["txtMaterial16"]);
                htmlCode = htmlCode.Replace("[Quantity16]", Request.Form["txtQuantity16"]);
                htmlCode = htmlCode.Replace("[Price16]", Request.Form["txtPrice16"]);
                htmlCode = htmlCode.Replace("[Material17]", Request.Form["txtMaterial17"]);
                htmlCode = htmlCode.Replace("[Quantity17]", Request.Form["txtQuantity17"]);
                htmlCode = htmlCode.Replace("[Price17]", Request.Form["txtPrice17"]);
                htmlCode = htmlCode.Replace("[Material18]", Request.Form["txtMaterial18"]);
                htmlCode = htmlCode.Replace("[Quantity18]", Request.Form["txtQuantity18"]);
                htmlCode = htmlCode.Replace("[Price18]", Request.Form["txtPrice18"]);
                htmlCode = htmlCode.Replace("[Material19]", Request.Form["txtMaterial19"]);
                htmlCode = htmlCode.Replace("[Quantity19]", Request.Form["txtQuantity19"]);
                htmlCode = htmlCode.Replace("[Price19]", Request.Form["txtPrice149"]);
                htmlCode = htmlCode.Replace("[ServiceDate]", DateTime.Now.ToShortDateString());
                htmlCode = htmlCode.Replace("[ServiceDriverName]", Request.Form["txtServiceDriverandNumber"]);
                htmlCode = htmlCode.Replace("[ServiceVehicleType]", Request.Form["txtServiceVehicelType"]);
                htmlCode = htmlCode.Replace("[ServiceRemarks]", Request.Form["txtServiceRemarks"]);
                htmlCode = htmlCode.Replace("[TotalServiceAmount]", hdnServiceTotal.Value);
                pnlServiceandOtherRepair.InnerHtml = htmlCode;
            }
        }
        string fileName = string.Empty;

        if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.GENSETREAPIRANDSERVICE).ToString())
        {
            hdnGensetAcaName.Value = hdnGensetAcaName.Value.Replace(" ", "");
            fileName = "GENSET_REAPIR_AND_SERVICE_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + hdnGensetAcaName.Value.Trim() + ".pdf";
        }
        else if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.BATTERYQUOTATION).ToString())
        {
            hdnBatteryAcaName.Value = hdnBatteryAcaName.Value.Replace(" ", "");
            fileName = "BATTERY_QUOTATION_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + hdnBatteryAcaName.Value.Trim() + ".pdf";
        }
        else if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.TYREREQUIREMENTORQUOTATION).ToString())
        {
            hdnTyreAcaName.Value = hdnTyreAcaName.Value.Replace(" ", "");
            fileName = "TYRE_REQUIREMENT_OR_QUOTATION_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + hdnTyreAcaName.Value.Trim() + ".pdf";
        }
        else if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.SERVICEOTHERREAPIRSOFVEHICLE).ToString())
        {
            hdnServiceAcaName.Value = hdnServiceAcaName.Value.Replace(" ", "");
            fileName = "SERVICE_OTHER_REAPIRS_OF_VEHICLE_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + hdnServiceAcaName.Value.Trim() + ".pdf";
        }

        string folderPath = Server.MapPath("TransportApplicationForm/Proform/");
        Utility.GeneratePDF(htmlCode, fileName, folderPath);
    }
}