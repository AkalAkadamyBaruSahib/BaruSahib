using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RateUpload : System.Web.UI.Page
{
    public static int UserTypeID = -1;

    public int MatTypeID { get; set; }
    public int MatID { get; set; }
    public int VendorID { get; set; }
    public decimal NewRate { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (Session["UserTypeID"] != null)
            {
                UserTypeID = int.Parse(Session["UserTypeID"].ToString());
                hdnInchargeID.Value = Session["InchargeID"].ToString();
                hdnUserName.Value = Session["InName"].ToString();
            }
            if (Request.QueryString["MatTypeID"] != null && Request.QueryString["MatID"] != null && Request.QueryString["VendorID"] != null && Request.QueryString["NewRate"] != null)
            {
                MatTypeID = Convert.ToInt32(Request.QueryString["MatTypeID"].ToString());
                MatID = Convert.ToInt32(Request.QueryString["MatID"].ToString());
                VendorID = Convert.ToInt32(Request.QueryString["VendorID"].ToString());
                NewRate = Convert.ToDecimal(Request.QueryString["NewRate"].ToString());

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "GetMaterialInfoByMatID(" + MatID + "," + VendorID + "," + NewRate + ");", true);
            }
        }
    }
}
