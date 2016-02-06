using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Workshop_AllBillDetails : System.Web.UI.Page
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

            if (Request.QueryString["AcaId"] != null)
            {
                BillDetails(Request.QueryString["AcaId"].ToString());

            }
        }
    }
    protected void BillDetails(string id)
    {
        DataSet dsBillDetails = new DataSet();
        dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AllBillDetailsByAcaId '" + id + "'");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Bills List</h2>";
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
        ZoneInfo += "<th width='15%'>Zone</th>";
        ZoneInfo += "<th width='15%'>Academy</th>";
        ZoneInfo += "<th width='35%'>Bill Details</th>";
        ZoneInfo += "<th width='15%'>Amount</th>";
        ZoneInfo += "<th width='20%'>Chargable To</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsBillDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='15%'>" + dsBillDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
            ZoneInfo += "<td width='15%'>" + dsBillDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td width='35%'><table>";
            ZoneInfo += "<tr><td><b>Bill No.</b> <a  href='Workshop_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</spam></a></td></tr>";
            ZoneInfo += "<tr><td><b>Bill Date:</b>" + dsBillDetails.Tables[0].Rows[i]["BillDate"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Agency Name:</b> " + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Description:</b> " + dsBillDetails.Tables[0].Rows[i]["BillDescr"].ToString() + "</td></tr>";
            ZoneInfo += "</table></td>";
            ZoneInfo += "<td width='15%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'><table>";
            if (dsBillDetails.Tables[0].Rows[i]["BillType"].ToString() == "Sanctioned")
            {
                ZoneInfo += "<tr><td><b>Estimate No.</b> " + dsBillDetails.Tables[0].Rows[i]["EstId"].ToString() + "</td></tr>";
                //ZoneInfo += "<tr><td><b>Bill Type:</b> " + dsBillDetails.Tables[0].Rows[i]["BillTypeName"].ToString() + "</td></tr>";
                ZoneInfo += "<tr><td><b>Work Name:</b> " + dsBillDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            }
            else
            {
                ZoneInfo += "<tr><td><b>Bill Type:</b> " + dsBillDetails.Tables[0].Rows[i]["BillTypeName"].ToString() + "</td></tr>";
                //ZoneInfo += "<tr><td><b>Work Name:</b> " + dsBillDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            }
            ZoneInfo += "</table></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();

    }
}