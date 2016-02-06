using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Country : System.Web.UI.Page
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

                BindCountryDetails();

                if (Request.QueryString["CountryId"] != null)
                {
                    getCountryDetails(Request.QueryString["CountryId"].ToString());
                    btnEdit.Visible = true;
                    btnSave.Visible = false;
                }
                if (Request.QueryString["CountryIdIA"] != null)
                {
                    DeactiveCountry(Request.QueryString["CountryIdIA"].ToString());
                }
                if (Request.QueryString["CountryIdA"] != null)
                {
                    ActiveCountry(Request.QueryString["CountryIdA"].ToString());
                }
            
        }
    }
    protected void BindCountryDetails()
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowCountryDetails_ByUser '" + lblUser.Text + "'");
        divCouDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='60%'>Country Name</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsCouDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='60%'>" + dsCouDetails.Tables[0].Rows[i]["CountryName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsCouDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' style='font-size: 15.998px;' title='Country Active'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;' title='Country Inactive'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_Country.aspx?CountryIdA=" + dsCouDetails.Tables[0].Rows[i]["CountryId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-info' href='Admin_Country.aspx?CountryId=" + dsCouDetails.Tables[0].Rows[i]["CountryId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_Country.aspx?CountryIdIA=" + dsCouDetails.Tables[0].Rows[i]["CountryId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divCouDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct CountryName from Country where CountryName='" + txtCountry.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Country Already Exist.');", true);
        }
        else if (txtCountry.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter country name.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewCountryProc '" + txtCountry.Text + "','" + lblUser.Text + "','1','','1'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Country Create Successfully.');", true);
            BindCountryDetails();
            txtCountry.Text = "";
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        string CouId = Request.QueryString["CountryId"];
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct CountryName from Country where CountryName='" + txtCountry.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Country Already Exist.');", true);
        }
        else if (txtCountry.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter country name.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewCountryProc '" + txtCountry.Text + "','" + lblUser.Text + "','2','" + CouId + "','1'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Country Edit Successfully.');", true);
            BindCountryDetails();
            txtCountry.Text = "";
            btnEdit.Visible = false;
            btnSave.Visible = true;
        }
    }
    private void getCountryDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowCountryDetails_ByID '"+ ID +"'");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtCountry.Text = dsCouDetails.Tables[0].Rows[0]["CountryName"].ToString();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active Country Status before edit.');", true);
        }
        BindCountryDetails();
    }
    protected void DeactiveCountry(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_ActiveInactiveCountry '0','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','"+ lblUser.Text +"','"+ ID +"'");
        BindCountryDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Country Deactive Successfully.');", true);

    }
    protected void ActiveCountry(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_ActiveInactiveCountry '1','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + lblUser.Text + "','" + ID + "'");
        BindCountryDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Country Active Successfully.');", true);

    }
    protected void btnCl_Click(object sender, EventArgs e)
    {

        txtCountry.Text = "";
    }
}