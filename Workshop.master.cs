using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Workshop : System.Web.UI.MasterPage
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

        DataSet dsCount = new DataSet();
        dsCount = DAL.DalAccessUtility.GetDataInDataSet("exec USP_PurchaseCount '" + lblUser.Text + "'");
        lblUserName.Text = dsCount.Tables[0].Rows[0]["InName"].ToString();
        lblMsg.Text = dsCount.Tables[4].Rows[0]["MsgCo"].ToString();

        if (Session["UserTypeID"].ToString() == "6")
        {
            liMaterialDispatch.Visible = false;
            liEstimateView.Visible = false;
        }
        else
        {
            liEmployee.Visible = false;
            liMaterial.Visible = false;
            liViewNewEstimate.Visible = false;
            liMaterialAssign.Visible = false;
            liViewNonAEstimate.Visible = false;
        } 
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
}
