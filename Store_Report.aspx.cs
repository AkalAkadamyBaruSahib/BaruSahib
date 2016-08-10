using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using excel = Microsoft.Office.Interop.Excel;

public partial class Store_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "StockRegister-" + txtfirstDate.Text + "-" + txtlastDate.Text + ".xls"));
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
        string UserTypeID = Session["UserTypeID"].ToString();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_NewDispatchExcelForPurchaserAndStore] '" + txtfirstDate.Text + "','" + txtlastDate.Text + "','2'").Tables[0];
        return dt;
        // ds = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StockReport] 2");
    }
}
