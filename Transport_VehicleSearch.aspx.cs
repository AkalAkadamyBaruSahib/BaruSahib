using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transport_VehicleSearch : System.Web.UI.Page
{
    private bool IsApproved = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
                hdnInchargeID.Value = Session["InchargeID"].ToString();
                hdnUserTypeID.Value = Session["UserTypeID"].ToString();
             
            }
            if (Request.QueryString["ActiveVehicleID"] != null)
            {
                InActiveVechicle(Request.QueryString["ActiveVehicleID"].ToString());
            }

            if (Request.QueryString["DeActiveVehicleID"] != null)
            {
                ActiveVechicleDetail(Request.QueryString["DeActiveVehicleID"].ToString());
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string name = Request.Form["txtVehicle"];
        DataSet dsVehicleDetails = new DataSet();
        int InchargeID = int.Parse(Session["InchargeID"].ToString());
        int UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        dsVehicleDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_SearchVehicleInTransport '" + name.Trim() + "'," + InchargeID);
        divVehicleDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Vehicles List</h2>";
        ZoneInfo += "<div class='box-icon'>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='35%' height='5%'>Vehicle No.</th>";
        ZoneInfo += "<th width='30%'>Vehicle For</th>";
        ZoneInfo += "<th width='20%'>Details of Vehicle</th>";
        ZoneInfo += "<th width='5%'>Norms Completed (out of 16)</th>";
        ZoneInfo += "<th width='5%'>Document Completed (out of 8)</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsVehicleDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            var msg = dsVehicleDetails.Tables[0].Rows[i]["Number"].ToString();
            var vehileno = msg.Replace("-", "");
            if (dsVehicleDetails.Tables[0].Rows[i]["IsApproved"].ToString() == "True")
            {
                ZoneInfo += "<td width='35'><table><tr><td><a class='btn btn-danger' href='AddEditVehicle.aspx?VehicleID=" + dsVehicleDetails.Tables[0].Rows[i]["ID"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>" + dsVehicleDetails.Tables[0].Rows[i]["Number"].ToString() + "</span></a></td><td><a href='javascript: openModelPopUp(\"" + vehileno + "\");'><span class='label label-warning'  style='font-size: 15.998px;'>GPS</span></a></td><td><a href='Transport_VehicleDetails.aspx?ActiveVehicleID=" + dsVehicleDetails.Tables[0].Rows[i]["ID"].ToString() + "'><span class='label label-success' style='font-size: 15.998px;' title='Vehicle Active'>Deactivate</span></a></td></tr><tr><td><b>Driver Name:</b> " + dsVehicleDetails.Tables[0].Rows[i]["DriverName"].ToString() + "</td></tr><tr><td><b>Conductor Name:</b> " + dsVehicleDetails.Tables[0].Rows[i]["ConducterName"].ToString() + "</td></tr><tr><td><b>Driver Number:</b> " + dsVehicleDetails.Tables[0].Rows[i]["DriverNumber"].ToString() + "</td></tr><tr><td><b>Conductor Number:</b> " + dsVehicleDetails.Tables[0].Rows[i]["ConducterNumber"].ToString() + "</td></tr></table></td>";
            }
            else
            {
                ZoneInfo += "<td width='35%'><table><tr><td><a class='btn btn-danger' href='AddEditVehicle.aspx?VehicleID=" + dsVehicleDetails.Tables[0].Rows[i]["ID"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>" + dsVehicleDetails.Tables[0].Rows[i]["Number"].ToString() + "</span></a></td><td><a href='javascript: openModelPopUp(\"" + vehileno + "\");'><span class='label label-warning'  style='font-size: 15.998px;'>GPS</span></a></td><td><a href='Transport_VehicleDetails.aspx?DeActiveVehicleID=" + dsVehicleDetails.Tables[0].Rows[i]["ID"].ToString() + "'><span class='label label-success' style='font-size: 15.998px;' title='Vehicle Active'>Activate</span></a></td></tr><tr><td><b>Driver Name:</b> " + dsVehicleDetails.Tables[0].Rows[i]["DriverName"].ToString() + "</td></tr><tr><td><b>Conductor Name:</b> " + dsVehicleDetails.Tables[0].Rows[i]["ConducterName"].ToString() + "</td></tr><tr><td><b>Driver Number:</b> " + dsVehicleDetails.Tables[0].Rows[i]["DriverNumber"].ToString() + "</td></tr><tr><td><b>Conductor Number:</b> " + dsVehicleDetails.Tables[0].Rows[i]["ConducterNumber"].ToString() + "</td></tr></table></td>";
  
            }
            ZoneInfo += "<td width='30%'><table><tr><td><b>Zone</b>: " + dsVehicleDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr><tr><td><b>Academy</b>: " + dsVehicleDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr><tr><td><b>Transport Manager</b>: " + dsVehicleDetails.Tables[0].Rows[i]["InName"].ToString() + "</td></tr><tr><td><b>Transport Manager Number</b>: " + dsVehicleDetails.Tables[0].Rows[i]["TransportManagerNumber"].ToString() + "</td></tr></table>";
            ZoneInfo += "<td class='center'width='20%'><table>";
            ZoneInfo += "<tr><td><b>Vehicle Type:</b>" + dsVehicleDetails.Tables[0].Rows[i]["Type"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Sitter:</b> " + dsVehicleDetails.Tables[0].Rows[i]["Sitter"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Owner Name:</b> " + dsVehicleDetails.Tables[0].Rows[i]["OwnerName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Owner Number:</b> " + dsVehicleDetails.Tables[0].Rows[i]["OwnerNumber"].ToString() + "</td></tr>";
            ZoneInfo += "</table></td>";
            ZoneInfo += "<td width='5%'>" + dsVehicleDetails.Tables[0].Rows[i]["Norms"].ToString() + "</td>";
            ZoneInfo += "<td width='5%'>" + dsVehicleDetails.Tables[0].Rows[i]["DocumentCount"].ToString() + "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divVehicleDetails.InnerHtml = ZoneInfo.ToString();

    }

    protected void InActiveVechicle(string vid)
    {
        TransportController transportcontroller = new TransportController();
        transportcontroller.DeleteVechicleInfo(Convert.ToInt32(vid));
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vehicle Information  Delete Successfully.');", true);
    }

    protected void ActiveVechicleDetail(string vid)
    {
        TransportController transportcontroller = new TransportController();
        transportcontroller.ActiveVechicleInfo(Convert.ToInt32(vid));
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vehicle Information  Active Successfully.');", true);
    }
}
