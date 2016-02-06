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
        lblBillStatus.Text = dsCount.Tables[1].Rows[0]["co"].ToString();
        lblRejectBill.Text = dsCount.Tables[2].Rows[0]["co"].ToString();
        lblEst.Text = dsCount.Tables[3].Rows[0]["EstCo"].ToString();
        lblMsg.Text = dsCount.Tables[4].Rows[0]["MsgCo"].ToString();
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
}
