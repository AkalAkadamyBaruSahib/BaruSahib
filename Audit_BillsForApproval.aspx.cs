using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Audit_BillsForApproval : System.Web.UI.Page
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
                lblUser.Text = Session["EmailId"].ToString();
            }

            getBillDetails();
        }
    }
    private void getBillDetails()
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_VerifiedBillViewByAudit '"+ lblUser.Text +"'");
        divBillsDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Bills Detail</h2>";
        ZoneInfo += "<div class='box-icon'>";
        ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='20%'>Location</th>";
        ZoneInfo += "<th width='30%'>Bills Details</th>";
        //ZoneInfo += "<th width='30%'>Varification Details</th>";
        //ZoneInfo += "<th width='10%'>Remark</th>";
        ZoneInfo += "<th width='15%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='20%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Zone:</b> " + dsAcaDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Academy:</b> " + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='35%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Bill No:</b> " + dsAcaDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Bill Submission Date and Time:</b> " + dsAcaDetails.Tables[0].Rows[i]["BillDate"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>AgencyName:</b> " + dsAcaDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Total Amount:</b> <span style='font-size: 15.998px;color:Red;'><b>" + dsAcaDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</b></span></td></tr>";
            ZoneInfo += "</table>";
            //ZoneInfo += "</td>";
            //ZoneInfo += "<td class='center' width='35%'>";
            //ZoneInfo += "<table>";
            //ZoneInfo += "<tr><td><b>Varification By:</b> " + dsAcaDetails.Tables[0].Rows[i]["FirstVarify"].ToString() + "</td></tr>";
            //ZoneInfo += "<tr><td><b>Varification Date:</b>" + dsAcaDetails.Tables[0].Rows[i]["FirstVarifyOn"].ToString() + "</td></tr>";
            //ZoneInfo += "</table>";
            //ZoneInfo += "</td>";
            //ZoneInfo += "<td class='center'width='10%'> " + dsAcaDetails.Tables[0].Rows[i]["FirstVarifyRemark"].ToString() + "</span>";
            //ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='15%'>";
            ZoneInfo += "<a class='btn btn-info' href='Audit_ViewBillDetailsForApproval.aspx?SubBillId=" + dsAcaDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i>View Bill Details";
            ZoneInfo += "</a></br></br>";
            ZoneInfo += "<a  href='InVoice.aspx?BillId=" + dsAcaDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
            ZoneInfo += "<span class='label label-warning'  style='font-size: 15.998px;'>Print Bill</span>";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divBillsDetails.InnerHtml = ZoneInfo.ToString();
    }
}