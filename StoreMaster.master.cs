using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StoreMaster : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] != null)
        {
            lblUser.Text = Session["EmailId"].ToString();
            lblUserName.Text = Session["UserName"].ToString();
        }
        else
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/Default.aspx", true);
        }
    }

    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx", true);
    }
    

}
