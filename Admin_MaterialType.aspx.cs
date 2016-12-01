using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_MaterialType : System.Web.UI.Page
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

            BindMatTypeDetails();
            if (Request.QueryString["MatTypeId"] != null)
            {
                getMatTypeDetails(Request.QueryString["MatTypeId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
            }
            if (Request.QueryString["MatTypeIdIA"] != null)
            {
                DeactiveMatType(Request.QueryString["MatTypeIdIA"].ToString());
            }
            if (Request.QueryString["MatTypeIdA"] != null)
            {
                ActiveMatType(Request.QueryString["MatTypeIdA"].ToString());
            }
        }
    }
    protected void BindMatTypeDetails()
    {
        DataSet dsMatTypeDetails = new DataSet();
        dsMatTypeDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowMatTypeDetails_ByUser '" + lblUser.Text + "'");
        divMatTypeDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='30%'>Material Type</th>";
        ZoneInfo += "<th width='30%'>Status</th>";
        ZoneInfo += "<th width='40%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsMatTypeDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='30%'>" + dsMatTypeDetails.Tables[0].Rows[i]["MatTypeName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='30%'>";
            if (dsMatTypeDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='40%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_MaterialType.aspx?MatTypeIdA=" + dsMatTypeDetails.Tables[0].Rows[i]["MatTypeId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-info' href='Admin_MaterialType.aspx?MatTypeId=" + dsMatTypeDetails.Tables[0].Rows[i]["MatTypeId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_MaterialType.aspx?MatTypeIdIA=" + dsMatTypeDetails.Tables[0].Rows[i]["MatTypeId"].ToString() + "'>";
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
         dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct MatTypeName from MaterialType where MatTypeName='" + txtMatType.Text + "'");
         if (dsExist.Tables[0].Rows.Count > 0)
         {
             ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Type Already Exist.');", true);
         }
         else
         {
             if (txtMatType.Text == "")
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Material Type.');", true);
             }
             else
             {
                 DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewMatTypeProc '" + txtMatType.Text + "','" + lblUser.Text + "','1','','1'");
                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Type Create Successfully.');", true);
                 BindMatTypeDetails();
                 txtMatType.Text = "";
             }
         }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
         DataSet dsExist = new DataSet();
         dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct MatTypeName from MaterialType where MatTypeName='" + txtMatType.Text + "'");
         if (dsExist.Tables[0].Rows.Count > 0)
         {
             ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Type Already Exist.');", true);
         }
         else
         {
             if (txtMatType.Text == "")
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Material Type.');", true);
             }
             else
             {
                 string MTId = Request.QueryString["MatTypeId"];
                 DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewMatTypeProc '" + txtMatType.Text + "','" + lblUser.Text + "','2','" + MTId + "','1'");
                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Type Edit Successfully.');", true);
                 BindMatTypeDetails();
                 txtMatType.Text = "";
                 btnEdit.Visible = false;
                 btnSave.Visible = true;
             }
         }
    }
    private void getMatTypeDetails(string ID)
    {
        DataSet dsMatTyDetails = new DataSet();
        dsMatTyDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowMatTypeDetails_ByID '" + ID + "'");
        if (dsMatTyDetails.Tables[0].Rows.Count > 0)
        {
            txtMatType.Text = dsMatTyDetails.Tables[0].Rows[0]["MatTypeName"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active Material Type Status before edit..');", true);
        }
        BindMatTypeDetails();
    }
    protected void DeactiveMatType(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_ActiveInactiveMatType '0','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + lblUser.Text + "','" + ID + "'");
        BindMatTypeDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Type Deactive Successfully.');", true);

    }
    protected void ActiveMatType(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_ActiveInactiveMatType '1','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + lblUser.Text + "','" + ID + "'");
        BindMatTypeDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Type Active Successfully.');", true);

    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtMatType.Text = "";
    }
}