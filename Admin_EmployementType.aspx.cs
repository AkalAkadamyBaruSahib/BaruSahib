using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_EmployementType : System.Web.UI.Page
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

            BindEmpTypeDetails();

            if (Request.QueryString["EmpTypeId"] != null)
            {
                getEmpTypeDetails(Request.QueryString["EmpTypeIdl"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
            }
            if (Request.QueryString["EmpTypeIdIA"] != null)
            {
                DeactiveEmpType(Request.QueryString["EmpTypeIdIA"].ToString());
            }
            if (Request.QueryString["EmpTypeIdA"] != null)
            {
                ActiveEmpType(Request.QueryString["EmpTypeIdA"].ToString());
            }

        }
    }
    private void getEmpTypeDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("select EmplType from EmployementType where EmpTypeId='" + ID + "'");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtEmpType.Text = dsCouDetails.Tables[0].Rows[0]["EmplType"].ToString();


        }
        BindEmpTypeDetails();
    }
    protected void DeactiveEmpType(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewEmpTypeProc '','" + lblUser.Text + "','4','" + ID + "','0'");
        BindEmpTypeDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Designation Deactive Successfully.');", true);

    }
    protected void ActiveEmpType(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewEmpTypeProc '','" + lblUser.Text + "','4','" + ID + "','1'");
        BindEmpTypeDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Designation Active Successfully.');", true);

    }
    protected void BindEmpTypeDetails()
    {
        DataSet dsDegisDetails = new DataSet();
        dsDegisDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowEmpTypeDetails_ByUser '" + lblUser.Text + "'");
        divDesigDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='60%'>Employement Type Name</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsDegisDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='60%'>" + dsDegisDetails.Tables[0].Rows[i]["EmplType"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsDegisDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_EmployementType.aspx?DesgIdA=" + dsDegisDetails.Tables[0].Rows[i]["EmpTypeId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>";
            ZoneInfo += "<a class='btn btn-info' href='Admin_EmployementType.aspx?EmpTypeId=" + dsDegisDetails.Tables[0].Rows[i]["EmpTypeId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_EmployementType.aspx?DesgIdIA=" + dsDegisDetails.Tables[0].Rows[i]["EmpTypeId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divDesigDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtEmpType.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Employement Type name.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewEmpTypeProc '" + txtEmpType.Text + "','" + lblUser.Text + "','1','','1'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Employement Type Create Successfully.');", true);
            BindEmpTypeDetails();
            txtEmpType.Text = "";
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtEmpType.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter degisnation name.');", true);
        }
        else
        {
            string ETId = Request.QueryString["EmpTypeId"];
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewEmpTypeProc '" + txtEmpType.Text + "','" + lblUser.Text + "','2','" + ETId + "','1'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Department Edit Successfully.');", true);
            BindEmpTypeDetails();
            txtEmpType.Text = "";
            btnEdit.Visible = false;
            btnSave.Visible = true;
        }
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtEmpType.Text = "";
    }
}