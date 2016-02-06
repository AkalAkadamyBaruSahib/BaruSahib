using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Purchase_Home : System.Web.UI.Page
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
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowAllZoneDetails");

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
            ZoneInfo += "<td width='20%' class='center'><a href='Purchase_AcademiesDetails.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='35%' class='center'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td> <b>State:</b> " + dsZoneDetails.Tables[0].Rows[i]["StateName"].ToString() + " </td></tr>";
            ZoneInfo += "<tr><td> <b>City:</b> " + dsZoneDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsZoneDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            DataSet dseEmp = new DataSet();
            dseEmp = DAL.DalAccessUtility.GetDataInDataSet("exec USP_LocationEmployee '" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'");
            if (dseEmp.Tables[0].Rows.Count>0)
            {
                for (int j = 0; j < dseEmp.Tables[0].Rows.Count; j++)
                {
                    ZoneInfo += "<tr><td><span  title=' Mobile: " + dseEmp.Tables[0].Rows[j]["InMobile"].ToString() + " \n Department: " + dseEmp.Tables[0].Rows[j]["department"].ToString() + " \n Designation: " + dseEmp.Tables[0].Rows[j]["Designation"].ToString() + "\n'  href='#'> " + dseEmp.Tables[0].Rows[j]["InName"].ToString() + " </span></td></tr>";
                    //class='btn btn-setting btn-round'
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