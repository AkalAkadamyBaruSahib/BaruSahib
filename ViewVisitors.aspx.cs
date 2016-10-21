using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewVisitors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindVisitorType();
         
        }
    }

    private void BindVisitorType()
    {
        DataTable visitortype = new DataTable();
        visitortype = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.VisitorType").Tables[0];
        if (visitortype != null && visitortype.Rows.Count > 0)
        {
            ddltypeofvisitor.DataSource = visitortype;
            ddltypeofvisitor.DataValueField = "ID";
            ddltypeofvisitor.DataTextField = "VisitorType";
            ddltypeofvisitor.DataBind();
        }
    }
}