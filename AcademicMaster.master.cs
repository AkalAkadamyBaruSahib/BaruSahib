using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AcademicMaster : System.Web.UI.MasterPage
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
        DataSet dsUser = new DataSet();
        dsUser = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_UserCount '" + lblUser.Text + "'");
        lblUserName.Text = dsUser.Tables[0].Rows[0]["InName"].ToString();
        lblWorkCount.Text = dsUser.Tables[1].Rows[0]["workAllot"].ToString();
        lblBillStatus.Text = dsUser.Tables[2].Rows[0]["Co"].ToString();
        lblRejBills.Text = dsUser.Tables[3].Rows[0]["Cou"].ToString();
        lblEstimate.Text = dsUser.Tables[4].Rows[0]["EstCo"].ToString();
        lblMsg.Text = dsUser.Tables[5].Rows[0]["Msgco"].ToString();
        lblTicketCount.Text = dsUser.Tables[6].Rows[0]["TicketCount"].ToString();


    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
}
