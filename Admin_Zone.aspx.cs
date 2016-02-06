using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Zone : System.Web.UI.Page
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
            BindZoneDetails();
            BindCountry();
            //BindStateLoad();
            //BindCityLoad();
            txtPin.Enabled = false;

            if (Request.QueryString["ZoneId"] != null)
            {
                getZoneDetails(Request.QueryString["ZoneId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
                ddlCountry.Enabled = false;
                ddlState.Enabled = false;
                ddlCity.Enabled = false;
                txtPin.Enabled = true;

            }
            if (Request.QueryString["ZoneIdIA"] != null)
            {
                DeactiveZone(Request.QueryString["ZoneIdIA"].ToString());
            }
            if (Request.QueryString["ZoneIdA"] != null)
            {
                ActiveZone(Request.QueryString["ZoneIdA"].ToString());
            }
        }
    }
    protected void DeactiveZone(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewZone '"+ ID +"','','0','" + lblUser.Text + "','4','','','',''");
        BindZoneDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Zone deactive successfully.');", true);

    }
    protected void ActiveZone(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewZone '" + ID + "','','1','" + lblUser.Text + "','4','','','',''");
        BindZoneDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Zone active successfully.');", true);
        //string message = "Row Index: " + index + "\\nName: " + name + "\\nCountry: " + country;
        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
    }
    //protected void BindCityLoad()
    //{
    //    DataSet dsState = new DataSet();
    //    dsState = DAL.DalAccessUtility.GetDataInDataSet(" select CityId,CityName from City");
    //    ddlCity.DataSource = dsState;
    //    ddlCity.DataValueField = "CityId";
    //    ddlCity.DataTextField = "CityName";
    //    ddlCity.DataBind();
    //    ddlCity.Items.Insert(0, "Select City");
    //    ddlCity.SelectedIndex = 0;
    //}
    //protected void BindStateLoad()
    //{
    //    DataSet dsState = new DataSet();
    //    dsState = DAL.DalAccessUtility.GetDataInDataSet(" Select StateId,StateName from State");
    //    ddlState.DataSource = dsState;
    //    ddlState.DataValueField = "StateId";
    //    ddlState.DataTextField = "StateName";
    //    ddlState.DataBind();
    //    ddlState.Items.Insert(0, "Select State");
    //    ddlState.SelectedIndex = 0;
    //}
    private void getZoneDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName,CountryId,StateId,CityId,Pincode from Zone where ZoneId='" + ID + "' and Active=1");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtZone.Text = dsCouDetails.Tables[0].Rows[0]["ZoneName"].ToString();
            BindCountry();
            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["CountryId"].ToString().Trim()));
            BindState();
            ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["StateId"].ToString().Trim()));
            BindCity();
            ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["CityId"].ToString().Trim()));
            txtPin.Text = dsCouDetails.Tables[0].Rows[0]["Pincode"].ToString();
           
        }
        BindZoneDetails();
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
    protected void BindCity()
    {
        DataSet dsState = new DataSet();
        dsState = DAL.DalAccessUtility.GetDataInDataSet(" exec  USP_ShowCityDetails '" + ddlState.SelectedValue + "'");
        ddlCity.DataSource = dsState;
        ddlCity.DataValueField = "CityId";
        ddlCity.DataTextField = "CityName";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, "Select City");
        ddlCity.SelectedIndex = 0;
    }
    protected void BindZoneDetails()
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_ShowZoneDetails_ByUser '" + lblUser.Text + "'");
        divZoneDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo +="<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
	    ZoneInfo +="<thead>";
	    ZoneInfo +="<tr>";
	    ZoneInfo +="<th width='40%'>Zone Name</th>";
        ZoneInfo += "<th width='40%'>Location</th>";
        //ZoneInfo +="<th width='20%'>Status</th>";
	    ZoneInfo +="<th width='20%'>Edit</th>";
	    ZoneInfo +="</tr>";
	    ZoneInfo +="</thead>";
	    ZoneInfo +="<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='40%'><table><tr><td><b>Zone Code</b> :" + dsZoneDetails.Tables[0].Rows[i]["ZoId"].ToString() + "</td>";
            ZoneInfo += "<tr><td><b>Zone Name</b> : " + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td width='40%'><table><tr><td><b>State</b> : " + dsZoneDetails.Tables[0].Rows[i]["StateName"].ToString() + "</td>";
            ZoneInfo += "<tr><td><b>City</b> : " + dsZoneDetails.Tables[0].Rows[i]["CityName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Pincode</b> : " + dsZoneDetails.Tables[0].Rows[i]["Pincode"].ToString() + "</td></tr></table></td>";
            //ZoneInfo += "<td class='center' width='20%'>";
            //if (dsZoneDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            //{
            //    ZoneInfo += "<span class='label label-success' title='Active'>Active</span>";
            //}
            //else
            //{
            //    ZoneInfo += "<span class='label label-important' title='Inactive'>InActive</span>";
            //}
            //ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            //ZoneInfo += "<a class='btn btn-success' href='Admin_Zone.aspx?ZoneIdA=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'>";
            //ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            //ZoneInfo += "</a>";
            ZoneInfo += "<a class='btn btn-info' href='Admin_Zone.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit Zone";
            ZoneInfo += "</a>";
            //ZoneInfo += "<a class='btn btn-danger' href='Admin_Zone.aspx?ZoneIdIA=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'>";
            //ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            //ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
	    ZoneInfo +="</tbody>";
        ZoneInfo += "</table>";
        
        divZoneDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct ZoneName from Zone where ZoneName='" + txtZone.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Country Already Exist.');", true);
        }
        else
        {
            if (txtZone.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter zone name.');", true);
            }

            else if (ddlState.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select state.');", true);
            }
            else if (ddlCountry.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select country.');", true);
            }
            else if (ddlCity.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select city.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewZone '','" + txtZone.Text + "','1','" + lblUser.Text + "','1','" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + txtPin.Text + "'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Zone Create Successfully.');", true);
                BindZoneDetails();
                Clr();
            }
        }
    }
    protected void Clr()
    {
        txtZone.Text = "";
        txtPin.Text = "";
        ddlCity.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindState();
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsPin = new DataSet();
        dsPin = DAL.DalAccessUtility.GetDataInDataSet("select Pincode from city where CityId='"+ ddlCity.SelectedValue +"'");
        txtPin.Text = dsPin.Tables[0].Rows[0]["Pincode"].ToString();
        
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCity();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        string Zid = Request.QueryString["ZoneId"];
         DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct ZoneName from Zone where ZoneName='" + txtZone.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Country Already Exist.');", true);
        }
        else
        {
            if (txtZone.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter zone name.');", true);
            }

            else if (ddlState.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select state.');", true);
            }
            else if (ddlCountry.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select country.');", true);
            }
            else if (ddlCity.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select city.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewZone_V2 '" + Zid + "','" + txtZone.Text + "','1','" + lblUser.Text + "','2','" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "','" + txtPin.Text + "'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Zone edit Successfully.');", true);
                BindZoneDetails();
                Clr();
            }
        }
    }
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelZone");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Zone.xls"));
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

}