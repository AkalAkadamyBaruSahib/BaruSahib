﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_BillDetailsAfterApproval : System.Web.UI.Page
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
            if (Request.QueryString["SubBillId"] != null)
            {
                ShowBillDetails(Request.QueryString["SubBillId"].ToString());
            }
        }
    }
    protected void ShowBillDetails(string ID)
    {
        DataSet dsBill = new DataSet();
        dsBill = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillViewByBillId_V2 '" + ID + "'");
        lblBillNo.Text = dsBill.Tables[0].Rows[0]["SubBillId"].ToString();
        //lblBillType.Text = dsBill.Tables[0].Rows[0]["BillTypeName"].ToString();
        if (dsBill.Tables[0].Rows[0]["BillType"].ToString() == ((int)TypeEnum.BillType.Sanctioned).ToString())
        { lblChargeableTo.Text = "Sanctioned"; }
        else
        { lblChargeableTo.Text = "Non Sanctioned"; }
        
        lblAgencyName.Text = dsBill.Tables[0].Rows[0]["AgencyName"].ToString();
        lblBillDate.Text = dsBill.Tables[0].Rows[0]["BillDate"].ToString();
        lblGateEntry.Text = dsBill.Tables[0].Rows[0]["GateEntryNo"].ToString();
        lblZone.Text = dsBill.Tables[0].Rows[0]["ZoneName"].ToString();
        lblAca.Text = dsBill.Tables[0].Rows[0]["AcaName"].ToString();
        aAgencyBill.Text = GetFileName(dsBill.Tables[0].Rows[0]["AgencyBill"].ToString(), dsBill.Tables[0].Rows[0]["AgencyName"].ToString());
        lblAgencyBillNo.Text = dsBill.Tables[0].Rows[0]["AgencyBillNumber"].ToString();
        if (dsBill.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "")
        {
            lblHqUser.Text = "Not Varified";
            lblHqAppDate.Text = "Not Varified";
            lblHqRemark.Text = "Not Varified";
        }
        else if (dsBill.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "0")
        {
            lblHqUser.Text = "Bill Rejected";
            lblHqAppDate.Text = "Bill Rejected";
            lblHqRemark.Text = dsBill.Tables[0].Rows[0]["FirstVarifyRemark"].ToString();
        }
        else
        {
            DataTable ds1stVerfiName = new DataTable();
            ds1stVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["FirstVarify"].ToString() + "'").Tables[0];
            if (ds1stVerfiName != null && ds1stVerfiName.Rows.Count > 0)
            {
                lblHqUser.Text = ds1stVerfiName.Rows[0]["InName"].ToString();
            }
            lblHqAppDate.Text = dsBill.Tables[0].Rows[0]["FirstVarifyOn"].ToString();
            lblHqRemark.Text = dsBill.Tables[0].Rows[0]["FirstVarifyRemark"].ToString();
        }
        if (dsBill.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "")
        {
            lbl2ndUser.Text = "Not Varified";
            lbl2ndAppOn.Text = "Not Varified";
            lbl2ndRemark.Text = "Not Varified";
        }
        else if (dsBill.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "0")
        {
            lbl2ndUser.Text = "Bill Rejected";
            lbl2ndAppOn.Text = "Bill Rejected";
            lbl2ndRemark.Text = dsBill.Tables[0].Rows[0]["SecondVarifyRemark"].ToString();
        }
        else
        {
            DataSet ds2ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["SeccondVarify"].ToString() + "'");
            lbl2ndUser.Text = ds2ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
            lbl2ndAppOn.Text = dsBill.Tables[0].Rows[0]["SeccondVarifyOn"].ToString();
            lbl2ndRemark.Text = dsBill.Tables[0].Rows[0]["SecondVarifyRemark"].ToString();
        }
        if (dsBill.Tables[0].Rows[0]["PaymentStatus"].ToString() == "")
        {
            lbl3rdUser.Text = "Not Varified";
            lbl3rdAppOn.Text = "Not Varified";
            lblAccRemark.Text = "Not Varified";
            lbl3rdPayMode.Text = "Not Varified";
            lbl3rdPayDetails.Text = "Not Varified";
        }
        else if (dsBill.Tables[0].Rows[0]["PaymentStatus"].ToString() == "0")
        {
            DataSet ds3ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["ThirdVarifyBy"].ToString() + "'");
            lbl3rdUser.Text = ds3ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
            lbl3rdAppOn.Text = dsBill.Tables[0].Rows[0]["ThirdVarifyOn"].ToString();
            if (dsBill.Tables[1].Rows.Count > 0)
            {
                lblAccRemark.Text = dsBill.Tables[1].Rows[0]["Remark"].ToString();
                lbl3rdPayMode.Text = dsBill.Tables[1].Rows[0]["PayModeName"].ToString();
                lbl3rdPayDetails.Text = dsBill.Tables[1].Rows[0]["PayDetails"].ToString();
            }
            else
            {
                lblAccRemark.Text = "REJECTED";
                lbl3rdPayMode.Text = "Not Varified";
                lbl3rdPayDetails.Text = "Not Varified";
            }
        }
        else
        {
            DataSet ds3ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["ThirdVarifyBy"].ToString() + "'");
            lbl3rdUser.Text = ds3ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
            lbl3rdAppOn.Text = dsBill.Tables[0].Rows[0]["ThirdVarifyOn"].ToString();
            lblAccRemark.Text = dsBill.Tables[1].Rows[0]["Remark"].ToString();
            lbl3rdPayMode.Text = dsBill.Tables[1].Rows[0]["PayModeName"].ToString();
            lbl3rdPayDetails.Text = dsBill.Tables[1].Rows[0]["PayDetails"].ToString();
        }

        if (dsBill.Tables[0].Rows[0]["RecevingBy"].ToString() == "")
        {
            lblRecUser.Text = "Not Varified";
            lblRecAppOn.Text = "Not Varified";
            lblRecVocNo.Text = "Not Varified";
            lblRecRemark.Text = "Not Varified";
        }
        else
        {
            DataSet ds4ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["RecevingBy"].ToString() + "'");
            lblRecUser.Text = ds4ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
            lblRecAppOn.Text = dsBill.Tables[0].Rows[0]["DateOfReceving"].ToString();
            lblRecVocNo.Text = dsBill.Tables[0].Rows[0]["ReciptNoByEmp"].ToString();
            lblRecRemark.Text = dsBill.Tables[0].Rows[0]["RecevingRemark"].ToString();
        }
        divBillMaterialDetails.InnerHtml = string.Empty;
        string BillInfo = string.Empty;
        BillInfo += "<div class='box span12'>";
        BillInfo += "<div class='box-header well' data-original-title>";
        BillInfo += "<h2><i class='icon-user'></i> Bills Material Detail</h2>";
        BillInfo += "</div>";
        BillInfo += "<div class='box-content'>";
        BillInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
        BillInfo += "<tr>";
        BillInfo += "<th width='15%'>Stock No</th>";
        BillInfo += "<th width='10%'>Material</th>";
        BillInfo += "<th width='10%'>Quantity</th>";
        BillInfo += "<th width='10%'>Unit</th>";
        BillInfo += "<th width='10%'>Rate</th>";
        BillInfo += "<th width='10%'>Amount</th>";
        BillInfo += "<th width='35%'>Remarks</th>";
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
            // <span class='label label-success'>No Data</span>
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
            BillInfo += "<td colspan='7'><a href='Admin_ParticularEstimateView.aspx?EstId=" + dsBill.Tables[0].Rows[0]["EstId"].ToString() + "'>Estimate Details</a> </td>";
            BillInfo += "</tr>";
        }

        BillInfo += "</table>";
        BillInfo += "</div>";

        divBillMaterialDetails.InnerHtml = BillInfo.ToString();
    }

    private string GetFileName(string filepaths, string fileName)
    {
        string anchorLink = string.Empty;
        string[] filePath = filepaths.Split(';');
        int count = 0;
        foreach (string path in filePath)
        {
            count++;
            anchorLink += "<a href= Bills/" + path + " target='_blank'>" + fileName + "_" + count + "</a> , ";
        }

        return anchorLink.Substring(0, anchorLink.Length - 3);

    }
}