using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Admin_UserControls_Admin_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public DataTable BindDatatable()
    {
        string UserTypeID = Session["UserTypeID"].ToString();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        if (UserTypeID == "4")
        {
            ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DispatchExcel4PurchaseAndWorkShop '2'");
        }
        else if (UserTypeID == "1")
        {
            ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DispatchExcel");
        }
        else if (UserTypeID == "2")
        {
            ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DispatchExcel");
        }

        dt = ds.Tables[0];
        return dt;
    }


      protected void btnExecl_Click(object sender, EventArgs e)
      {
          //      System.Threading.Thread.Sleep(1000);
          //    Response.ClearContent();
          //    Response.Buffer = true;
          //    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Dispatch.xls"));
          //    Response.ContentType = "application/ms-excel";
          //    DataTable dt = BindDatatable();
          //    string str = string.Empty;
          //    if (btnExecl.Visible == true)
          //    {
          //        TextBox1.Text=
          //    }
          //    foreach (DataColumn dtcol in dt.Columns)
          //    {
          //        Response.Write(str + dtcol.ColumnName);
          //        str = "\t";
          //    }
          //    Response.Write("\n");
          //    foreach (DataRow dr in dt.Rows)
          //    {
          //        str = "";
          //        for (int j = 0; j < dt.Columns.Count; j++)
          //        {
          //            Response.Write(str + Convert.ToString(dr[j]));
          //            str = "\t";
          //        }
          //        Response.Write("\n");
          //    }
          //    Response.End();
      }


    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        //  TextBox1.Text = Convert.ToDateTime(Calendar2.SelectedDate, CultureInfo.GetCultureInfo("en-US")).ToString("MM/dd/yyyy");
    }
}