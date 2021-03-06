﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Workshop_BillDetails : System.Web.UI.Page
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
            if (Request.QueryString["BillId"] != null)
            {
                ShowBillDetails(Request.QueryString["BillId"].ToString());
            }
        }
    }
    protected void ShowBillDetails(string ID)
    {
        DataSet dsBill = new DataSet();
        dsBill = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillViewByBillId_V2 '" + ID + "'");
        lblBillNo.Text = dsBill.Tables[0].Rows[0]["SubBillId"].ToString();
        //lblBillType.Text = dsBill.Tables[0].Rows[0]["BillTypeName"].ToString();
        lblChargeableTo.Text = dsBill.Tables[0].Rows[0]["BillType"].ToString();
        lblBillDesc.Text = dsBill.Tables[0].Rows[0]["BillDescr"].ToString();
        lblAgencyName.Text = dsBill.Tables[0].Rows[0]["AgencyName"].ToString();
        lblBillDate.Text = dsBill.Tables[0].Rows[0]["BillDate"].ToString();
        lblGateEntry.Text = dsBill.Tables[0].Rows[0]["GateEntryNo"].ToString();
        lblZone.Text = dsBill.Tables[0].Rows[0]["ZoneName"].ToString();
        lblAca.Text = dsBill.Tables[0].Rows[0]["AcaName"].ToString();

        divBillMaterialDetails.InnerHtml = string.Empty;
        string BillInfo = string.Empty;
        BillInfo += "<div class='box span12'>";
        BillInfo += "<div class='box-header well' data-original-title>";
        BillInfo += "<h2><i class='icon-user'></i> Bills Material Detail</h2>";
        BillInfo += "</div>";
        BillInfo += "<div class='box-content'>";
        BillInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
        BillInfo += "<tr>";
        BillInfo += "<th width='10%'>Stock No</th>";
        BillInfo += "<th width='10%'>Material</th>";
        BillInfo += "<th width='10%'>Quantity</th>";
        BillInfo += "<th width='10%'>Unit</th>";
        BillInfo += "<th width='10%'>Rate</th>";
        BillInfo += "<th width='10%'>Amount</th>";
        BillInfo += "<th width='40%'>Remarks</th>";
        BillInfo += "</tr>";
        for (int i = 0; i < dsBill.Tables[2].Rows.Count; i++)
        {
            BillInfo += "<tr>";
            if (dsBill.Tables[2].Rows[i]["StockEntryNo"].ToString() == "" || dsBill.Tables[2].Rows[i]["StockEntryNo"].ToString() == null)
            {
                BillInfo += "<td width='15%'><span class='label label-success'>No Data</span></td>";
            }
            else
            {
                BillInfo += "<td width='15%'>" + dsBill.Tables[2].Rows[i]["StockEntryNo"].ToString() + "</td>";
            }
            BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["MatName"].ToString() + "</td>";
            BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["Qty"].ToString() + "</td>";
            BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["UnitName"].ToString() + "</td>";
            BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["Rate"].ToString() + "</td>";
            BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["Amount"].ToString() + "</td>";
            if (dsBill.Tables[2].Rows[i]["Remark"].ToString() == "" || dsBill.Tables[2].Rows[i]["Remark"].ToString() == null)
            {
                BillInfo += "<td width='15%'><span class='label label-success'>No Data</span></td>";
            }
            else
            {
                BillInfo += "<td width='15%'>" + dsBill.Tables[2].Rows[i]["Remark"].ToString() + "</td>";
            }
            BillInfo += "</tr>";

        }
        if (dsBill.Tables[0].Rows[0]["EstId"].ToString() == "0")
        {
            BillInfo += "<tr>";
            BillInfo += "<td colspan='7'></td>";
            BillInfo += "</tr>";
        }
        else
        {
            BillInfo += "<tr>";
            BillInfo += "<td colspan='7'><a href='Workshop_ParticularEstView.aspx?EstId=" + dsBill.Tables[0].Rows[0]["EstId"].ToString() + "'>View Estimate Details</a> </td>";
            BillInfo += "</tr>";
        }

        BillInfo += "</table>";
        BillInfo += "</div>";

        divBillMaterialDetails.InnerHtml = BillInfo.ToString();
    }
}