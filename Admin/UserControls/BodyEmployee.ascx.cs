using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;


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
            BindUserRole();
            BindUserType();
            btnEdit.Visible = false;
            BindDepartment();
            if (Request.QueryString["InchargeId"] != null)
            {
                getInchargeDetails(Request.QueryString["InchargeId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
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
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("Select InName,InMobile,LoginId,UserPwd,DepId,DesigId,UserTypeId,U.RoleID,EmailID FROM Incharge  LEFT OUTER JOIN UserRole U ON U.UserID = Incharge.InchargeId WHERE InchargeId='" + ID + "' and Active=1");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtName.Text = dsCouDetails.Tables[0].Rows[0]["InName"].ToString();
            txtMob.Text = dsCouDetails.Tables[0].Rows[0]["InMobile"].ToString();
            txtLoginId.Text = dsCouDetails.Tables[0].Rows[0]["LoginId"].ToString();
            txtUserPwd.Text = dsCouDetails.Tables[0].Rows[0]["UserPwd"].ToString();
            ddlDept.SelectedValue = dsCouDetails.Tables[0].Rows[0]["DepId"].ToString();
            ddlDesig.SelectedValue = dsCouDetails.Tables[0].Rows[0]["DesigId"].ToString();
            ddlUserType.SelectedValue = dsCouDetails.Tables[0].Rows[0]["UserTypeId"].ToString();
            txtEmailID.Text = dsCouDetails.Tables[0].Rows[0]["EmailID"].ToString();
            if (dsCouDetails.Tables[0].Rows[0]["RoleID"] != null)
            {
                foreach (ListItem item in chkUserRole.Items)
                {
                    var foundId = dsCouDetails.Tables[0].Select("RoleID= '" + item.Value + "'");
                    if (foundId.Length > 0)
                    {
                        item.Selected = true;
                    }
                }
            }
        }
    }
    protected void BindInchargeDetails()
    {
        UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());

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
        ZoneInfo += "<th width='20%'>Assign Locations</th>";
        ZoneInfo += "<th>Status</th>";
        ZoneInfo += "<th>Actions</th>";
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

            List<Academy> Academies = repo.GetAcademyByInchargeID(int.Parse(dsDegisDetails.Tables[0].Rows[i]["InchargeId"].ToString()));

            if (Academies != null)
            {
                ZoneInfo += "<td  width='20%'><table><tr><td><b>Academies:</b></td></tr>";
                foreach (Academy aca in Academies)
                {
                    ZoneInfo += "<tr><td>" + aca.AcaName + "</td></tr>";
                }
            }

            ZoneInfo += "</table></td>";


            ZoneInfo += "<td class='center'>";
            if (dsDegisDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";




            ZoneInfo += "<td class='center'>";

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
            Hashtable param = new Hashtable();
            param.Add("InId", string.Empty);
            param.Add("InName", txtName.Text);
            param.Add("InMob", txtMob.Text);
            param.Add("desigId", ddlDesig.SelectedValue);
            param.Add("DeptId", ddlDept.SelectedValue);
            param.Add("CreatedBy", lblUser.Text);
            param.Add("type", 1);
            param.Add("Active", 1);
            param.Add("LoginId", txtLoginId.Text);
            param.Add("UserPwd", txtUserPwd.Text);
            param.Add("userType", ddlUserType.SelectedValue);
            param.Add("moduleID", ModuleID);
            param.Add("EmailID", txtEmailID.Text);
            int InchargeID = DAL.DalAccessUtility.GetDataInScaler("USP_NewInchargeProc", param);

            UserRole role = null;
            foreach (ListItem chk in chkUserRole.Items)
            {
                if (chk.Selected == true)
                {
                    role = new UserRole();
                    role.UserID = InchargeID;
                    role.RoleID = Convert.ToInt32(chk.Value);
                    AdminRepository repo = new AdminRepository(new AkalAcademy.DataContext());
                    if (role.ID == 0)
                    {
                        repo.AddNewRoleByUserID(role);
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Create Successfully.');", true);
            BindInchargeDetails();
            Clr();
        }
    }
    protected void Clr()
    {
        txtMob.Text = "";
        txtName.Text = "";
        ddlDept.SelectedIndex = 0;
        ddlDesig.SelectedIndex = 0;
        ddlUserType.SelectedIndex = 0;
        chkUserRole.ClearSelection();
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
            string InId = Request.QueryString["InchargeId"];
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewInchargeProc '" + InId + "','" + txtName.Text + "','" + txtMob.Text + "','','','" + lblUser.Text + "','2','1','" + txtLoginId.Text + "','" + txtUserPwd.Text + "','" + ddlUserType.SelectedValue + "','" + ModuleID + "','" + txtEmailID.Text + "'");
            DAL.DalAccessUtility.ExecuteNonQuery("Delete From UserRole where UserID='" + Convert.ToInt32(InId) + "'");
            UserRole role = null;
            foreach (ListItem chk in chkUserRole.Items)
            {
                if (chk.Selected == true)
                {
                    role = new UserRole();
                    role.UserID = Convert.ToInt32(InId);
                    role.RoleID = Convert.ToInt32(chk.Value);
                    AdminRepository repo = new AdminRepository(new AkalAcademy.DataContext());
                    repo.AddNewRoleByUserID(role);
                }
            }

            BindInchargeDetails();
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge edit Successfully.');", true);
            Clr();
        }
    }
    protected void BindDesignation()
    {
        DataTable dsDesgis = new DataTable();
        dsDesgis = DAL.DalAccessUtility.GetDataInDataSet("select DesgId,Designation from Designation where Active=1 and ModuleID=" + ModuleID + " order by Designation asc").Tables[0];
        if (dsDesgis != null && dsDesgis.Rows.Count > 0)
        {
            ddlDesig.DataSource = dsDesgis;
            ddlDesig.DataValueField = "DesgId";
            ddlDesig.DataTextField = "Designation";
            ddlDesig.DataBind();
            ddlDesig.Items.Insert(0, new ListItem("--Select Designation--", "0"));
        }
    }
    protected void BindDepartment()
    {
        DataTable dsDept = new DataTable();
        dsDept = DAL.DalAccessUtility.GetDataInDataSet("select DepId,department from Department where Active=1 and ModuleID=" + ModuleID + " order by department asc").Tables[0];
        if (dsDept != null && dsDept.Rows.Count > 0)
        {
            ddlDept.DataSource = dsDept;
            ddlDept.DataValueField = "DepId";
            ddlDept.DataTextField = "department";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("--Select Department--", "0"));
        }
    }
    protected void BindUserType()
    {
        DataTable dsUsType = new DataTable();
        dsUsType = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId,UserTypeName from UserType where Active=1 and ModuleID=" + ModuleID + " order by UserTypeName asc").Tables[0];
        if (dsUsType != null && dsUsType.Rows.Count > 0)
        {
            ddlUserType.DataSource = dsUsType;
            ddlUserType.DataValueField = "UserTypeId";
            ddlUserType.DataTextField = "UserTypeName";
            ddlUserType.DataBind();
            ddlUserType.Items.Insert(0, new ListItem("--Select User Type--", "0"));
        }
     
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Clr();
    }

    protected void BindUserRole()
    {
        List<Role> dsUsRole = new List<Role>();
        AdminRepository repo = new AdminRepository(new AkalAcademy.DataContext());
        dsUsRole = repo.GetUserRole();
        if (dsUsRole != null)
        {
            chkUserRole.DataSource = dsUsRole;
            chkUserRole.DataValueField = "ID";
            chkUserRole.DataTextField = "RoleName";
            chkUserRole.DataBind();
        }
    }
}