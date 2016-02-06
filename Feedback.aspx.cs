using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Feedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divIsEmp.Visible = false;
            BindFeedBack();
            BindDepartment();
        }
    }

    protected void ddlIsEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIsEmp.SelectedValue == "1")
        {
            divIsEmp.Visible = true;
            divIsNotEmp.Visible=false;
        }
        else
        {
            divIsEmp.Visible = false;
            divIsNotEmp.Visible = true;
        }
    }
    protected void BindFeedBack()
    {
        DataSet dsFb = new DataSet();
        dsFb = DAL.DalAccessUtility.GetDataInDataSet("select FId,FType from FeedbackType where Active=1");
        ddlFbType.DataSource = dsFb;
        ddlFbType.DataValueField = "FId";
        ddlFbType.DataTextField = "FType";
        ddlFbType.DataBind();
        ddlFbType.Items.Insert(0, "Select Feedback Type");
        ddlFbType.SelectedIndex = 0;
    }
    protected void BindDepartment()
    {
        DataSet dsDept = new DataSet();
        dsDept = DAL.DalAccessUtility.GetDataInDataSet("select DepId,department from Department where Active=1");
        ddlDep.DataSource = dsDept;
        ddlDep.DataValueField = "DepId";
        ddlDep.DataTextField = "department";
        ddlDep.DataBind();
        ddlDep.Items.Insert(0, "Select Deparment");
        ddlDep.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlFbType.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Feedback Type.');", true);
        }
        else if (ddlIsEmp.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Employement.');", true);
        }
        else
        {
            if (ddlIsEmp.SelectedValue == "2")
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_FeedBack '" + ddlFbType.SelectedValue + "','" + txtName.Text + "','" + txtEmail.Text + "','" + txtAns.Text + "','" + ddlIsEmp.SelectedValue + "','','0','"+ txtMob.Text +"'");
            }
            else
            {
                DataSet dsDetails = DAL.DalAccessUtility.GetDataInDataSet("select InName,InMobile from Incharge where LoginId='" + txtUserId.Text + "'");
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_FeedBack '" + ddlFbType.SelectedValue + "','" + dsDetails.Tables[0].Rows[0]["InName"].ToString() + "','','" + txtAns.Text + "','" + ddlIsEmp.SelectedValue + "','" + txtUserId.Text + "','" + ddlDep.SelectedValue + "','" + dsDetails.Tables[0].Rows[0]["InMobile"].ToString() + "'");
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Feedback send Successfully, We'll Contact you shotyly....');", true);
        }
        Clr();
    }
    protected void Clr()
    {
        ddlDep.SelectedIndex = 0;
        ddlFbType.SelectedIndex = 0;
        ddlIsEmp.SelectedIndex = 0;
        txtAns.Text = "";
        txtEmail.Text = "";
        txtMob.Text = "";
        txtName.Text = "";
        txtUserId.Text = "";
    }
}