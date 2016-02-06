using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transport_Alerts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Utility.SendDocumentAlerts();
        msg.Visible = true;
        box.Visible = false;
    }
}