using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_CreateTicket : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hdnUserType.Value = Session["UserTypeID"].ToString();
            hdnLoginID.Value = Session["EmailId"].ToString();
            hdnUserID.Value = Session["InchargeID"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}