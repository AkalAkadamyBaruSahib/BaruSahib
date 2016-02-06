using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_AssignLoginId : System.Web.UI.Page
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
        dsZone = DAL.DalAccessUtility.GetDataInDataSet(" exec  USP_ShowZoneDetails");
        txtUserType.DataSource = dsZone;
        txtUserType.DataValueField = "ZoneId";
        txtUserType.DataTextField = "ZoneName";
        txtUserType.DataBind();
        txtUserType.Items.Insert(0, "Select Zone");
        txtUserType.SelectedIndex = 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("insert into Test(Tname)values('"+ txtUserType.SelectedValue +"')");
    }
}