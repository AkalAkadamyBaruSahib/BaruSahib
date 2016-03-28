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

        if (Request.QueryString["report"] != null)
        {
            downloadExcel();
        }
        if (!Page.IsPostBack)
        {
            if (Session["ModuleID"] != null)
            {
                ModuleID = int.Parse(Session["ModuleID"].ToString());
            } BindEmployee();
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
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_ExcelWorkingSecurityEmployee] '" + ddlReport.SelectedValue + "'");
       dt = ds.Tables[0];
        return dt;
    }

    private void BindEmployee()
    {
        DataSet dsDesgis = new DataSet();
        dsDesgis = DAL.DalAccessUtility.GetDataInDataSet("Select distinct D.DesgId,D.Designation from SecurityEmployeeInfo S Inner Join Designation D on S.DesigID = D.DesgId where D.ModuleID='" + ModuleID + "' order by Designation asc");
        ddlReport.DataSource = dsDesgis;
        ddlReport.DataValueField = "DesgId";
        ddlReport.DataTextField = "Designation";
        ddlReport.DataBind();
        ddlReport.Items.Insert(0, "--Select Designation--");
        ddlReport.SelectedIndex = 0;
    
    }
    private void downloadExcel()
    {
        //Response.Redirect(Server.MapPath("~/VehicleDoc/Maintenance_schedule.xlsx"));

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Maintenance_schedule.xlsx"));
        Response.ContentType = "application/ms-excel";
        Response.WriteFile(Server.MapPath("~/VehicleDoc/Maintenance_schedule.xlsx"));
        Response.End();
    }
}