﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class PurchaseMaster : System.Web.UI.MasterPage
{
    public int UserTypeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
            UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        }


        if (Session["UserTypeID"].ToString() == "4")
        {
            liReport.Visible = true;
            liestimate.Visible = true;
        }
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx", true);
    }
}
