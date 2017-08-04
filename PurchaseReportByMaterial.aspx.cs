using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

public partial class PurchaseReportByMaterial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string name = Request.Form["txtMaterial"];
        DataTable dt = new DataTable();
        dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_PurchaseReportByMaterial] '" + txtfirstDate.Text + "','" + txtlastDate.Text + "','" + (int)TypeEnum.PurchaseSourceID.Mohali + "','" + name.Trim() + "'").Tables[0];
        string FileName = "PurchaseReportByMaterial" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xlsx";

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