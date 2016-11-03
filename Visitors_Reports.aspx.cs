using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Visitors_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "VisitorReport.xls"));
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
        string from = string.Empty;
        from = txtCheckInDate.Text == string.Empty ? DateTime.MinValue.ToShortDateString() : txtCheckInDate.Text;
        dt = DAL.DalAccessUtility.GetDataInDataSet("exec GetVisitorsReports '" + txtCheckInDate.Text + "','" + txtCheckOutDate.Text + "'").Tables[0];
        return dt;
    }

}