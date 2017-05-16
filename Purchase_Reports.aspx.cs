using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using excel = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;


public partial class Purchase_Reports : System.Web.UI.Page
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
        DataTable dt = new DataTable();
        dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_NewDispatchExcelForPurchaserAndStore] '" + txtfirstDate.Text + "','" + txtlastDate.Text + "','2'").Tables[0];
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