using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_UploadEstimate : System.Web.UI.UserControl
{
    public static int UserTypeID = -1;

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

            if (Session["UserTypeID"] != null)
            {
                UserTypeID = int.Parse(Session["UserTypeID"].ToString());
            }
            BindZone();
            BindTypeOfWork();
        }
    }

    public void BindZone()
    {
        DataSet dsZone = new DataSet();
        if (UserTypeID == 1)
        {
            dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName  from Zone where Active=1");
        }
        else
        {
            dsZone = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_GetZoneByUserID] '" + Session["InchargeID"] + "'");
        }
        ddlZone.DataSource = dsZone;
        ddlZone.DataValueField = "ZoneId";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }

    public void BindAcademy()
    {
        DataSet dsAca = new DataSet();
        dsAca = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where Active=1 and ZoneId='" + ddlZone.SelectedValue + "'");
        ddlAcademy.DataSource = dsAca;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, "Select Academy");
        ddlAcademy.SelectedIndex = 0;
    }

    public void BindWork()
    {

        DataSet dsWork = new DataSet();
        dsWork = DAL.DalAccessUtility.GetDataInDataSet("select WAId,WorkAllotName from WorkAllot where AcaId='" + ddlAcademy.SelectedValue + "' and ZoneId='" + ddlZone.SelectedValue + "' and active=1");
        ddlWorkAllot.DataSource = dsWork;
        ddlWorkAllot.DataValueField = "WAId";
        ddlWorkAllot.DataTextField = "WorkAllotName";
        ddlWorkAllot.DataBind();
        ddlWorkAllot.Items.Insert(0, "Select Work Allot");
        ddlWorkAllot.SelectedIndex = 0;
    }

    public void BindTypeOfWork()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select TypeWorkId,TypeWorkName from TypeOfWork where Active=1");
        ddlTypeOfWork.DataSource = dsZone;
        ddlTypeOfWork.DataValueField = "TypeWorkId";
        ddlTypeOfWork.DataTextField = "TypeWorkName";
        ddlTypeOfWork.DataBind();
        ddlTypeOfWork.Items.Insert(0, "Select Type Of Work");
        ddlTypeOfWork.SelectedIndex = 0;
    }

    public void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
        DataSet dsZCode = new DataSet();
        dsZCode = DAL.DalAccessUtility.GetDataInDataSet("select ZoId from Zone where ZoneId='" + ddlZone.SelectedValue + "'");
        lblZoneCode.Text = dsZCode.Tables[0].Rows[0]["ZoId"].ToString();
    }

    public void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        tdWorkAllot.Visible = true;
        BindWork();
        DataSet dsACode = new DataSet();
        dsACode = DAL.DalAccessUtility.GetDataInDataSet("select AcId from Academy where Active=1 and ZoneId='" + ddlZone.SelectedValue + "'");
        lblAcaCode.Text = dsACode.Tables[0].Rows[0]["AcId"].ToString();
    }
    
    public void ddlWorkAllot_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblWorkNameReflect.Visible = true;
        lblWorkNameReflect.Text = ddlWorkAllot.SelectedItem.Text;
    }

    private void SetDefaultMaterialTypes()
    {
      
        
    }
}