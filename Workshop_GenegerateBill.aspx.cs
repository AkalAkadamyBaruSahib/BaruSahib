﻿using System;
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
                hdnInchargeID.Value = Session["InchargeID"].ToString();
                
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
        string estIDS = hdnEstNo.Value.Replace(',', '_');
        int UserId = Convert.ToInt32(Session["InchargeID"].ToString());
        DataSet dtAcaID = new DataSet();
        dtAcaID = DAL.DalAccessUtility.GetDataInDataSet("Select AcaId from AcademyAssignToEmployee where EmpId='" + UserId + "'");
        WorkshopBills wb = new WorkshopBills();
        wb.WSId = Convert.ToInt32(dtAcaID.Tables[0].Rows[0]["AcaId"].ToString());
        hdnUserId.Value = hdnUserId.Value.Replace(" ", "");
        wb.BillPath = "WorkshopBills/" + hdnUserId.Value.Trim() + "_" + estIDS + ".pdf";
        wb.EstID = Convert.ToInt32(hdnEstNo.Value);
        wb.CreatedBy = UserId;
        wb.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        WorkshopRepository repo = new WorkshopRepository(new AkalAcademy.DataContext());
        if (wb.ID == 0)
        {
            repo.AddNewBill(wb);
            hdnBillID.Value = wb.ID.ToString();
            DAL.DalAccessUtility.ExecuteNonQuery("Update WorkshopBills Set BillPath= 'WorkshopBills/" + hdnUserId.Value.Trim() + "_" + estIDS + "_" + hdnBillID.Value + ".pdf' where ID='" + hdnBillID.Value + "'");
        }
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
            htmlCode = client.DownloadString(hostName + "/WorkshopBillTemplate.html");
        }
        htmlCode = htmlCode.Replace("[BillNo]", hdnBillID.Value);
        htmlCode = htmlCode.Replace("[UserName]", hdnUserId.Value);
        htmlCode = htmlCode.Replace("[CurntDate]", hdnCurrentDate.Value);

        htmlCode = htmlCode.Replace("[To]", hdnAcademy.Value);
        htmlCode = htmlCode.Replace("[Total]", hdnTotal.Value);
        htmlCode = htmlCode.Replace("[Date]", hdnCurrentDate.Value);
        htmlCode = htmlCode.Replace("[Signature]", string.Empty);
        htmlCode = htmlCode.Replace("[SignatureSuprevisor]", String.Empty);
        htmlCode = htmlCode.Replace("[Grid]", getGrid());
        htmlCode = htmlCode.Replace("[EstimateNo]", hdnEstNo.Value);

        pnlHtml.InnerHtml = htmlCode;
        string folderPath = Server.MapPath("WorkshopBills");
        hdnUserId.Value = hdnUserId.Value.Replace(" ", "");
        string estIDS = hdnEstNo.Value.Replace(',', '_');

        Utility.GeneratePDF(htmlCode, (hdnUserId.Value.Trim() + "_" + estIDS + "_" + hdnBillID.Value + ".pdf"), folderPath);
    }

    public string getGrid()
    {
        decimal total = 0;
        DataTable dt = new DataTable();
        dt = DAL.DalAccessUtility.GetDataInDataSet("Select EMR.EstID,EMR.Sno,M.MatName,EMR.PurchaseQty,M.AkalWorkshopRate,U.UnitName from EstimateAndMaterialOthersRelations EMR " +
        "INNER JOIN Material M  on M.MatId = EMR.MatId INNER JOIN Unit U on U.UnitId = EMR.UnitId where Sno in (" + hdnItemsLength.Value + ")").Tables[0];
        string MaterialInfo = string.Empty;
        MaterialInfo += "<table border='1' style='width:100%'>";
        MaterialInfo += "<thead>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<th style='width: 10%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Sr.No</th>";
        MaterialInfo += "<th style='width: 35%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>NameofItem</th>";
        MaterialInfo += "<th style='width: 10%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Qty</th>";
        MaterialInfo += "<th style='width: 10%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Unit</th>";
        MaterialInfo += "<th style='width: 35%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Rate</th>";
        MaterialInfo += "<th style='width: 10%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'>Amount</th>";
        MaterialInfo += "</tr>";
        MaterialInfo += "</thead>";
        MaterialInfo += "<tbody>";
        hdnEstNo.Value = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        { 
            DataTable dtWorkDisptchDetail = new DataTable();
            dtWorkDisptchDetail = DAL.DalAccessUtility.GetDataInDataSet("Select EstID,EMRID,DispatchQty,DispatchRate,ID from WorkshopDispatchMaterial where EMRID='" + dt.Rows[i]["Sno"].ToString() + "' Order By ID Desc").Tables[0];
            MaterialInfo += "<tr>";
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + (i + 1) + "</td>";
            MaterialInfo += "<td style='width: 35%; text-align: center; vertical-align: middle;'>" + dt.Rows[i]["MatName"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + dtWorkDisptchDetail.Rows[0]["DispatchQty"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + dt.Rows[i]["UnitName"].ToString() + "</td>";
            MaterialInfo += "<td style='width: 35%; text-align: center; vertical-align: middle;'>" + dtWorkDisptchDetail.Rows[0]["DispatchRate"].ToString() + "</td>";
            var SubTotal = Convert.ToDecimal(dtWorkDisptchDetail.Rows[0]["DispatchQty"].ToString()) * Convert.ToDecimal(dtWorkDisptchDetail.Rows[0]["DispatchRate"].ToString());
            SubTotal = Math.Round(SubTotal, 2);
            MaterialInfo += "<td style='width: 10%; text-align: center; vertical-align: middle;'>" + SubTotal + "</td>";
            MaterialInfo += "</tr>";
            if (!hdnEstNo.Value.Contains(dt.Rows[i]["EstID"].ToString()))
            {
                hdnEstNo.Value += dt.Rows[i]["EstID"].ToString() + ",";
            }
            total += SubTotal;

            DAL.DalAccessUtility.ExecuteNonQuery("Update WorkshopDispatchMaterial Set BillNo='" + hdnBillID.Value + "' where ID='" + dtWorkDisptchDetail.Rows[0]["ID"].ToString() + "'");
           
        }
        MaterialInfo += "</tbody>";
        MaterialInfo += "<tfoot>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<td rowspan='1' colspan='4'>";  
        MaterialInfo += "</td>";
        MaterialInfo += "<td style='width: 35%; background-color: #CCCCCC; text-align: center; vertical-align: middle;'><b>Total</b></td>";
        MaterialInfo += "<td style='width: 10%; text-align: center; background-color: #CCCCCC; vertical-align: middle;'>" + total + "</td>";
        MaterialInfo += "</tr>";
        MaterialInfo += "</tfoot>";
        MaterialInfo += "</table>";
        hdnEstNo.Value = hdnEstNo.Value.Substring(0, hdnEstNo.Value.Length - 1);

        return MaterialInfo;
    }

}