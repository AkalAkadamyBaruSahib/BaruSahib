using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EstimateViewDetail : System.Web.UI.Page
{
    private int WorkAllotID = -1;

    private int PurchaseSourceID = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["WAIdEstimate"] != null)
            {
                WorkAllotID = Convert.ToInt32(Request.QueryString["WAIdEstimate"]);
                PurchaseSourceID = Convert.ToInt32(Request.QueryString["PsId"]);
                hdnWorkAllotID.Value = WorkAllotID.ToString();
                BindMaterialDetails();
            }
        }
    }

    protected void BindMaterialDetails()
    {
        DataTable dsEstimate = new DataTable();
        dsEstimate = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetailByWorkAllot " + Convert.ToInt32(hdnWorkAllotID.Value) + "," + Convert.ToInt32(PurchaseSourceID)).Tables[0];
        if (dsEstimate != null && dsEstimate.Rows.Count > 0)
        {
            divEstimateDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;

            ZoneInfo += "<div class='box-content'>";
            ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<thead>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<th width='5%'>Sr.No</th>";
            ZoneInfo += "<th width='10%'>Estmate No.</th>";
            ZoneInfo += "<th width='30%'>Sub Estimate</th>";
            ZoneInfo += "<th width='30%'>Zone Name</th>";
            ZoneInfo += "<th width='30%'>Academy Name</th>";
            ZoneInfo += "<th width='15%'>Estimate Cost</th>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</thead>";
            ZoneInfo += "<tbody>";
            for (int i = 0; i < dsEstimate.Rows.Count; i++)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td width='5%'>" + (i + 1) + "</td>";
                ZoneInfo += "<td width='10%'>" + dsEstimate.Rows[i]["EstId"].ToString() + "</td>";
                ZoneInfo += "<td><a href='Admin_ParticularEstimateView.aspx?EstId=" + dsEstimate.Rows[i]["EstId"].ToString() + "'>" + dsEstimate.Rows[i]["SubEstimate"].ToString() + "</a></td>";
                ZoneInfo += "<td>" + dsEstimate.Rows[i]["ZoneName"].ToString() + "</td>";
                ZoneInfo += "<td>" + dsEstimate.Rows[i]["AcaName"].ToString() + "</td>";
                ZoneInfo += "<td width='20%'>" + dsEstimate.Rows[i]["EstimateCost"].ToString() + "</td>";
                ZoneInfo += "</tr>";
            }
            ZoneInfo += "</tbody>";
            ZoneInfo += "</table>";
            divEstimateDetails.InnerHtml = ZoneInfo.ToString();
        }
        else {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Estimate not Exit');</script>", false);
        }
    }
}