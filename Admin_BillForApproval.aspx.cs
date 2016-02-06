using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_BillForApproval : System.Web.UI.Page
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

                getBillDetails();
        }
    }
    private void getBillDetails()
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillView_V2");
        divBillsDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Bills Detail</h2>";
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
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='35%'>Bills Details</th>";
        ZoneInfo += "<th width='20%'>Location</th>";
        ZoneInfo += "<th width='20%'>Agency Name</th>";
        ZoneInfo += "<th width='10%'>Bill Amount</th>";
        ZoneInfo += "<th width='15%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td class='center' width='35%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Bill No:</b> " + dsAcaDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Bill Submission Date: </b>" + dsAcaDetails.Tables[0].Rows[i]["BillDate"].ToString() + "</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td width='20%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Zone:</b> " + dsAcaDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Academy:</b> " + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
          
            ZoneInfo += "<td class='center' width='20%'> " + dsAcaDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center'width='10%'> " + dsAcaDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</span>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='15%'>";
                if (dsAcaDetails.Tables[0].Rows[i]["MatStatus"].ToString() != "1" && dsAcaDetails.Tables[0].Rows[i]["UnitStatus"].ToString() != "1")
                {
                    ZoneInfo += "<a class='btn btn-danger'  href='Admin_Unit.aspx'>";
                    ZoneInfo += "<i class='icon-edit icon-white'></i>Click To Varify New Material and Unit";
                    ZoneInfo += "</a>";
                }
                else if (dsAcaDetails.Tables[0].Rows[i]["AuditProStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-danger' href='Admin_ViewBillDetailsForApproval.aspx?SubBillIdAu=" + dsAcaDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<i class='icon-edit icon-white'></i>Proceed By Audit";
                    ZoneInfo += "</a>";
                }
                else if (dsAcaDetails.Tables[0].Rows[i]["AccProStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-danger' href='Admin_ViewBillDetailsForApproval.aspx?SubBillIdAc=" + dsAcaDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<i class='icon-edit icon-white'></i>Proceed By Account";
                    ZoneInfo += "</a>";
                }
                else if (dsAcaDetails.Tables[0].Rows[i]["UserProStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-danger' href='Admin_ViewBillDetailsForApproval.aspx?SubBillIdU=" + dsAcaDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<i class='icon-edit icon-white'></i>Proceed By User";
                    ZoneInfo += "</a>";
                }
                else if (dsAcaDetails.Tables[0].Rows[i]["PurProStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-danger' href='Admin_ViewBillDetailsForApproval.aspx?SubBillIdP=" + dsAcaDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<i class='icon-edit icon-white'></i>Proceed By Purchase";
                    ZoneInfo += "</a>";
                }
                else
                {
                    ZoneInfo += "<a class='btn btn-info' href='Admin_ViewBillDetailsForApproval.aspx?SubBillId=" + dsAcaDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<i class='icon-edit icon-white'></i>View Bill Details";
                    ZoneInfo += "</a>";
                }
            
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divBillsDetails.InnerHtml = ZoneInfo.ToString();
    }
}