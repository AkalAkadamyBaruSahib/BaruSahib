using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transport_ContractRate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
            }
            BindNonApprovedRateMaterial();
        }
    }

    protected void BindNonApprovedRateMaterial()
    {
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("Select * from VehicleContractRate");
        grvNonApprovedRateDetails.DataSource = dsMat;
        grvNonApprovedRateDetails.DataBind();
    }
    protected void btn_Approved_Click(object sender, EventArgs e)
    {
       VehicleContractRate vehiclecontractrate = new VehicleContractRate();
        GridViewRow gr = (GridViewRow)((DataControlFieldCell)((Button)sender).Parent).Parent;
        Button btnapproved = (Button)gr.FindControl("btn_Approved");
        Label txtSeatCapacity = (Label)gr.FindControl("txtSeatCapacity");
        TextBox txtCurrentYear = (TextBox)gr.FindControl("txtCurrentYear");
        TextBox txt5Years = (TextBox)gr.FindControl("txt5Years");
        TextBox txtAverage = (TextBox)gr.FindControl("txtAverage");
        TextBox txt10Years = (TextBox)gr.FindControl("txt10Years");
        string approvedid = btnapproved.CommandArgument.ToString();
        DataSet dsContractRate = new DataSet();
        dsContractRate = DAL.DalAccessUtility.GetDataInDataSet("Select * from VehicleContractRate  where ID = '" + approvedid + "'");
        if (approvedid == dsContractRate.Tables[0].Rows[0]["ID"].ToString())
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Update VehicleContractRate set FiveYears='" + txt5Years.Text + "',TenYears='" + txt10Years.Text + "',Average='" + txtAverage.Text + "',CurrentYear='" + txtCurrentYear.Text + "' where ID = '" + approvedid + "'");
        }
        vehiclecontractrate.ID = Convert.ToInt32(approvedid);
        BindNonApprovedRateMaterial();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Vehicle Contract Rate Saved Successfully');</script>", false);
    }
}