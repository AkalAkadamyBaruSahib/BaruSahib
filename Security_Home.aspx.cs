using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_Home : System.Web.UI.Page
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

           BindZoneDetails();
        }
    }
    protected void BindZoneDetails()
    {
        int UsrTypeID = int.Parse(Session["UserTypeID"].ToString());
        int InchargeID = int.Parse(Session["InchargeID"].ToString());
        DataSet dsZoneDetails = new DataSet();
        string username = Session["EmailId"].ToString();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_ZoneAndUserDetails] '" + username + "'");
     
        divZone.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-bordered table-striped table-condensed'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='10%'>Zone Code</th>";
        ZoneInfo += "<th width='20%'>Zone Name</th>";
        ZoneInfo += "<th width='20%'>Location</th>";
        ZoneInfo += "<th width='20%'>Zonal Security Supervisor</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='10%' class='center'>" + dsZoneDetails.Tables[0].Rows[i]["ZoId"].ToString() + "</td>";
            //Session["ZoneId"] = dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString();
            ZoneInfo += "<td width='20%' class='center'><a href='#'>" + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='20%' class='center'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td> <b>State:</b> " + dsZoneDetails.Tables[0].Rows[i]["StateName"].ToString() + " </td></tr>";
            ZoneInfo += "<tr><td> <b>City:</b> " + dsZoneDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsZoneDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            DataTable dsINcharge = new DataTable();
            dsINcharge = DAL.DalAccessUtility.GetDataInDataSet("select distinct Inc.InName from  AcademyAssignToEmployee AAE INNER JOIN Incharge Inc on Inc.InchargeId = AAE.EmpID where AAE.ZoneId='" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "' and Inc.ModuleID = 3 and Inc.DesigId = 26").Tables[0];
            if (dsINcharge != null && dsINcharge.Rows.Count > 0)
            {
                for (int j = 0; j < dsINcharge.Rows.Count; j++)
                {
                    ZoneInfo += "<td width='20%' class='center'>" + dsINcharge.Rows[j]["InName"].ToString() + "</td>";
                }
            }
            else
            {
                ZoneInfo += "<td width='20%' class='center'>No Zonal Officer Assign</td>";
            }
            ZoneInfo += "</td>";
            
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divZone.InnerHtml = ZoneInfo.ToString();

    }
}