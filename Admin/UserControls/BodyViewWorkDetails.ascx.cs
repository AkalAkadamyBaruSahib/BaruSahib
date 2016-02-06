using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_BodyViewWorkDetails : System.Web.UI.UserControl
{
    private int WorkAllotID = -1;
    private int AcademyID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["WAID"] != null)
            {
                WorkAllotID = int.Parse(Request.QueryString["WAID"]);
                hdnWorkAllotID.Value = WorkAllotID.ToString();
                BindMaterialDetails();
            }
            else
            {
                AcademyID = int.Parse(Request.QueryString["AcaID"]);
                hdnWorkAllotID.Value = WorkAllotID.ToString();
                BindMaterialDetailsByAcademyID();
            }
        }
    }

    private void BindMaterialDetails()
    {
        DataSet dsAcademy = new DataSet();
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_getEstimateBalanceNew'" + WorkAllotID + "','1'");
        GridView1.DataSource = dsAcademy;
        GridView1.DataBind();
    }

    private void BindMaterialDetailsByAcademyID()
    {
        DataSet dsAcademy = new DataSet();
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_getEstimateBalanceByAcademyID'" + AcademyID + "'");
        GridView1.DataSource = dsAcademy;
        GridView1.DataBind();
        if (dsAcademy.Tables[0].Rows.Count > 0)
        {
            lblHeader.Text = "Material Details for Academy " + dsAcademy.Tables[0].Rows[0]["AcaName"].ToString();
        }
        else
            lblHeader.Text = "No Record Found";
    }
}