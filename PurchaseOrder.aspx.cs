using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;


public partial class PurchaseOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
            htmlCode = client.DownloadString(hostName + "/PurchaseOrderTemplate.html");
        }

        htmlCode = htmlCode.Replace("[Vendor]", hdnVendorName.Value.ToUpper() + ",<br/>" + hdnVendorAddress.Value.ToUpper() + "" + hdnCity.Value.ToUpper());
        if (drpPOFor.SelectedValue == "1")
        {
            htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR SOCIETY,<br/>" + hdnTrustName.Value + ",<br/>" + hdnDeliveryAddress.Value);
            htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR SOCIETY,<br/>" + hdnBillingName.Value + ",<br/>" + hdnBillingAddres.Value);
            htmlCode = htmlCode.Replace("[POheader]", "THE KALGIDHAR SOCIETY");
            htmlCode = htmlCode.Replace("[status]", "Society");
            htmlCode = htmlCode.Replace("[src]", Server.MapPath("img") + "/Logo_Society.png");
        }
        else
        {
            htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR TRUST,<br/>" + hdnTrustName.Value + ",<br/>" + hdnDeliveryAddress.Value);
            htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR TRUST,<br/>" + hdnBillingName.Value + ",<br/>" + hdnBillingAddres.Value);
            htmlCode = htmlCode.Replace("[src]", Server.MapPath("img") + "/Logo_Small.png");
            htmlCode = htmlCode.Replace("[POheader]", "THE KALGIDHAR TRUST");
            htmlCode = htmlCode.Replace("[status]", "Trust");
        }

        htmlCode = htmlCode.Replace("[ContactPerson]", txtcontact.Text);
        htmlCode = htmlCode.Replace("[PO]", txtPO.Text);
        DateTime date = Convert.ToDateTime(txtDate.Text.Trim());
        string dateString = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        htmlCode = htmlCode.Replace("[DATE]", dateString);
        htmlCode = htmlCode.Replace("[PaymentMode]", txtPaymentMode.Text);

        htmlCode = htmlCode.Replace("[Excise]", hdnExciseStatus.Value);
        if (txtExcise.Text == "")
        {
            htmlCode = htmlCode.Replace("[ExciseTextBox]", "INCLUDED");
        }
        else
        {
            htmlCode = htmlCode.Replace("[ExciseTextBox]", txtExcise.Text);
        }

        htmlCode = htmlCode.Replace("[SubTotal]", hdnSubTotal.Value);
        htmlCode = htmlCode.Replace("[GrandTotal]", hdnGrandTotal.Value);
        htmlCode = htmlCode.Replace("[Freight]", hdnFreight.Value);
        htmlCode = htmlCode.Replace("[FrieghtCartage]", txtFrieght.Text);
        htmlCode = htmlCode.Replace("[Loading]", txtLoading.Text);
        htmlCode = htmlCode.Replace("[CompletedBy]", txtCompleted.Text);
        htmlCode = htmlCode.Replace("[Grid]", getGrid());
        htmlCode = htmlCode.Replace("[EstimateNo]", hdnIndentNo.Value);
        htmlCode = htmlCode.Replace("[VAT]", hdnVatStatus.Value);
        htmlCode = htmlCode.Replace("[RefernceNo]", txtRefernceNo.Text);

        pnlHtml.InnerHtml = htmlCode;

        hdnIndentNo.Value = hdnIndentNo.Value.Replace(',', '_');

        var fileName = "PurchaseOrder_" + txtPO.Text.Trim() + ".pdf";

        string pathToSave = Server.MapPath("Bills") + "//PurchaseOrder";

        Utility.GeneratePDF(htmlCode, fileName, pathToSave);
    }

    protected void btnpdf_Click(object sender, EventArgs e)
    {
        getHTML();
    }

    public string getGrid()
    {
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        PONumber ponum = new PONumber();
        DataTable   dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct ID,PurchaseOrderNumber from PONumber where PurchaseOrderNumber='" + txtPO.Text + "'").Tables[0];
        if (dsExist.Rows.Count > 0)
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Delete From PurchaseOrderDetail where PONumberID='" + dsExist.Rows[0]["ID"].ToString() + "'");
        }
        else
        {
            ponum.PurchaseOrderNumber = txtPO.Text;
            repo.SavePONumber(ponum);
            hdnPoID.Value = ponum.ID.ToString();
        }
        PurchaseOrderDetail po = new PurchaseOrderDetail();
        DataTable dt = new DataTable();
        dt = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatName,M.MatCost,EMR.EstID,EMR.Sno,EMR.Qty,EMR.Rate,EMR.MatID,U.UnitName  from EstimateAndMaterialOthersRelations EMR INNER JOIN Material M  on M.MatId = EMR.MatId INNER JOIN Unit U  on U.UnitId = EMR.UnitId where Sno in (" + hdnItemsLength.Value + ")").Tables[0];
        string MaterialInfo = string.Empty;
        MaterialInfo += "<table style='width:100%' border='1' cellspacing='0' cellpadding='0'>";
        MaterialInfo += "<thead>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>Sr.No</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>DESCRIPTION</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>DETAIL</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>UNIT</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>QTY</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>PRICE</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>VAT</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>NET PRICE</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>LINE TOTAL</th>";
        MaterialInfo += "</tr>";
        MaterialInfo += "</thead>";
        MaterialInfo += "<tbody>";
        hdnIndentNo.Value = string.Empty;
        hdnVatStatus.Value = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string description = Request.Form["txtdescription" + i];
            string qty = Request.Form["txtQty" + i];
            string vat = Request.Form["txtvat" + i];
            string mat = Request.Form["txtMatName" + i];
            string unit = Request.Form["txtUnitName" + i];
            string cost = Request.Form["txtMatCost" + i];
            string sno = Request.Form["txtSno" + i];
            MaterialInfo += "<tr>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + (i + 1) + "</td>";
            MaterialInfo += "<td style='width: 30px; text-align: center; vertical-align: middle;font-size: 12px;'>" + description + "</td>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;font-size: 12px;'>" + mat + "</td>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + unit + "</td>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + qty + "</td>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + cost + "</td>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + vat + "</td>";
            var SubTotal = Convert.ToDecimal(qty) * Convert.ToDecimal(cost);
            SubTotal = Math.Round(SubTotal, 2);
            if (vat == "0")
            {
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + cost + "</td>";
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + SubTotal + "</td>";
            }
            else
            {
                var vatTotal = Convert.ToDecimal(SubTotal) * Convert.ToDecimal(vat) / 100;
                var totalAmount = Convert.ToDecimal(SubTotal) + Convert.ToDecimal(vatTotal);
                var netAmount = Convert.ToDecimal(cost) * Convert.ToDecimal(vat) / 100;
                var toatlNetAmount = Convert.ToDecimal(cost) + +Convert.ToDecimal(netAmount);
                totalAmount = Math.Round(totalAmount, 2);
                toatlNetAmount = Math.Round(toatlNetAmount, 2);
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + toatlNetAmount + "</td>";
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + totalAmount + "</td>";
            }
            MaterialInfo += "</tr>";
            if (!hdnIndentNo.Value.Contains(dt.Rows[i]["EstID"].ToString()))
            {
                hdnIndentNo.Value += dt.Rows[i]["EstID"].ToString() + ",";
            }
            if (!hdnVatStatus.Value.Contains(vat))
            {
                hdnVatStatus.Value += vat + "%" + ",";
            }
            po.CreatedOn = Utility.GetLocalDateTime(System.DateTime.UtcNow);
            if (dsExist.Rows.Count > 0)
            {
                po.PONumberID = Convert.ToInt32(dsExist.Rows[0]["ID"].ToString());
            }
            else
            {
                po.PONumberID = Convert.ToInt32(hdnPoID.Value);
            }
            po.VendorID = Convert.ToInt32(hdnVendorID.Value);
            po.EstID = Convert.ToInt32(dt.Rows[i]["EstID"].ToString());
            po.MatID = Convert.ToInt32(dt.Rows[i]["MatID"].ToString());
            po.Qty = Convert.ToDecimal(qty);
            po.Rate = Convert.ToDecimal(cost);
            po.Description = Request.Form["txtdescription" + i];
            po.Vat = Convert.ToDecimal(vat);
            po.UnitName = unit;
            po.FrieghtCharges = Convert.ToDecimal(txtFrieght.Text);
            po.LoadingCharges = Convert.ToDecimal(txtLoading.Text);
            po.SnoID = Convert.ToInt32(sno);
            if (txtExcise.Text != "")
            {
                po.Excise = Convert.ToDecimal(txtExcise.Text);
            }

            repo.AddNewPODetail(po);
        }
        MaterialInfo += "</tbody>";
        MaterialInfo += "</table>";

        hdnIndentNo.Value = hdnIndentNo.Value.Substring(0, hdnIndentNo.Value.Length - 1);

        return MaterialInfo;
    }

    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        getGrid();
    }
}