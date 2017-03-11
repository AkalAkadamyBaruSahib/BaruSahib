using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
public partial class Admin_DrawingView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAcademy();
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
            }

            divDrawingView.Visible = false;
            divAllDrawingView.Visible = true;
            if (Request.QueryString["DwgIdIA"] != null && Request.QueryString["IsApproved"] == null)
            {
                DeactiveDrawing(Request.QueryString["DwgIdIA"].ToString());
            }
            else if (Request.QueryString["DwgIdA"] != null && Request.QueryString["IsApproved"] == null)
            {
                ActiveDrawing(Request.QueryString["DwgIdA"].ToString());
            }
            else if (Request.QueryString["DwgIdIA"] != null && Request.QueryString["IsApproved"] != null)
            {
                ApprovedDrawing(Request.QueryString["DwgIdIA"].ToString());
            }
            else if (Request.QueryString["DwgIdD"] != null)
            {
                DeleteDrawing(Request.QueryString["DwgIdD"].ToString());
            }
            else
            {
                if (Session["UserTypeID"].ToString() == "21")
                {
                    BindAllDrawing(false, -1);
                    btnNonApproved.Visible = false;
                }
                else
                {
                    BindAllDrawing(true, -1);
                }
            }
            
            //BindDrawing();
            BinddWGtYPE();
            
            
        }
        var js = new HtmlGenericControl("script");
        js.Attributes["type"] = "text/javascript";
        js.Attributes["src"] = "JavaScripts/AdminDrawingView.js?time=" + DateTime.Now.Ticks.ToString();
        Page.Form.Controls.Add(js);
    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        int acaID = int.Parse(ddlAcademy.SelectedValue);
        BindAllDrawing(true, acaID);

    }

    private void ApprovedDrawing(string p)
    {

        DAL.DalAccessUtility.ExecuteNonQuery("update drawing set IsApproved=1 where DwgId=" + p);
        System.Reflection.PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        // make collection editable
        isreadonly.SetValue(this.Request.QueryString, false, null);
        // remove
        this.Request.QueryString.Clear();

        BindAllDrawing(false, -1);
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Drawing has been approved successfully.');", true);
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
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelDrawing");
        dt = ds.Tables[0];
        return dt;
    }
    protected void BinddWGtYPE()
    {
        DataTable dsZone = new DataTable();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("SELECT DwTypeId,DwTypeName FROM DrawingType where Active=1").Tables[0];
        if (dsZone != null && dsZone.Rows.Count > 0)
        {
            ddlDwgType.DataSource = dsZone;
            ddlDwgType.DataValueField = "DwTypeId";
            ddlDwgType.DataTextField = "DwTypeName";
            ddlDwgType.DataBind();
            ddlDwgType.Items.Insert(0, "SELECT DRAWING TYPE");
            ddlDwgType.SelectedIndex = 0;
        }
    }
    protected void DeactiveDrawing(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewDrawingProc '" + ID + "','','','','','','','','','','','','4','0',1,0");
        BindAllDrawing(true, -1);
        //BindDrawing();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Drawing deactive successfully.');", true);

    }

    protected void DeleteDrawing(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("Delete From Drawing where DwgID=" + ID);
        BindAllDrawing(false, -1);
        //BindDrawing();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Drawing Delete successfully.');", true);

    }
    protected void ActiveDrawing(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewDrawingProc '" + ID + "','','','','','','','','','','','','4','1',1,0");
        BindAllDrawing(true, -1);
        //BindDrawing();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Drawing active successfully.');", true);

    }
    protected void BindAllDrawing(bool IsApproved, int acaID)
    {
        if (IsApproved)
            btnNonApproved.Text = "View Non Approved Drawing";
        else
            btnNonApproved.Text = "View Approved Drawing";

       
        DataTable dtZoneDetails = null;
        
        if (acaID > 0)
        {

            dtZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DrwaingShowForAdminByAcaID'" + acaID + "','" + IsApproved + "'").Tables[0];
        }
        else
        {
            dtZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DrwaingShowForAdmin'" + IsApproved + "'").Tables[0]; ;
        }
     
        divAllDrawingView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='5%'></th>";
        ZoneInfo += "<th width='40%'>Drawing Details</th>";
        ZoneInfo += "<th width='25%'>Drawing File</th>";
        ZoneInfo += "<th width='35%'>Action</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dtZoneDetails.Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td><input type='checkbox' id='chkdrwing" + i + "' drwingPath='../" + dtZoneDetails.Rows[i]["PdfFilePath"].ToString() + "' /></td>";
            ZoneInfo += "<td width='40%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Zone: " + dtZoneDetails.Rows[i]["ZoneName"].ToString() + "</td>";
            ZoneInfo += "<td>Drawing No: " + dtZoneDetails.Rows[i]["DwgNo"].ToString() + "</td>";
            ZoneInfo += "<td>";
            if (dtZoneDetails.Rows[i]["Active"].ToString() == "1")
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
            ZoneInfo += "<td>Academy: " + dtZoneDetails.Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td>Revision No: " + dtZoneDetails.Rows[i]["RevisionNo"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td colspan='2'>Drawing Name: " + dtZoneDetails.Rows[i]["DrawingName"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
         
            ZoneInfo += "<td width='25%'><table><tr><td>PDF: <a href='" + dtZoneDetails.Rows[i]["PdfFilePath"].ToString() + "' target='_blank'>" + dtZoneDetails.Rows[i]["PdfFileName"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td>DWG: <a href='" + dtZoneDetails.Rows[i]["DwgFilePath"].ToString() + "' target='_blank'>" + dtZoneDetails.Rows[i]["DwgFileName"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td>Uploaded Date: " + dtZoneDetails.Rows[i]["CreatedOn"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td>Uploaded By: " + dtZoneDetails.Rows[i]["InName"].ToString() + "</a></td></tr></table></td>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='35%'>";
            if (IsApproved)
            {
                ZoneInfo += "<a class='btn btn-success' href='Admin_DrawingView.aspx?DwgIdA=" + dtZoneDetails.Rows[i]["DwgId"].ToString() + "'>";
                ZoneInfo += "<i class='icon-zoom-in icon-white'></i>Active ";
                ZoneInfo += "<a class='btn btn-danger' href='Admin_DrawingView.aspx?DwgIdIA=" + dtZoneDetails.Rows[i]["DwgId"].ToString() + "'>";
                ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
                ZoneInfo += "</a>";
            }
            else
            {
                ZoneInfo += "<a class='btn btn-danger' href='Admin_DrawingView.aspx?DwgIdD=" + dtZoneDetails.Rows[i]["DwgId"].ToString() + "'>";
                ZoneInfo += "<i class='icon-trash-in icon-white'></i>Delete ";
                ZoneInfo += "</a>";
                ZoneInfo += "<a class='btn btn-danger' href='Admin_DrawingView.aspx?DwgIdIA=" + dtZoneDetails.Rows[i]["DwgId"].ToString() + "&IsApproved=1'>";
                ZoneInfo += "<i class='icon-trash icon-white'></i> Approved";
                ZoneInfo += "</a>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divAllDrawingView.InnerHtml = ZoneInfo.ToString();

    }
    protected void BindDrawing()
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_DrwaingShowByDwgTypeForAdmin  '" + ddlDwgType.SelectedValue + "'");

        divDrawingView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='5%'></th>";
        ZoneInfo += "<th width='50%'>Drawing Details</th>";
        ZoneInfo += "<th width='25%'>Drawing File</th>";
        ZoneInfo += "<th width='25%'>Action</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td><input type='checkbox' id='chkdrwing" + i + "' drwingPath='../" + dsZoneDetails.Tables[0].Rows[i]["PdfFilePath"].ToString() + "' /></td>";
            ZoneInfo += "<td width='50%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Zone: " + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
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
            ZoneInfo += "<td>Academy: " + dsZoneDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td>Revision No: " + dsZoneDetails.Tables[0].Rows[i]["RevisionNo"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>Drawing Name: " + dsZoneDetails.Tables[0].Rows[i]["DrawingName"].ToString() + "</td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td width='25%'><table><tr><td><a href='" + dsZoneDetails.Tables[0].Rows[i]["PdfFilePath"].ToString() + "' target='_blank'>" + dsZoneDetails.Tables[0].Rows[i]["PdfFileName"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td>DWG: <a href='" + dsZoneDetails.Tables[0].Rows[i]["DwgFilePath"].ToString() + "' target='_blank'>" + dsZoneDetails.Tables[0].Rows[i]["DwgFileName"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td>Uploaded Date: " + dsZoneDetails.Tables[0].Rows[i]["CreatedOn"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td>Uploaded By: " + dsZoneDetails.Tables[0].Rows[i]["CreatedBy"].ToString() + "</a></td></tr></table></td>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_DrawingView.aspx?DwgIdA=" + dsZoneDetails.Tables[0].Rows[i]["DwgId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i>Active ";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_DrawingView.aspx?DwgIdIA=" + dsZoneDetails.Tables[0].Rows[i]["DwgId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
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
            btnNonApproved.Text = "View Non Approved Drawing";
            BindAllDrawing(true, -1);
            divAllDrawingView.Visible = true;
            divDrawingView.Visible = false;
        }
        else
        {
            BindDrawing();
            divDrawingView.Visible = true;
            divAllDrawingView.Visible = false;
        }
    }

    protected void btnNonApproved_Click(object sender, EventArgs e)
    {
        if (((Button)sender).Text == "View Non Approved Drawing")
        {
            BindAllDrawing(false, -1);
            ((Button)sender).Text = "View Approved Drawing";
        }
        else
        {
            BindAllDrawing(true, -1);
            ((Button)sender).Text = "View Non Approved Drawing";
        }
    }
    private void BindAcademy()
    {
        DataTable dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("Select AcaName,AcaID FROM Academy order by AcaName asc").Tables[0];
        if (dsBillDetails != null && dsBillDetails.Rows.Count > 0)
        {
            ddlAcademy.DataTextField = "AcaName";
            ddlAcademy.DataValueField = "AcaID";
            ddlAcademy.DataSource = dsBillDetails;
            ddlAcademy.DataBind();
            ddlAcademy.Items.Insert(0, new ListItem("--All Academy---", "0"));
            ddlAcademy.SelectedIndex = 0;
        }

    }
}