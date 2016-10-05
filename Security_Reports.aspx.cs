using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using excel = Microsoft.Office.Interop.Excel;

public partial class Security_Reports : System.Web.UI.Page
{
    public static int ModuleID = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindZones();
            BindEmployee();
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "WorkingEmployeeReport.xls"));
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
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_ExcelWorkingSecurityEmployee] '" + ddlDesignation.SelectedValue + "', '" + ddlZone.SelectedValue + "'");
        dt = ds.Tables[0];
        return dt;
    }

    private void BindEmployee()
    {
        DataTable dsDesgis = new DataTable();
        dsDesgis = DAL.DalAccessUtility.GetDataInDataSet("Select  D.DesgId,D.Designation from  Designation D  where D.ModuleID='" + (int)TypeEnum.Module.Security + "' order by Designation asc").Tables[0];
        if (dsDesgis != null && dsDesgis.Rows.Count > 0)
        {
            ddlDesignation.DataSource = dsDesgis;
            ddlDesignation.DataValueField = "DesgId";
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--Select Designation--", "0"));
            ddlDesignation.SelectedIndex = 0;
        }
    }
   
    private void BindZones()
    {
        DataTable allZone = new DataTable();
        allZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneID,ZoneName from dbo.Zone").Tables[0];
        if (allZone != null && allZone.Rows.Count > 0)
        {
            ddlZone.DataSource = allZone;
            ddlZone.DataValueField = "ZoneID";
            ddlZone.DataTextField = "ZoneName";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("--Select Zone--", "0"));
        }
    }
  
}