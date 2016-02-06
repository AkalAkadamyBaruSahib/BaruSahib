using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class BillStatus : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lbMothWise_Click(object sender, EventArgs e)
    {
        Response.Redirect("BillMonthWise.aspx");
    }
    protected void lbZoneAca_Click(object sender, EventArgs e)
    {
        Response.Redirect("BillStatusAcaZoneWise.aspx");
    }
    protected void lbAlloted_Click(object sender, EventArgs e)
    {
        Response.Redirect("BillAllotWork.aspx");
    }
    protected void lblPaidBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("BillPaid.aspx");
    }
    protected void lbRejectBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("BillRejectWise.aspx");
    }
    protected DataTable BindDatatable2()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatus");
        dt = ds.Tables[0];
        return dt;
    }
   
    protected void btnExecl_Click(object sender, EventArgs e)
    {
 System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "BillStatus.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindDatatable2();
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
}
