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

            DataSet dsAdminCount = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminCount '" + lblUser.Text + "'");
            lblZoneCo.Text = dsAdminCount.Tables[1].Rows[0]["Zoco"].ToString();
            lblAcaCo.Text = dsAdminCount.Tables[2].Rows[0]["Acaco"].ToString();
            lblUnitCo.Text = dsAdminCount.Tables[6].Rows[0]["Unitco"].ToString();
            lblMatTCo.Text = dsAdminCount.Tables[7].Rows[0]["MatTco"].ToString();
            lblBillCount.Text = dsAdminCount.Tables[10].Rows[0]["StatusCount"].ToString();
            lblMsg.Text = dsAdminCount.Tables[11].Rows[0]["Msgco"].ToString();
            lblDrwCo.Text = dsAdminCount.Tables[12].Rows[0]["Dwgco"].ToString();
            lblFtCount.Text = dsAdminCount.Tables[13].Rows[0]["FBcount"].ToString();
            lblFBCount.Text = dsAdminCount.Tables[14].Rows[0]["FB"].ToString();
            SetControls();
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
}
