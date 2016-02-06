using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Account_WorkAllot : System.Web.UI.Page
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
                GetWorkAllotDetailsByClick(Request.QueryString["AcaId"].ToString());

            }
        }
    }
    protected void GetWorkAllotDetailsByClick(string id)
    {
        DataSet dsSateDetails = new DataSet();
        dsSateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowWorkAllotDetailsByAcaId '" + id + "'");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='20%'>Zone & Academy</th>";
        ZoneInfo += "<th width='25%'>Name of Work</th>";
        ZoneInfo += "<th width='25%'>Image of Work</th>";
        ZoneInfo += "<th width='10%'>Status</th>";
        //ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";

        for (int i = 0; i < dsSateDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='20%'><table><tr><td><b>Zone:</b> " + dsSateDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr>";
            ZoneInfo += " <tr><td><b>Academy:</b> " + dsSateDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td width='25%'>" + dsSateDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</td>";
            ZoneInfo += "<td width='25%'><ul class='thumbnails gallery><li id='image-1' class='thumbnail'>";
            ZoneInfo += "<a  style='background:url(" + dsSateDetails.Tables[0].Rows[i]["ImageFilePath"].ToString() + ")'  href='" + dsSateDetails.Tables[0].Rows[i]["ImageFilePath"].ToString() + "'>";
            ZoneInfo += "<img class='grayscale' width='75Px' height='75PX' src='" + dsSateDetails.Tables[0].Rows[i]["ImageFilePath"].ToString() + "' ></a></li></ul></td>";
            ZoneInfo += "<td class='center' width='10%'>";
            if (dsSateDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            //ZoneInfo += "<td class='center' width='20%'>";
            //ZoneInfo += "<a class='btn btn-success' href='Admin_WorkAllot.aspx?WAIdA=" + dsSateDetails.Tables[0].Rows[i]["WAId"].ToString() + "'>";
            //ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            //ZoneInfo += "</a>&nbsp;";
            //ZoneInfo += "<a class='btn btn-info' href='Admin_WorkAllot.aspx?WAId=" + dsSateDetails.Tables[0].Rows[i]["WAId"].ToString() + "'>";
            //ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            //ZoneInfo += "</a>&nbsp;";
            //ZoneInfo += "<a class='btn btn-danger' href='Admin_WorkAllot.aspx?WAIdIA=" + dsSateDetails.Tables[0].Rows[i]["WAId"].ToString() + "'>";
            //ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            //ZoneInfo += "</a>";
            //ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();
    }
}