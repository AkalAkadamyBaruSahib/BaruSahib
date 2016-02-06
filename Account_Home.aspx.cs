using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Account_Home : System.Web.UI.Page
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
            
            if (Request.QueryString["ZoneId"] != null)
            {

            }
        }
    }
    protected void BindZoneDetails()
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_ShowZoneforAccount");

        divZone.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-bordered table-striped table-condensed'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='20%'>Zone Code</th>";
        ZoneInfo += "<th width='20%'>Zone Name</th>";
        ZoneInfo += "<th width='40%'>Location</th>";
        //ZoneInfo += "<th width='25%'>Zone Incharge</th>";
        ZoneInfo += "<th width='20%'>Total Nos. of Academy</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='10%' class='center'>" + dsZoneDetails.Tables[0].Rows[i]["ZoId"].ToString() + "</td>";
            ZoneInfo += "<td width='20%' class='center'><a href='Account_AcademiesDetails.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='35%' class='center'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td> <b>State:</b> " + dsZoneDetails.Tables[0].Rows[i]["StateName"].ToString() + " </td></tr>";
            ZoneInfo += "<tr><td> <b>City:</b> " + dsZoneDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsZoneDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            //ZoneInfo += "<td class='center' width='25%'>";
            //ZoneInfo += "<table>";
            //DataSet dseEmp = new DataSet();
            //dseEmp = DAL.DalAccessUtility.GetDataInDataSet("SELECT Incharge.InName FROM  AcademyAssignToEmployee INNER JOIN Incharge ON AcademyAssignToEmployee.EmpId = Incharge.InchargeId WHERE ZoneId='" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'");
            //if (dseEmp.Tables[0].Rows.Count > 0)
            //{
            //    ZoneInfo += "<tr><td> " + dseEmp.Tables[0].Rows[0]["InName"].ToString() + " </td></tr>";

            //}
            //else
            //{
            //    ZoneInfo += "<a class='btn btn-setting btn-round' href='AdminHome.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'><span class='label label-important' >Please Allot Incharge</span></a>";
            //}
            //ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            Session["ZoneId"] = dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString();
            DataSet dsAcaCount = new DataSet();
            dsAcaCount = DAL.DalAccessUtility.GetDataInDataSet("select COUNT(*) as Coun from Academy where ZoneId='" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'");
            ZoneInfo += "<td width='10%' class='center'>" + dsAcaCount.Tables[0].Rows[0]["Coun"].ToString() + "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divZone.InnerHtml = ZoneInfo.ToString();
        //lblZone.Text = dsZoneDetails.Tables[0].Rows[0]["ZoneName"].ToString();
    }
}