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
        divName.Visible = false;
        divCheckInDate.Visible = false;
        divCheckOutDate.Visible = false;
    }
    protected void drpFilterData_SelectedIndexChanged(object sender, EventArgs e)
    {
        divCheckOutDate.Visible = true;
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "StockRegister.xls"));
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
        string from = string.Empty;
        from = txtCheckInDate.Text == string.Empty ? DateTime.MinValue.ToShortDateString() : txtCheckInDate.Text;
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec GetVisitorsReports '" + from + "','" + txtCheckOutDate.Text + "'");
        dt = ds.Tables[0];
        return dt;
     }
    private void BindVisitor()
    {
        ddlName.ClearSelection();
        ddlName.Items.Clear();
        DataSet dsMat = new DataSet();
        var dsVType = DAL.DalAccessUtility.GetDataInDataSet("select V.ID,V.Name from Visitors V inner join VisitorType VT on V.VisitorTypeID = VT.ID where VisitorType = '" + drpFilterData.SelectedValue + "' ");
        ddlName.DataTextField = "Name";
        ddlName.DataValueField = "ID";
        ddlName.DataSource = dsVType;
        ddlName.DataBind();
   }
    private void BindByReference()
    {
        ddlName.ClearSelection();
        ddlName.Items.Clear();
        DataSet dsMat = new DataSet();
        var dsVType = DAL.DalAccessUtility.GetDataInDataSet("select V.ID,V.VisitorReference from Visitors V");
        ddlName.DataTextField = "VisitorReference";
        ddlName.DataValueField = "ID";
        ddlName.DataSource = dsVType;
        ddlName.DataBind();
    }
    private void BindDateAccordingData()
    {
        ddlName.ClearSelection();
        ddlName.Items.Clear();
        DataSet dsMat = new DataSet();
        var dsVType = DAL.DalAccessUtility.GetDataInDataSet("select V.ID,V.Name from Visitors V where V.TimePeriodTo >='" + txtCheckInDate + "' AND V.TimePeriodFrom <= '" + txtCheckOutDate + "'");
        ddlName.DataTextField = "Name";
        ddlName.DataValueField = "ID";
        ddlName.DataSource = dsVType;
        ddlName.DataBind();
    }
    
}