using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
public partial class Emp_EstimateView : System.Web.UI.Page
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
            lblUser.Text = Session["EmailId"].ToString();
        }
        if (Request.QueryString["AcaId"] != null)
        {
            
            getEstimateDetails(Request.QueryString["AcaId"].ToString());
        }
        if (Request.QueryString["EstId"] != null)
        {
            GetPrint(Request.QueryString["EstId"].ToString());

        }
    }
    protected void GetPrint(string id)
    {
        DataSet dsValue = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateWithMaterialForEmp_V2 '" + id + "'");
        string EstInfo = string.Empty;
        EstInfo += "<div style='width:100%; margin:20px; font-family:Calibri;'>";
        EstInfo += "<table style='width:100%;'>";
        EstInfo += "<tr>";
        EstInfo += "<td style='padding:0px; text-align:left; width:50%' valign='top'>";
        EstInfo += "<img src='http://barusahib.org/wp-content/uploads/2013/06/Logo.png' style='width:100%;' />";
        EstInfo += "</td>";
        EstInfo += "<td style='text-align: right; width:40%;'>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='font-style:italic; text-align: right;'>";
        EstInfo += "Baru Shahib,";
        EstInfo += "<br />Dist: Sirmaur";
        EstInfo += "<br />Himachal Pradesh-173001";
        EstInfo += "<br />XXXXXXXXXXX";
        EstInfo += "</div>";
        EstInfo += "</td>";
        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='font-size:25px; margin-top:20px; font-weight:bold; width:100%;'>" + dsValue.Tables[0].Rows[0]["WorkAllotName"].ToString() + "</div>";
        EstInfo += "<table style='width:100%; margin-top:20px;'>";
        EstInfo += "<tr>";
        EstInfo += "<td style='padding:0px; text-align:left;' valign='top'>";
        EstInfo += "<p>";
        EstInfo += "Estimate No: " + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "<br />";
        EstInfo += "Zone:" + dsValue.Tables[0].Rows[0]["ZoneName"].ToString() + "<br />";
        EstInfo += "Estimate Title: " + dsValue.Tables[0].Rows[0]["SubEstimate"].ToString() + "<br />";
        EstInfo += "Sanction Date: " + dsValue.Tables[0].Rows[0]["SanctionDate"].ToString();
        EstInfo += "</p>";
        EstInfo += "</td>";
        EstInfo += "<td style='text-align: right;'>";
        EstInfo += "<p style='text-align: right;'>";
        EstInfo += "Academy: " + dsValue.Tables[0].Rows[0]["AcaName"].ToString() + "<br />";
        EstInfo += "Type of Work: " + dsValue.Tables[0].Rows[0]["TypeWorkName"].ToString() + "<br />";
        EstInfo += "Estimate Cost: " + dsValue.Tables[0].Rows[0]["EstmateCost"].ToString();
        EstInfo += "</p>";
        EstInfo += "</td>";
        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='width:100%; font-size:20px; font-weight:bold; text-align:center;'>Estimate Particular Details</div>";
        EstInfo += "<br />";
        EstInfo += "<table style='width:100%; margin-top:20px;' border='1'>";
        EstInfo += "<thead>";
        EstInfo += "<tr>";
        EstInfo += "<th>Material</th>";
        EstInfo += "<th>Source Type</th>";
        EstInfo += "<th>Qty</th>";
        EstInfo += "<th>Unit</th>";
        EstInfo += "<th>Rate</th>";
        EstInfo += "<th style='width:152px;'>Amount</th>";
        EstInfo += "</tr>";
        EstInfo += "</thead>";
        EstInfo += "<tbody>";
        for (int i = 0; i < dsValue.Tables[1].Rows.Count; i++)
        {

            EstInfo += "<tr>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["MatName"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["PSName"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["Qty"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["UnitName"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["Rate"].ToString() + "</td>";
            EstInfo += "<td style='width:152px;'>" + dsValue.Tables[1].Rows[i]["Amount"].ToString() + "</td>";
            EstInfo += "</tr>";

        }
        EstInfo += "<tr>";
        EstInfo += "<td></td><td></td><td></td><td></td><td><b>Total</b></td>";
        EstInfo += "<td style='width:152px; font-weight:bold;'>" + dsValue.Tables[2].Rows[0]["Ttl"].ToString() + "</td>";
        EstInfo += "</tr>";
        EstInfo += "</tbody>";
        EstInfo += "<tr>";
        EstInfo += "</table>";
        EstInfo += "<br />";
        EstInfo += "<div style='margin-top:50px; width:100%; text-align:center;'>&copy; The Kalgidhar Socity All Rights Reserved</div>";
        EstInfo += "</div>";

        dt.Columns.Add("HtmlContent");
        dr = dt.NewRow();
        dr["HtmlContent"] = EstInfo;
        dt.Rows.Add(dr);
        pnlPdf.InnerHtml = dt.Rows[0][0].ToString();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=PdfReport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        pnlPdf.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 50f, 10f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    private void getEstimateDetails(string ID)
    {
        DataSet dsEstimateDetails = new DataSet();
        //dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetailsByEmpAndZone  '" + ID + "','"+ lblUser.Text +"'");
        dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstViewByEstIdForUser '" + lblUser.Text + "' , '" + ID + "'");
        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Estimate List</h2>";
        ZoneInfo += "<div class='box-icon'>";
        //ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='10%'>Estmate No.</th>";
        //ZoneInfo += "<th width='15%'>Sanction Date</th>";
        ZoneInfo += "<th width='25%'>Details of Sactioned Estimates</th>";
        ZoneInfo += "<th width='15%'>Estimate Cost</th>";
        ZoneInfo += "<th width='35%'><table width='100%'><tr><th colspan='2' align='center'>Commulative Total Expenditure as On</th></tr><tr><th width='33%'>Bill Id</th><th width='34%'>Bill Date</th><th width='33%'>Bill Amount</th></tr></table></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsEstimateDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='10%'>" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "</td>";
            //ZoneInfo += "<td class='center' width='15%'>" + dsEstimateDetails.Tables[0].Rows[i]["SanctionDate"].ToString() + "</td>";
            ZoneInfo += "<td class='center'width='25%'><table>";
            ZoneInfo += "<tr><td><b>Sub Head:</b><a href='Emp_ParticularEstimateView.aspx?EstId=" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'>" + dsEstimateDetails.Tables[0].Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td><b>Work Name:</b> " + dsEstimateDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Sanction Date:</b>" + dsEstimateDetails.Tables[0].Rows[i]["SanctionDate"].ToString() + "</td></tr>";
            ZoneInfo += "</table></td>";
            DataSet dsBal = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateBalAmt '" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'");
            ZoneInfo += "<td width='20%'><table><tr><td> " + dsEstimateDetails.Tables[0].Rows[i]["EstmateCost"].ToString() + "</td></tr><tr><td><b>Bal</b>: " + dsBal.Tables[0].Rows[0]["BalAmt"].ToString() + "</td> </tr> <tr><td><a href='Emp_EstimateAcademyWise.aspx?EstId=" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'><span class='label label-info'  style='font-size: 15.998px;'>Print Estimate</span></a></td> </tr><tr><td><b>Estimate File</b>: " + GetFileName(dsEstimateDetails.Tables[0].Rows[i]["FilePath"].ToString(), dsEstimateDetails.Tables[0].Rows[i]["FileNme"].ToString()) + "</td></tr></table></td>";
            //ZoneInfo += "<td width='35%'><table width='100%'><tr><td>--</td><td>--</td></tr></table></td>";
            ZoneInfo += "<td width='35%'><table width='100%'>";
            DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("SELECT SubBillId,CONVERT (VARCHAR(20),BillDate,107) AS bdATE,TotalAmount FROM SubmitBillByUser WHERE EstId='" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'");
            DataSet dsTa = DAL.DalAccessUtility.GetDataInDataSet("exec USP_TotalEstimateCostAfterBillSubmit '" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'");
            for (int j = 0; j < dsBillDetails.Tables[0].Rows.Count; j++)
            {
                ZoneInfo += "<tr><td width='33%'><a href='Emp_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "'>" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "</a></td><td width='34%'>" + dsBillDetails.Tables[0].Rows[j]["bdATE"].ToString() + "</td><td width='33%'>" + dsBillDetails.Tables[0].Rows[j]["TotalAmount"].ToString() + "</td></tr>";
                //if (dsTa.Tables[0].Rows.Count > 0 && dsTa.Tables[1].Rows.Count > 0)

            }
            if (dsTa.Tables[0].Rows[0]["Ta"].ToString() != "" && dsTa.Tables[1].Rows[0]["BDate"].ToString() != "")
            {
                ZoneInfo += "<td></td><td><b>Total Amount</b></td><td>" + dsTa.Tables[0].Rows[0]["Ta"].ToString() + "</td>";

            }
            else
            {
                ZoneInfo += "<td colspan='2'><span class='label label-important' style='font-size: 15.998px;'>No Bill Submit</span></td>";
            }
            ZoneInfo += "</table></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        //ZoneInfo += "</div>";
        //ZoneInfo += "</div>";
        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }


    private string GetFileName(string filepaths, string fileName)
    {
        string anchorLink = string.Empty;
        string[] filePath = filepaths.Split(',');
        int count = 0;
        foreach (string path in filePath)
        {
            count++;
            anchorLink += "<a href='" + path + "' target='_blank'>" + fileName + "_" + count + "</a> , ";
        }

        return anchorLink.Substring(0, anchorLink.Length - 3);

    }
}