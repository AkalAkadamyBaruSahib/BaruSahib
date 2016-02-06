using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emp_ViewMatDispatchStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
        }
        if (Request.QueryString["EstId"] != null)
        {
            getZoneAcaName(Request.QueryString["EstId"].ToString());
            getMaterialDisPatchStatus(Request.QueryString["EstId"].ToString());

        }
    }
    private void getMaterialDisPatchStatus(string id)
        {
            DataSet dsAcaDetails = new DataSet();
            dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDispatchStatusForEmp '"+ id +"' ");
            gvMaterailDetailForPurchase.DataSource = dsAcaDetails;
            gvMaterailDetailForPurchase.DataBind();
        }
    private void getZoneAcaName(string id)
    {
        DataSet dsDetails = new DataSet();
        dsDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_ZoneAndAcademnyNameByEstId '"+ id +"'");
        lblAcademy.Text = dsDetails.Tables[0].Rows[0]["AcaName"].ToString();
       
        lblEstId.Text = dsDetails.Tables[0].Rows[0]["EstId"].ToString();
    }

    }
