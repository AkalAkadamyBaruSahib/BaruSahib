using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emp_Home : System.Web.UI.Page
{
    private int UserTypeID { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
            UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        }

        BindZoneDetails();
        
        if (Request.QueryString["ZoneId"] != null)
        {
            divallotedZone.Visible = false;
            getAcaDetails(Request.QueryString["ZoneId"].ToString());
        }
    }
    protected void BindZoneDetails()
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_ZoneAndUserDetails '"+ lblUser.Text +"'");

        divZone.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-bordered table-striped table-condensed'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='10%'>Zone Code</th>";
        ZoneInfo += "<th width='25%'>Zone Name</th>";
        ZoneInfo += "<th width='35%'>Location</th>";
        //ZoneInfo += "<th width='25%'>Zone Incharge</th>";
        ZoneInfo += "<th width='30%'>Total Nos. of Academy</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {

            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='10%' class='center'>" + dsZoneDetails.Tables[0].Rows[i]["ZoId"].ToString() + "</td>";
            ZoneInfo += "<td width='25%' class='center'>" + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
            ZoneInfo += "<td width='35%' class='center'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td> <b>State:</b> " + dsZoneDetails.Tables[0].Rows[i]["StateName"].ToString() + " </td></tr>";
            ZoneInfo += "<tr><td> <b>City:</b> " + dsZoneDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsZoneDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            DataSet dsAcaCount = new DataSet();
            if (Session["UserTypeID"].ToString() == "10")
            {
                dsAcaCount = DAL.DalAccessUtility.GetDataInDataSet("select count(*) as Coun from AcademyAssignToEmployee aae inner join Incharge inc on inc.InchargeId=aae.EmpId where inc.LoginId='" + lblUser.Text + "'");
            }
            else
            {
                dsAcaCount = DAL.DalAccessUtility.GetDataInDataSet("select COUNT(*) as Coun from Academy where ZoneId='" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'");
            }

            for (int j = 0; j < dsAcaCount.Tables[0].Rows.Count; j++)
            {
                ZoneInfo += "<td width='30%' class='center'><a href='Emp_Home.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'>Academy(" + dsAcaCount.Tables[0].Rows[j]["Coun"].ToString() + ")</a></td>";
            }
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divZone.InnerHtml = ZoneInfo.ToString();
        //lblZone.Text = dsZoneDetails.Tables[0].Rows[0]["ZoneName"].ToString();
    }
    private void getAcaDetails(string ID)
    {
        DataSet dsAcaDetails = new DataSet();
        if (Session["UserTypeID"].ToString() == "10")
        {
            dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowAcademayWith_Emp  '" + lblUser.Text + "'");
        }
        else
        {
            dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AcademayWithZone_Emp  '" + ID + "'");
        }
        
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
        ZoneInfo += "<th width='20%'>Academy</th>";
        ZoneInfo += "<th width='20%'>Location</th>";
        ZoneInfo += "<th width='10%'>Status</th>";
        if (UserTypeID != (int)TypeEnum.UserType.COMPLAINT)
        {
            ZoneInfo += "<th width='50%'>Actions</th>";
        }
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='20%'>" + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>State:</b>" + dsAcaDetails.Tables[0].Rows[i]["StateName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>City:</b>" + dsAcaDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsAcaDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center'width='10%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><span class='label label-success' title='" + dsAcaDetails.Tables[0].Rows[i]["StatusTypeName"].ToString() + "' >" + dsAcaDetails.Tables[0].Rows[i]["StatusTypeName"].ToString() + "</span></td></tr>";
            //style='font-size: 15.998px;'
            if (dsAcaDetails.Tables[0].Rows[i]["lastZoneId"].ToString() == "")
            {
                ZoneInfo += "<tr><td></td></tr>";
            }
            else
            {
                ZoneInfo += "<tr><td><span class='label label-warning' title='Shifted Academy from another zone.' style='font-size: 15.998px;'>Shifted Academy</span></td></tr>";
            }
            ZoneInfo += "</table>";
           // ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>" + dsAcaDetails.Tables[0].Rows[i]["StatusTypeName"].ToString() + "</span>";
            ZoneInfo += "</td>"; Session["AcaId"] = dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString();
            if (UserTypeID != (int)TypeEnum.UserType.COMPLAINT)
            {
                ZoneInfo += "<td class='center' width='50%' align='center'>";
                ZoneInfo += "<a class='btn btn-info' href='Emp_DrawingView.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>";
                ZoneInfo += "<i class='icon-edit icon-white'></i>Drawings";
                ZoneInfo += "</a>  ";
                //ZoneInfo += "<a class='btn btn-info' href='Emp_MaterialView.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>";
                ////ZoneInfo += "<a class='btn btn-info' href='Emp_MaterialView.aspx'>";
                //ZoneInfo += "<i class='icon-edit icon-white'></i>MAS Account";
                //ZoneInfo += "</a>  ";
                ZoneInfo += "<a class='btn btn-info' href='Emp_EstimateView.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>";
                ZoneInfo += "<i class='icon-edit icon-white'></i>Estimates";
                ZoneInfo += "</a>  ";
                ZoneInfo += "<a class='btn btn-info' href='Emp_BillSubmit.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>";
                ZoneInfo += "<i class='icon-edit icon-white'></i>Submit Bills";
                ZoneInfo += "</a>  ";
                //ZoneInfo += "<a class='btn btn-info' href='Emp_BillSubmit.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>";
                //ZoneInfo += "<i class='icon-edit icon-white'></i>Bill ";
                //ZoneInfo += "</a>  ";
            }
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