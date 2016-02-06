using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using excel = Microsoft.Office.Interop.Excel;

public partial class Transport_ReporteDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["report"] != null)
        {
            BindDatatable();
          
        }
       
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "UploadedData.xls"));
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
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec GetDocumentInfo");
        dt = ds.Tables[0];
        System.Data.EnumerableRowCollection<System.Data.DataRow> dtapproved = null;
        if (dt.Rows.Count > 0)
        {
            dtapproved = (from mytable in ds.Tables[0].AsEnumerable()
                          where Convert.ToDateTime(mytable.Field<DateTime>("CreatedOnDate")) >= Convert.ToDateTime(txtfirstDate.Text)
                          && Convert.ToDateTime(mytable.Field<DateTime>("CreatedOnDate")) <= Convert.ToDateTime(txtlastDate.Text)
                          select mytable);

        }
        else
        {
            return dt;
        }
        return dtapproved.CopyToDataTable();
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