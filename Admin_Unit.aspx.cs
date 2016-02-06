using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Unit : System.Web.UI.Page
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

            BindUnitDetails();
            //BindUnitbyUserDetails();
            if (Request.QueryString["UnitId"] != null)
            {
                getUnitDetails(Request.QueryString["UnitId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
            }
            if (Request.QueryString["UnitIdIA"] != null)
            {
                DeactiveUnit(Request.QueryString["UnitIdIA"].ToString());
            }
            if (Request.QueryString["UnitIdA"] != null)
            {
                ActiveUnit(Request.QueryString["UnitIdA"].ToString());
            }
            DataSet dsCountUnitByUser = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowUnitDetails_CreatedByUser");
            lblUnitCount.Text = dsCountUnitByUser.Tables[1].Rows[0]["Cou"].ToString();
        }
    }
    protected void BindUnitDetails()
    {
        DataSet dsUnitDetails = new DataSet();
        dsUnitDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowUnitDetails_ByUser '" + lblUser.Text + "'");
        divUnitDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='60%'>Unit</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsUnitDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='60%'>" + dsUnitDetails.Tables[0].Rows[i]["UnitName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsUnitDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_Unit.aspx?UnitIdA=" + dsUnitDetails.Tables[0].Rows[i]["UnitId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-info' href='Admin_Unit.aspx?UnitId=" + dsUnitDetails.Tables[0].Rows[i]["UnitId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_Unit.aspx?UnitIdIA=" + dsUnitDetails.Tables[0].Rows[i]["UnitId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divUnitDetails.InnerHtml = ZoneInfo.ToString();
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
         DataSet dsExist = new DataSet();
         dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct UnitName from Unit where UnitName='" + txtUnit.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Already Exist.');", true);
        }
        else
        {
            if (txtUnit.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Unit.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewUnitProc '" + txtUnit.Text + "','" + lblUser.Text + "','1','','1'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Create Successfully.');", true);
                BindUnitDetails();
                txtUnit.Text = "";
            }
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
         DataSet dsExist = new DataSet();
         dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct UnitName from Unit where UnitName='" + txtUnit.Text + "'");
         if (dsExist.Tables[0].Rows.Count > 0)
         {
             ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Already Exist.');", true);
         }
         else
         {
             if (txtUnit.Text == "")
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Unit.');", true);
             }
             else
             {
                 string UId = Request.QueryString["UnitId"];
                 DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewUnitProc '" + txtUnit.Text + "','" + lblUser.Text + "','2','"+ UId +"','1'");
                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Edit Successfully.');", true);
                 BindUnitDetails();
                 txtUnit.Text = "";
                 btnEdit.Visible = false;
                 btnSave.Visible = true;
             }
         }
    }
    private void getUnitDetails(string ID)
    {
        DataSet dsMatTyDetails = new DataSet();
        dsMatTyDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowUnitDetails_ByID '" + ID + "'");
        if (dsMatTyDetails.Tables[0].Rows.Count > 0)
        {
            txtUnit.Text = dsMatTyDetails.Tables[0].Rows[0]["UnitName"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active Unit Status before edit..');", true);
        }
        BindUnitDetails();
    }
    protected void DeactiveUnit(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewUnitProc '','" + lblUser.Text + "','4','" + ID + "','0'");
        BindUnitDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Deactive Successfully.');", true);

    }
    protected void ActiveUnit(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewUnitProc '','" + lblUser.Text + "','4','" + ID + "','1'");
        BindUnitDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Active Successfully.');", true);

    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtUnit.Text = "";
    }
}