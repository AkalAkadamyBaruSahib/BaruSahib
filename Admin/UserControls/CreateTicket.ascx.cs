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
        txtuserID.Value = Session["InchargeID"].ToString();
        hdnUserType.Value = Session["UserTypeID"].ToString();
        hdnLoginID.Value = Session["EmailId"].ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}