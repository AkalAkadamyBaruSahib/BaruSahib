using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;

public partial class Transport_AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUserName.Text = Session["InName"].ToString();

        }
        if (Session["UserTypeID"].ToString() != "13")
        {
            // lireport.Visible = false;
            liDesignation.Visible = false;
            liDepartment.Visible = false;
            liCreateEditEmployee.Visible = false;
            liLocationAssign.Visible = false;
            liEstimateNewEstimate.Visible = false;
            liCreateMaterial.Visible = false;
            liContractRate.Visible = false;
        }
        else
        {
            liEstimateiewForEmp.Visible = false;
        }

    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
}
