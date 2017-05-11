using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using excel = Microsoft.Office.Interop.Excel;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;

public partial class PurchasePendingReport : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        BindDatatable();
        //Response.ClearContent();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PendingReport.xls"));
        //Response.ContentType = "application/ms-excel";
        //DataTable dt = BindDatatable();

        //string str = string.Empty;
        //foreach (DataColumn dtcol in dt.Columns)
        //{
        //    Response.Write(str + dtcol.ColumnName.Trim());
        //    str = "\t";
        //}
        //Response.Write("\n");
        //foreach (DataRow dr in dt.Rows)
        //{
        //    str = "";
        //    for (int j = 0; j < dt.Columns.Count; j++)
        //    {
        //        Response.Write(str + Convert.ToString(dr[j]).Trim());
        //        str = "\t";
        //    }
        //    Response.Write("\n");
        //}
        //Response.End();

    }

    protected void BindDatatable()
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        int UserID = Convert.ToInt32(Session["InchargeID"].ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        if (UserTypeID == (int)(TypeEnum.UserType.PURCHASE) || UserTypeID == (int)(TypeEnum.UserType.PURCHASECOMMITTEE))
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_PendingStatusForPurchaser] '" + txtfirstDate.Text + "','" + txtlastDate.Text + "','" + (int)TypeEnum.PurchaseSourceID.Mohali + "'").Tables[0];
        }
        else
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_DispatchExcelForPurchaser] '" + txtfirstDate.Text + "','" + txtlastDate.Text + "','" + (int)TypeEnum.PurchaseSourceID.Mohali + "','" + UserID + "'").Tables[0];
        }
        

        string FileName = "PurchasersPendingReport" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xlsx";

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