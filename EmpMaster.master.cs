using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmpMaster : System.Web.UI.MasterPage
{
    private int _UserTypeID { get; set; }
    private int InchargeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["InName"].ToString();
            _UserTypeID = int.Parse(Session["UserTypeID"].ToString());
            InchargeID = int.Parse(Session["InchargeID"].ToString());
            LoadLinks();
        }
       
       
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
    private void LoadLinks()
    {
        List<UserRole> role = new List<UserRole>();
        TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());
        role = repo.GetUserRoleByInchargeID(InchargeID);
        if (role != null && role.Count != 0)
        {
            var userComplaintRole = role.Where(document => document.RoleID == (int)TypeEnum.UserRole.Complaint).FirstOrDefault();
            if (userComplaintRole != null)
            {
                liComplaints.Visible = true;
            }
        }

        if (_UserTypeID == (int)TypeEnum.UserType.COMPLAINT)
        {
            liBillStatus.Visible = false;
            liComplaints.Visible = true;
            liDispatchStatusForLocal.Visible = false;
            liEstimate.Visible = false;
            liEstimateStatus.Visible = false;
            liGallary.Visible = false;
            liHome.Visible = true;
            liLocalPurchase.Visible = false;
            liMaterial.Visible = false;
            liVendor.Visible = false;
            liWorkAlloted.Visible = false;
            liRejectedBills.Visible = false;
            liUploadEstimate.Visible = false;
        }
    }
}
