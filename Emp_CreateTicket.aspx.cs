using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Emp_CreateTicket : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var js = new HtmlGenericControl("script");
        js.Attributes["type"] = "text/javascript";
        js.Attributes["src"] = "JavaScripts/CreateTickets.js?t=" + DateTime.Now.Ticks;
        Page.Controls.Add(js);
    }
}