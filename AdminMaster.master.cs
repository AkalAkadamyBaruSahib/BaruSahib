﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class AdminMaster : System.Web.UI.MasterPage
{
    private static int UserType = -1;
    private int AdminType = -1;

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
                lblUserName.Text = Session["EmailId"].ToString();
                UserType = Convert.ToInt16(Session["UserTypeID"].ToString());
                AdminType = Convert.ToInt16(Session["AdminType"].ToString());
            }

            //DataSet dsAdminCount = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminCount '" + lblUser.Text + "'");
            //lblMsg.Text = dsAdminCount.Tables[11].Rows[0]["Msgco"].ToString();
            SetControls();
         //   SendAutoGeneratedComplaintsReport();
        }
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
    private void SetControls()
    {
        if (AdminType == (int)TypeEnum.SubAdminName.Electrical)
        {
            liAcademy.Visible = false;
            liComplaints.Visible = false;
            liEmployee.Visible = false;
            liDrawingUploadDrawing.Visible = false;
            liFAQs.Visible = false;
            liFeedback.Visible = false;
            liFinancial.Visible = false;
            liGallery.Visible = false;
            liGeography.Visible = false;
            liZone.Visible = false;
            liPurchaseSource.Visible = false;
            liDrawing.Visible = false;
            liMohaliReort.Visible = false;
            liMaterialDisatch.Visible = false;
            liBill.Visible = false;
            liBillStatus.Visible = false;
            liBillDetail.Visible = false;
            liMaterialDisatchLocal.Visible = false;
            liBilldata.Visible = false;
        }
        else if (AdminType == (int)TypeEnum.SubAdminName.Barusahib)
        {
            liAcademy.Visible = false;
            liComplaints.Visible = false;
            liEmployee.Visible = false;
            liDrawingUploadDrawing.Visible = false;
            liFAQs.Visible = false;
            liFeedback.Visible = false;
            liFinancial.Visible = false;
            liGallery.Visible = false;
            liGeography.Visible = false;
            liZone.Visible = false;
            liPurchaseSource.Visible = false;
            liDrawing.Visible = false;
            liMohaliReort.Visible = false;
            liMaterialDisatch.Visible = true;
            liBill.Visible = false;
            liBillStatus.Visible = false;
            liBillDetail.Visible = false;
            liMaterialDisatchLocal.Visible = false;

        }
    }


    public void SendAutoGeneratedComplaintsReport()
    {
        string msg = "Attached is the  more than 2 days Compliants Ticket Report are not InProgress.";
        string FileName = string.Empty;
        DataTable PendingCompliants = new DataTable();

        DateTime date = DateTime.Now.AddDays(-2);
        PendingCompliants = DAL.DalAccessUtility.GetDataInDataSet("Select * from ComplaintTickets Where CreatedOn < '" + date + "' and Status='Assigned'").Tables[0];
        if (PendingCompliants != null)
        {
            FileName = "PendingCompliants" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls";

            string FilePath = Server.MapPath("Bills") + "\\" + FileName;
            PendingCompliants.TableName = FileName;
            PendingCompliants.WriteXml(@FilePath);

            string to = "akalconstruction@barusahib.org";
            string cc = string.Empty;
            Utility.SendEmailUsingAttachments(@FilePath, to, cc, msg, "Pending Compliants");
        }
    }
}