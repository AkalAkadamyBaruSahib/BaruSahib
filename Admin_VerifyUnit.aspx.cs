using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_VerifyUnit : System.Web.UI.Page
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
            BindUnitbyUserDetails();
            if (Request.QueryString["UnitIdV"] != null)
            {
                VarifyUnit(Request.QueryString["UnitIdV"].ToString());
            }
            if (Request.QueryString["UnitIdR"] != null)
            {
                RejectUnit(Request.QueryString["UnitIdR"].ToString());
            }
        }
    }
    protected void VarifyUnit(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("update Unit set Active=1,CreatedBy='"+ lblUser.Text +"',CreatedOn=GETDATE() where UnitId='"+ ID +"'");
        BindUnitbyUserDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Varifted Successfully.');", true);

    }
    protected void RejectUnit(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("update Unit set Active=0,CreatedBy='" + lblUser.Text + "',CreatedOn=GETDATE() where UnitId='" + ID + "'");
        BindUnitbyUserDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Unit Reject Successfully.');", true);

    }

    protected void BindUnitbyUserDetails()
    {
        DataSet dsUnitDetails = new DataSet();
        dsUnitDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowUnitDetails_CreatedByUser ");
        divUnitByUser.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='60%'>Unit Details</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsUnitDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='60%'><table><tr><td>" + dsUnitDetails.Tables[0].Rows[i]["UnitName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td>Created By: " + dsUnitDetails.Tables[0].Rows[i]["CreatedBy"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td>Created On: " + dsUnitDetails.Tables[0].Rows[i]["CreatedOn"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsUnitDetails.Tables[0].Rows[i]["Active"].ToString() == "2")
            {
                ZoneInfo += "<span class='label label-success' title='Not Varified' style='font-size: 15.998px;'>Not Varified</span>";
            }
             else if (dsUnitDetails.Tables[0].Rows[i]["Active"].ToString() == "0")
            {
                 ZoneInfo += "<span class='label label-success' title='Rejected by Admin' style='font-size: 15.998px;'>Rejected</span>";
               
            }
            else
             {
              ZoneInfo += "<span class='label label-important' title='Active' style='font-size: 15.998px;'>Active</span>";
             }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_VerifyUnit.aspx?UnitIdV=" + dsUnitDetails.Tables[0].Rows[i]["UnitId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Varify";
            ZoneInfo += "</a>&nbsp;";

            ZoneInfo += "<a class='btn btn-danger' href='Admin_VerifyUnit.aspx?UnitIdR=" + dsUnitDetails.Tables[0].Rows[i]["UnitId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Reject";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divUnitByUser.InnerHtml = ZoneInfo.ToString();
    }
}