using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_BodyVendorInformation : System.Web.UI.UserControl
{
    private bool _IsPopUP = false;
    public static int UserTypeID = -1;
    public bool IsOpenInPopUP
    {
        get
        {
            return _IsPopUP;
        }
        set
        {
            _IsPopUP = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            if (!IsOpenInPopUP)
            {
                vendorMainbox.Attributes.Add("style", "display:block");
            }
            else
            {
               vendorMainbox.Attributes.Add("style", "display:none");
            }

            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["InName"].ToString();
                hdnInchargeID.Value = Session["InchargeID"].ToString();
                hdnUserType.Value = Session["UserTypeID"].ToString();
            }
        }
    }

}