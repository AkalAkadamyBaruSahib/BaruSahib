using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindEstimate();
        }
    }
    protected void BindEstimate()
    {
        DataSet dsAcademy = new DataSet();
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("select EstId,SubEstimate from Estimate");
        ddl_Names.DataSource = dsAcademy;
        ddl_Names.DataValueField = "EstId";
        ddl_Names.DataTextField = "SubEstimate";
        ddl_Names.DataBind();
        ddl_Names.Items.Insert(0, "Select Estimate");
        ddl_Names.SelectedIndex = 0;
    }
}