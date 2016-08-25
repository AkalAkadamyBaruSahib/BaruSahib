﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Admin_UserControls_BodyEmployee : System.Web.UI.UserControl
{
    public static int ModuleID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["ModuleID"] != null)
            {
                ModuleID = int.Parse(Session["ModuleID"].ToString());
            }
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
            }

            BindInchargeDetails();
            BindDesignation();
            BindUserType();
            btnEdit.Visible = false;
            BindDepartment();
            if (Request.QueryString["InchargeId"] != null)
            {
                getInchargeDetails(Request.QueryString["InchargeId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
                ddlDept.Enabled = false;
                ddlDesig.Enabled = false;
                ddlUserType.Enabled = false;
            }

        }
    }
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelEmployee");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Employee.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindDatatable();
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    private void getInchargeDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet(" select InName,InMobile,LoginId,UserPwd,DepId,DesigId,UserTypeId from Incharge where InchargeId='" + ID + "' and Active=1");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtName.Text = dsCouDetails.Tables[0].Rows[0]["InName"].ToString();
            txtMob.Text = dsCouDetails.Tables[0].Rows[0]["InMobile"].ToString();
            txtLoginId.Text = dsCouDetails.Tables[0].Rows[0]["LoginId"].ToString();
            txtUserPwd.Text = dsCouDetails.Tables[0].Rows[0]["UserPwd"].ToString();
            BindDepartment();
            ddlDept.SelectedIndex = ddlDept.Items.IndexOf(ddlDept.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["DepId"].ToString().Trim()));
            BindDesignation();
            ddlDesig.SelectedIndex = ddlDesig.Items.IndexOf(ddlDesig.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["DesigId"].ToString().Trim()));
            BindUserType();
            ddlUserType.SelectedIndex = ddlUserType.Items.IndexOf(ddlUserType.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["UserTypeId"].ToString().Trim()));
        }
    }
    protected void BindInchargeDetails()
    {
        DataSet dsDegisDetails = new DataSet();
        dsDegisDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowInchargeDetails_ByUser '" + lblUser.Text + "'," + ModuleID);
        divDesigDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='30%'>Incharge Details</th>";
        ZoneInfo += "<th width='30%'>Details</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsDegisDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='30%'><table><tr><td><b>Name</b> :" + dsDegisDetails.Tables[0].Rows[i]["InName"].ToString() + "</td>";
            ZoneInfo += "<tr><td><b>Mobile</b> : " + dsDegisDetails.Tables[0].Rows[i]["InMobile"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td width='30%'><table><tr><td><b>Department</b> :" + dsDegisDetails.Tables[0].Rows[i]["department"].ToString() + "</td>";
            ZoneInfo += "<tr><td><b>Designation</b> : " + dsDegisDetails.Tables[0].Rows[i]["Designation"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Login ID</b> :" + dsDegisDetails.Tables[0].Rows[i]["LoginId"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Password</b> : " + dsDegisDetails.Tables[0].Rows[i]["UserPwd"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsDegisDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";

            if (ModuleID == (int)TypeEnum.Module.Purchase)
            {
                ZoneInfo += "<a class='btn btn-info' href='Admin_Incharge.aspx?InchargeId=" + dsDegisDetails.Tables[0].Rows[i]["InchargeId"].ToString() + "'>";
            }
            else if (ModuleID == (int)TypeEnum.Module.Workshop)
            {
                ZoneInfo += "<a class='btn btn-info' href='AkalWorkshop_Incharge.aspx?InchargeId=" + dsDegisDetails.Tables[0].Rows[i]["InchargeId"].ToString() + "'>";
            }
            else
            {
                ZoneInfo += "<a class='btn btn-info' href='Transport_NewEmployee.aspx?InchargeId=" + dsDegisDetails.Tables[0].Rows[i]["InchargeId"].ToString() + "'>";
            }
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>";
            //ZoneInfo += "<a class='btn btn-info' href='Admin_Incharge.aspx?InchargeId=" + dsDegisDetails.Tables[0].Rows[i]["InchargeId"].ToString() + "'>";
            //ZoneInfo += "<i class='icon-edit icon-white'></i> Edit Location";
            //ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divDesigDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsExistMo = new DataSet();
        DataSet dsExitUser = new DataSet();
        dsExistMo = DAL.DalAccessUtility.GetDataInDataSet("select distinct InMobile from Incharge where InMobile='" + txtMob.Text + "'");
        dsExitUser = DAL.DalAccessUtility.GetDataInDataSet("select distinct LoginId from Incharge where LoginId='" + txtLoginId.Text + "'");
        if (dsExistMo.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Mobile Already Exist.');", true);
        }
        else if (dsExitUser.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Login Id Already Exist.');", true);
        }
        else
        {
            if (txtName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Incharge name.');", true);
            }
            if (txtMob.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Incharge Mobile.');", true);
            }
            if (ddlDept.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Incharge Department.');", true);
            }
            if (ddlDesig.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Incharge Designation.');", true);
            }
            if (ddlUserType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select User Type.');", true);
            }
            if (txtLoginId.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Incharge Login Id.');", true);
            }
            if (txtUserPwd.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Incharge Password.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewInchargeProc '','" + txtName.Text + "','" + txtMob.Text + "','" + ddlDesig.SelectedValue + "','" + ddlDept.SelectedValue + "','" + lblUser.Text + "','1','1','" + txtLoginId.Text + "','" + txtUserPwd.Text + "','" + ddlUserType.SelectedValue + "','" + ModuleID + "'");
                //DAL.DalAccessUtility.ExecuteNonQuery("insert into StockRegister(AcaId,MatId,CreatedBy) values(0,0,'" + ddlEmpl.SelectedItem.Text + "')");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Create Successfully.');", true);
                BindInchargeDetails();
                Clr();
            }
        }
    }
    protected void Clr()
    {
        txtMob.Text = "";
        txtName.Text = "";
        ddlDept.SelectedIndex = 0;
        ddlDesig.SelectedIndex = 0;
        txtUserPwd.Text = "";
        txtLoginId.Text = "";
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataSet dsExistMo = new DataSet();
        dsExistMo = DAL.DalAccessUtility.GetDataInDataSet("select distinct InMobile from Incharge where InMobile='" + txtMob.Text + "' and InchargeID <> " + Request.QueryString["InchargeId"].ToString());

        if (dsExistMo.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Mobile Already Exist.');", true);
        }
        else
        {
            if (txtName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Incharge name.');", true);
            }
            if (txtMob.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Incharge Mobile.');", true);
            }
            if (ddlDept.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Incharge Department.');", true);
            }
            if (ddlDesig.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Incharge Designation.');", true);
            }
            if (ddlUserType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select User Type.');", true);
            }
            if (txtLoginId.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Incharge Login Id.');", true);
            }
            if (txtUserPwd.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Incharge Password.');", true);
            }
            else
            {
                string InId = Request.QueryString["InchargeId"];
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewInchargeProc '" + InId + "','" + txtName.Text + "','" + txtMob.Text + "','','','" + lblUser.Text + "','2','1','" + txtLoginId.Text + "','" + txtUserPwd.Text + "','" + ddlUserType.SelectedValue + "','" + ModuleID + "'");
                BindInchargeDetails();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge edit Successfully.');", true);
                Clr();
            }
        }

    }
    protected void BindDesignation()
    {
        DataSet dsDesgis = new DataSet();
        dsDesgis = DAL.DalAccessUtility.GetDataInDataSet("select DesgId,Designation from Designation where Active=1 and ModuleID=" + ModuleID + " order by Designation asc");
        ddlDesig.DataSource = dsDesgis;
        ddlDesig.DataValueField = "DesgId";
        ddlDesig.DataTextField = "Designation";
        ddlDesig.DataBind();
        ddlDesig.Items.Insert(0, "Select");
        ddlDesig.SelectedIndex = 0;
    }
    protected void BindDepartment()
    {
        DataSet dsDept = new DataSet();
        dsDept = DAL.DalAccessUtility.GetDataInDataSet("select DepId,department from Department where Active=1 and ModuleID=" + ModuleID + " order by department asc");
        ddlDept.DataSource = dsDept;
        ddlDept.DataValueField = "DepId";
        ddlDept.DataTextField = "department";
        ddlDept.DataBind();
        ddlDept.Items.Insert(0, "Select");
        ddlDept.SelectedIndex = 0;
    }
    protected void BindUserType()
    {
        DataSet dsUsType = new DataSet();
        dsUsType = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId,UserTypeName from UserType where Active=1 and ModuleID=" + ModuleID + " order by UserTypeName asc");
        ddlUserType.DataSource = dsUsType;
        ddlUserType.DataValueField = "UserTypeId";
        ddlUserType.DataTextField = "UserTypeName";
        ddlUserType.DataBind();
        ddlUserType.Items.Insert(0, "Select");
        ddlUserType.SelectedIndex = 0;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Clr();
    }
}