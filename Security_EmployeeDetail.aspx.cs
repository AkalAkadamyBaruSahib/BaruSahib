using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_EmployeeDetail : System.Web.UI.Page
{
    private bool IsApproved = true;
    private int InchargeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           getSecurityDetails();
           
        }

    }
    private void getSecurityDetails()
    {
        DataSet dsSecurityEmpDetails = new DataSet();
        dsSecurityEmpDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_GetSecurityEmpDetails]");
        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Vehicles List</h2>";
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
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='30%'>Employee Names.</th>";
        ZoneInfo += "<th width='25%'>Assigned For</th>";
        //ZoneInfo += "<th width='15%'>Sanction Date</th>";
        ZoneInfo += "<th width='20%'>Address</th>";
        ZoneInfo += "<th width='5%'>Salary</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsSecurityEmpDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='30%'><table><tr><td><a class='btn btn-danger' href='Security_NewEmployee.aspx?EmployeeID=" + dsSecurityEmpDetails.Tables[0].Rows[i]["ID"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>" + dsSecurityEmpDetails.Tables[0].Rows[i]["Name"].ToString() + "</span></a></td></tr><tr><td><b>Designation</b>: " + dsSecurityEmpDetails.Tables[0].Rows[i]["Designation"].ToString() + "</td></tr><tr><td><b>Department</b>: " + dsSecurityEmpDetails.Tables[0].Rows[i]["department"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td width='30%'><table><tr><td><b>Zone</b>: " + dsSecurityEmpDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr><tr><td><b>Academy</b>: " + dsSecurityEmpDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr></table>";
            //ZoneInfo += "<td class='center' width='15%'>" + dtapproved.Rows[i]["dt"].ToString() + "</td>";
            ZoneInfo += "<td class='center'width='20%'><table>";
            ZoneInfo += "<tr><td><b>Address:</b>" + dsSecurityEmpDetails.Tables[0].Rows[i]["Address"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>MobileNo:</b> " + dsSecurityEmpDetails.Tables[0].Rows[i]["MobileNo"].ToString() + "</td></tr>";
            ZoneInfo += "</table></td>";
            ZoneInfo += "<td width='5%'>" + dsSecurityEmpDetails.Tables[0].Rows[i]["Salary"].ToString() + "</td>";
          
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }
}