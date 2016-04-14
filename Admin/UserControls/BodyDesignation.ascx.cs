using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_UserControls_BodyDesignation : System.Web.UI.UserControl
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

            BindDesigDetails();

            if (Request.QueryString["DesgId"] != null)
            {
                getDegisDetails(Request.QueryString["DesgId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
            }
            if (Request.QueryString["DesgIdIA"] != null)
            {
                DeactiveDegis(Request.QueryString["DesgIdIA"].ToString());
            }
            if (Request.QueryString["DesgIdA"] != null)
            {
                ActiveDegis(Request.QueryString["DesgIdA"].ToString());
            }

        }
    }
    protected void BindDesigDetails()
    {


        string transportPageName = "Transport_Designation.aspx";
        string AdminPageName = "Admin_Designation.aspx";
        string pageName = string.Empty;

        if (ModuleID == (int)TypeEnum.Module.Purchase)
        {
            pageName = AdminPageName;
        }
        else if (ModuleID == (int)TypeEnum.Module.Transport)
        {
            pageName = transportPageName;
        }


        DataSet dsDegisDetails = new DataSet();
        dsDegisDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowDegisDetails_ByUser '" + lblUser.Text + "'," + ModuleID);
        divDesigDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='40%'>Designation Name</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='30%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsDegisDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='40%'>" + dsDegisDetails.Tables[0].Rows[i]["Designation"].ToString() + "</td>";
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
            ZoneInfo += "<td class='center' width='30%'>";

            ZoneInfo += "<a class='btn btn-success' href='" + pageName + "?DesgIdA=" + dsDegisDetails.Tables[0].Rows[i]["DesgId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-info' href='" + pageName + "?DesgId=" + dsDegisDetails.Tables[0].Rows[i]["DesgId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='" + pageName + "?DesgIdIA=" + dsDegisDetails.Tables[0].Rows[i]["DesgId"].ToString() + "'>";
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
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct Designation from Designation where Designation='" + txtDegis.Text + "' and ModuleID=" + ModuleID);
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Designation Already Exist.');", true);
        }
        else
        {
            if (txtDegis.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter degisnation name.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewDegisProc '" + txtDegis.Text + "','" + lblUser.Text + "','1','','1'," + ModuleID);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Degisnation Create Successfully.');", true);
                BindDesigDetails();
                txtDegis.Text = "";
            }
        }
    }
    private void getDegisDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet(" select Designation from Designation where DesgId='" + ID + "' and Active=1");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtDegis.Text = dsCouDetails.Tables[0].Rows[0]["Designation"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active Designation Status before edit..');", true);
        }
        BindDesigDetails();
    }

    protected void DeactiveDegis(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_ActiveInactiveDegis '0','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + lblUser.Text + "','" + ID + "'");
        BindDesigDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Designation Deactive Successfully.');", true);

    }
    protected void ActiveDegis(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_ActiveInactiveDegis '1','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + lblUser.Text + "','" + ID + "'");
        BindDesigDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Designation Active Successfully.');", true);

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct Designation from Designation where Designation='" + txtDegis.Text + "' and ModuleID=" + ModuleID);
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Designation Already Exist.');", true);
        }
        else
        {
            if (txtDegis.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter degisnation name.');", true);
            }
            else
            {
                string DegisId = Request.QueryString["DesgId"];
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewDegisProc '" + txtDegis.Text + "','" + lblUser.Text + "','2','" + DegisId + "','1'," + ModuleID);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Department Edit Successfully.');", true);
                BindDesigDetails();
                txtDegis.Text = "";
                btnEdit.Visible = false;
                btnSave.Visible = true;
            }
        }
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtDegis.Text = "";
    }
}