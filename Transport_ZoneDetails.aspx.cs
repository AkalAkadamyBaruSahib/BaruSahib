using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Transport_ZoneDetails : System.Web.UI.Page
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

            if (Request.QueryString["ZoneId"] != null)
            {
                getAcaDetails(Request.QueryString["ZoneId"].ToString());
            }
        }
    }

    private void getAcaDetails(string ID)
    {
        DataSet dsCount = new DataSet();
        UsersRepository users = new UsersRepository(new AkalAcademy.DataContext());
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = users.GetAcademyByUserNameAndZoneID(lblUser.Text, 2, int.Parse(ID));
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Academies Details</h2>";
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
        ZoneInfo += "<th width='20%'>Academy</th>";
        ZoneInfo += "<th width='25%'>Location</th>";
        ZoneInfo += "<th width='30%'>No. Of Vehicles</th>";
        //ZoneInfo += "<th width='10%'>Status</th>";
        //ZoneInfo += "<th width='15%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='20%'>" + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>State:</b>" + dsAcaDetails.Tables[0].Rows[i]["StateName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>City:</b>" + dsAcaDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsAcaDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='30%'>";
            ZoneInfo += "<table>";
            string sqlQuery = "SELECT Count(*) AS Count,Incharge.InName, Incharge.InMobile FROM Vehicles " +
                                    "INNER JOIN AcademyAssignToEmployee AAE ON AAE.AcaID=Vehicles.AcademyID " +
                                    "INNER JOIN Incharge ON AAE.EmpId = Incharge.InchargeId " +
                                    "WHERE IsApproved=1 and  AcademyID= @AcaID AND Incharge.UserTypeID=14 " +
                                    "group by Incharge.InName, Incharge.InMobile ";

            sqlQuery=sqlQuery.Replace("@AcaID",dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString());
            dsCount = DAL.DalAccessUtility.GetDataInDataSet(sqlQuery);
            if (dsCount.Tables[0].Rows.Count > 0)
            {
                ZoneInfo += "<tr><td><b>Alloted To:</b></td>";
                for (int j = 0; j < dsCount.Tables[0].Rows.Count; j++)
                {
                    ZoneInfo += " <td>" + dsCount.Tables[0].Rows[j]["InName"].ToString() + "(" + dsCount.Tables[0].Rows[j]["InMobile"].ToString() + ")";
                    ZoneInfo += " </td></tr>";
                    ZoneInfo += "<tr><td><b><a href='Transport_VehicleDetails.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>Vehicals(" + dsCount.Tables[0].Rows[j]["Count"].ToString() + ")</a></b></td></tr>";
                }
            }
            else
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>No Incharge Alloted</span>";
            }

            //ZoneInfo += "<tr><td><b>Site Engineer:</b> Employee Name(Mobile No)</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center'width='10%'>";
            //if (dsAcaDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            //{
            //    ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            //}
            //else
            //{
            //ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 12.998px;'>" + dsAcaDetails.Tables[0].Rows[i]["StatusTypeName"].ToString() + "</span>";
            //}
            //ZoneInfo += "</td>";
            //ZoneInfo += "<td class='center' width='15%'>";
            //ZoneInfo += "<a class='btn btn-info' href='#'>";
            //ZoneInfo += "<i class='icon-edit icon-white'></i>Edit";
            //ZoneInfo += "</a>";
            //ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();
    }
}