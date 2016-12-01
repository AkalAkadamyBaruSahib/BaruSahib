using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Visitor_AdminMaster : System.Web.UI.MasterPage
{
    public int UserType = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
            UserType = Convert.ToInt16(Session["UserTypeID"].ToString());
        }
        if (UserType != (int)TypeEnum.UserType.RECEPTIONADMIN)
        {
            liReport.Visible = false;
        }
        DataSet dsUser = new DataSet();
        dsUser = DAL.DalAccessUtility.GetDataInDataSet("exec USP_UserCount '" + lblUser.Text + "'");
        lblUserName.Text = dsUser.Tables[0].Rows[0]["InName"].ToString();
        showUnCheckOutCount();
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }

    private void showUnCheckOutCount()
    {
        VisitorUserRepository repo = new VisitorUserRepository(new AkalAcademy.DataContext());
        if (!spnNewNotification.InnerText.Contains('('))
        {
            spnNewNotification.InnerText = spnNewNotification.InnerText + " (" + repo.GetUnCheckOutRoomCount(DateTime.Now, true, (int)TypeEnum.VisitoryType.Visitor) + ")";
        }
    }
}
