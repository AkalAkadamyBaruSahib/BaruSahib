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


public partial class Workshop_GenegerateBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["InName"].ToString();
                hdnUserId.Value = Session["InName"].ToString();
            }
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

    protected void btnpdf_Click(object sender, EventArgs e)
    {
        getHTML();
    }

    public void getHTML()
    {
        string htmlCode = string.Empty;


        Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);

        string pathQuery = myuri.PathAndQuery;
        string hostName = myuri.ToString().Replace(pathQuery, "");
        using (var client = new CookieAwareWebClient())
        {
            htmlCode = client.DownloadString(hostName + "/Barusahib/WorkshopBillTemplate.html");
        }
        htmlCode = htmlCode.Replace("[BillNo]", txtBillNo.Text);
        htmlCode = htmlCode.Replace("[UserName]", hdnUserId.Value);
        htmlCode = htmlCode.Replace("[CurntDate]", hdnCurrentDate.Value);
        htmlCode = htmlCode.Replace("[EstimateNo]", hdnEstNo.Value);
        htmlCode = htmlCode.Replace("[To]", hdnAcademy.Value);
        htmlCode = htmlCode.Replace("[Total]", hdnTotal.Value);
        htmlCode = htmlCode.Replace("[Scrap]", hdnScrap.Value);
        htmlCode = htmlCode.Replace("[GrandTotal]", hdnGrandTotal.Value);
        htmlCode = htmlCode.Replace("[Date]", hdnCurrentDate.Value);
        htmlCode = htmlCode.Replace("[Signature]", txtSignature.Text);
        htmlCode = htmlCode.Replace("[SignatureSuprevisor]", txtSupervisorSign.Text);
        htmlCode = htmlCode.Replace("[Grid]", getGrid());
        pnlHtml.InnerHtml = htmlCode;

        Utility.GeneratePDF(htmlCode, "WorkshopBill.pdf");
    }

    public string getGrid()
    {
        DataTable dt = new DataTable();
        dt = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatName,EMR.Sno,EMR.Qty,EMR.Rate,U.UnitName from EstimateAndMaterialOthersRelations EMR INNER JOIN Material M  on M.MatId = EMR.MatId INNER JOIN Unit U on U.UnitId =EMR.UnitId where Sno in (" + hdnItemsLength.Value + ")").Tables[0];
        string MaterialInfo = string.Empty;
        MaterialInfo += "<table border='1' style='width:100%'>";
        MaterialInfo += "<thead>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<th style='width: 10%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Sr.No</th>";
        MaterialInfo += "<th style='width: 35%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>NameofItem</th>";
        MaterialInfo += "<th style='width: 10%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Qty</th>";
        MaterialInfo += "<th style='width: 10%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Pcs/Kg</th>";
        MaterialInfo += "<th style='width: 35%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Rate</th>";
        MaterialInfo += "<th style='width: 10%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Amount</th>";
        MaterialInfo += "</tr>";
        MaterialInfo += "</thead>";
        MaterialInfo += "<tbody>";
     
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            MaterialInfo += "<tr>";
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + (i + 1) + "</td>";
            MaterialInfo += "<td style='width: 35%; text-align: center; vertical-align: middle;'>" +dt.Rows[i]["MatName"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + dt.Rows[i]["Qty"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + dt.Rows[i]["UnitName"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 35%; text-align: center; vertical-align: middle;'>" + dt.Rows[i]["Rate"].ToString() + "</td>";
            var SubTotal = Convert.ToDecimal(dt.Rows[i]["Qty"].ToString()) * Convert.ToDecimal(dt.Rows[i]["Rate"].ToString());
            SubTotal = Math.Round(SubTotal, 2);
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + SubTotal + "</td>";
            MaterialInfo += "</tr>";
        }
        MaterialInfo += "</tbody>";
        MaterialInfo += "<tfoot>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<td rowspan='3' colspan='4'>";  
        MaterialInfo += "</td>";
        MaterialInfo += "<td style='width: 35%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'><b>Total</b></td>";
        MaterialInfo += "<td style='width: 10%; text-align: center; background-color: #CCCCCC; vertical-align: middle;'>" + hdnTotal.Value + "</td>";
        MaterialInfo += "</tr>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<td style='width: 35%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'><b>Scarp 2%</b></td>";
        MaterialInfo += "<td style='width: 10%; text-align: center; background-color: #CCCCCC; vertical-align: middle;'>" + hdnScrap.Value + "</td>";
        MaterialInfo += "</tr>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<td style='width: 35%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'><b>Grand Total</b></td>";
        MaterialInfo += "<td style='width: 10%; text-align: center; background-color: #CCCCCC; vertical-align: middle;'>" + hdnGrandTotal.Value + "</td>";
        MaterialInfo += "</tr>";
        MaterialInfo += "</tfoot>";
        MaterialInfo += "</table>";

        return MaterialInfo;
    }

}