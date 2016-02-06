using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Example_MultiSelect : System.Web.UI.Page
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
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName from Zone where Active=1");
        if (dsZone.Tables[0].Rows.Count > 0)
        {
            ddchkCountry.DataSource = dsZone.Tables[0];
            ddchkCountry.DataValueField = "ZoneId";
            ddchkCountry.DataTextField = "ZoneName";
            ddchkCountry.DataBind();
        }
    }
}