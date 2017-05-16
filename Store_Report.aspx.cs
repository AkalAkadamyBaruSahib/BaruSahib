using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;

public partial class Store_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        BindDatatable();
    }
    protected void BindDatatable()
    {
        string UserTypeID = Session["UserTypeID"].ToString();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_NewDispatchExcelForPurchaserAndStore] '" + txtfirstDate.Text + "','" + txtlastDate.Text + "','2'").Tables[0];
        // ds = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StockReport] 2");
        string FileName = "StorStatusReport" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xlsx";

        string FilePath = Server.MapPath("EstFile") + "\\" + FileName;
        try
        {
            XLWorkbook workbook = new XLWorkbook();
            DataTable table = dt;
            workbook.Worksheets.Add(table);
            workbook.SaveAs(FilePath);

            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FileName));
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(@FilePath);
            Response.End();

        }
        catch (Exception ex)
        { }
        finally
        {

        }
    }
}
