using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_MaterialDetails : System.Web.UI.Page
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
        if (Request.QueryString["MatId"] != null)
        {
            GetMaterialDetails(Request.QueryString["MatId"].ToString());

        }
    }
    private void GetMaterialDetails(string ID)
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AuditMaterialDetails '" + Session["EstId"].ToString() + "', '" + ID + "'");
        lblEstimateNo.Text = Session["EstId"].ToString();
        lblMaterial.Text = dsAcaDetails.Tables[0].Rows[0]["MatName"].ToString();
        lblTtlAmt.Text = dsAcaDetails.Tables[0].Rows[0]["TtlAmt"].ToString();
        lblTtlQty.Text = dsAcaDetails.Tables[0].Rows[0]["TtlQty"].ToString();
        lblEstRate.Text = dsAcaDetails.Tables[0].Rows[0]["EstRate"].ToString();
        lblBalAmt.Text = dsAcaDetails.Tables[2].Rows[0]["BalAmt"].ToString();
        lblBalQty.Text = dsAcaDetails.Tables[3].Rows[0]["BalQty"].ToString();
        divEstimateMaterailView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span10'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Material Details</h2>";
        ZoneInfo += "<div class='box-icon'>";
        //ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        //ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        //ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        //ZoneInfo += "<th width='5%'>Sno</th>";
        ZoneInfo += "<th width='20%'>Bill Date</th>";
        ZoneInfo += "<th width='20%'>Bill Id</th>";
        ZoneInfo += "<th width='20%'>Qty</th>";
        ZoneInfo += "<th width='20%'>Rate</th>";
        ZoneInfo += "<th width='20%'>Amount</th>";

        ZoneInfo += "</tr>";

        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        if (dsAcaDetails.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < dsAcaDetails.Tables[1].Rows.Count; i++)
            {

                ZoneInfo += "<tr>";
                ZoneInfo += "<td width='20%'>" + dsAcaDetails.Tables[1].Rows[i]["BillDate"].ToString() + "</td>";
                ZoneInfo += "<td width='20%'><a href='Admin_ViewBillDetailsForApproval.aspx?SubBillId=" + dsAcaDetails.Tables[1].Rows[i]["SubBillId"].ToString() + "'>" + dsAcaDetails.Tables[1].Rows[i]["SubBillId"].ToString() + "</a></td>";
                ZoneInfo += "<td width='20%'>" + dsAcaDetails.Tables[1].Rows[i]["Qty"].ToString() + "</td>";
                ZoneInfo += "<td width='20%'>" + dsAcaDetails.Tables[1].Rows[i]["Rate"].ToString() + "</td>";
                ZoneInfo += "<td width='20%'>" + dsAcaDetails.Tables[1].Rows[i]["Amount"].ToString() + "</td>";
                ZoneInfo += "</tr>";

            }
        }
        else
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td colspan='5'><span class='label label-success'>No bill submit against this estimate.</span></td>";
            ZoneInfo += "</tr>";
        }





        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divEstimateMaterailView.InnerHtml = ZoneInfo.ToString();
    }
}