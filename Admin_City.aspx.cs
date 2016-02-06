using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_City : System.Web.UI.Page
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
            BindCityDetails();
            if (Request.QueryString["CityId"] != null)
            {
                getCityDetails(Request.QueryString["CityId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
                ddlCountry.Enabled = false;
                ddlState.Enabled = false;

            }
            if (Request.QueryString["CityIdIA"] != null)
            {
                DeactiveState(Request.QueryString["CityIdIA"].ToString());
            }
            if (Request.QueryString["CityIdA"] != null)
            {
                ActiveState(Request.QueryString["CityIdA"].ToString());
            }
        }
    }
    protected void BindCountry()
    {
        DataSet dsCount = new DataSet();
        dsCount = DAL.DalAccessUtility.GetDataInDataSet(" exec  USP_ShowCountryDetails");
        ddlCountry.DataSource = dsCount;
        ddlCountry.DataValueField = "CountryId";
        ddlCountry.DataTextField = "CountryName";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, "Select Country");
        ddlCountry.SelectedIndex = 0;
    }
    protected void BindState()
    {
        DataSet dsState = new DataSet();
        dsState = DAL.DalAccessUtility.GetDataInDataSet(" exec  USP_ShowStateDetails '" + ddlCountry.SelectedValue + "'");
        ddlState.DataSource = dsState;
        ddlState.DataValueField = "StateId";
        ddlState.DataTextField = "StateName";
        ddlState.DataBind();
        ddlState.Items.Insert(0, "Select State");
        ddlState.SelectedIndex = 0;
    }

    
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindState();
    }
    protected void BindCityDetails()
    {
        DataSet dsSateDetails = new DataSet();
        dsSateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowCityDetails_ByUser '" + lblUser.Text + "'");
        divCityDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='20%'>Country Name</th>";
        ZoneInfo += "<th width='20%'>State Name</th>";
        ZoneInfo += "<th width='25%'>City Name (Pincode)</th>";
        ZoneInfo += "<th width='15%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsSateDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='20%'>" + dsSateDetails.Tables[0].Rows[i]["CountryName"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'>" + dsSateDetails.Tables[0].Rows[i]["StateName"].ToString() + "</td>";
            ZoneInfo += "<td width='25%'>" + dsSateDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsSateDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td>";
            ZoneInfo += "<td class='center' width='15%'>";
            if (dsSateDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_City.aspx?CityIdA=" + dsSateDetails.Tables[0].Rows[i]["CityId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-info' href='Admin_City.aspx?CityId=" + dsSateDetails.Tables[0].Rows[i]["CityId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_City.aspx?CityIdIA=" + dsSateDetails.Tables[0].Rows[i]["CityId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divCityDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct CityName from City where CityName='" + txtCity.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Country Already Exist.');", true);
        }
        else
        {
            if (txtCity.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter city name.');", true);
            }
            else if (txtPinCode.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter city pincode.');", true);
            }
            else if (ddlState.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select state.');", true);
            }
            else if (ddlCountry.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select country.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewCityProc '" + txtCity.Text + "','" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + lblUser.Text + "','1','','" + txtPinCode.Text + "','1'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('City create successfully.');", true);
                BindCityDetails();
                Clr();
            }
        }
    }
    protected void Clr()
    {
        txtCity.Text = "";
        ddlState.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        txtPinCode.Text = "";
    }

    private void getCityDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowCityDetails_ById '" + ID + "'");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtCity.Text = dsCouDetails.Tables[0].Rows[0]["CityName"].ToString();
            BindCountry();
            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["CountryId"].ToString().Trim()));
            BindState();
            ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["StateId"].ToString().Trim()));
            txtPinCode.Text = dsCouDetails.Tables[0].Rows[0]["Pincode"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active City Status before edit.');", true);
        }
        BindCityDetails();
    }
    protected void DeactiveState(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewCityProc '','','','" + lblUser.Text + "','4','" + ID + "','','0'");
        BindCityDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('City deactive successfully.');", true);

    }
    protected void ActiveState(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewCityProc '','','','" + lblUser.Text + "','4','" + ID + "','','1'");
        BindCityDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('City active successfully.');", true);

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        if (txtCity.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter city name.');", true);
        }
        if (txtPinCode.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter city pincode.');", true);
        }
        if (ddlState.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select state.');", true);
        }
        if (ddlCountry.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select country.');", true);
        }
        else
        {
            string CtyId = Request.QueryString["CityId"];
            //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewCityProc '" + txtCity.Text + "','" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + lblUser.Text + "','2','"+ CtyId +"','" + txtPinCode.Text + "','1'");
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewCityProc '" + txtCity.Text + "','','','" + lblUser.Text + "','2','" + CtyId + "','" + txtPinCode.Text + "','1'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('City create successfully.');", true);
            BindCityDetails();
            Clr();
        }
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        Clr();
    }
}