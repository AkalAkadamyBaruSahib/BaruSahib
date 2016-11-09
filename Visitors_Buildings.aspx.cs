using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Visitors_Buildings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindBuildingDetail();
            if (Request.QueryString["BIdEdit"] != null)
            {
                getBuildingDetails(Request.QueryString["BIdEdit"].ToString());
                btnSave.Visible = false;
                btnEdit.Visible = true;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BuildingName bName = new BuildingName();
        DataTable BuildingExit = new DataTable();
        BuildingExit = DAL.DalAccessUtility.GetDataInDataSet("select * from BuildingName where Name ='" + txtBuildingName.Text + "'").Tables[0];
        if (BuildingExit.Rows.Count > 0 && BuildingExit != null)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Building Name Already Exits');</script>", false);
        }
        else
        {
            bName.Name = txtBuildingName.Text;
            VisitorUserRepository repo = new VisitorUserRepository(new AkalAcademy.DataContext());
            if (bName.ID == 0)
            {
                repo.AddNewBuilding(bName);
            }

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Record Saved Successfully');</script>", false);
        }
        BindBuildingDetail();
        Clear();
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtBuildingName.Text = "";
    }

    protected void Clear()
    {
        txtBuildingName.Text = "";
    }

    protected void BindBuildingDetail()
    {
        DataSet dsPSDetails = new DataSet();
        dsPSDetails = DAL.DalAccessUtility.GetDataInDataSet("Select * from BuildingName");
        divBuildingNameDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='50%'  style='color: #cc3300;'>Building Name</th>";
        ZoneInfo += "<th width='50%'  style='color: #cc3300;'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsPSDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='50%'>" + dsPSDetails.Tables[0].Rows[i]["Name"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='50%'>";
            ZoneInfo += "<a class='btn btn-info' href='Visitors_Buildings.aspx?BIdEdit=" + dsPSDetails.Tables[0].Rows[i]["ID"].ToString() + "'>";
            ZoneInfo += "<i  class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divBuildingNameDetails.InnerHtml = ZoneInfo.ToString();
    }

    private void getBuildingDetails(string bid)
    {
        DataTable dsGetBDetail = new DataTable();
        dsGetBDetail = DAL.DalAccessUtility.GetDataInDataSet("Select Name  from BuildingName Where ID ='" + bid + "'").Tables[0];
        if (dsGetBDetail.Rows.Count > 0)
        {
            txtBuildingName.Text = dsGetBDetail.Rows[0]["Name"].ToString();
        }
        BindBuildingDetail();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string BIdEdit = Request.QueryString["BIdEdit"];
        DataTable dsExist = new DataTable();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct Name from BuildingName where Name='" + txtBuildingName.Text + "' and ID!=" + BIdEdit + "").Tables[0];
        if (dsExist.Rows.Count > 0 && dsExist != null)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Building Name Already Exist.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Update  BuildingName set  Name ='" + txtBuildingName.Text + "'where ID =" + BIdEdit + "");
            btnEdit.Visible = false;
            btnSave.Visible = true;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Record Update Successfully');</script>", false);
        }
        BindBuildingDetail();
        Clear();
    }
}