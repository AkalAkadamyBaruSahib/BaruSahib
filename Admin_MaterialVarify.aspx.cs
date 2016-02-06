using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_MaterialVarify : System.Web.UI.Page
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
            BindMatbyUserDetails();
            if (Request.QueryString["MatIdV"] != null)
            {
                VarifyMat(Request.QueryString["MatIdV"].ToString());
            }
            if (Request.QueryString["MatIdR"] != null)
            {
                RejectMat(Request.QueryString["MatIdR"].ToString());
            }
        }
    }
    protected void VarifyMat(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("update Material set Active=1,CreatedBy='" + lblUser.Text + "',CreatedOn=GETDATE() where MatId='" + ID + "'");
        BindMatbyUserDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Varifted Successfully.');", true);

    }
    protected void RejectMat(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("update Material set Active=0,CreatedBy='" + lblUser.Text + "',CreatedOn=GETDATE() where MatId='" + ID + "'");
        BindMatbyUserDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Reject Successfully.');", true);

    }
    protected void BindMatbyUserDetails()
    {
        DataSet dsUnitDetails = new DataSet();
        dsUnitDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowMaterialDetails_CreatedByUser ");
        divUnitByUser.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='60%'>Material Details</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsUnitDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='60%'><table><tr><td>" + dsUnitDetails.Tables[0].Rows[i]["MatName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td>Created By: " + dsUnitDetails.Tables[0].Rows[i]["CreatedBy"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td>Created On: " + dsUnitDetails.Tables[0].Rows[i]["CreatedOn"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsUnitDetails.Tables[0].Rows[i]["Active"].ToString() == "2")
            {
                ZoneInfo += "<span class='label label-inverse' title='Not Varified' style='font-size: 15.998px;'>Not Varified</span>";
            }
            else if (dsUnitDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>Reject</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_MaterialVarify.aspx?MatIdV=" + dsUnitDetails.Tables[0].Rows[i]["MatId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Varify";
            ZoneInfo += "</a>&nbsp;";

            ZoneInfo += "<a class='btn btn-danger' href='Admin_MaterialVarify.aspx?MatIdR=" + dsUnitDetails.Tables[0].Rows[i]["MatId"].ToString() + "'>";
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