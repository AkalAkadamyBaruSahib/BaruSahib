using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Adm_State : System.Web.UI.Page
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
            BindCountry();
            BindCountryDetails();
            if (Request.QueryString["StateId"] != null)
            {
                getCountryDetails(Request.QueryString["StateId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
                ddlCountry.Enabled = false;

            }
            if (Request.QueryString["StateIdIA"] != null)
            {
                DeactiveState(Request.QueryString["StateIdIA"].ToString());
            }
            if (Request.QueryString["StateIdA"] != null)
            {
                ActiveState(Request.QueryString["StateIdA"].ToString());
            }
        }

    }
    protected void BindCountry()
    {
        DataSet dsEvents = new DataSet();
        dsEvents = DAL.DalAccessUtility.GetDataInDataSet(" exec  USP_ShowCountryDetails");
        ddlCountry.DataSource = dsEvents;
        ddlCountry.DataValueField = "CountryId";
        ddlCountry.DataTextField = "CountryName";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, "Select Country");
        ddlCountry.SelectedIndex = 0;
    }
    protected void BindCountryDetails()
    {
        DataSet dsSateDetails = new DataSet();
        dsSateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowStateDetails_ByUser '" + lblUser.Text + "'");
        divStateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='25%'>Country Name</th>";
        ZoneInfo += "<th width='25%'>State Name</th>";
        ZoneInfo += "<th width='15%'>Status</th>";
        ZoneInfo += "<th width='35%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsSateDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='25%'>" + dsSateDetails.Tables[0].Rows[i]["CountryName"].ToString() + "</td>";
            ZoneInfo += "<td width='25%'>" + dsSateDetails.Tables[0].Rows[i]["StateName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='15%'>";
            if (dsSateDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' style='font-size: 15.998px;' title='Active'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;' title='Inactive'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='35%'>";
            ZoneInfo += "<a class='btn btn-success' href='Adm_State.aspx?StateIdA=" + dsSateDetails.Tables[0].Rows[i]["StateId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-info' href='Adm_State.aspx?StateId=" + dsSateDetails.Tables[0].Rows[i]["StateId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='Adm_State.aspx?StateIdIA=" + dsSateDetails.Tables[0].Rows[i]["StateId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divStateDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct StateName from State where StateName='" + txtState.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('State Already Exist.');", true);
        }
        else
        {
            if (txtState.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter state name.');", true);
            }
            else if (ddlCountry.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select country.');", true);
            }
            else
            {
                string ddl = ddlCountry.SelectedValue;
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewStateProc '" + txtState.Text + "','','" + lblUser.Text + "','1','" + ddl + "','1'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('State create successfully.');", true);
                BindCountryDetails();
                txtState.Text = "";
                ddlCountry.SelectedIndex = 0;
            }
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct StateName from State where StateName='" + txtState.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('State Already Exist.');", true);
        }
        else
        {
            if (txtState.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter state name.');", true);
            }
            else if (ddlCountry.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select country.');", true);
            }
            else
            {
                string StId = Request.QueryString["StateId"];
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewStateProc '" + txtState.Text + "','" + StId + "','" + lblUser.Text + "','2','','1'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('State edit successfully.');", true);
                BindCountryDetails();
                txtState.Text = "";
                ddlCountry.SelectedIndex = 0;
                btnEdit.Visible = false;
                btnSave.Visible = true;
            }
        }
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtState.Text = "";
        ddlCountry.SelectedIndex = 0;
    }
    private void getCountryDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowStateDetails_ByID '" + ID + "'");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtState.Text = dsCouDetails.Tables[0].Rows[0]["StateName"].ToString();
            BindCountry();
            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["CountryId"].ToString().Trim()));
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active State Status before edit.');", true);
        }
        BindCountryDetails();
    }

    protected void DeactiveState(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewStateProc '','" + ID + "','" + lblUser.Text + "','4','','0'");
        BindCountryDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('State deactive successfully.');", true);

    }
    protected void ActiveState(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewStateProc '','" + ID + "','" + lblUser.Text + "','4','','1'");
        BindCountryDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('State active successfully.');", true);

    }
}