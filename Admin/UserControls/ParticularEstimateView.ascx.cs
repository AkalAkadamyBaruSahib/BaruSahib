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

public partial class Admin_UserControls_ParticularEstimateView : System.Web.UI.UserControl
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
        if (Request.QueryString["EstId"] != null)
        {
            GetEstimateDetails(Request.QueryString["EstId"].ToString());
            getEstimateWithParticularDetails(Request.QueryString["EstId"].ToString());
        }
    }
    private void GetEstimateDetails(string ID)
    {
        DataSet dsEstimate1Details = new DataSet();
        dsEstimate1Details = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetails  '" + ID + "'");
        lblEstimateNo.Text = dsEstimate1Details.Tables[0].Rows[0]["EstId"].ToString();
        lblZone.Text = dsEstimate1Details.Tables[0].Rows[0]["ZoneName"].ToString();
        lblZoneCode.Text = dsEstimate1Details.Tables[0].Rows[0]["ZoId"].ToString();
        lblAca.Text = dsEstimate1Details.Tables[0].Rows[0]["AcaName"].ToString();
        lblAcaCode.Text = dsEstimate1Details.Tables[0].Rows[0]["AcId"].ToString();
        lblSubEstimate.Text = dsEstimate1Details.Tables[0].Rows[0]["SubEstimate"].ToString();
        lblTypeOfWork.Text = dsEstimate1Details.Tables[0].Rows[0]["TypeWorkName"].ToString();
        lblSanctionDate.Text = dsEstimate1Details.Tables[0].Rows[0]["SanctionDate"].ToString();
        lblEstimateCost.Text = dsEstimate1Details.Tables[0].Rows[0]["EstmateCost"].ToString();
        hdnIsApproved.Value = dsEstimate1Details.Tables[0].Rows[0]["IsApproved"].ToString();
        hdnIsItemRejected.Value = dsEstimate1Details.Tables[0].Rows[0]["IsItemRejected"].ToString();
        //if (hdnIsApproved.Value == "True" && hdnIsItemRejected.Value =="False")
        //{
        //    btnPdf.Visible = true;
        //}


    }
    private void getEstimateWithParticularDetails(string ID)
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateParticularDetails  '" + ID + "'");
        divEstimateMaterailView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Estimate Particular Details</h2>";
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
        //ZoneInfo += "<th width='5%'>Sno</th>";
        ZoneInfo += "<th width='10%'>Material Type</th>";
        ZoneInfo += "<th width='10%'>Material</th>";
        ZoneInfo += "<th width='15%'>Source Type</th>";
        ZoneInfo += "<th width='10%'>Qty</th>";
        ZoneInfo += "<th width='10%'>Unit</th>";
        ZoneInfo += "<th width='10%'>Rate</th>";
        ZoneInfo += "<th width='10%'>Amount</th>";
        ZoneInfo += "<th width='20%'>Remark</th>";
        ZoneInfo += "</tr>";
        //ZoneInfo += "<tr>";
        //ZoneInfo += "<th width='5%'></th>";
        //ZoneInfo += "<th width='10%'></th>";
        //ZoneInfo += "<th width='10%'></th>";
        //ZoneInfo += "<th width='15%'></th>";
        //ZoneInfo += "<th width='10%'></th>";
        //ZoneInfo += "<th width='10%'></th>";
        //ZoneInfo += "<th width='10%'></th>";
        //ZoneInfo += "<th width='10%'></th>";
        //ZoneInfo += "<th width='20%'></th>";
        //ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            //ZoneInfo += "<td width='5%'>" + dsAcaDetails.Tables[0].Rows[i]["Sno"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["MatTypeName"].ToString() + "</td>";
            Session["EstId"] = dsAcaDetails.Tables[0].Rows[i]["EstId"].ToString();
            ZoneInfo += "<td width='10%'><a href='Emp_MaterialDetails.aspx?MatId=" + dsAcaDetails.Tables[0].Rows[i]["MatId"].ToString() + "'>" + dsAcaDetails.Tables[0].Rows[i]["MatName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='15%'>" + dsAcaDetails.Tables[0].Rows[i]["PSName"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["Qty"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["UnitName"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["Rate"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["Amount"].ToString() + "</td>";
            // ZoneInfo += "<td width='20%'>" + dsAcaDetails.Tables[0].Rows[i]["Remark"].ToString() + "</td>";
            if (dsAcaDetails.Tables[0].Rows[i]["Remark"].ToString() == "" || dsAcaDetails.Tables[0].Rows[i]["Remark"].ToString() == null)
            {
                ZoneInfo += "<td width='15%'><span class='label label-success'>No Data</span></td>";
            }
            else
            {
                ZoneInfo += "<td width='15%'>" + dsAcaDetails.Tables[0].Rows[i]["Remark"].ToString() + "</td>";
            }
            ZoneInfo += "</tr>";

            //ZoneInfo += "<tr>";
            //ZoneInfo += "<td width='5%'></td>";
            //ZoneInfo += "<td width='10%'></td>";
            //ZoneInfo += "<td width='10%'></td>";
            //ZoneInfo += "<td width='15%'></td>";
            //ZoneInfo += "<td width='10%'></td>";
            //ZoneInfo += "<td width='10%'></td>";
            //ZoneInfo += "<td width='10%'>Total</td>";
            //ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["Amount"].ToString() + "</td>";
            //ZoneInfo += "<td width='20%'></td>";
            //ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divEstimateMaterailView.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        DataSet dsValue = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateWithMaterialForEmp_V2 '" + Request.QueryString["EstId"].ToString() + "'");
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
        EstInfo += "<th>EstQty</th>";
        EstInfo += "<th>PurchaseQty</th>";
        EstInfo += "<th>Rate</th>";
        EstInfo += "<th style='width:152px;'>Amount</th>";
        EstInfo += "</tr>";
        EstInfo += "</thead>";
        EstInfo += "<tbody>";
        for (int i = 0; i < dsValue.Tables[1].Rows.Count; i++)
        {
            if (i != dsValue.Tables[1].Rows.Count)
            {
                EstInfo += "<tr>";
                EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["MatName"].ToString() + "(" + dsValue.Tables[1].Rows[i]["UnitName"].ToString() + ")</td>";
                EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["PSName"].ToString() + "</td>";
                EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["EstQty"].ToString() + "</td>";
                EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["PurchaseQty"].ToString() + "</td>";
                EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["Rate"].ToString() + "</td>";
                EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["Amount"].ToString() + "</td>";
                EstInfo += "</tr>";
            }
            else
            {
                EstInfo += "<tr>";
                EstInfo += "<td></td><td></td><td></td><td></td><td><b>Total</b></td>";
                EstInfo += "<td>" + dsValue.Tables[2].Rows[0]["Ttl"].ToString() + "</td>";
                EstInfo += "</tr>";
            }
        }
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

        Utility.GeneratePDF(pnlPdf.InnerHtml, "PdfReport.pdf", string.Empty);
        
    }
}