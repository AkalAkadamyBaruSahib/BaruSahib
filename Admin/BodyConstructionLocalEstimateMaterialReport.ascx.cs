using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BodyConstructionLocalEstimateMaterialReport : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string name = Request.Form["txtMaterial"];
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Local Rate Report(" + name + ").xls"));
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
        string UserID = Session["InchargeID"].ToString();
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string name = Request.Form["txtMaterial"];
        if (UserTypeID == (int)(TypeEnum.UserType.ADMIN))
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EmpLocalEstimateMaterialReortForAdmin]  '" + name.Trim() + "'").Tables[0];
        }
        else
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EmpLocalEstimateMaterial]  '" + name.Trim() + "','" + UserID + "'").Tables[0];
        }
        return dt;
    }
}