using System;
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
                htmlCode = htmlCode.Replace("[txtAmount0]", Request.Form["txtAmount0"]);
                htmlCode = htmlCode.Replace("[txtAmount1]", Request.Form["txtAmount1"]);
                htmlCode = htmlCode.Replace("[txtAmount2]", Request.Form["txtAmount2"]);
                htmlCode = htmlCode.Replace("[txtAmount3]", Request.Form["txtAmount3"]);
                htmlCode = htmlCode.Replace("[txtAmount4]", Request.Form["txtAmount4"]);
                htmlCode = htmlCode.Replace("[txtAmount5]", Request.Form["txtAmount5"]);
                htmlCode = htmlCode.Replace("[txtAmount6]", Request.Form["txtAmount6"]);
                htmlCode = htmlCode.Replace("[txtAmount7]", Request.Form["txtAmount7"]);
                htmlCode = htmlCode.Replace("[txtAmount8]", Request.Form["txtAmount8"]);
                htmlCode = htmlCode.Replace("[txtAmount9]", Request.Form["txtAmount9"]);
                htmlCode = htmlCode.Replace("[txtAmount10]", Request.Form["txtAmount10"]);
                htmlCode = htmlCode.Replace("[txtAmount11]", Request.Form["txtAmount11"]);
                htmlCode = htmlCode.Replace("[txtAmount12]", Request.Form["txtAmount12"]);
                htmlCode = htmlCode.Replace("[txtAmount13]", Request.Form["txtAmount13"]);
                htmlCode = htmlCode.Replace("[txtAmount14]", Request.Form["txtAmount14"]);
                htmlCode = htmlCode.Replace("[txtAmount15]", Request.Form["txtAmount15"]);
                htmlCode = htmlCode.Replace("[txtAmount16]", Request.Form["txtAmount16"]);
                htmlCode = htmlCode.Replace("[txtAmount17]", Request.Form["txtAmount17"]);
                htmlCode = htmlCode.Replace("[txtAmount18]", Request.Form["txtAmount18"]);
                htmlCode = htmlCode.Replace("[txtAmount19]", Request.Form["txtAmount19"]);
                htmlCode = htmlCode.Replace("[txtUnit0]", Request.Form["txtUnit0"]);
                htmlCode = htmlCode.Replace("[txtUnit1]", Request.Form["txtUnit1"]);
                htmlCode = htmlCode.Replace("[txtUnit2]", Request.Form["txtUnit2"]);
                htmlCode = htmlCode.Replace("[txtUnit3]", Request.Form["txtUnit3"]);
                htmlCode = htmlCode.Replace("[txtUnit4]", Request.Form["txtUnit4"]);
                htmlCode = htmlCode.Replace("[txtUnit5]", Request.Form["txtUnit5"]);
                htmlCode = htmlCode.Replace("[txtUnit6]", Request.Form["txtUnit6"]);
                htmlCode = htmlCode.Replace("[txtUnit7]", Request.Form["txtUnit7"]);
                htmlCode = htmlCode.Replace("[txtUnit8]", Request.Form["txtUnit8"]);
                htmlCode = htmlCode.Replace("[txtUnit9]", Request.Form["txtUnit9"]);
                htmlCode = htmlCode.Replace("[txtUnit10]", Request.Form["txtUnit10"]);
                htmlCode = htmlCode.Replace("[txtUnit11]", Request.Form["txtUnit11"]);
                htmlCode = htmlCode.Replace("[txtUnit12]", Request.Form["txtUnit12"]);
                htmlCode = htmlCode.Replace("[txtUnit13]", Request.Form["txtUnit13"]);
                htmlCode = htmlCode.Replace("[txtUnit14]", Request.Form["txtUnit14"]);
                htmlCode = htmlCode.Replace("[txtUnit15]", Request.Form["txtUnit15"]);
                htmlCode = htmlCode.Replace("[txtUnit16]", Request.Form["txtUnit16"]);
                htmlCode = htmlCode.Replace("[txtUnit17]", Request.Form["txtUnit17"]);
                htmlCode = htmlCode.Replace("[txtUnit18]", Request.Form["txtUnit18"]);
                htmlCode = htmlCode.Replace("[txtUnit19]", Request.Form["txtUnit19"]);
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
                htmlCode = htmlCode.Replace("[SerAmount0]", Request.Form["txtSerAmount0"]);
                htmlCode = htmlCode.Replace("[SerAmount1]", Request.Form["txtSerAmount1"]);
                htmlCode = htmlCode.Replace("[SerAmount2]", Request.Form["txtSerAmount2"]);
                htmlCode = htmlCode.Replace("[SerAmount3]", Request.Form["txtSerAmount3"]);
                htmlCode = htmlCode.Replace("[SerAmount4]", Request.Form["txtSerAmount4"]);
                htmlCode = htmlCode.Replace("[SerAmount5]", Request.Form["txtSerAmount5"]);
                htmlCode = htmlCode.Replace("[SerAmount6]", Request.Form["txtSerAmount6"]);
                htmlCode = htmlCode.Replace("[SerAmount7]", Request.Form["txtSerAmount7"]);
                htmlCode = htmlCode.Replace("[SerAmount8]", Request.Form["txtSerAmount8"]);
                htmlCode = htmlCode.Replace("[SerAmount9]", Request.Form["txtSerAmount9"]);
                htmlCode = htmlCode.Replace("[SerAmount10]", Request.Form["txtSerAmount10"]);
                htmlCode = htmlCode.Replace("[SerAmount11]", Request.Form["txtSerAmount11"]);
                htmlCode = htmlCode.Replace("[SerAmount12]", Request.Form["txtSerAmount12"]);
                htmlCode = htmlCode.Replace("[SerAmount13]", Request.Form["txtSerAmount13"]);
                htmlCode = htmlCode.Replace("[SerAmount14]", Request.Form["txtSerAmount14"]);
                htmlCode = htmlCode.Replace("[SerAmount15]", Request.Form["txtSerAmount15"]);
                htmlCode = htmlCode.Replace("[SerAmount16]", Request.Form["txtSerAmount16"]);
                htmlCode = htmlCode.Replace("[SerAmount17]", Request.Form["txtSerAmount17"]);
                htmlCode = htmlCode.Replace("[SerAmount18]", Request.Form["txtSerAmount18"]);
                htmlCode = htmlCode.Replace("[SerAmount19]", Request.Form["txtSerAmount19"]);
                htmlCode = htmlCode.Replace("[SerUnit0]", Request.Form["txtSerUnit0"]);
                htmlCode = htmlCode.Replace("[SerUnit1]", Request.Form["txtSerUnit1"]);
                htmlCode = htmlCode.Replace("[SerUnit2]", Request.Form["txtSerUnit2"]);
                htmlCode = htmlCode.Replace("[SerUnit3]", Request.Form["txtSerUnit3"]);
                htmlCode = htmlCode.Replace("[SerUnit4]", Request.Form["txtSerUnit4"]);
                htmlCode = htmlCode.Replace("[SerUnit5]", Request.Form["txtSerUnit5"]);
                htmlCode = htmlCode.Replace("[SerUnit6]", Request.Form["txtSerUnit6"]);
                htmlCode = htmlCode.Replace("[SerUnit7]", Request.Form["txtSerUnit7"]);
                htmlCode = htmlCode.Replace("[SerUnit8]", Request.Form["txtSerUnit8"]);
                htmlCode = htmlCode.Replace("[SerUnit9]", Request.Form["txtSerUnit9"]);
                htmlCode = htmlCode.Replace("[SerUnit10]", Request.Form["txtSerUnit10"]);
                htmlCode = htmlCode.Replace("[SerUnit11]", Request.Form["txtSerUnit11"]);
                htmlCode = htmlCode.Replace("[SerUnit12]", Request.Form["txtSerUnit19"]);
                htmlCode = htmlCode.Replace("[SerUnit13]", Request.Form["txtSerUnit12"]);
                htmlCode = htmlCode.Replace("[SerUnit14]", Request.Form["txtSerUnit13"]);
                htmlCode = htmlCode.Replace("[SerUnit15]", Request.Form["txtSerUnit14"]);
                htmlCode = htmlCode.Replace("[SerUnit16]", Request.Form["txtSerUnit15"]);
                htmlCode = htmlCode.Replace("[SerUnit17]", Request.Form["txtSerUnit16"]);
                htmlCode = htmlCode.Replace("[SerUnit18]", Request.Form["txtSerUnit17"]);
                htmlCode = htmlCode.Replace("[SerUnit19]", Request.Form["txtSerUnit18"]);
                pnlServiceandOtherRepair.InnerHtml = htmlCode;
            }
        }
        string fileName = string.Empty;

        TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());
        ProformaDetail genSerDetail = new ProformaDetail();
        if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.GENSETREAPIRANDSERVICE).ToString())
        {
            hdnGensetAcaName.Value = hdnGensetAcaName.Value.Replace(" ", "");
            fileName = "GENSET_REAPIR_AND_SERVICE_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + hdnGensetAcaName.Value.Trim() + ".pdf";
            genSerDetail.AcaID = Convert.ToInt32(hdnGenAcaID.Value);
            // genSerDetail.VehicleID = 0;
            genSerDetail.ProformaType = Convert.ToInt32(ddlproforma.SelectedValue);
            genSerDetail.GensetCompany = Request.Form["txtGansetCompany"];
            genSerDetail.GensetSrNo = Request.Form["txtGansetSrNumber"];
            genSerDetail.GensetPowerInKVA = Request.Form["txtGensetPower"];
            genSerDetail.GensetLastRepairDate = Convert.ToDateTime(Request.Form["txtLastRepairDate"]);
            genSerDetail.GensetLastQuotationAmount = Convert.ToDecimal(Request.Form["txtLastRepairAmount"]);
            genSerDetail.GensetCurrentQuotationAmount = Convert.ToDecimal(Request.Form["txtQuotationAmount"]);
            genSerDetail.GensetTotalRunning = Request.Form["txtGensetTotalRunning"];
            genSerDetail.AverageRunning = Convert.ToDecimal(Request.Form["txtGensetAverageRunning"]);
            genSerDetail.ServicePlaceAgency = Request.Form["txtService"];
            genSerDetail.CreatedBy = Convert.ToInt32(hdnInchargeID.Value);
            genSerDetail.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
            genSerDetail.GensetDate = Convert.ToDateTime(Request.Form["txtDate"]);

            repo.SaveProformaDetail(genSerDetail);
            hdnProformaID.Value = genSerDetail.ID.ToString();
            htmlCode = htmlCode.Replace("[Proformano]", hdnProformaID.Value);
            ProformaMaterialDetail matDetail = null;
            for (var i = 0; i < int.Parse(hdnGensetTableLength.Value); i++)
            {
                if (Request.Form["hdnMatID" + i] != "" && Request.Form["hdnMatID" + i] != null)
                {
                    matDetail = new ProformaMaterialDetail();
                    matDetail.ProformaID = Convert.ToInt32(hdnProformaID.Value);
                    matDetail.MatID = Convert.ToInt32(Request.Form["hdnMatID" + i]);
                    matDetail.UnitID = Convert.ToInt32(Request.Form["hdnUnitID" + i]);
                    matDetail.Qty = Convert.ToDecimal(Request.Form["txtQty" + i]);
                    if (Request.Form["txtRate" + i] == "0")
                    {
                        matDetail.Rate = 0;
                        matDetail.Amount = 0;
                    }
                    else
                    {
                        matDetail.Rate = Convert.ToDecimal(Request.Form["txtRate" + i]);
                        matDetail.Amount = Convert.ToDecimal(Request.Form["txtQty" + i]) * Convert.ToDecimal(Request.Form["txtRate" + i]);
                    }
                    repo.SaveGensetProformaMaterialDetail(matDetail);
                }
            }
        }

        else if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.BATTERYQUOTATION).ToString())
        {
            hdnBatteryAcaName.Value = hdnBatteryAcaName.Value.Replace(" ", "");
            fileName = "BATTERY_QUOTATION_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + hdnBatteryAcaName.Value.Trim() + ".pdf";

            ProformaDetail batryDetail = new ProformaDetail();
            batryDetail.ProformaType = Convert.ToInt32(ddlproforma.SelectedValue);
            batryDetail.BatteryBillNo = Convert.ToInt32(Request.Form["txtBillNo"]);
            batryDetail.AcaID = Convert.ToInt32(hdnBatteryAcaID.Value);
            batryDetail.BatteryType = Request.Form["ddlBatteryTye"];
            batryDetail.CurrentMeterReading = Convert.ToInt32(Request.Form["txtCurrentMeterReading"]);
            batryDetail.NoOfRequiredNewBattery = Convert.ToInt32(Request.Form["txtNoRequird"]);
            batryDetail.NewMakeOfBattery = Request.Form["txtNewMakeBattery"];
            batryDetail.NewBatteryCapacity = Request.Form["txtNewBatteryCapacity"];
            batryDetail.NewBatterySrNo = Request.Form["txtNewBatterySrNum"];
            batryDetail.NewBatteryLifeInYears = Request.Form["txtNewBatteryLife"];
            batryDetail.MakeOfBatteryAndCapacityOldBattery = Request.Form["txtBatteryCapacity"];
            batryDetail.OldBatterySrNo = Request.Form["txtOldBatterySrNum"];
            batryDetail.OldBatteryPurchaseDate = Convert.ToDateTime(Request.Form["txtPurchaseDate"]);
            batryDetail.OldBatterySalePrice = Convert.ToDecimal(Request.Form["txtOldBatterySale"]);
            batryDetail.ApprovalAmount = Convert.ToDecimal(Request.Form["txtBatteryApprovalAmount"]);
            batryDetail.MicrotekSizeOfBattery = Request.Form["txtMocrotaxSize"];
            if (Request.Form["txtMocrotax"] != "")
            {
                batryDetail.MicrotekNoOfRequired = Convert.ToInt32(Request.Form["txtMocrotax"]);
            }
            if (Request.Form["txtMocrotaxPrice"] != "")
            {
                batryDetail.MicrotekPriceOfBattery = Convert.ToDecimal(Request.Form["txtMocrotaxPrice"]);
            }
            batryDetail.TataSizeOfBattery = Request.Form["txtAmaronSize"];
            if (Request.Form["txtAmaron"] != "")
            {
                batryDetail.TataNoOfRequired = Convert.ToInt32(Request.Form["txtAmaron"]);
            }
            if (Request.Form["txtAmaronPrice"] != "")
            {
                batryDetail.TataPriceOfBattery = Convert.ToDecimal(Request.Form["txtAmaronPrice"]);
            }
            batryDetail.ExideSizeOfBattery = Request.Form["txtExideSize"];
            if (Request.Form["txtExide"] != "")
            {
                batryDetail.ExideNoOfRequired = Convert.ToInt32(Request.Form["txtExide"]);
            }
            if (Request.Form["txtExidePrice"] != "")
            {
                batryDetail.ExidePriceOfBattery = Convert.ToDecimal(Request.Form["txtExidePrice"]);
            }
            batryDetail.OkayaSizeOfBattery = Request.Form["txtMicroTechSize"];
            if (Request.Form["txtMicroTech"] != "")
            {
                batryDetail.OkayaNoOfRequired = Convert.ToInt32(Request.Form["txtMicroTech"]);
            }
            if (Request.Form["txtMicroTechPrice"] != "")
            {
                batryDetail.OkayaPriceOfBattery = Convert.ToDecimal(Request.Form["txtMicroTechPrice"]);
            }
            batryDetail.CreatedBy = Convert.ToInt32(hdnInchargeID.Value);
            batryDetail.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
            if (Request.Form["ddlBatteryTye"] == "Vehicle Battery")
            {
                batryDetail.VehicleID = Convert.ToInt32(hdnBatteryVehicleID.Value);
                batryDetail.GensetPowerInKVA = string.Empty;
                batryDetail.GensetSrNo = string.Empty;
                batryDetail.GensetCompany = string.Empty;
                batryDetail.InvertarCompany = string.Empty;
            }
            else if (Request.Form["ddlBatteryTye"] == "Genset Battery")
            {
                // batryDetail.VehicleID = 0;
                batryDetail.GensetPowerInKVA = Request.Form["txtBatteryGensetPower"];
                batryDetail.GensetSrNo = Request.Form["txtBatteryGensetNo"];
                batryDetail.GensetCompany = Request.Form["txtBatteryGensetCompany"];
                batryDetail.InvertarCompany = string.Empty;
            }
            else
            {
                // batryDetail.VehicleID = 0;
                batryDetail.GensetPowerInKVA = string.Empty;
                batryDetail.GensetSrNo = string.Empty;
                batryDetail.GensetCompany = string.Empty;
                batryDetail.InvertarCompany = Request.Form["txtBatteryInvertarCompany"];
            }

            repo.SaveProformaDetail(batryDetail);
            hdnProformaID.Value = batryDetail.ID.ToString();
            htmlCode = htmlCode.Replace("[Proformano]", hdnProformaID.Value);
        }
        else if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.TYREREQUIREMENTORQUOTATION).ToString())
        {
            hdnTyreAcaName.Value = hdnTyreAcaName.Value.Replace(" ", "");
            fileName = "TYRE_REQUIREMENT_OR_QUOTATION_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + hdnTyreAcaName.Value.Trim() + ".pdf";

            ProformaDetail tyreDetail = new ProformaDetail();
            tyreDetail.ProformaType = Convert.ToInt32(ddlproforma.SelectedValue);
            tyreDetail.AcaID = Convert.ToInt32(hdnTyreAcaID.Value);
            tyreDetail.VehicleID = Convert.ToInt32(hdnTyreVehicleID.Value);
            tyreDetail.TyreTotalRunningKm = Convert.ToDecimal(Request.Form["txtTotalRuningKm"]);
            tyreDetail.FrontLeftSerialNum = Request.Form["txtFrontLeftOldTyreNo"];
            tyreDetail.FrontRightSerialNum = Request.Form["txtFrontRightOldTyreNo"];
            tyreDetail.FrontLeftKm = Convert.ToInt32(Request.Form["txtFrontLeftRunning"]);
            tyreDetail.FrontRightKm = Convert.ToInt32(Request.Form["txtFrontRightRunning"]);
            tyreDetail.RearLeftKm = Convert.ToInt32(Request.Form["txtRearLeftRunning"]);
            tyreDetail.RearRightKm = Convert.ToInt32(Request.Form["txtRearRightRunning"]);
            tyreDetail.RearLeftSerialNum = Request.Form["txtRearLeftOldTyreNo"];
            tyreDetail.RearRightSerialNum = Request.Form["txtRearRightOldTyreNo"];
            tyreDetail.FrontLeftOldTyreCondition = Request.Form["txtFrontLeftCondition"];
            tyreDetail.FrontRightOldTyreCondition = Request.Form["txtFrontRightCondition"];
            tyreDetail.RearLeftOldTyreCondition = Request.Form["txtRearLeftCondition"];
            tyreDetail.RearRightOldTyreCondition = Request.Form["txtRearRightCondition"];
            tyreDetail.StafneyOldTyreCondition = Request.Form["txtStafneyCondition"];
            if (Request.Form["txtFrontLeftRequired"] != "")
            {
                tyreDetail.FrontLeftNewTyreRequired = Convert.ToInt32(Request.Form["txtFrontLeftRequired"]);
            }
            if (Request.Form["txtFrontRightRequired"] != "")
            {
                tyreDetail.FrontRightNewTyreRequired = Convert.ToInt32(Request.Form["txtFrontRightRequired"]);
            }
            if (Request.Form["txtRearLeftRequired"] != "")
            {
                tyreDetail.RearLeftNewTyreRequired = Convert.ToInt32(Request.Form["txtRearLeftRequired"]);
            }
            if (Request.Form["txtRearRightRequired"] != "")
            {
                tyreDetail.RearRightNewTyreRequired = Convert.ToInt32(Request.Form["txtRearRightRequired"]);
            }
            if (Request.Form["txtStafneyRequired"] != "")
            {
                tyreDetail.StafneyNewTyreRequired = Convert.ToInt32(Request.Form["txtStafneyRequired"]);
            }
            tyreDetail.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
            tyreDetail.CreatedBy = Convert.ToInt32(hdnInchargeID.Value);
            tyreDetail.StafneyKm = Convert.ToInt32(Request.Form["txtStafneyRunning"]);
            tyreDetail.StafneySerialNum = Request.Form["txtStafneyldTyreNo"];
            tyreDetail.LastDateTyreChanged = Convert.ToDateTime(Request.Form["txtTyreLastChangeDate"]);
            tyreDetail.TyreSize = Request.Form["txtTyreSize"];
            tyreDetail.NoOfTyreRequired = Convert.ToInt32(Request.Form["txtNoofRequird"]);
            tyreDetail.CurrentMeterReading = Convert.ToInt32(Request.Form["txtCurrentMeter"]);
            tyreDetail.NewTyreAmount = Request.Form["txtNewTyreAmount"];
            tyreDetail.LastMeterReadingOfTyreChanged = Request.Form["txtTyreMeterReading"];
            tyreDetail.OldTyreSaleAmount = Request.Form["txtOldTyreSaleAmount"];
            tyreDetail.ApprovalAmount = Convert.ToDecimal(Request.Form["txtTyreApproval"]);
            tyreDetail.TyreChangOnlastMeterReading = Request.Form["txtTyreLastReading"];
            if (Request.Form["txtMrfRates"] != "")
            {
                tyreDetail.MrfRates = Convert.ToDecimal(Request.Form["txtMrfRates"]);
            }
            if (Request.Form["txtMrfQty"] != "")
            {
                tyreDetail.MrfQty = Convert.ToDecimal(Request.Form["txtMrfQty"]);
            }
            if (Request.Form["txtMrfAmount"] != "")
            {
                tyreDetail.MrfAmount = Convert.ToDecimal(Request.Form["txtMrfAmount"]);
            }
            if (Request.Form["txtApoloRates"] != "")
            {
                tyreDetail.ApoloRates = Convert.ToDecimal(Request.Form["txtApoloRates"]);
            }
            if (Request.Form["txtApoloQty"] != "")
            {
                tyreDetail.ApoloQty = Convert.ToDecimal(Request.Form["txtApoloQty"]);
            }
            if (Request.Form["txtApoloAmount"] != "")
            {
                tyreDetail.ApoloAmount = Convert.ToDecimal(Request.Form["txtApoloAmount"]);
            }
            if (Request.Form["txtCeatRates"] != "")
            {
                tyreDetail.CeatRates = Convert.ToDecimal(Request.Form["txtCeatRates"]);
            }
            if (Request.Form["txtCeatQty"] != "")
            {
                tyreDetail.CeatQty = Convert.ToDecimal(Request.Form["txtCeatQty"]);
            }
            if (Request.Form["txtCeatAmount"] != "")
            {
                tyreDetail.CeatAmount = Convert.ToDecimal(Request.Form["txtCeatAmount"]);
            }
            if (Request.Form["txtJkRates"] != "")
            {
                tyreDetail.JkRates = Convert.ToDecimal(Request.Form["txtJkRates"]);
            }
            if (Request.Form["txtJkQty"] != "")
            {
                tyreDetail.JkQty = Convert.ToDecimal(Request.Form["txtJkQty"]);
            }
            if (Request.Form["txtJkAmount"] != "")
            {
                tyreDetail.JkAmount = Convert.ToDecimal(Request.Form["txtJkAmount"]);
            }
            repo.SaveProformaDetail(tyreDetail);
            hdnProformaID.Value = tyreDetail.ID.ToString();
            htmlCode = htmlCode.Replace("[Proformano]", hdnProformaID.Value);
        }
        else if (ddlproforma.SelectedValue == ((int)TypeEnum.TransportProformaType.SERVICEOTHERREAPIRSOFVEHICLE).ToString())
        {
            hdnServiceAcaName.Value = hdnServiceAcaName.Value.Replace(" ", "");
            fileName = "SERVICE_OTHER_REAPIRS_OF_VEHICLE_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + hdnServiceAcaName.Value.Trim() + ".pdf";

            genSerDetail.AcaID = Convert.ToInt32(hdnServiceAcaID.Value);
            genSerDetail.VehicleID = Convert.ToInt32(hdnServiceVehicleID.Value);
            genSerDetail.ProformaType = Convert.ToInt32(ddlproforma.SelectedValue);
            genSerDetail.ServicePlaceAgency = Request.Form["txtServicePlace"];
            genSerDetail.ServiceLastMeterReading = Request.Form["txtSrvicLastMetrReading"];
            genSerDetail.ServiceCurrentMeterReading = Request.Form["txtSrvicCurntMetrReading"];
            genSerDetail.ServiceQuotationAmount = Convert.ToDecimal(Request.Form["txtServiceQuotationAmount"]);
            genSerDetail.ServiceApprovalAmount = Convert.ToDecimal(Request.Form["txtServiceApprovalAmount"]);
            genSerDetail.AverageOfVehicle = Request.Form["txtAvergeVehicle"];
            genSerDetail.CreatedBy = Convert.ToInt32(hdnInchargeID.Value);
            genSerDetail.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);

            repo.SaveProformaDetail(genSerDetail);
            hdnProformaID.Value = genSerDetail.ID.ToString();
            htmlCode = htmlCode.Replace("[Proformano]", hdnProformaID.Value);
            ProformaMaterialDetail matDetail = null;
            for (var i = 0; i < int.Parse(hdnServiceTableLength.Value); i++)
            {
                if (Request.Form["hdnSerMatID" + i] != "" && Request.Form["hdnSerMatID" + i] != null)
                {
                    matDetail = new ProformaMaterialDetail();
                    matDetail.ProformaID = Convert.ToInt32(hdnProformaID.Value);
                    matDetail.MatID = int.Parse(Request.Form["hdnSerMatID" + i]);
                    matDetail.UnitID = int.Parse(Request.Form["hdnSerUnitID" + i]);
                    matDetail.Qty = Convert.ToDecimal(Request.Form["txtQuantity" + i]);
                    if (Request.Form["txtPrice" + i] == "0")
                    {
                        matDetail.Rate = 0;
                        matDetail.Amount = 0;
                    }
                    else
                    {
                        matDetail.Rate = Convert.ToDecimal(Request.Form["txtPrice" + i]);
                        matDetail.Amount = Convert.ToDecimal(Request.Form["txtQuantity" + i]) * Convert.ToDecimal(Request.Form["txtPrice" + i]);
                    }



                    repo.SaveGensetProformaMaterialDetail(matDetail);
                }
            }
        }
        string folderPath = Server.MapPath("TransportApplicationForm/Proform/");
        Utility.GeneratePDF(htmlCode, fileName, folderPath);
    }
}