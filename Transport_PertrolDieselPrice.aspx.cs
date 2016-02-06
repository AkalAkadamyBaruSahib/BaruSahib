using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PertrolDieselPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindZone();
            BindPriceDetails();
            if (Request.QueryString["ID"] != null)
            {
                EditPrice();
            }
        }
    }

    private void EditPrice()
    {
        int ID = int.Parse(Request.QueryString["ID"].ToString());
        DataTable dtPrice = DAL.DalAccessUtility.GetDataInDataSet("Select PDP.*,Z.ZoneID from DieselPetrolPrice PDP INNER JOIN Academy A ON A.AcaID=PDP.AcaID INNER JOIN Zone Z ON Z.ZoneID=A.ZoneID where ID=" + ID).Tables[0];

        hdnID.Value = ID.ToString();
        ddlZone.ClearSelection();
        ddlZone.Items.FindByValue(dtPrice.Rows[0]["ZoneID"].ToString()).Selected = true;
        ddlZone.Enabled = false;

        BindAcademy();

        ddlAcademy.ClearSelection();
        ddlAcademy.Items.FindByValue(dtPrice.Rows[0]["AcaID"].ToString()).Selected = true;
        ddlAcademy.Enabled = false;

        txtDieselPrice.Text = dtPrice.Rows[0]["DieselPrice"].ToString();
        txtPetrolPrice.Text = dtPrice.Rows[0]["PetrolPrice"].ToString();

    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Zone.');", true);
        }
        else if (ddlAcademy.SelectedValue == "0")
        { ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Academy.');", true); }
        else if (txtPetrolPrice.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Petrol Price.');", true);
        }
        else if (txtDieselPrice.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Diesel Price.');", true);
        }
        else
        {
            if (hdnID.Value != string.Empty)
            {
                DAL.DalAccessUtility.ExecuteNonQuery("Update DieselPetrolPrice SET DieselPrice=" + txtDieselPrice.Text + ", PetrolPrice=" + txtPetrolPrice.Text + ", CreatedOn=getdate() WHERE ID=" + hdnID.Value);

                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                // make collection editable
                isreadonly.SetValue(this.Request.QueryString, false, null);
                // remove
                this.Request.QueryString.Remove("ID");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Price has been updated.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("Insert into DieselPetrolPrice values(" + ddlAcademy.SelectedValue + "," + txtDieselPrice.Text + "," + txtPetrolPrice.Text + ",getdate())");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Diesel and Petrol price has been saved.');", true);
            }
            Clear();
            BindPriceDetails();
        }
    }

    private void Clear()
    {
        hdnID.Value = string.Empty;
        ddlZone.SelectedIndex = 0;
        ddlZone.Enabled = true;
        ddlAcademy.SelectedIndex = 0;
        ddlAcademy.Enabled = true;
        txtDieselPrice.Text = string.Empty;
        txtPetrolPrice.Text = string.Empty;
    }


    protected void BindPriceDetails()
    {
        DataSet dsPriceDetails = new DataSet();
        dsPriceDetails = DAL.DalAccessUtility.GetDataInDataSet("Select PDP.*,Z.ZoneName,A.AcaName from DieselPetrolPrice PDP INNER JOIN Academy A ON A.AcaID=PDP.AcaID INNER JOIN Zone Z ON Z.ZoneID=A.ZoneID");
        divDesigDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='20%'>Zone</th>";
        ZoneInfo += "<th width='20%'>Academy</th>";
        ZoneInfo += "<th width='10%'>Diesel Price</th>";
        ZoneInfo += "<th width='10%'>Petrol Price</th>";
        ZoneInfo += "<th width='20%'>Date Created</th>";
        ZoneInfo += "<th width='10%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsPriceDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='20%'>" + dsPriceDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'>" + dsPriceDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsPriceDetails.Tables[0].Rows[i]["DieselPrice"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsPriceDetails.Tables[0].Rows[i]["PetrolPrice"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'>" + dsPriceDetails.Tables[0].Rows[i]["CreatedOn"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='10%'>";
            ZoneInfo += "<a class='btn btn-info' href='Transport_PertrolDieselPrice.aspx?ID=" + dsPriceDetails.Tables[0].Rows[i]["ID"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divDesigDetails.InnerHtml = ZoneInfo.ToString();
    }

    private void BindZone()
    {
        DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("Select * FROM Zone order by ZoneName asc");
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataValueField = "ZoneID";
        ddlZone.DataSource = dsBillDetails.Tables[0];
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, new ListItem("--All Zones---", "0"));
        ddlZone.SelectedIndex = 0;
    }

    private void BindAcademy()
    {
        DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("Select * FROM academy where ZoneID=" + ddlZone.SelectedValue + " order by AcaName asc");
        if (dsBillDetails.Tables.Count > 0 && dsBillDetails.Tables[0].Rows.Count > 0)
        {
            ddlAcademy.DataTextField = "AcaName";
            ddlAcademy.DataValueField = "AcaID";
            ddlAcademy.DataSource = dsBillDetails.Tables[0];
            ddlAcademy.DataBind();
            ddlAcademy.Items.Insert(0, new ListItem("--All Academies---", "0"));
            ddlAcademy.SelectedIndex = 0;
        }
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }
}