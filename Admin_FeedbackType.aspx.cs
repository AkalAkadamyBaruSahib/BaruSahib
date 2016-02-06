using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_FeedbackType : System.Web.UI.Page
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

            BindFbTypeDetails();
            if (Request.QueryString["FId"] != null)
            {
                getFbTypeDetails(Request.QueryString["FId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
            }
            if (Request.QueryString["FIdIA"] != null)
            {
                DeactiveFbType(Request.QueryString["FIdIA"].ToString());
            }
            if (Request.QueryString["FIdA"] != null)
            {
                ActiveFbType(Request.QueryString["FIdA"].ToString());
            }
        }
    }
    protected void DeactiveFbType(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewFeedbackType '','','4','"+ ID +"','0'");
        BindFbTypeDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Feedback Type Deactive Successfully.');", true);

    }
    protected void ActiveFbType(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewFeedbackType '','','4','" + ID + "','1'");
        BindFbTypeDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Feedback Type Active Successfully.');", true);

    }
    private void getFbTypeDetails(string ID)
    {
        DataSet dsMatTyDetails = new DataSet();
        dsMatTyDetails = DAL.DalAccessUtility.GetDataInDataSet("select FType from FeedbackType where FId= '" + ID + "'");
        if (dsMatTyDetails.Tables[0].Rows.Count > 0)
        {
            txtFbType.Text = dsMatTyDetails.Tables[0].Rows[0]["FType"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active Feedback Type Status before edit..');", true);
        }
        BindFbTypeDetails();
    }
    protected void BindFbTypeDetails()
    {
        DataSet dsMatTypeDetails = new DataSet();
        dsMatTypeDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowFeedbackTypeDetails '" + lblUser.Text + "'");
        divMatTypeDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='60%'>Feedback Type</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsMatTypeDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='60%'>" + dsMatTypeDetails.Tables[0].Rows[i]["FType"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsMatTypeDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_FeedbackType.aspx?FIdA=" + dsMatTypeDetails.Tables[0].Rows[i]["FId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-info' href='Admin_FeedbackType.aspx?FId=" + dsMatTypeDetails.Tables[0].Rows[i]["FId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_FeedbackType.aspx?FIdIA=" + dsMatTypeDetails.Tables[0].Rows[i]["FId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divMatTypeDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct FType from FeedbackType where FType='" + txtFbType.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Feedback Type Already Exist.');", true);
        }
        else
        {
            if (txtFbType.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Feedback Type.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewFeedbackType '" + txtFbType.Text + "','" + lblUser.Text + "','1','','1'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Feedback Type Create Successfully.');", true);
                BindFbTypeDetails();
                txtFbType.Text = "";
            }
        }
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtFbType.Text = "";
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct FType from FeedbackType where FType='" + txtFbType.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Feedback Type Already Exist.');", true);
        }
        else
        {
            if (txtFbType.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Feedback Type.');", true);
            }
            else
            {
                string fId = Request.QueryString["FId"];
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewFeedbackType '" + txtFbType.Text + "','" + lblUser.Text + "','2','"+ fId +"','1'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Feedback Type Edit Successfully.');", true); 
                BindFbTypeDetails();
                txtFbType.Text = "";
                btnEdit.Visible = false;
                btnSave.Visible = true;
            }
        }
    }
}