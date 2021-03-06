﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class PurchaseMaster : System.Web.UI.MasterPage
{
    public int UserTypeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
            lblUserName.Text = Session["Inname"].ToString();
            UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        }
        if (Session["UserTypeID"].ToString() == ((int)TypeEnum.UserType.PURCHASE).ToString())
        {
            liReport.Visible = true;
            liPurchaseOrder.Visible = true;
            liMaterialReport.Visible = false;
            liHome.Visible = true;
        }
        else if (Session["UserTypeID"].ToString() == ((int)TypeEnum.UserType.PURCHASECOMMITTEE).ToString())
        {
            liRateApproved.Visible = true;
            liPurchaseOrder.Visible = true;
            liMaterialReport.Visible = true;
            liHome.Visible = true;
        }
        else
        {
            liHome.Visible = true;
            liStatusReport.Visible = false;
            liMaterialReport.Visible = false;
          
        }

      //  SendAutoGeneratedComplaintsReport();
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx", true);
    }

    public void SendAutoGeneratedComplaintsReport()
    {
        string msg = "Attached are the Pending Compliants Tickets on which no action has been taken.";
        string FileName = string.Empty;
        DataTable PendingEstimates = new DataTable();

        DateTime date = DateTime.Now.AddDays(-10);

        PendingEstimates = DAL.DalAccessUtility.GetDataInDataSet("Select distinct E.EstId,M.MatName from Estimate E INNER JOIN EstimateAndMaterialOthersRelations ER ON E.EstId = ER.EstId INNER JOIN Material M ON M.MatId = ER.MatId WHERE E.IsApproved=1 and ER.PSId='" + (int)TypeEnum.PurchaseSourceID.Mohali + "'    and ER.PurchaseEmpID=0 and E.CreatedOn < '" + date + "'").Tables[0];

        string MsgInfo = string.Empty;
        MsgInfo += "<table style='width:100%;'>";
        MsgInfo += "<tr>";
        MsgInfo += "<td style='padding:0px; text-align:left; width:50%' valign='top'>";
        MsgInfo += "<img src='http://akalsewa.org/img/logoakalnew.png' style='width:100%;' />";
        MsgInfo += "</td>";
        MsgInfo += "<td style='text-align: right; width:40%;'>";
        MsgInfo += "<br /><br />";
        MsgInfo += "<div style='font-style:italic; text-align: right;'>";
        MsgInfo += "Baru Shahib,";
        MsgInfo += "<br />Dist: Sirmaur";
        MsgInfo += "<br />Himachal Pradesh-173001";
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "<tr>";
        MsgInfo += "<td colspan='2' style='height:50px'>";
        MsgInfo += "Pending Estimates Which are not assign.Please click on <a href='http://akalsewa.org/'>Akal Sewa</a>";
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "</table>";

        MsgInfo += "<table border='1' style='width:100%'>";
        MsgInfo += "<thead>";
        MsgInfo += "<tr>";
        MsgInfo += "<th>Estimate Number</th>";
        MsgInfo += "<th>Material Name</th>";
        MsgInfo += "</tr>";
        MsgInfo += "</thead>";
        MsgInfo += "<tbody>";
        for (int i = 0; i < PendingEstimates.Rows.Count; i++)
        {
            MsgInfo += "<tr>";
            MsgInfo += "<td>";
            MsgInfo += PendingEstimates.Rows[i]["EstId"].ToString();
            MsgInfo += "</td>";
            MsgInfo += "<td>";
            MsgInfo += PendingEstimates.Rows[i]["MatName"].ToString();
            MsgInfo += "</td>";
            MsgInfo += "</tr>";
        }
        MsgInfo += "</tbody>";
        MsgInfo += "</table>";
        string to = string.Empty;
        string cc = "itmohali@barusahib.org" ;
        try
        {
          //  Utility.SendEmailWithoutAttachments(to, cc, MsgInfo, "All Pending Estimates.");
        }
        catch { }
        finally
        {

        }
    }

    public void AutoGeneratedCompliantsReport()
    {
        string FileName = string.Empty;

        FileName = "PendingEstimates" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls";
        string FilePath = Server.MapPath("Bills") + "\\" + FileName;
        if (!System.IO.File.Exists(FilePath))
        {
            SendAutoGeneratedComplaintsReport();
        }
    }
}
