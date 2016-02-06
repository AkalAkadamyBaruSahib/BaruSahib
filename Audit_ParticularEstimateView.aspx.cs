using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Audit_ParticularEstimateView : System.Web.UI.Page
{
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
        lblWorkAllotName.Text = dsEstimate1Details.Tables[0].Rows[0]["WorkAllotName"].ToString();

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
            ZoneInfo += "<td width='10%'><a href='Audit_MaterialDetails.aspx?MatId=" + dsAcaDetails.Tables[0].Rows[i]["MatId"].ToString() + "'>" + dsAcaDetails.Tables[0].Rows[i]["MatName"].ToString() + "</a></td>";
            Session["EstId"] = dsAcaDetails.Tables[0].Rows[i]["EstId"].ToString();
            ZoneInfo += "<td width='15%'>" + dsAcaDetails.Tables[0].Rows[i]["PSName"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["Qty"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["UnitName"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["Rate"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsAcaDetails.Tables[0].Rows[i]["Amount"].ToString() + "</td>";
            if (dsAcaDetails.Tables[0].Rows[i]["Remark"].ToString() == "" || dsAcaDetails.Tables[0].Rows[i]["Remark"].ToString() == null)
            {
                ZoneInfo += "<td width='15%'><span class='label label-success'>No Data</span></td>";
            }
            else
            {
                ZoneInfo += "<td width='15%'>" + dsAcaDetails.Tables[0].Rows[i]["Remark"].ToString() + "</td>";
            }
            //ZoneInfo += "<td width='20%'>" + dsAcaDetails.Tables[0].Rows[i]["Remark"].ToString() + "</td>";

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
}