using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Transport_VehicleDetails : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataRow dr;
    private bool IsApproved = true;
    private int InchargeID = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InchargeID"] != null)
        {
            InchargeID = int.Parse(Session["InchargeID"].ToString());
        }

        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
        }
        if (!Page.IsPostBack)
        {
            BindAcademy();
            BindTransportType();
            getVehicleDetails(true, -1,-1);
            if (Request.QueryString["ActiveVehicleID"] != null)
            {
                InActiveVechicle(Request.QueryString["ActiveVehicleID"].ToString());
            }

            if (Request.QueryString["DeActiveVehicleID"] != null)
            {
                ActiveVechicleDetail(Request.QueryString["DeActiveVehicleID"].ToString());
            }
        }
        if (Request.QueryString["AcaId"] != null)
        {
            getVehicleDetails(true, int.Parse(Request.QueryString["AcaId"].ToString()),-1);
            //btnExcel2.Visible = false;
            //btnExecl.Visible = false;
        }
    }

    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelEstimate");
        dt = ds.Tables[0];
        return dt;
    }

    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Estimate.xls"));
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

    private void getVehicleDetails(bool isApproved, int AcaID,int typeID)
    {
        int UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        DataSet dsVehicleDetails = new DataSet();
        if (UserTypeID == (int)(TypeEnum.UserType.TRANSPORTADMIN))
        {
            dsVehicleDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_GetVehiclesDetails]" + isApproved);
        }
        else
        {
            dsVehicleDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_GetVehiclesDetailsByInchargeID]" + isApproved + "," + InchargeID);
        }
        System.Data.EnumerableRowCollection<System.Data.DataRow> dtApproved = null;
        DataTable dtapproved = new DataTable();

        if (AcaID > 0 && typeID > 0)
        {
            dtApproved = (from mytable in dsVehicleDetails.Tables[0].AsEnumerable()
                          where mytable.Field<int>("AcademyID") == AcaID && mytable.Field<int>("TypeID") == typeID
                          select mytable);
        }
        else if (AcaID > 0)
        {
            dtApproved = (from mytable in dsVehicleDetails.Tables[0].AsEnumerable()
                          where mytable.Field<int>("AcademyID") == AcaID 
                          select mytable);
        }
        else if (typeID > 0)
        {
            dtApproved = (from mytable in dsVehicleDetails.Tables[0].AsEnumerable()
                          where mytable.Field<int>("TypeID") == typeID
                          select mytable);
        }
        else
        {
            dtApproved = (from mytable in dsVehicleDetails.Tables[0].AsEnumerable()
                          where mytable.Field<bool>("IsApproved") == isApproved
                          select mytable);
        }
        if (dtApproved.Count() > 0)
        {
            dtapproved = dtApproved.CopyToDataTable();
        }

        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Vehicles List</h2>";
        ZoneInfo += "<div class='box-icon'>";
        //ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='30%'>Vehicle No.</th>";
        ZoneInfo += "<th width='25%'>Vehicle For</th>";
        //ZoneInfo += "<th width='15%'>Sanction Date</th>";
        ZoneInfo += "<th width='20%'>Details of Vehicle</th>";
        ZoneInfo += "<th width='5%'>Norms Completed (out of 16)</th>";

        //ZoneInfo += "<th width='40%'><table width='100%'><tr><th colspan='3' align='center'Vehicle Documents</th></tr><tr><th width='17%'>Insurance</th><th width='50%'>Polution</th><th width='33%'>Permit</th></tr></table></th>";
        ZoneInfo += "<th width='5%'>Document Completed (out of 8)</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dtapproved.Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            var msg = dtapproved.Rows[i]["Number"].ToString();
            var vehileno = msg.Replace("-", "");
            if (isApproved == true)
            {
                ZoneInfo += "<td width='30%'><table><tr><td><a class='btn btn-danger' href='AddEditVehicle.aspx?VehicleID=" + dtapproved.Rows[i]["ID"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>" + dtapproved.Rows[i]["Number"].ToString() + "</span></a></td><td><a href='javascript: openModelPopUp(\"" + vehileno + "\");'><span class='label label-warning'  style='font-size: 15.998px;'>GPS</span></a></td><td><a href='Transport_VehicleDetails.aspx?ActiveVehicleID=" + dtapproved.Rows[i]["ID"].ToString() + "'><span class='label label-success' style='font-size: 15.998px;' title='Vehicle Active'>Deactivate</span></a></td></tr><tr><td><b>Driver Name:</b> " + dtapproved.Rows[i]["DriverName"].ToString() + "</td></tr><tr><td><b>Conductor Name:</b> " + dtapproved.Rows[i]["ConducterName"].ToString() + "</td></tr><tr><td><b>Driver Number:</b> " + dtapproved.Rows[i]["DriverNumber"].ToString() + "</td></tr><tr><td><b>Conductor Number:</b> " + dtapproved.Rows[i]["ConducterNumber"].ToString() + "</td></tr></table></td>";
            }
            else
            {
                ZoneInfo += "<td width='30%'><table><tr><td><a class='btn btn-danger' href='AddEditVehicle.aspx?VehicleID=" + dtapproved.Rows[i]["ID"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>" + dtapproved.Rows[i]["Number"].ToString() + "</span></a></td><td><a href='javascript: openModelPopUp(\"" + vehileno + "\");'><span class='label label-warning'  style='font-size: 15.998px;'>GPS</span></a></td><td><a href='Transport_VehicleDetails.aspx?DeActiveVehicleID=" + dtapproved.Rows[i]["ID"].ToString() + "'><span class='label label-success' style='font-size: 15.998px;' title='Vehicle Active'>Activate</span></a></td></tr><tr><td><b>Driver Name:</b> " + dtapproved.Rows[i]["DriverName"].ToString() + "</td></tr><tr><td><b>Conductor Name:</b> " + dtapproved.Rows[i]["ConducterName"].ToString() + "</td></tr><tr><td><b>Driver Number:</b> " + dtapproved.Rows[i]["DriverNumber"].ToString() + "</td></tr><tr><td><b>Conductor Number:</b> " + dtapproved.Rows[i]["ConducterNumber"].ToString() + "</td></tr></table></td>";
            }
            ZoneInfo += "<td width='30%'><table><tr><td><b>Zone</b>: " + dtapproved.Rows[i]["ZoneName"].ToString() + "</td></tr><tr><td><b>Academy</b>: " + dtapproved.Rows[i]["AcaName"].ToString() + "</td></tr><tr><td><b>Transport Manager</b>: " + dtapproved.Rows[i]["InName"].ToString() + "</td></tr><tr><td><b>Transport Manager Number</b>: " + dtapproved.Rows[i]["TransportManagerNumber"].ToString() + "</td></tr></table>";
            //ZoneInfo += "<td class='center' width='15%'>" + dtapproved.Rows[i]["dt"].ToString() + "</td>";
            ZoneInfo += "<td class='center'width='20%'><table>";
            ZoneInfo += "<tr><td><b>Vehicle Type:</b>" + dtapproved.Rows[i]["Type"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Sitter:</b> " + dtapproved.Rows[i]["Sitter"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Owner Name:</b> " + dtapproved.Rows[i]["OwnerName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Owner Number:</b> " + dtapproved.Rows[i]["OwnerNumber"].ToString() + "</td></tr>";
            //ZoneInfo += "<tr><td><a class='btn btn-danger' href='Admin_EstimateEdit.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>Edit Estimate</span></a>   <a href='Admin_EstimateView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'><span class='label label-info'  style='font-size: 15.998px;'>Print Estimate</span></a></td></tr>";
            ZoneInfo += "</table></td>";


            ZoneInfo += "<td width='5%'>" + dtapproved.Rows[i]["Norms"].ToString() + "</td>";
            ZoneInfo += "<td width='5%'>" + dtapproved.Rows[i]["DocumentCount"].ToString() + "</td>";
            //ZoneInfo += "<td width='40%'><table width='100%'>";
            //ZoneInfo += "<tr><td width='17%'>" + dtapproved.Rows[i]["InsuranceEndDate"].ToString() + "</td><td width='50%'>" + dtapproved.Rows[i]["Pollution"].ToString() + "</td><td width='33%'>" + dtapproved.Rows[i]["PermitDate"].ToString() + "</td></tr>";
            //ZoneInfo += "</table></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }

    private string GetFileName(string filepaths, string fileName)
    {
        string anchorLink = string.Empty;
        string[] filePath = filepaths.Split(',');
        int count = 0;
        foreach (string path in filePath)
        {
            count++;
            anchorLink += "<a href='" + path + "' target='_blank'>" + fileName + "_" + count + "</a> , ";
        }

        return anchorLink.Substring(0, anchorLink.Length - 3);
    }

    protected DataTable BindDatatable2()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateWithMaterial");
        dt = ds.Tables[0];
        return dt;
    }

    protected void btnExcel2_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Estimate.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindDatatable2();
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

    protected void btnNonApproved_Click(object sender, EventArgs e)
    {
        string acaID = ddlAcademy.SelectedIndex == -1 ? "-1" : ddlAcademy.SelectedValue;
        string typeID = ddlTransportType.SelectedIndex == -1 ? "-1" : ddlTransportType.SelectedValue;
        if (((Button)sender).Text == "View Non Approved Vehicle(s)")
        {
            IsApproved = false;
            getVehicleDetails(false, int.Parse(acaID), int.Parse(typeID));
            ((Button)sender).Text = "View Approved Vehicle(s)";
        }
        else
        {
            IsApproved = true;
            ((Button)sender).Text = "View Non Approved Vehicle(s)";
            getVehicleDetails(true, int.Parse(acaID), int.Parse(typeID));
        }
    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        string acaID = ddlAcademy.SelectedIndex == -1 ? "-1" : ddlAcademy.SelectedValue;
        string typeID = ddlTransportType.SelectedIndex == -1 ? "-1" : ddlTransportType.SelectedValue;
        getVehicleDetails(IsApproved, int.Parse(acaID), int.Parse(typeID));

    }

    private void BindAcademy()
    {
        UsersRepository users = new UsersRepository(new AkalAcademy.DataContext());
        List<Academy> acaList = new List<Academy>();
        if (Session["UserTypeID"].ToString() == "13")
        {
            acaList = users.GetAllAcademy(2);
        }
        else
        {
            acaList = users.GetAcademyByInchargeID(InchargeID);
        }

        ddlAcademy.DataSource = acaList;
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataValueField = "AcaID";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, (new System.Web.UI.WebControls.ListItem("-- All Academy --", "0")));
        ddlAcademy.SelectedIndex = 0;

    }

    protected void InActiveVechicle(string vid)
    {
        TransportController transportcontroller = new TransportController();
        transportcontroller.DeleteVechicleInfo(Convert.ToInt32(vid));
        getVehicleDetails(true, -1,-1);
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vehicle Information  Delete Successfully.');", true);
    }

    protected void ActiveVechicleDetail(string vid)
    {
        TransportController transportcontroller = new TransportController();
        transportcontroller.ActiveVechicleInfo(Convert.ToInt32(vid));
        getVehicleDetails(true, -1,-1);
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vehicle Information  Active Successfully.');", true);
    }

    public void BindTransportType()
    {
        DataSet TranspotType = new DataSet();
        TranspotType = DAL.DalAccessUtility.GetDataInDataSet("select * from TransportTypes");
        ddlTransportType.DataSource = TranspotType;
        ddlTransportType.DataValueField = "ID";
        ddlTransportType.DataTextField = "Type";
        ddlTransportType.DataBind();
        ddlTransportType.Items.Insert(0, (new System.Web.UI.WebControls.ListItem("-- All Transport Type--", "0")));
        ddlTransportType.SelectedIndex = 0;
    
    }

    protected void ddlTransportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string acaID = ddlAcademy.SelectedIndex == -1 ? "-1" : ddlAcademy.SelectedValue;
        string typeID = ddlTransportType.SelectedIndex == -1 ? "-1" : ddlTransportType.SelectedValue;
        getVehicleDetails(IsApproved, int.Parse(acaID), int.Parse(typeID));
    }
}