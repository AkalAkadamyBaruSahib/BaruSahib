﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Drawing;
using ClosedXML.Excel;

public partial class Purchase_Home : System.Web.UI.Page
{
    public static int UserTypeID = -1;
    public static int InchargeID = -1;
    public int incId;
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
                hbnInchargeID.Value = Session["InchargeID"].ToString();
                InchargeID = Convert.ToInt32(Session["InchargeID"].ToString());
                UserTypeID = int.Parse(Session["UserTypeID"].ToString());
            }

            if (UserTypeID == (int)TypeEnum.UserType.PURCHASEEMPLOYEE)
            {
                getPendingItem();
                getRecentRateApproved();
            }
            else
            {
                getPendingItem();
                getRecentRateApproved();
                getPurchaserPendingItems();
                spnPurchaserPendingItem.Visible = true;
            }
            if (Request.QueryString["incId"] != null)
            {
                incId = Convert.ToInt32(Request.QueryString["incId"].ToString());
                downloadPurchaserPendencyReport(incId);
            }
            //  AutoGeneratedPendencyReport();
        }
    }
    public void SendAutoGeneratedPendencyReport()
    {
        string msg = "Attached is the auto generated report of more than 15 days pending estimates.";
        string FileName = string.Empty;
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        int UserID = Convert.ToInt32(Session["InchargeID"].ToString());
        DataTable PendingEst = new DataTable();
        DataSet ds = new DataSet();
        PendingEst = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_PendingReportForPurchaser]" + (int)TypeEnum.PurchaseSourceID.Mohali).Tables[0];
        if (PendingEst != null)
        {
            FileName = "PendingReport" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls";

            string FilePath = Server.MapPath("EstFile") + "\\" + FileName;
            PendingEst.TableName = FileName;
            PendingEst.WriteXml(@FilePath);

            string to = "akalconstruction@barusahib.org,dsingh@barusahib.org,csmavi@gmail.com";
            string cc = "dshah@barusahib.org";
            Utility.SendEmailUsingAttachments(@FilePath, to, cc, msg, "Pending Estimates");
        }
    }

    public void AutoGeneratedPendencyReport()
    {
        AutogeneratedEmail emaildays = new AutogeneratedEmail();
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        emaildays = repo.GetReportDay((int)TypeEnum.AutoGenerateReportType.PendingReport);
        string[] days = emaildays.DayOfMonth.Split(',');

        if ((DateTime.Now.Day == int.Parse(days[0]) || DateTime.Now.Day == int.Parse(days[1])) && emaildays.EmailSent == false)
        {
            emaildays.EmailSent = true;
            SendAutoGeneratedPendencyReport();
            repo.UpdateAutogeneratedEmail(emaildays);
        }
        else if ((DateTime.Now.Day == int.Parse(days[0]) || DateTime.Now.Day == int.Parse(days[1])) && emaildays.EmailSent == true)
        {
            emaildays.EmailSent = true;
            repo.UpdateAutogeneratedEmail(emaildays);
        }
        else
        {
            emaildays.EmailSent = false;
            repo.UpdateAutogeneratedEmail(emaildays);
        }
    }

    private void getPendingItem()
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());

        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        List<Estimate> EstimateView = new List<Estimate>();

        if (UserTypeID == (int)(TypeEnum.UserType.PURCHASE) || UserTypeID == (int)(TypeEnum.UserType.PURCHASECOMMITTEE))
        {
            EstimateView = repo.EstimateViewForPurchase((int)TypeEnum.PurchaseSourceID.Mohali, UserTypeID, Convert.ToInt32(hbnInchargeID.Value));
            spnPendingItem.Visible = false;
            spnNotAssigned.Visible = true;
          
        }
        else if (UserTypeID == (int)(TypeEnum.UserType.PURCHASEEMPLOYEE))
        {
            EstimateView = repo.EstimateViewForPurchaseByEmployeeID((int)TypeEnum.PurchaseSourceID.Mohali, UserTypeID, Convert.ToInt32(hbnInchargeID.Value), 0);
            spnPendingItem.Visible = true;
            spnNotAssigned.Visible = false;
        }
      
        divPendingItem.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;

        ZoneInfo += "<table class='table table-striped table-bordered'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='40%' style='color: #cc3300;'>EstID</th>";
        ZoneInfo += "<th width='40%' style='color: #cc3300;'>Pending Item</th>";
        ZoneInfo += "<th width='20%' style='color: #cc3300;'>View</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";

        ZoneInfo += "<tbody>";
        string roomnumbers = string.Empty;

        foreach (Estimate estimate in EstimateView)
        {
            roomnumbers = string.Empty;
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='40%'>" + estimate.EstId + "</td>";
            ZoneInfo += "<td width='40%'>" + repo.GetPendingEstimateCount((int)TypeEnum.PurchaseSourceID.Mohali, Convert.ToInt32(hbnInchargeID.Value), estimate.EstId, UserTypeID) + "</td>";
            if (UserTypeID == (int)(TypeEnum.UserType.PURCHASE) || UserTypeID == (int)(TypeEnum.UserType.PURCHASECOMMITTEE))
            {
                ZoneInfo += "<td  width='20%' class='center'><a href='Purchase_ViewEstMaterial.aspx?EstId=" + estimate.EstId + "'>View</a></td>";
            }
            else
            {
                ZoneInfo += "<td  width='20%' class='center'><a href='PurchaseEmployee_ViewEstMaterial.aspx?IsLocal=2&EstId=" + estimate.EstId + "'>View</a></td>";
            }
            ZoneInfo += "</tr>";
        }

        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divPendingItem.InnerHtml = ZoneInfo.ToString();
    }

    private void getRecentRateApproved()
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        DataTable dsMatUnApprovdRate = new DataTable();
        DataTable dsMatRate = new DataTable();


        DateTime dt1 = DateTime.Now.AddDays(-7);
        if (UserTypeID == (int)TypeEnum.UserType.PURCHASECOMMITTEE)
        {
            spnNewRate.Visible = true;
            if (InchargeID == (int)TypeEnum.PurchaseCommittee.FirstApproval)
            {
                dsMatUnApprovdRate = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatName,MR.NetRate,Inc.InName from Material M INNER JOIN MaterialNonApprovedRate MR on M.MatId = MR.MatID INNER JOIN MaterialRateApproved MRA on MRA.MatId = MR.MatID  INNER JOIN Incharge Inc ON Inc.InchargeId = MR.CreatedBy Where MRA.FirstApproval is null and MRA.SecondApproval is null").Tables[0];
            }
            else
            {
                dsMatUnApprovdRate = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatName,MR.NetRate,Inc.InName from Material M INNER JOIN MaterialNonApprovedRate MR on M.MatId = MR.MatID INNER JOIN MaterialRateApproved MRA on MRA.MatId = MR.MatID INNER JOIN Incharge Inc ON Inc.InchargeId = MR.CreatedBy Where MRA.FirstApproval ='" + (int)TypeEnum.PurchaseCommittee.FirstApproval + "' and MRA.SecondApproval is null").Tables[0];
            }
        }
        else
        {
            spnRateAproved.Visible = true;
            dsMatRate = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatName,MR.ApprovedOn,MR.ApprovedRate from Material M INNER JOIN MaterialRateApproved MR on M.MatId = MR.MatID where  MR.ApprovedOn >= '" + dt1 + "' and MR.FirstApproval = '" + (int)TypeEnum.PurchaseCommittee.FirstApproval + "' and MR.SecondApproval = '" + (int)TypeEnum.PurchaseCommittee.SecondApproval + "'").Tables[0];
        }
        divRecentRateApproved.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;

        ZoneInfo += "<table class='table table-striped table-bordered'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='40%' style='color: #cc3300;'>Item Name</th>";
        if (UserTypeID == (int)TypeEnum.UserType.PURCHASECOMMITTEE)
        {
            ZoneInfo += "<th width='20%' style='color: #cc3300;'>Rate</th>";
            ZoneInfo += "<th width='20%' style='color: #cc3300;'>Requested By</th>";
            ZoneInfo += "<th width='20%' style='color: #cc3300;'>View</th>";
        }
        else
        {
            ZoneInfo += "<th width='30%' style='color: #cc3300;'>Approved Rate</th>";
            ZoneInfo += "<th width='30%' style='color: #cc3300;'>Approved On</th>";
        }
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";

        ZoneInfo += "<tbody>";
        if (UserTypeID == (int)TypeEnum.UserType.PURCHASECOMMITTEE)
        {
            for (int i = 0; i < dsMatUnApprovdRate.Rows.Count; i++)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td width='40%'>" + dsMatUnApprovdRate.Rows[i]["MatName"].ToString() + "</td>";
                ZoneInfo += "<td width='20%'>" + dsMatUnApprovdRate.Rows[i]["NetRate"].ToString() + "</td>";
                ZoneInfo += "<td width='20%'>" + dsMatUnApprovdRate.Rows[i]["InName"].ToString() + "</td>";
                ZoneInfo += "<td width='20%'><a href='RateApproved.aspx'>View</a></td>";
                ZoneInfo += "</tr>";
            }
        }
        else
        {
            for (int i = 0; i < dsMatRate.Rows.Count; i++)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td width='40%'>" + dsMatRate.Rows[i]["MatName"].ToString() + "</td>";
                ZoneInfo += "<td width='30%'>" + dsMatRate.Rows[i]["ApprovedRate"].ToString() + "</td>";
                ZoneInfo += "<td width='30%'>" + dsMatRate.Rows[i]["ApprovedOn"].ToString() + "</td>";
                ZoneInfo += "</tr>";
            }
        }

        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divRecentRateApproved.InnerHtml = ZoneInfo.ToString();
    }

    private void getPurchaserPendingItems()
    {
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());

        DataTable dsPurchasePendingItem = DAL.DalAccessUtility.GetDataInDataSet("Select distinct Inc.InName,Inc.InchargeId from EstimateAndMaterialOthersRelations ER INNER JOIN Incharge Inc ON Inc.InchargeId = ER.PurchaseEmpID where  Inc.Active=1 and Inc.UserTypeId ='" + (int)TypeEnum.UserType.PURCHASEEMPLOYEE + "'").Tables[0];
 
      
        divPurchaserPendingItems.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;

        ZoneInfo += "<table class='table table-striped table-bordered'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='50%' style='color: #cc3300;'>Purchaser Name</th>";
        ZoneInfo += "<th width='50%' style='color: #cc3300;'>Pending Items</th>";
        ZoneInfo += "<th width='50%' style='color: #cc3300;'>Action</th>";
       ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";

        ZoneInfo += "<tbody>";

        for (int i = 0; i < dsPurchasePendingItem.Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='50%'>" + dsPurchasePendingItem.Rows[i]["InName"].ToString() + "</td>";
            ZoneInfo += "<td width='50%'>" + repo.GetPurchaserPendingItemsCount((int)TypeEnum.PurchaseSourceID.Mohali, Convert.ToInt32(dsPurchasePendingItem.Rows[i]["InchargeId"].ToString())) + "</td>";
            ZoneInfo += "<td width='20%'><a href='PurchaserPendencyView.aspx?inchargeId=" + dsPurchasePendingItem.Rows[i]["InchargeId"].ToString() + "'>View</a>/<a href='Purchase_Home.aspx?incId=" + dsPurchasePendingItem.Rows[i]["InchargeId"].ToString() + "'>Download</a></td>";
            ZoneInfo += "</tr>";
        }

        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divPurchaserPendingItems.InnerHtml = ZoneInfo.ToString();
    }
    public void downloadPurchaserPendencyReport(int incId)
    {
        DataTable PendingEst = new DataTable();
        PendingEst = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_PurchaserPendencyView]'" + (int)TypeEnum.PurchaseSourceID.Mohali + "','" + incId + "'").Tables[0];
        string FileName = "PurchaserPendencyReport" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xlsx";

        string FilePath = Server.MapPath("EstFile") + "\\" + FileName;
        try
        {
            XLWorkbook workbook = new XLWorkbook();
            DataTable table = PendingEst;
            workbook.Worksheets.Add(table);
            workbook.SaveAs(FilePath);

            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FileName));
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(@FilePath);
            Response.End();
        }
        catch (Exception ex)
        { }
        finally
        {

        }
    }
}