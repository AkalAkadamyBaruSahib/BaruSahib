using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Visitor_AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
        }

        DataSet dsUser = new DataSet();
        dsUser = DAL.DalAccessUtility.GetDataInDataSet("exec USP_UserCount '" + lblUser.Text + "'");
        lblUserName.Text = dsUser.Tables[0].Rows[0]["InName"].ToString();

    }

    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
}
