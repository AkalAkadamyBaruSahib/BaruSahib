using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class ArchMaster : System.Web.UI.MasterPage
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
        DataSet dsName = new DataSet();
        dsName = DAL.DalAccessUtility.GetDataInDataSet(" select InName from Incharge where LoginId='" + lblUser.Text + "'");
        lblUserName.Text = dsName.Tables[0].Rows[0]["InName"].ToString();

        //DataSet dsWorkCount = DAL.DalAccessUtility.GetDataInDataSet("select COUNT(*)as workAllot from WorkAllot where ZoneId=(select distinct ZoneId from AcademyAssignToEmployee where EmpId=(select InchargeId from Incharge where LoginId='" + lblUser.Text + "'))");
        //lblWorkCount.Text = dsWorkCount.Tables[0].Rows[0]["workAllot"].ToString();

        //DataSet dsBillStatus = DAL.DalAccessUtility.GetDataInDataSet("select COUNT(*) as Co from SubmitBillByUser where FirstVarify is not null and SeccondVarify is not null and PaymentStatus is not null and RecevingStatus is null");
        //lblBillStatus.Text = dsBillStatus.Tables[0].Rows[0]["Co"].ToString();
        DataSet dsWork = DAL.DalAccessUtility.GetDataInDataSet("select COUNT(*) as co from WorkAllot where Active=1");
        lblWorkCount.Text=dsWork.Tables[0].Rows[0]["co"].ToString();
    }
}
