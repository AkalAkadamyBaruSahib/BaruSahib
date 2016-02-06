using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Example_Example : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindZone();
        }
    }
    protected void BindZone()
    {
        //DataSet dsZone = new DataSet();
        //dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName  from Zone where Active=1");
        //ddlTest.DataSource = dsZone;
        //ddlTest.DataValueField = "ZoneId";
        //ddlTest.DataTextField = "ZoneName";
        //ddlTest.DataBind();
        //ddlTest.Items.Insert(0, "SELECT ZONE");
        //ddlTest.SelectedIndex = 0;
    }
}