using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Emp_DrawingView : System.Web.UI.Page
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
              BinddWGtYPE();
            if (Request.QueryString["AcaId"] != null)
            {
                BindDrawing(Request.QueryString["AcaId"].ToString());
                BindAllDrawing(Request.QueryString["AcaId"].ToString());
            }
            divDrawingView.Visible = false;
            divAllDrawingView.Visible = true;
           
            //BindAllDrawing();
            //BindDrawing();
          

        }
        var js = new HtmlGenericControl("script");
        js.Attributes["type"] = "text/javascript";
        js.Attributes["src"] = "JavaScripts/AdminDrawingView.js?time=" + DateTime.Now.Ticks.ToString();
        Page.Form.Controls.Add(js);
    }
    protected void BinddWGtYPE()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("SELECT DwTypeId,DwTypeName FROM DrawingType where Active=1");
        ddlDwgType.DataSource = dsZone;
        ddlDwgType.DataValueField = "DwTypeId";
        ddlDwgType.DataTextField = "DwTypeName";
        ddlDwgType.DataBind();
        ddlDwgType.Items.Insert(0, "SELECT DRAWING TYPE");
        ddlDwgType.SelectedIndex = 0;
    }
    protected void BindAllDrawing(string id)
    {
        DataSet dsZoneDetails = new DataSet();
        //dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_DrwaingShowByAcaIdAndDwgTypeForAllDwg '" + lblUser.Text + "'");
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DrawingViewForUser '"+ lblUser.Text +"','"+ id +"'");
        divAllDrawingView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Drawing Details</h2>";
        
        ZoneInfo += "<div class='box-icon'>";
        //ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        ZoneInfo += "<h2><button type='button' id='btnshare' class='btn btn-primary'>Email Drawings</button> </h2>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='5%'></th>";
        ZoneInfo += "<th width='50%'>Drawing Details</th>";
        ZoneInfo += "<th width='25%'>Download File</th>";
        ZoneInfo += "<th width='25%'>Uploaded Date</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td><input type='checkbox' id='chkdrwing" + i + "' drwingPath='" + dsZoneDetails.Tables[0].Rows[i]["PdfFilePath"].ToString() + "' /></td>";
            ZoneInfo += "<td width='50%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Zone: " + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
            ZoneInfo += "<td>Drawing No: " + dsZoneDetails.Tables[0].Rows[i]["DwgNo"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Academy: " + dsZoneDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td>Revision No: " + dsZoneDetails.Tables[0].Rows[i]["RevisionNo"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td colspan='2'>Drawing Name: " + dsZoneDetails.Tables[0].Rows[i]["DrawingName"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td width='35%'><table><tr><td>PDF : <a href='" + dsZoneDetails.Tables[0].Rows[i]["PdfFilePath"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["PdfFileName"].ToString() + "</a></td></tr></table></td>";
            ZoneInfo += "<td width='25%'>" + dsZoneDetails.Tables[0].Rows[i]["CreatedOn"].ToString() + "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divAllDrawingView.InnerHtml = ZoneInfo.ToString();

    }
    protected void BindDrawing(string id)
    {
        if (ddlDwgType.SelectedIndex == 0)
        {

        }
        else
        {
            DataSet dsZoneDetails = new DataSet();
            dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_DrwaingShowByAcaIdAndDwgType '" + id + "' , '" + ddlDwgType.SelectedValue + "'");
            divDrawingView.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> " + ddlDwgType.SelectedItem.Text + " drawing Details</h2>";
            
            ZoneInfo += "<div class='box-icon'>";
            //ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
            ZoneInfo += "<h2><button type='button' id='btnshare' class='btn btn-primary'>Email Drawings</button> </h2>";
            ZoneInfo += "</div>";
            ZoneInfo += "</div>";
            ZoneInfo += "<div class='box-content'>";
            ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<thead>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<th width='5%'></th>";
            ZoneInfo += "<th width='50%'>Drawing Details</th>";
            ZoneInfo += "<th width='25%'>Drawing File</th>";
            ZoneInfo += "<th width='25%'>Uploaded Date</th>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</thead>";
            ZoneInfo += "<tbody>";
            for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td><input type='checkbox' id='chkdrwing" + i + "' drwingPath='" + dsZoneDetails.Tables[0].Rows[i]["PdfFilePath"].ToString() + "' /></td>";
                ZoneInfo += "<td width='50%'>";
                ZoneInfo += "<table>";
                ZoneInfo += "<tr>";
                ZoneInfo += "<td>Zone: " + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
                ZoneInfo += "<td>Drawing No: " + dsZoneDetails.Tables[0].Rows[i]["DwgNo"].ToString() + "</td>";
                ZoneInfo += "</tr>";
                ZoneInfo += "<tr>";
                ZoneInfo += "<td>Academy: " + dsZoneDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
                ZoneInfo += "<td>Revision No: " + dsZoneDetails.Tables[0].Rows[i]["RevisionNo"].ToString() + "</td>";
                ZoneInfo += "</tr>";
                ZoneInfo += "<tr>";
                ZoneInfo += "<td colspan='2'>Drawing Name: " + dsZoneDetails.Tables[0].Rows[i]["DrawingName"].ToString() + "</td>";
                ZoneInfo += "</tr>";
                ZoneInfo += "</table>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='35%'><table><tr><td>PDF : <a href='" + dsZoneDetails.Tables[0].Rows[i]["PdfFilePath"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["PdfFileName"].ToString() + "</a></td></tr></table></td>";
                ZoneInfo += "<td width='25%'>" + dsZoneDetails.Tables[0].Rows[i]["CreatedOn"].ToString() + "</td>";
                ZoneInfo += "</tr>";
            }
            ZoneInfo += "</tbody>";
            ZoneInfo += "</table>";
            divDrawingView.InnerHtml = ZoneInfo.ToString();
        }
    }
    protected void ddlDwgType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDwgType.SelectedValue == "6")
        {

            BindAllDrawing(Request.QueryString["AcaId"].ToString());
            divAllDrawingView.Visible = true;
            divDrawingView.Visible = false;
        }
        else
        {
            BindDrawing(Request.QueryString["AcaId"].ToString());
            divDrawingView.Visible = true;
            divAllDrawingView.Visible = false;
        }
    }
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DrawingExeclForUsers '" + lblUser.Text + "'");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Drawing.xls"));
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