using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Workshop_AcademiesDetails : System.Web.UI.Page
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
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AcademayWithZone4Pusrchase '" + ID + "'");
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
        ZoneInfo += "<th width='30%'>Details</th>";
        ZoneInfo += "<th width='10%'>Status</th>";
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
            if (dsAcaDetails.Tables[1].Rows.Count > 0)
            {
                ZoneInfo += "<tr><td><b>Sewadar:</b> " + dsAcaDetails.Tables[1].Rows[0]["InName"].ToString() + "(" + dsAcaDetails.Tables[1].Rows[0]["InMobile"].ToString() + ")</td></tr>";
                DataSet dsCount = new DataSet();
                dsCount = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_AcaCountForEstWABill '" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'");
                for (int j = 0; j < dsCount.Tables[0].Rows.Count; j++)
                {
                    ZoneInfo += "<tr><td><b><a href='Workshop_EstimateView.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>Estimate(" + dsCount.Tables[0].Rows[j]["EstCount"].ToString() + ")</a></b></td></tr>";
                    ZoneInfo += "<tr><td><b><a href='Workshop_WorkAllot.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>Work Allot(" + dsCount.Tables[1].Rows[j]["WACount"].ToString() + ")</a></b></td></tr>";
                    ZoneInfo += "<tr><td><b><a href='Workshop_AllBillDetails.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>Bills(" + dsCount.Tables[2].Rows[j]["BillCount"].ToString() + ")</a></b></td></tr>";
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
            ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 12.998px;'>" + dsAcaDetails.Tables[0].Rows[i]["StatusTypeName"].ToString() + "</span>";
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