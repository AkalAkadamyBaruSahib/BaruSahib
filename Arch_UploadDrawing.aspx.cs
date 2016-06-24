using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

public partial class Arch_UploadDrawing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                hdnInchargeID.Value = Session["InchargeID"].ToString();
                hdnIsAdmin.Value = Session["UserTypeID"].ToString();
                lblUser.Text = Session["EmailId"].ToString();
            }
        }
    }

    //private void SendSMStoAdmin()
    //{
    //    int AcaID = Convert.ToInt32(ddlAcademy.SelectedValue);
    //    const int USERTYPE = 7;
    //    InchargeController conrtoller = new InchargeController();
    //    List<string> incharges = conrtoller.GetUsersByUserTypeAndAcademic(USERTYPE, AcaID);

    //    string adminNumber = System.Configuration.ConfigurationManager.AppSettings["AdminToSendDrawingSMS"].ToString();
    //    if (btnSave.Visible == true)
    //    {
    //        Utility.SendSMS(adminNumber, " Non-Approved Drawing of " + ddlAcademy.SelectedItem.Text + " has been uploaded to www.Akalsewa.org.");
    //    }
    //}
}