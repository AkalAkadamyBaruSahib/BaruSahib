using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Workshop_Home : System.Web.UI.Page
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

            BindAllZoneDetails();


        }
    }
    protected void BindAllZoneDetails()
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowAllZoneDetails ");
        DataTable dseEmp = new DataTable();
        dseEmp = DAL.DalAccessUtility.GetDataInDataSet("exec USP_LocationEmployee").Tables[0];
        divZone.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-bordered table-striped table-condensed'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='10%'>Zone Code</th>";
        ZoneInfo += "<th width='20%'>Zone Name</th>";
        ZoneInfo += "<th width='35%'>Location</th>";
        ZoneInfo += "<th width='25%'>Zone Incharge</th>";
        ZoneInfo += "<th width='10%'>Total Nos. of Academy</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='10%' class='center'>" + dsZoneDetails.Tables[0].Rows[i]["ZoId"].ToString() + "</td>";
            //Session["ZoneId"] = dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString();
            ZoneInfo += "<td width='20%' class='center'><a href='Workshop_AcademiesDetails.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='35%' class='center'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td> <b>State:</b> " + dsZoneDetails.Tables[0].Rows[i]["StateName"].ToString() + " </td></tr>";
            ZoneInfo += "<tr><td> <b>City:</b> " + dsZoneDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsZoneDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            DataRow[] dr = dseEmp.Select("ZoneID=" + dsZoneDetails.Tables[0].Rows[i]["ZoneID"].ToString());
            if (dr != null)
            {

                for (int j = 0; j < dr.Length; j++)
                {
                    ZoneInfo += "<tr><td><span  title=' Mobile: " + dr[j]["InMobile"].ToString() + " \n Department: " + dr[j]["department"].ToString() + " \n Designation: " + dr[j]["Designation"].ToString() + "\n'  href='#'> " + dr[j]["InName"].ToString() + " </span></td></tr>";
                }
            }
            else
            {
                //ZoneInfo += "<a class='btn btn-setting btn-round'  href='AdminHome.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Please Allot Incharge</span></a>";
                //Session["ZoneId"] = dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString();
                ZoneInfo += "<a href='#'><span class='label label-important' style='font-size: 15.998px;'>Requst To Admin</span></a>";
                //DataSet dsZoneName = new DataSet();
                //dsZoneName = DAL.DalAccessUtility.GetDataInDataSet("select ZoneName from zone where ZoneId='" + Session["ZoneId"].ToString() + "'");
                //lblZone.Text = dsZoneName.Tables[0].Rows[i]["ZoneName"].ToString();
            }
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            //Session["ZoneId"] = dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString();
            //DataSet dsZoneName = new DataSet();
            //dsZoneName = DAL.DalAccessUtility.GetDataInDataSet("select ZoneName from zone where ZoneId='" + Session["ZoneId"].ToString() + "'");
            //lblZone.Text = dsZoneName.Tables[0].Rows[0]["ZoneName"].ToString();

            //DataSet dsAcaCount = new DataSet();
            //dsAcaCount = DAL.DalAccessUtility.GetDataInDataSet("select COUNT(*) as Coun from Academy where ZoneId='" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'");
            ZoneInfo += "<td width='10%' class='center'>" + dsZoneDetails.Tables[0].Rows[i]["Coun"].ToString() + "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divZone.InnerHtml = ZoneInfo.ToString();
    }
}