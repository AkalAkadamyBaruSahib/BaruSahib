using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_UserControls_BodyDepartment : System.Web.UI.UserControl
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

            BindDeptDetails();

            if (Request.QueryString["DepId"] != null)
            {
                getDeptDetails(Request.QueryString["DepId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
            }
            if (Request.QueryString["DepIdIA"] != null)
            {
                DeactiveDept(Request.QueryString["DepIdIA"].ToString());
            }
            if (Request.QueryString["DepIdA"] != null)
            {
                ActiveDept(Request.QueryString["DepIdA"].ToString());
            }

        }
    }
    protected void BindDeptDetails()
    {

        string transportPageName = "Transport_Department.aspx";
        string AdminPageName = "Admin_Department.aspx";
        string pageName = string.Empty;

        if (ModuleID == (int)TypeEnum.Module.Purchase)
        {
            pageName = AdminPageName;
        }
        else if (ModuleID == (int)TypeEnum.Module.Transport)
        {
            pageName = transportPageName;
        }

        DataSet dsDeptDetails = new DataSet();
        dsDeptDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowDeptDetails_ByUser '" + lblUser.Text + "'," + ModuleID);
        divDeptDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='40%'>Department Name</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='30%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsDeptDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='40%'>" + dsDeptDetails.Tables[0].Rows[i]["Department"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsDeptDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='30%'>";
            ZoneInfo += "<a class='btn btn-success' href=" + pageName + "?DepIdA=" + dsDeptDetails.Tables[0].Rows[i]["DepId"].ToString() + ">";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-info' href=" + pageName + "?DepId=" + dsDeptDetails.Tables[0].Rows[i]["DepId"].ToString() + ">";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href=" + pageName + "?DepIdIA=" + dsDeptDetails.Tables[0].Rows[i]["DepId"].ToString() + ">";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divDeptDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct department from Department where department='" + txtDept.Text + "' and ModuleID=" + ModuleID);
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Department Already Exist.');", true);
        }
        else
        {

            if (txtDept.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter department name.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewDeptProc '" + txtDept.Text + "','" + lblUser.Text + "','1','','1'," + ModuleID);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Department Create Successfully.');", true);
                BindDeptDetails();
                txtDept.Text = "";
            }
        }
    }

    private void getDeptDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet(" select department from Department where DepId='" + ID + "' and Active=1");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtDept.Text = dsCouDetails.Tables[0].Rows[0]["department"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active Department Status before edit..');", true);
        }
        BindDeptDetails();
    }
    protected void DeactiveDept(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_ActiveInactiveDept '0','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + lblUser.Text + "','" + ID + "'");
        BindDeptDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Department Deactive Successfully.');", true);

    }
    protected void ActiveDept(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_ActiveInactiveDept '1','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + lblUser.Text + "','" + ID + "'");
        BindDeptDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Department Active Successfully.');", true);

    }
    protected void btnEdit_Click1(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct department from Department where department='" + txtDept.Text + "' and ModuleID=" + ModuleID);
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Department Already Exist.');", true);
        }
        else
        {

            if (txtDept.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter department name.');", true);
            }
            else
            {
                string DeptId = Request.QueryString["DepId"];
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewDeptProc '" + txtDept.Text + "','" + lblUser.Text + "','2','" + DeptId + "','1'," + ModuleID);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Department Edit Successfully.');", true);
                BindDeptDetails();
                txtDept.Text = "";
                btnEdit.Visible = false;
                btnSave.Visible = true;
            }
        }
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtDept.Text = "";
    }
}