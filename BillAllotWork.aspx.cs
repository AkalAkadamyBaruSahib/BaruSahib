﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class BillAllotWork : System.Web.UI.Page
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
            BindWorkAllot();
        }
    }
    protected void BindWorkAllot()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select WAId,WorkAllotName from WorkAllot where Active=1");
        ddlWorkAlloted.DataSource = dsZone;
        ddlWorkAlloted.DataValueField = "WAId";
        ddlWorkAlloted.DataTextField = "WorkAllotName";
        ddlWorkAlloted.DataBind();
        ddlWorkAlloted.Items.Insert(0, "SELECT ALLOTED WORK");
        ddlWorkAlloted.SelectedIndex = 0;
    }
    protected void ddlWorkAlloted_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWorkAlloted.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Alloted Work.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForAllotedWorkWise '" + ddlWorkAlloted.SelectedValue + "' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> Bill Status</h2>";
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
            ZoneInfo += "<th width='10%'>Zone</th>";
            ZoneInfo += "<th width='10%'>Academy</th>";
            ZoneInfo += "<th width='10%'>Bill No.</th>";
            ZoneInfo += "<th width='13%'>Agency Name</th>";
            ZoneInfo += "<th width='13%'>Amount</th>";
            ZoneInfo += "<th width='13%'>H/Q Activity</th>";
            ZoneInfo += "<th width='13%'>Audit Activity</th>";
            ZoneInfo += "<th width='13%'>Account Activity</th>";
            ZoneInfo += "<th width='13%'>Reciving</th>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</thead>";
            ZoneInfo += "<tbody>";
            for (int i = 0; i < dsBillDetails.Tables[0].Rows.Count; i++)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td style='display:none;'>1</td>";
                ZoneInfo += "<td width='10%'>" + dsBillDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
                ZoneInfo += "<td width='10%'>" + dsBillDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='10%' align='center'>";
                ZoneInfo += "<a  href='Admin_BillDetailsAfterApproval.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date: " + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<a  href='Admin_ViewBillDetailsForApproval.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Pending</span></a>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;' title='Rejected Date: " + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date: " + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Rejected Date: " + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date: " + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;' title='Rejected Date: " + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date: " + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Received to Agency</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;' >Pending</span>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px; coloe:Dark-Red;' title='Rejected Date: " + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span>";
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
}