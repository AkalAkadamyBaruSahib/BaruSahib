using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AuditHome : System.Web.UI.Page
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
        BindAcademies();

    }

    private void BindAcademies()
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AcademyShowForAudit  '" + lblUser.Text + "'");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Academies Details</h2>";
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
        ZoneInfo += "<th width='30%'>Zone and Academy</th>";
        ZoneInfo += "<th width='30%'>Location</th>";
        ZoneInfo += "<th width='40%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='30%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Zone:</b> " + dsAcaDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Academy:</b> " + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='30%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>State:</b> " + dsAcaDetails.Tables[0].Rows[i]["StateName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>City:</b> " + dsAcaDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsAcaDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='40%' align='center'>";
            //ZoneInfo += "<a class='btn btn-info' href='Emp_MaterialView.aspx'>";
            //ZoneInfo += "<i class='icon-edit icon-white'></i>MAS Account";
            //ZoneInfo += "</a>  ";
            ZoneInfo += "<a class='btn btn-info' href='Audit_EstimateView.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>";
            //
            ZoneInfo += "<i class='icon-edit icon-white'></i>Estimates";
            ZoneInfo += "</a>  ";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();

    }
}