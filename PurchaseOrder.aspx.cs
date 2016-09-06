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

        string pathQuery = myuri.PathAndQuery;
        string hostName = myuri.ToString().Replace(pathQuery, "");
        using (var client = new CookieAwareWebClient())
        {
            htmlCode = client.DownloadString(hostName + "/Barusahib/PurchaseOrderTemplate.html");
        }

        htmlCode = htmlCode.Replace("[Vendor]", hdnVendorName.Value + "" + hdnVendorAddress.Value + "" + hdnCity.Value);
        htmlCode = htmlCode.Replace("[ShipTo]", hdnTrustName.Value + "," + hdnDeliveryAddress.Value + "," + hdnDeliveryPhoneNo.Value);
        htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", hdnBillingName.Value + "," + hdnBillingAddres.Value + "," + hdnBillingPhone.Value);
        htmlCode = htmlCode.Replace("[ContactPerson]", txtcontact.Text);
        htmlCode = htmlCode.Replace("[PO]", txtPO.Text);
        htmlCode = htmlCode.Replace("[DATE]", hdnCurrentDate.Value);
        htmlCode = htmlCode.Replace("[VAT]", hdnVatStatus.Value);
        htmlCode = htmlCode.Replace("[Excise]", hdnExciseStatus.Value);
        htmlCode = htmlCode.Replace("[VAT/CST]", txtvat.Text);
        htmlCode = htmlCode.Replace("[Payment]", txtpayment.Text);
        htmlCode = htmlCode.Replace("[EstimateNo]", hdnIndentNo.Value);
        htmlCode = htmlCode.Replace("[Authorised]", txtAuthorised.Text);
        htmlCode = htmlCode.Replace("[SubTotal]", hdnSubTotal.Value);
        htmlCode = htmlCode.Replace("[GrandTotal]", hdnGrandTotal.Value);
        htmlCode = htmlCode.Replace("[Freight]", hdnFreight.Value);
        htmlCode = htmlCode.Replace("[Grid]", getGrid());
        htmlCode = htmlCode.Replace("[src]", Server.MapPath("img") + "/Logo_Small.png");
        pnlHtml.InnerHtml = htmlCode;

        Utility.GeneratePDF(htmlCode, "abc.pdf", string.Empty);
    }

    


    protected void btnpdf_Click(object sender, EventArgs e)
    {
        getHTML();
    }

    public string getGrid()
    {
        DataTable dt = new DataTable();
        dt = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatName,EMR.Sno,EMR.Qty,EMR.Rate from EstimateAndMaterialOthersRelations EMR INNER JOIN Material M  on M.MatId = EMR.MatId where Sno in (" + hdnItemsLength.Value + ")").Tables[0];
        string MaterialInfo = string.Empty;
        MaterialInfo += "<table style='width:100%'>";
        MaterialInfo += "<thead>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Sr.No</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>QTY</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>DESCRIPTION</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>DETAIL</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>UNIT PRICE</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>LINE TOTAL</th>";
        MaterialInfo += "</tr>";
        MaterialInfo += "</thead>";
        MaterialInfo += "<tbody>";
        
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string description = Request.Form["txtdescription" + i];
            MaterialInfo += "<tr>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + (i + 1) + "</td>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + dt.Rows[i]["Qty"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 30px; text-align: center; vertical-align: middle;'>" + description + "</td>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + dt.Rows[i]["MatName"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + dt.Rows[i]["Rate"].ToString() + "</td>";
            var SubTotal = Convert.ToDecimal(dt.Rows[i]["Qty"].ToString()) * Convert.ToDecimal(dt.Rows[i]["Rate"].ToString());
            SubTotal = Math.Round(SubTotal, 2);
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + SubTotal + "</td>";
            MaterialInfo += "</tr>";
        }
        MaterialInfo += "</tbody>";
        MaterialInfo += "</table>";

        return MaterialInfo;
    }
  
}