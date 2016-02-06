using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Arch_DrawingView : System.Web.UI.Page
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
            if (Request.QueryString["AcaId"] != null)
            {
                BindDrawing(Request.QueryString["AcaId"].ToString());
                
            }
            divDrawingView.Visible = false;
            divAllDrawingView.Visible = true;

            BindAllDrawing();
            //BindDrawing();
            BinddWGtYPE();

        }
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
    protected void BindAllDrawing()
    {
        string id = Request.QueryString["AcaId"].ToString();
        DataSet dsZoneDetails = new DataSet();
        //dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_DrwaingShowByAcaIdAndDwgTypeForAllDwg '" + lblUser.Text + "'");
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("SELECT DrawingType.DwTypeName, Drawing.DwgId, Drawing.DwgNo, Drawing.RevisionNo, Drawing.DwgFileName, Drawing.PdfFileName, Drawing.PdfFilePath, Drawing.Active,Convert(nvarchar(20), Drawing.CreatedOn,107) as CreatedOn, Drawing.CreatedBy, Drawing.DrawingName, Drawing.AcaId, Drawing.DwTypeId FROM Drawing INNER JOIN DrawingType ON Drawing.DwTypeId = DrawingType.DwTypeId WHERE Drawing.AcaId='" + id + "'");
        divAllDrawingView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='50%'>Drawing Details</th>";
        ZoneInfo += "<th width='25%'>Drawing File</th>";
        ZoneInfo += "<th width='25%'>Uploaded Date</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='50%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Drawing No: " + dsZoneDetails.Tables[0].Rows[i]["DwgNo"].ToString() + "</td>";
            ZoneInfo += "<td>";
            if (dsZoneDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "Status: <span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "Status: <span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Revision No: " + dsZoneDetails.Tables[0].Rows[i]["RevisionNo"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Drawing Name: " + dsZoneDetails.Tables[0].Rows[i]["DrawingName"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td width='25%'><a href='" + dsZoneDetails.Tables[0].Rows[i]["PdfFilePath"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["PdfFileName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='25%'><table><tr><td>" + dsZoneDetails.Tables[0].Rows[i]["CreatedOn"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td>" + dsZoneDetails.Tables[0].Rows[i]["CreatedBy"].ToString() + "</td></tr></td></table></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divAllDrawingView.InnerHtml = ZoneInfo.ToString();

    }
    protected void BindDrawing(string id)
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_DrwaingShowByAcaIdAndDwgType '" + id + "' , '" + ddlDwgType.SelectedValue + "'");

        divDrawingView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='50%'>Drawing Details</th>";
        ZoneInfo += "<th width='25%'>Drawing File</th>";
        ZoneInfo += "<th width='25%'>Uploaded Date</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='50%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Drawing No: " + dsZoneDetails.Tables[0].Rows[i]["DwgNo"].ToString() + "</td>";
            ZoneInfo += "<td>";
            if (dsZoneDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "Status: <span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "Status: <span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Revision No: " + dsZoneDetails.Tables[0].Rows[i]["RevisionNo"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Drawing Name: " + dsZoneDetails.Tables[0].Rows[i]["DrawingName"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td width='25%'><a href='" + dsZoneDetails.Tables[0].Rows[i]["PdfFilePath"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["PdfFileName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='25%'><table><tr><td>" + dsZoneDetails.Tables[0].Rows[i]["CreatedOn"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td>" + dsZoneDetails.Tables[0].Rows[i]["CreatedBy"].ToString() + "</td></tr></td></table></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divDrawingView.InnerHtml = ZoneInfo.ToString();

    }

    protected void ddlDwgType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDwgType.SelectedValue == "6")
        {

            BindAllDrawing();
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
}