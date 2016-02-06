using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Arch_WorkAlloted : System.Web.UI.Page
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
            BindAcademy();
            getWorkDetails();
            divDrawingView.Visible = false;
            divAllDrawingView.Visible = true;

            //BindAllWork();
            //BindWork();



        }
    }

    protected void BindAcademy()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where Active=1");
        ddlAcademy.DataSource = dsZone;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, "SELECT ACADEMY");
        ddlAcademy.SelectedIndex = 0;
    }
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelWorkAllotArch");
        dt = ds.Tables[0];
        return dt;
    }
  
    protected void rbAll_CheckedChanged(object sender, EventArgs e)
    {
        pnlAcademy.Visible = false;
        divAllDrawingView.Visible = true;
        divDrawingView.Visible = false;
        getWorkDetails();
    }
    protected void rbAca_CheckedChanged(object sender, EventArgs e)
    {
        pnlAcademy.Visible = true;
    }
    private void getWorkDetails()
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AllWorkAllot");
        divAllDrawingView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Academies Details</h2>";
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
        ZoneInfo += "<th width='20%'>Location</th>";
        ZoneInfo += "<th width='25%'>Work Allot Details</th>";
        ZoneInfo += "<th width='10%'>Files</th>";
        ZoneInfo += "<th width='45%'>Status</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Zone:</b> " + dsAcaDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "(" + dsAcaDetails.Tables[0].Rows[i]["ZoId"].ToString() + ")</td></tr>";
            ZoneInfo += "<tr><td><b>Academy:</b> " + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "(" + dsAcaDetails.Tables[0].Rows[i]["AcId"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Work Allot Name:</b> " + dsAcaDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Created On:</b> " + dsAcaDetails.Tables[0].Rows[i]["CreateOn"].ToString() + "</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Work File:</b> <a  href='" + dsAcaDetails.Tables[0].Rows[i]["ImageFilePath"].ToString() + "'>" + dsAcaDetails.Tables[0].Rows[i]["ImageFileName"].ToString() + "</a></td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center'width='10%'>";
            if (dsAcaDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' style='font-size: 15.998px;' title='Active'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;' title='Inactive'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divAllDrawingView.InnerHtml = ZoneInfo.ToString();

    }
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelect.SelectedValue == "1")
        {
            pnlAcademy.Visible = true;
        }
        else if (ddlSelect.SelectedValue == "2")
        {
            pnlAcademy.Visible = false;
            divAllDrawingView.Visible = true;
            divDrawingView.Visible = false;
            getWorkDetails();
        }
    }
    private void getWorkDetailsAcademyWise()
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AllWorkAllot4ParticularAca '" + ddlAcademy.SelectedValue + "'");
        divDrawingView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> "+ ddlAcademy.SelectedItem.Text +" Academy Details</h2>";
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
        ZoneInfo += "<th width='20%'>Location</th>";
        ZoneInfo += "<th width='25%'>Work Allot Details</th>";
        ZoneInfo += "<th width='10%'>Files</th>";
        ZoneInfo += "<th width='45%'>Status</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Zone:</b> " + dsAcaDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "(" + dsAcaDetails.Tables[0].Rows[i]["ZoId"].ToString() + ")</td></tr>";
            ZoneInfo += "<tr><td><b>Academy:</b> " + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "(" + dsAcaDetails.Tables[0].Rows[i]["AcId"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Work Allot Name:</b> " + dsAcaDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Created On:</b> " + dsAcaDetails.Tables[0].Rows[i]["CreateOn"].ToString() + "</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td><b>Work File:</b> <a  href='" + dsAcaDetails.Tables[0].Rows[i]["ImageFilePath"].ToString() + "'>" + dsAcaDetails.Tables[0].Rows[i]["ImageFileName"].ToString() + "</a></td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center'width='10%'>";
            if (dsAcaDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' style='font-size: 15.998px;' title='Active'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;' title='Inactive'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divDrawingView.InnerHtml = ZoneInfo.ToString();

    }
    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        divDrawingView.Visible = true;
        divAllDrawingView.Visible = false;
        getWorkDetailsAcademyWise();
    }
   
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "WorkAllot.xls"));
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