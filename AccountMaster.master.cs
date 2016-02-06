using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class AccountMaster : System.Web.UI.MasterPage
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
            DataSet dsCount = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AccountCount '"+ lblUser.Text +"'");
            lblUserName.Text = dsCount.Tables[0].Rows[0]["InName"].ToString();
            lblBillCount.Text = dsCount.Tables[1].Rows[0]["StatusCount"].ToString();
            lblBillStatus.Text = dsCount.Tables[2].Rows[0]["Co"].ToString();
            lblMsg.Text = dsCount.Tables[3].Rows[0]["Msgco"].ToString();
            lblReject.Text = dsCount.Tables[4].Rows[0]["RejBills"].ToString();
            lblEstCount.Text = dsCount.Tables[5].Rows[0]["EstCount"].ToString();
        }
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
}
