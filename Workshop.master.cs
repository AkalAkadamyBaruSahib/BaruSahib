using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Workshop : System.Web.UI.MasterPage
{
    int InchargeID = -1;
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
            liMaterialPending.Visible = false;
            liBill.Visible = false;
           
        }
        else
        {
            liEmployee.Visible = false;
            liWorkshop.Visible = false;
            liViewNewEstimate.Visible = false;
            liMaterialAssign.Visible = false;
            liMaterialUnAssign.Visible = false;
            liEstimateWorkAllot.Visible = false;
          
        }

        showUncompletedEstimatesCount(); 
        showUnAssignedEstimatesCount();
    }

    private void showUncompletedEstimatesCount()
    {
        InchargeID = int.Parse(Session["InchargeID"].ToString());
        WorkshopRepository repo = new WorkshopRepository(new AkalAcademy.DataContext());
        if (!spnPendingEstimates.InnerText.Contains('('))
        {
            spnPendingEstimates.InnerText = spnPendingEstimates.InnerText + " (" + repo.GetUnCompletedEstimate(InchargeID, (int)TypeEnum.PurchaseSourceID.AkalWorkshop) + ")";
        }

    }

    private void showUnAssignedEstimatesCount()
    {
        InchargeID = int.Parse(Session["InchargeID"].ToString());
        WorkshopRepository repo = new WorkshopRepository(new AkalAcademy.DataContext());
        if (!spnPendingAssignEstimates.InnerText.Contains('('))
        {
            spnPendingAssignEstimates.InnerText = spnPendingAssignEstimates.InnerText + " (" + repo.GetUnAssignedEstimate((int)TypeEnum.PurchaseSourceID.AkalWorkshop) + ")";
        }

    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
}
